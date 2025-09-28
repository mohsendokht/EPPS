using ProductionPlanning.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProductionPlanning.DBChange
{
    internal class DBChange
    {
        public int DBver = 0;
        
        public void UpdateDBversion()
        {
            
                var cn = new SqlConnection(Module1.PlanningCnnStr);
                var cm = new SqlCommand("Select TOP 1 DB_Version From Tbl_DB_Version", cn);
                cn.Open();
                var dr = cm.ExecuteReader();
                if (!dr.HasRows)
                {
                    Logger.SaveError("UpdateDBversion", "Tbl_DB_Version has no record.");
                    MessageBox.Show(" ورژن بانک اطلاعاتی مشخص نیست.", "اشکال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, false);
                }
                else
                {
                    dr.Read();
                    this.DBver = int.Parse(dr.GetString(0));
                    dr.Close();
                }


                // Get list of Scripts need to be run:
                var dbScripts = zzGetScripts(this.DBver);

                foreach (var dbScript in dbScripts)
                {
                    cm.CommandText = dbScript.Script;
                    cm.ExecuteNonQuery();

                cm.CommandText = $"UPDATE Tbl_DB_Version SET DB_Version = {dbScript.Version}, UpdateTime = GETDATE();";
                cm.ExecuteNonQuery();
            }

                cn.Close();
                cm.Dispose();
               
           
        }

        private List<DBScript> zzGetScripts(int DBver)
        {
            var scripts = new List<DBScript>();
            var allScripts = zzAllScripts();
            foreach (var script in allScripts)
            {
                if (script.Version > DBver)
                    scripts.Add(script);
            }
            
            return scripts;
        }

        private List<DBScript> zzAllScripts()
        {
            var scripts = new List<DBScript>();
            scripts.Add(new DBScript() { 
                Number= 1,
                Version= 14010101,
                Script= @"--------------------------------------------------------------------------
                        INSERT INTO Tbl_RelationTypes (TypeCode, TypeTitle)                      
                        SELECT                                                                  
                           RT.TypeCode,                                                          
                           RT.TypeTitle                                                          
                        FROM(SELECT TypeCode = 1, TypeTitle = 'FS'   UNION                       
                             SELECT TypeCode = 2, TypeTitle = 'FF'   UNION                       
                             SELECT TypeCode = 3, TypeTitle = 'SS'   UNION                       
                             SELECT TypeCode = 4, TypeTitle = 'SF'   UNION                       
                             SELECT TypeCode = 5, TypeTitle = 'ASAP'                             
                            )                                                                    
                         AS RT                                                                   
                         LEFT JOIN Tbl_RelationTypes ON Tbl_RelationTypes.TypeCode = RT.TypeCode 
                        WHERE Tbl_RelationTypes.TypeCode IS NULL ;                               
                        ----------------------------------------------------------------------------
                        ----------------------------------------------------------------------------
                        INSERT INTO Tbl_Machines
                                   ([Code]
                                   ,[Name]
                                   ,[Producer]
                                   ,[ProducerCountry]
                                   ,[Application]
                                   ,[CalendarCode]
                                   ,[Description])
                        SELECT TOP 1
                             Code            = '-1',
                             Name            = '-',
                             Producer        = '-',
                             ProducerCountry = '-',
                             Application     = '-',
                             CalendarCode    = Tbl_Calendar.CalendarCode,
                             Description     = '-'
                        FROM Tbl_Calendar
                        WHERE NOT EXISTS(SELECT * FROM [Tbl_Machines] WHERE Code ='-1')
                        ORDER BY Tbl_Calendar.CalendarCode ;
                         "
            });

            scripts.Add(new DBScript()
            {
                Number = 2,
                Version = 14010128,
                Script = @"-----------------------------------------------------------------------
                        CREATE OR ALTER Procedure [dbo].[SP_CalculateMaterialAP]
                                             @MPSCode INT,
					                         @MPSYear INT 
                        As

                         DECLARE @TreeCode  INT = -1, 
     	                         @NodeLevel INT = 0;

                          -- Get list of product trees:
                         SELECT 
                             Tbl_ProductTree.TreeCode
                         INTO #Trees
                         FROM Tbl_MPSDetails
                              INNER JOIN Tbl_ProductTree ON Tbl_ProductTree.ProductCode = Tbl_MPSDetails.ProductCode
                         WHERE MPSCode= @MPSCode 
                           AND MPSYear= @MPSYear
                           AND Tbl_ProductTree.DefualtTree = 1;
 
                         SELECT 
	                         Tbl_OperationMaterials.TreeCode,
	                         OperationCode = Tbl_OperationMaterials.CurrentOperationCode,
	                         Tbl_PrimaryMaterials.MaterialCode,
	                         Tbl_PrimaryMaterials.MaterialTitle,
	                         Value     = CASE WHEN Tbl_PrimaryMaterials.StoreUnit <> Tbl_OperationMaterials.ProductionTestUnit 
				                               AND Tbl_UnitsRelations.CommunicationFactorUnit IS NOT NULL 
				                              THEN Tbl_OperationMaterials.OneUnitBuildAmount / Tbl_UnitsRelations.CommunicationFactorUnit 
				                              ELSE Tbl_OperationMaterials.OneUnitBuildAmount
	    		                         END,
	                         UnitCode  = CASE WHEN Tbl_PrimaryMaterials.StoreUnit <> Tbl_OperationMaterials.ProductionTestUnit 
				                               AND Tbl_UnitsRelations.CommunicationFactorUnit IS NOT NULL 
				                              THEN Tbl_PrimaryMaterials.StoreUnit 
				                              ELSE Tbl_OperationMaterials.ProductionTestUnit
				                         END,
	                         UnitTitle = CASE WHEN Tbl_PrimaryMaterials.StoreUnit <> Tbl_OperationMaterials.ProductionTestUnit 
				                               AND Tbl_UnitsRelations.CommunicationFactorUnit IS NOT NULL 
				                              THEN StoreUnit.Title
				                              ELSE Tbl_TestUnits.Title
				                         END
				 
                        INTO #OperationMaterials 
                        FROM Tbl_OperationMaterials
                        INNER JOIN #Trees ON #Trees.TreeCode = Tbl_OperationMaterials.TreeCode
                        INNER JOIN Tbl_TestUnits ON Tbl_TestUnits.Code = Tbl_OperationMaterials.ProductionTestUnit
                        INNER JOIN Tbl_PrimaryMaterials ON Tbl_PrimaryMaterials.MaterialCode = Tbl_OperationMaterials.MaterialCode
                        INNER JOIN Tbl_TestUnits AS StoreUnit ON StoreUnit.Code = Tbl_PrimaryMaterials.StoreUnit
                        LEFT  JOIN Tbl_UnitsRelations ON Tbl_UnitsRelations.BaseUnitCode = Tbl_PrimaryMaterials.StoreUnit 
                                                     AND Tbl_UnitsRelations.RelatedUnitCode = Tbl_OperationMaterials.ProductionTestUnit 


                        CREATE TABLE #Parts 
                                     (TreeCode         INT,       
                                      ParentDetailCode VARCHAR(50) COLLATE database_default,     
                                      DetailCode       VARCHAR(50) COLLATE database_default,
			                          Quantity         INT,
			                          NodeLevel        INT) 
			  
                        INSERT INTO #Parts(TreeCode, ParentDetailCode, DetailCode, Quantity, NodeLevel)
                        SELECT 
	                         TreeCode         = Tbl_ProductTreeDetails.TreeCode,
	                         ParentDetailCode = Tbl_ProductTreeDetails.ParentDetailCode,
	                         DetailCode       = Tbl_ProductTreeDetails.DetailCode,
	                         Quantity         = Tbl_ProductTreeDetails.ParentQuantity,
	                         NodeLevel        = @NodeLevel
                        From Tbl_ProductTreeDetails
                         WHERE Tbl_ProductTreeDetails.TreeCode = @TreeCode OR @TreeCode = -1
                           AND Tbl_ProductTreeDetails.ParentDetailCode = '0'

                        WHILE EXISTS(SELECT * FROM #Parts WHERE NodeLevel = @NodeLevel) OR @NodeLevel = 30
                        BEGIN
                            SET @NodeLevel = @NodeLevel + 1

	                        INSERT INTO #Parts(TreeCode, ParentDetailCode, DetailCode, Quantity, NodeLevel)
	                        SELECT 
	                            Tbl_ProductTreeDetails.TreeCode,
	                            Tbl_ProductTreeDetails.ParentDetailCode,
	                            Tbl_ProductTreeDetails.DetailCode,
	                            Quantity   =  Tbl_ProductTreeDetails.ParentQuantity * Parent.Quantity,
	                            NodeLevel  = @NodeLevel
	                        From Tbl_ProductTreeDetails 
		                         INNER JOIN (SELECT DISTINCT
						                         TreeCode,
						                         DetailCode,
						                         Quantity
					                         FROM #Parts
					                        )
				                         AS Parent   
				                         ON Parent.TreeCode = Tbl_ProductTreeDetails.TreeCode 
				                        AND Parent.DetailCode = Tbl_ProductTreeDetails.ParentDetailCode
		                         LEFT JOIN (SELECT DISTINCT
						                         TreeCode,
						                         ParentDetailCode
					                         FROM #Parts
					                        )
				                         AS GrandParent   
				                         ON GrandParent.TreeCode         = Tbl_ProductTreeDetails.TreeCode 
				                        AND GrandParent.ParentDetailCode = Tbl_ProductTreeDetails.ParentDetailCode
	                        WHERE GrandParent.TreeCode IS NULL 
                        END
 
                        SELECT 
                           TreeCode,
                           ParentDetailCode,
                           DetailCode,
                           Quantity = SUM(Quantity)
                        INTO #SummaryParts 
                        From #Parts
                        GROUP BY TreeCode,ParentDetailCode,DetailCode

                        SELECT 
                          Tbl_Products.ProductCode,
                          #SummaryParts.TreeCode,
                          #OperationMaterials.OperationCode,
                          #OperationMaterials.MaterialCode,
                          #OperationMaterials.MaterialTitle,
                          Value = #OperationMaterials.Value *  #SummaryParts.Quantity,
                          #OperationMaterials.UnitCode,
                          #OperationMaterials.UnitTitle
                        INTO #ProductMaterials
                        FROM #SummaryParts
                             INNER JOIN Tbl_ProductTree ON Tbl_ProductTree.TreeCode = #SummaryParts.TreeCode 
	                         INNER JOIN Tbl_Products    ON Tbl_Products.ProductCode = Tbl_ProductTree.ProductCode
                             INNER JOIN Tbl_ProductOPCs ON Tbl_ProductOPCs.TreeCode   = #SummaryParts.TreeCode
	                                                   AND Tbl_ProductOPCs.DetailCode = #SummaryParts.DetailCode
                             INNER JOIN #OperationMaterials ON #OperationMaterials.TreeCode      = Tbl_ProductOPCs.TreeCode
	                                                       AND #OperationMaterials.OperationCode = Tbl_ProductOPCs.OperationCode

                        
                        -- Delete existing records:
                        DELETE FROM Tbl_MaterialCapametry WHERE MPSCode= @MPSCode AND MPSYear= @MPSYear;

                        INSERT INTO Tbl_MaterialCapametry
                             (MPSCode,
                              MPSYear,
                              MPSDetailPriority,
                              MonthNo,
                              MaterialCode,
                              RequirementTime,
                              UnitCode)
                        SELECT 
                              MPSCode           = Tbl_MPSDetails.MPSCode,
                              MPSYear           = Tbl_MPSDetails.MPSYear,
                              MPSDetailPriority = Tbl_MPSDetails.MPSDetailPriority,
                              MonthNo           = YearMonth.M,
                              MaterialCode      = #ProductMaterials.MaterialCode,
                              RequirementTime   = SUM(CASE WHEN YearMonth.M =  1 THEN Tbl_MPSDetails.Order1 * #ProductMaterials.Value
	                                                       WHEN YearMonth.M =  2 THEN Tbl_MPSDetails.Order2 * #ProductMaterials.Value
                                                           WHEN YearMonth.M =  3 THEN Tbl_MPSDetails.Order3 * #ProductMaterials.Value
							                               WHEN YearMonth.M =  4 THEN Tbl_MPSDetails.Order4 * #ProductMaterials.Value
							                               WHEN YearMonth.M =  5 THEN Tbl_MPSDetails.Order5 * #ProductMaterials.Value
							                               WHEN YearMonth.M =  6 THEN Tbl_MPSDetails.Order6 * #ProductMaterials.Value
							                               WHEN YearMonth.M =  7 THEN Tbl_MPSDetails.Order7 * #ProductMaterials.Value
							                               WHEN YearMonth.M =  8 THEN Tbl_MPSDetails.Order8 * #ProductMaterials.Value
							                               WHEN YearMonth.M =  9 THEN Tbl_MPSDetails.Order9 * #ProductMaterials.Value
							                               WHEN YearMonth.M = 10 THEN Tbl_MPSDetails.Order10 * #ProductMaterials.Value
							                               WHEN YearMonth.M = 11 THEN Tbl_MPSDetails.Order11 * #ProductMaterials.Value
							                               WHEN YearMonth.M = 12 THEN Tbl_MPSDetails.Order12 * #ProductMaterials.Value
	                                                  ELSE 0
	                                                  END),
                              UnitCode          = MAX(#ProductMaterials.UnitCode)
                        FROM Tbl_MPSDetails
                             CROSS JOIN (SELECT M = 1 UNION 
	                                     SELECT M = 2 UNION
				                         SELECT M = 3 UNION
				                         SELECT M = 4 UNION
				                         SELECT M = 5 UNION
				                         SELECT M = 6 UNION
				                         SELECT M = 7 UNION
				                         SELECT M = 8 UNION
				                         SELECT M = 9 UNION
				                         SELECT M = 10 UNION
				                         SELECT M = 11 UNION
				                         SELECT M = 12 ) 
			                        AS YearMonth
	                          INNER JOIN #ProductMaterials ON #ProductMaterials.ProductCode = Tbl_MPSDetails.ProductCode		
                        WHERE MPSCode= @MPSCode 
                          AND MPSYear= @MPSYear
                        GROUP BY Tbl_MPSDetails.MPSCode,
                                 Tbl_MPSDetails.MPSYear,
		                         Tbl_MPSDetails.MPSDetailPriority,
		                         YearMonth.M,#ProductMaterials.MaterialCode
                       
                "
            });

            scripts.Add(new DBScript()
            {
                Number = 3,
                Version = 14010601,
                Script = @"--------------------------------------------------------------------------
                        INSERT INTO Tbl_RelationTypes (TypeCode, TypeTitle)                      
                        SELECT                                                                  
                           RT.TypeCode,                                                          
                           RT.TypeTitle                                                          
                        FROM(SELECT TypeCode = 1, TypeTitle = 'FS'   UNION                       
                             SELECT TypeCode = 2, TypeTitle = 'FF'   UNION                       
                             SELECT TypeCode = 3, TypeTitle = 'SS'   UNION                       
                             SELECT TypeCode = 4, TypeTitle = 'SF'   UNION                       
                             SELECT TypeCode = 5, TypeTitle = 'ASAP'                             
                            )                                                                    
                         AS RT                                                                   
                         LEFT JOIN Tbl_RelationTypes ON Tbl_RelationTypes.TypeCode = RT.TypeCode 
                        WHERE Tbl_RelationTypes.TypeCode IS NULL ;                               
                        ----------------------------------------------------------------------------
                        ----------------------------------------------------------------------------
                        INSERT INTO Tbl_Machines
                                   ([Code]
                                   ,[Name]
                                   ,[Producer]
                                   ,[ProducerCountry]
                                   ,[Application]
                                   ,[CalendarCode]
                                   ,[Description])
                        SELECT TOP 1
                             Code            = '-1',
                             Name            = '-',
                             Producer        = '-',
                             ProducerCountry = '-',
                             Application     = '-',
                             CalendarCode    = Tbl_Calendar.CalendarCode,
                             Description     = '-'
                        FROM Tbl_Calendar
                        WHERE NOT EXISTS(SELECT * FROM [Tbl_Machines] WHERE Code ='-1')
                        ORDER BY Tbl_Calendar.CalendarCode ;
                         "
            });

            scripts.Add(new DBScript()
            {
                Number = 4,
                Version = 14010830,
                Script = @"--------------------------------------------------------------------------
                         IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_24HM]') AND type in (N'U'))
                             DROP TABLE [dbo].[tbl_24HM] ;

                        CREATE TABLE [dbo].[tbl_24HM](
                        	[Hour] [int] NULL,
                        	[Minute] [int] NULL,
                        	[HM] [int] NULL,
                        	[HMStr] [char](4) NULL,
                        	[HourMinute] [char](5) NULL
                        ) ON [PRIMARY] ;
                        
                        
                        WITH cte_Mins
                              AS (SELECT    0 AS M
                                  UNION ALL
                                  SELECT    M+ 1
                                  FROM      cte_Mins
                                  WHERE     M < 59
                                 )
                        SELECT M
                        INTO #Mins       
                        FROM    cte_Mins
                        OPTION
                            (MAXRECURSION 0);  
                        
                        WITH cte_Hours
                              AS (SELECT    0 AS H
                                  UNION ALL
                                  SELECT    H+ 1
                                  FROM      cte_Hours
                                  WHERE     H < 23
                                 )
                        SELECT H
                        INTO #Hours       
                        FROM    cte_Hours
                        OPTION
                            (MAXRECURSION 0);
                        
                        
                        INSERT INTO [dbo].[tbl_24HM]
                                   ([Hour]
                                   ,[Minute]
                                   ,[HM]
                                   ,[HMStr]
                                   ,[HourMinute])
                        SELECT 
                             [Hour]   = Hours.H
                            ,[Minute] = Mins.M
                            ,[HM]     = Hours.H *100 + Mins.M
                            ,[HMStr]  = FORMAT((Hours.H *100 + Mins.M),'0000')
                            ,[HourMinute] = FORMAT(Hours.H ,'00') + ':' + FORMAT(Mins.M,'00')
                        FROM #Hours AS Hours 
                             OUTER APPLY (SELECT M FROM #Mins) AS Mins ;
                        ----------------------------------------------------------------------------
                        ----------------------------------------------------------------------------
                         "
            });

            scripts.Add(new DBScript()
            {
                Number = 5,
                Version = 14010830,
                Script = @"--------------------------------------------------------------------------
                        -- =============================================
                        -- Author:		Hamid Mohsendokht
                        -- Create date: 13/11/2022
                        -- =============================================
                        CREATE OR ALTER FUNCTION GetShamsiDMH 
                        (
                        	@D INT,
                        	@H INT,
                        	@M INT 
                        )
                        RETURNS BIGINT 
                        AS
                        BEGIN
                        	-- Declare the return variable here
                        	
                        
                        	-- Return the result of the function
                        	RETURN CAST(((@D * 100) + @H) AS BIGINT) * 100  + @M
                        
                        END; "
                 });

            scripts.Add(new DBScript()
            {
                Number = 6,
                Version = 14010830,
                Script = @"--------------------------------------------------------------------------
                        -- =============================================
                        -- Author:		Hamid
                        -- Create date: 21/11/2022
                        -- Description:	Create working time for a day based on shift times and holidays
                        -- =============================================
                        CREATE OR ALTER PROCEDURE sp_Get_DayWorkingTime 
                        	@CalendarCode VARCHAR(10), 
                        	@ShamsiDate   VARCHAR(8)
                        AS
                        BEGIN
                        	SELECT 
                             Shift_SE_Time.WorkingTime,
                        	 Shift_SE_Time.StartTime,
                        	 Shift_SE_Time.EndTime
                        INTO #DayWorkingtime
                        From Tbl_CalendarShifts 
                             OUTER APPLY (SELECT 
                        	                   StartH = CAST(SubString(ShiftStart,1,2) AS INT) ,
                        					   StartM = CAST(SubString(ShiftStart,4,2) AS INT),
                                               Duration  = CAST(SubString(ShiftDuration,1,2) AS INT) * 60 + CAST(SubString(ShiftDuration,4,2) AS INT) +
                        					               CAST(SubString(ShiftExtraTime,1,2) AS INT) * 60 + CAST(SubString(ShiftExtraTime,4,2) AS INT) -1
                        	             ) AS Shift
                        	 OUTER APPLY (SELECT 
                        	                    EndHour = (Shift.StartH  + (Shift.StartM + Shift.Duration)/60)* 100 , 
                        	                    HasNextDay = CASE WHEN (Shift.StartH  + (Shift.StartM + Shift.Duration)/60)* 100 >= 2400
                        					                    THEN 1
                        					                    ELSE 0
                        					               END 
                        	             ) AS ShiftEnd
                              OUTER APPLY(SELECT CurrentDay = 1 
                        	              UNION 
                        				  SELECT CurrentDay = 0 WHERE ShiftEnd.HasNextDay = 1  )
                        	            AS Parts
                              OUTER APPLY (SELECT
                        	                   WorkingTime = 1,
                        	                   StartTime = CASE WHEN CurrentDay = 1 
                        					                    THEN Shift.StartH * 100 + Shift.StartM 
                        										ELSE 0
                        									END,
                                               EndTime   = CASE WHEN ShiftEnd.EndHour < 2400
                        					                    THEN ShiftEnd.EndHour  + ((Shift.StartM + Shift.Duration) % 60) 
                        									    ELSE CASE  WHEN CurrentDay = 1 
                        										           THEN 2359
                        												   ELSE (ShiftEnd.EndHour - 2400)  + ((Shift.StartM + Shift.Duration) % 60)
                        											 END 
                        					               END 
                        	             ) AS Shift_SE_Time
                        Where CalendarCode = @CalendarCode 
                        UNION ALL 
                        SELECT 
                             DownTime_SE.WorkingTime,
                        	 DownTime_SE.StartTime,
                        	 DownTime_SE.EndTime
                        
                        From Tbl_CalendarShiftDownTimes 
                             OUTER APPLY (SELECT 
                        	                   StartH = CAST(SubString(DownTimeStart,1,2) AS INT),
                        					   StartM = CAST(SubString(DownTimeStart,4,2) AS INT),
                                               EndH   = CAST(SubString(DownTimeEnd,1,2) AS INT),
                        					   EndM   = CAST(SubString(DownTimeEnd,4,2) AS INT)
                        	             ) AS ShiftDownTime
                            OUTER APPLY (SELECT
                        	                   WorkingTime = -1,
                        	                   StartTime = ShiftDownTime.StartH * 100 + ShiftDownTime.StartM,
                                               EndTime   = ShiftDownTime.EndH   * 100 + ShiftDownTime.EndM
                        	             ) AS DownTime_SE
                        Where CalendarCode = @CalendarCode ;
                        ---------------------------------------
                        SELECT 
                            SHDateTime = dbo.GetShamsiDMH(@ShamsiDate, DayHM.[Hour], DayHM.[Minute]),
                            DayHM.HM,
                        	DayHM.WorkingTime
                        FROM(Select 
                                  HM          =  tbl_24HM.HM ,
                        		  [Hour]      = MAX(tbl_24HM.[Hour]), 
                        		  [Minute]    = MAX(tbl_24HM.[Minute]),
                                  WorkingTime = SUM(#DayWorkingtime.WorkingTime)
                             From #DayWorkingtime  
                                  INNER JOIN tbl_24HM 
                        	              ON tbl_24HM.HM >= #DayWorkingtime.StartTime 
                        			     AND tbl_24HM.HM <= #DayWorkingtime.EndTime
                             GROUP BY tbl_24HM.HM 
                            ) DayHM             
                        Order By DayHM.HM;
                        
                        END ; "
            });

            scripts.Add(new DBScript()
            {
                Number = 7,
                Version = 14011030,
                Script = @"--------------------------------------------------------------------------
                        -- =============================================
                        -- Author:		Hamid
                        -- Create date: 07/1/2023
                        -- Description:	Create working time for a day based on shift times and holidays
                        -- =============================================
                        IF OBJECT_ID (N'dbo.[Tbl_OperatorTask]', N'U') IS NULL 
                           BEGIN
                              CREATE TABLE [dbo].[Tbl_OperatorTask](
                              	[ID] [int] NOT NULL IDENTITY(1,1),
                              	[OperatorCode] [varchar](50) NOT NULL,
                              	[PlanningCode] [bigint] NOT NULL,
                              	[StartDate] [varchar](8) NOT NULL,
                              	[StartHour] [varchar](5) NOT NULL,
                              	[EndDate] [varchar](8) NOT NULL,
                              	[EndHour] [varchar](5) NOT NULL
                               CONSTRAINT [PK_Tbl_OperatorTask] PRIMARY KEY CLUSTERED ([ID] ASC ) ON [PRIMARY]
                              ) ON [PRIMARY] ;
                              
                              ALTER TABLE [dbo].[Tbl_OperatorTask]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_OperatorTask_Tbl_Operators] FOREIGN KEY([OperatorCode])
                              REFERENCES [dbo].[Tbl_Operators] ([OperatorCode])
                              ON DELETE CASCADE;
                              
                              
                              ALTER TABLE [dbo].[Tbl_OperatorTask] CHECK CONSTRAINT [FK_Tbl_OperatorTask_Tbl_Operators];
                              
                              ALTER TABLE [dbo].[Tbl_OperatorTask]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_OperatorTask_Tbl_Planning] FOREIGN KEY([PlanningCode])
                              REFERENCES [dbo].[Tbl_Planning] ([PlanningCode])
                              ON DELETE CASCADE;
                              
                              ALTER TABLE [dbo].[Tbl_OperatorTask] CHECK CONSTRAINT [FK_Tbl_OperatorTask_Tbl_Planning];
                              
                           END 
                         "
            });
            return scripts;
        }
    }
}
