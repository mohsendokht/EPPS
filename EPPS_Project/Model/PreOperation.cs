using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning.Model
{
    public class PreOperation
    {

        public string Code { get; set; }
        public EnumRelationType RelationType { get; set; }
        public int LagTime { get; set; }

        public EnumExecutionMethod ExecutionMethod { get; set; }
        public EnumTimeType TimeType { get; set; }
        public double  LT { get; set; } // Calculated lag time base on  hour scale


    }
}
