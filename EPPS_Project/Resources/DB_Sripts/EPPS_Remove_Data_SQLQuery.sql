-----------------------------------------
-- THIS SCRIPT DELETE ALL EPPS RECORDS.
-----------------------------------------
--USE [PSB_ProductionPlanning]

-- BEGIN TRAN
DELETE FROM [Tbl_Batch_Order]

DELETE FROM Tbl_CalendarParticularDays
DELETE FROM Tbl_CalendarDaysDownTimes
DELETE FROM Tbl_CalendarDays
DELETE FROM Tbl_CalendarShiftDownTimes
DELETE FROM Tbl_CalendarParticularShifts
DELETE FROM Tbl_CalendarShifts
DELETE FROM Tbl_CapametryAccessibleTimes
DELETE FROM Tbl_ContractorOperations
DELETE FROM Tbl_Contractors
DELETE FROM Tbl_CustomerOrders
DELETE FROM Tbl_Customers
DELETE FROM Tbl_HoliDays
DELETE FROM Tbl_MachineCapametry
DELETE FROM Tbl_MachineNotAvailableTimes
DELETE FROM Tbl_MatchedOperations
DELETE FROM Tbl_MaterialCapametry
DELETE FROM Tbl_MPSDetails
DELETE FROM Tbl_MPSs
DELETE FROM Tbl_OEEDetails
DELETE FROM Tbl_OEEs
DELETE FROM Tbl_Operation_Measurement_UnitIndex
DELETE FROM Tbl_OperationMaterials
DELETE FROM Tbl_OperationNetworkPaths
DELETE FROM Tbl_OperationsDefaultTitles
DELETE FROM Tbl_OperatorWorkPeriods
DELETE FROM Tbl_ParticularShiftDownTimes
DELETE FROM Tbl_PersonnelCapametry
DELETE FROM Tbl_Planning
DELETE FROM Tbl_PreOperations
DELETE FROM Tbl_PrimaryMaterials
DELETE FROM Tbl_ProductionHalts
DELETE FROM Tbl_ProductionOperators
DELETE FROM Tbl_ProductionSubbatchsDetail
DELETE FROM Tbl_ProductionSubbatchs
DELETE FROM Tbl_ProductionBatchsDetail
DELETE FROM Tbl_ProductionBatchs
DELETE FROM Tbl_RealProduction
DELETE FROM Tbl_ReplacementMachines
DELETE FROM Tbl_SubbatchPlanningAlerts
DELETE FROM Tbl_TestUnits
DELETE FROM Tbl_UnitsRelations
DELETE FROM Tbl_Suppliers
DELETE FROM Tbl_ProductOPCsExecutorMachines
DELETE FROM Tbl_ProductOPCs
DELETE FROM Tbl_ProductTreeDetails
DELETE FROM Tbl_ProductTree
DELETE FROM Tbl_Products
DELETE FROM Tbl_Operators
DELETE FROM Tbl_HaltReasons
DELETE FROM Tbl_Machines
DELETE FROM Tbl_Natures 
DELETE FROM [Tbl_Calendar]

INSERT INTO Tbl_RelationTypes (TypeCode, TypeTitle)
SELECT 
     RT.TypeCode, RT.TypeTitle
FROM (SELECT TypeCode = 1, TypeTitle = 'FS'   UNION
      SELECT TypeCode = 2, TypeTitle = 'FF'   UNION
	  SELECT TypeCode = 3, TypeTitle = 'SS'   UNION
      SELECT TypeCode = 4, TypeTitle = 'SF'   UNION
      SELECT TypeCode = 5, TypeTitle = 'ASAP' 
     ) 
  AS RT
  LEFT JOIN Tbl_RelationTypes ON Tbl_RelationTypes.TypeCode = RT.TypeCode
WHERE Tbl_RelationTypes.TypeCode IS NULL;

-- ROLLBACK
-- Create Tbl_DB_Version
if Not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Tbl_DB_Version]') And OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
   CREATE TABLE [dbo].[Tbl_DB_Version]([DB_Version] [char] (10) NULL ,
                                       [UpdateTime] [datetime] NULL) ON [PRIMARY]
   
   INSERT INTO  [dbo].[Tbl_DB_Version]([DB_Version], [UpdateTime]) VALUES('13900130',GetDate())
End
Else
Begin
   DELETE FROM [Tbl_DB_Version]
   INSERT INTO  [dbo].[Tbl_DB_Version]([DB_Version], [UpdateTime]) VALUES('13900130',GetDate())
   
End
GO