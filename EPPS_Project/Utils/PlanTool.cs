using ProductionPlanning.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductionPlanning.MyEnums;
using static ProductionPlanning.Module1;

namespace ProductionPlanning.Utils
{
    public class PlanTool
    {
       
        public PlanOperation CreatePlanOpertion(string SubbatchCode, string TreeCode, string OperationCode)
        {
            PlanOperation planOp = new PlanOperation();
            planOp.TreeCode = TreeCode;
            planOp.SubbatchCode = SubbatchCode;
            planOp.Op = new Operation();
            planOp.Op.Code = OperationCode;
            // Get all operation details from opc and machine ,....
            planOp.Op.ExecutionMethod = GetExcutionMethod(TreeCode, OperationCode);
            planOp.Op.Machines = GetOpMachines(TreeCode, OperationCode, planOp.Op.ExecutionMethod);
            planOp.Op.Contractor  = GetOpContractor(TreeCode, OperationCode);
            planOp.PreOps = GetPreOperations(TreeCode, OperationCode);
            planOp.Qty = GetOperationPlanningQuantity(SubbatchCode, TreeCode, OperationCode);

            // initialise other properties
            //planOp.StartDH = new ShDateHour();
            planOp.planningRec = new PlanningRec();
            planOp.planningRecs = new List<PlanningRec>();

            return planOp;
        }

        #region Private Methods

        private int GetOperationPlanningQuantity(string SubbatchCode, string TreeCode, string OperationCode)
        {
            int mPlanningQuantity = -1;
            var selectQry = $"Select RequirementQuantity From Tbl_ProductionSubbatchsDetail Where SubbatchCode='{SubbatchCode}' And DetailCode IN (Select DetailCode From Tbl_ProductOPCs Where TreeCode ={TreeCode} And OperationCode ='{OperationCode}')";
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand(selectQry, cn);
                var dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    mPlanningQuantity = int.Parse(dr["RequirementQuantity"].ToString());
                }

                dr.Close();
                cn.Close();
            }

            if (mPlanningQuantity < 0)
            {
                mPlanningQuantity = 0;
                Logger.SaveError("GetOperationPlanningQuantity", "تعداد مورد نیاز برای برنامه ریزی عملیات {" + OperationCode + "} در ساب بچ {" + SubbatchCode + "{ یافت نشد");
            }

