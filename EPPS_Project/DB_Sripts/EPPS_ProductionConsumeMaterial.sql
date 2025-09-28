SELECT 
   MaterialCode      = Tbl_PrimaryMaterials.MaterialCode,
   MaterialTitle     = Tbl_PrimaryMaterials.MaterialTitle,
   ConsumeDate       = Tbl_RealProduction.StartDate,
   MachineCode       = Tbl_RealProduction.MachineCode,
   ProductCode       = Tbl_Products.ProductCode,
   OneOprationAmount = Tbl_OperationMaterials.OneUnitBuildAmount,
   OneOprationUnit   = Tbl_TestUnits.Title,
   NumberOfOperation = Tbl_RealProduction.IntactQuantity,
   ConsumeMaterial   = Tbl_RealProduction.IntactQuantity * Op_Material.Consume,
   StockUnit         = Op_Material.StockUnit,
   --  
   ProductName       = Tbl_Products.ProductName,
   MachineName       = Tbl_Machines.Name
   
FROM Tbl_RealProduction
     INNER JOIN Tbl_ProductTree 
             ON Tbl_ProductTree.TreeCode = Tbl_RealProduction.TreeCode
     INNER JOIN Tbl_Products
	         ON Tbl_Products.ProductCode = Tbl_ProductTree.ProductCode 
     INNER JOIN Tbl_Machines 
	         ON Tbl_Machines.Code = Tbl_RealProduction.MachineCode
	 INNER JOIN Tbl_ProductOPCs 
	         ON Tbl_ProductOPCs.TreeCode = Tbl_RealProduction.TreeCode
	        AND Tbl_ProductOPCs.OperationCode = Tbl_RealProduction.OperationCode
     INNER JOIN Tbl_OperationMaterials
	         ON Tbl_OperationMaterials.TreeCode = Tbl_RealProduction.TreeCode
	        AND Tbl_OperationMaterials.CurrentOperationCode = Tbl_RealProduction.OperationCode
     INNER JOIN Tbl_TestUnits 
	         ON Tbl_TestUnits.Code = Tbl_OperationMaterials.ProductionTestUnit
     INNER JOIN Tbl_PrimaryMaterials 
	         ON Tbl_PrimaryMaterials.MaterialCode = Tbl_OperationMaterials.MaterialCode
	 INNER JOIN Tbl_TestUnits 
	         AS StoreUnit 
	         ON StoreUnit.Code = Tbl_PrimaryMaterials.StoreUnit
     LEFT  JOIN Tbl_UnitsRelations 
	         ON Tbl_UnitsRelations.BaseUnitCode = Tbl_PrimaryMaterials.StoreUnit 
            AND Tbl_UnitsRelations.RelatedUnitCode = Tbl_OperationMaterials.ProductionTestUnit 
     OUTER APPLY (SELECT
	                  Consume       = CASE WHEN Tbl_PrimaryMaterials.StoreUnit <> Tbl_OperationMaterials.ProductionTestUnit 
	                                        AND Tbl_UnitsRelations.CommunicationFactorUnit IS NOT NULL 
				                           THEN Tbl_OperationMaterials.OneUnitBuildAmount / Tbl_UnitsRelations.CommunicationFactorUnit 
				                           ELSE Tbl_OperationMaterials.OneUnitBuildAmount
	    		                      END,
	                  StockUnitCode = CASE WHEN Tbl_PrimaryMaterials.StoreUnit <> Tbl_OperationMaterials.ProductionTestUnit 
				                            AND Tbl_UnitsRelations.CommunicationFactorUnit IS NOT NULL 
				                           THEN Tbl_PrimaryMaterials.StoreUnit 
				                           ELSE Tbl_OperationMaterials.ProductionTestUnit
			                          END,
	                  StockUnit     = CASE WHEN Tbl_PrimaryMaterials.StoreUnit <> Tbl_OperationMaterials.ProductionTestUnit 
				                            AND Tbl_UnitsRelations.CommunicationFactorUnit IS NOT NULL 
				                           THEN StoreUnit.Title
				                           ELSE Tbl_TestUnits.Title
				                     END
	              ) AS Op_Material