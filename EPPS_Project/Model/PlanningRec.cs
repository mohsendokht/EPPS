
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning
{
    public class PlanningRec
    {
        public string OperationCode { get; set; }
        public string MachineCode { get; set; }
        public long ProductionQty { get; set; }

        public double PlanningDuration { get; set; }
        public string PlanningStartDate { get; set; }
        public string PlanningStartHour { get; set; }
        public string PlanningEndDate { get; set; }
        public string PlanningEndHour { get; set; }

        public double SetupDuration { get; set; }
        public string SetupStartDate { get; set; }
        public string SetupStartHour { get; set; }
        public string SetupEndDate { get; set; }
        public string SetupEndHour { get; set; }
               
        public double OperationDuration { get; set; }
        public string OperationStartDate { get; set; }
        public string OperationStartHour { get; set; }
        public string OperationEndDate { get; set; }
        public string OperationEndHour { get; set; }
        
        public long CopyPlanningCode { get; set; }

        public PlanningRec()
        {
            MachineCode = "-1";
        }
    }
}