            return mPlanningQuantity;
        }

        private List<PreOperation> GetPreOperations(string TreeCode, string OperationCode)
        {
            var preOps = new List<PreOperation>();
            var selectQry = $"Select * From Tbl_PreOperations Where TreeCode={TreeCode} And CurrentOperationCode='{OperationCode}'";
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand(selectQry, cn);
                var dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var preOp =new PreOperation() { 
                        Code = dr["PreOperationCode"].ToString(),
                        RelationType = (EnumRelationType) short.Parse(dr["RelationType"].ToString()),
                        LagTime = int.Parse(dr["LagTime"].ToString()),
                        TimeType = (EnumTimeType)short.Parse(dr["TimeType"].ToString()) 
                    };
                    preOp.LT = Module1.ConvertToHour(preOp.TimeType, preOp.LagTime);
                    preOps.Add(preOp);
                }

                dr.Close();
                cn.Close();
            }

            return preOps;
        }

        private EnumExecutionMethod GetExcutionMethod(string TreeCode, string OperationCode)
        {
            var exeMethod = EnumExecutionMethod.EM_NONMACHINE;
            var selectQry = $"Select ExecutionMethod From Tbl_ProductOPCs Where TreeCode={TreeCode} And OperationCode='{OperationCode}'";
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand(selectQry, cn);
                var dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    exeMethod = (EnumExecutionMethod)short.Parse(dr["ExecutionMethod"].ToString());
                }

                dr.Close();
                cn.Close();
            }

            return exeMethod;
        }
        private List<Machine> GetOpMachines(string TreeCode, string OperationCode, EnumExecutionMethod method)
        {
            var machines = new List<Machine>();
            
            if (method == EnumExecutionMethod.EM_CONTRACTOR)
            {
                machines.Add(new Machine()
                {
                    MachineCode = "-1",
                    CalendarCode = 0
                    
                });
                return machines;
            }
            string selectQry = "Select  " +
                               "   MachineCode      = Tbl_Machines.Code, " +
                               "   CalendarCode     = CASE WHEN Tbl_Machines.Code = '-1' THEN Tbl_ProductOPCsExecutorMachines.CalendarCode ELSE Tbl_Machines.CalendarCode END,  " +
                               "   MachinePriority  = Tbl_ProductOPCsExecutorMachines.MachinePriority,  " +
                               "   MachineSetupTime = Tbl_ProductOPCsExecutorMachines.MachineSetupTime, " +
                               "   SetupTimeType    = Tbl_ProductOPCsExecutorMachines.SetupTimeType, " +
                               "   OneExecutionTime = Tbl_ProductOPCsExecutorMachines.OneExecutionTime, " +
                               "   TimeType         = Tbl_ProductOPCsExecutorMachines.TimeType " +
                               " From Tbl_ProductOPCsExecutorMachines  " +
                               " INNER JOIN Tbl_Machines ON Tbl_Machines.Code = Tbl_ProductOPCsExecutorMachines.MachineCode " +
                               " Where TreeCode=" + TreeCode + " And OperationCode='" + OperationCode + "'" + 
                               " Order BY MachinePriority";
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand(selectQry, cn);
                var dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    var machine = new Machine()
                    {
                        MachineCode = dr["MachineCode"].ToString(),
                        CalendarCode = int.Parse(dr["CalendarCode"].ToString()),
                        MachinePriority = int.Parse(dr["MachinePriority"].ToString()),
                        MachineSetupTime = double.Parse(dr["MachineSetupTime"].ToString()),
                        SetupTimeType = (EnumTimeType)int.Parse(dr["SetupTimeType"].ToString()),
                        OneExecutionTime = double.Parse(dr["OneExecutionTime"].ToString()),
                        TimeType = (EnumTimeType)short.Parse(dr["TimeType"].ToString()),
                        Speed = 1    //TODO: complete
                    };

                    machine.SetupDuration = Module1.ConvertToHour(machine.SetupTimeType, machine.MachineSetupTime, machine.CalendarCode);
                    machine.OneDuration = Module1.ConvertToHour(machine.TimeType, machine.OneExecutionTime, machine.CalendarCode); ;

                    machines.Add(machine); 
                }

                dr.Close();
                cn.Close();

                if (machines.Count == 0)
                {
                    Logger.SaveError("GetOpMachines", "اشکال در یافتن مشخصات ماشین برای عملیات" + Environment.NewLine + $"Operation: {OperationCode}");
                    throw new Exception("اشکال در یافتن مشخصات ماشین برای عملیات" + Environment.NewLine + $"Operation: {OperationCode}");
                }
            }
                       
            return machines;
        }

        //
        private Contractor GetOpContractor(string TreeCode, string OperationCode)
        {
            var cntr = new Contractor();

            
            var selectQry = $"Select * From Tbl_ContractorOperations Where TreeCode={TreeCode} And OperationCode='{OperationCode}'";
            using (var cn = new SqlConnection(PlanningCnnStr))
            {
                cn.Open();
                var cm = new SqlCommand(selectQry, cn);
                var dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    cntr.CalendarCode = int.Parse(dr["CalendarCode"].ToString());
                    cntr.TransferBatchExecutionTime = double.Parse(dr["CalendarCode"].ToString());
                    cntr.TimeType = short.Parse(dr["TimeType"].ToString());
                    var BatchCapacitySpeed = Module1.GetAnyTime_TO_Hour(cntr.TimeType, cntr.TransferBatchExecutionTime, Module1.GetCalendarAccessibleTime(cntr.CalendarCode.ToString()));
                    cntr.Speed = BatchCapacitySpeed == 0 ? 0: int.Parse(dr["BatchCapacity"].ToString()) / BatchCapacitySpeed;
                }

                dr.Close();
                cn.Close();
            }

            return cntr;
        }
        #endregion

    }
}
