using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionPlanning.Model
{
    internal class CalendarDayWorkingTime
    {
        public string CalendarCode { get; set; }
        public String Shdate { get; set; }
        public int HourMinute { get; set; }

        public Int64 DHM => Int64.Parse(Shdate) * 10000 + HourMinute;
        public int WorkingTime { get; set;}
    }
}
