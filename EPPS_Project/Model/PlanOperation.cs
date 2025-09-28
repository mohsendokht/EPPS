using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductionPlanning.MyEnums;
using static ProductionPlanning.Module1;

namespace ProductionPlanning.Model
{
    public class PlanOperation
    {
        public Operation Op { get; set; }
        public List<PreOperation> PreOps { get; set; }
        public List<Operation> PostOps { get; set; }

        public string TreeCode  { get; set; }
        public string SubbatchCode { get; set; }
                          
        public int SubbatchStartDate { get; set; }
        public int Qty { get; set; }
        //public ShDateHour StartDH { get; set; }
        public PlanningRec planningRec { get; set; }

        public List<PlanningRec> planningRecs {get; set;}


       

    }
}
