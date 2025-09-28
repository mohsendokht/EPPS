using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductionPlanning.MyEnums;

namespace ProductionPlanning.Model
{
    public class Operation
    {

        public string Code { get; set; }
        public string ProductCode { get; set; }
        public string TreeCode { get; set; }

        public EnumExecutionMethod ExecutionMethod { get; set; }
        public List<Machine> Machines { get; set; }
        public Contractor Contractor { get; set; }
       

    }
}
