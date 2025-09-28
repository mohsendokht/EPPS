using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning.Model
{
    public class Machine
    {
        public string MachineCode { get; set; }
        public int MachinePriority { get; set; }
        public double OneExecutionTime { get; set; }
        public double MachineSetupTime { get; set; }
        public EnumTimeType SetupTimeType { get; set; }
        public double SetupDuration { get; set; }
        public int CalendarCode { get; set; }
        public double Speed { get; set; }
        public EnumTimeType TimeType { get; set; }
        public double OneDuration { get; set; }


    }
}
