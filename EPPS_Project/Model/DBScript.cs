using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning.Model
{
    public class DBScript
    {
        public int Number { get; set; }
        public int Version { get; set; }
        public string Script { get; set; }
        
    }
}
