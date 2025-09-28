using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionPlanning.Model
{
    public class OperatorTask
    {
        public int ID { get; set; }
        public string OperatorCode { get; set; }
        public string PlanningCode { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string StartDate { get; set; }
        public string StartHour { get; set; }
        public string EndDate { get; set; }
        public string EndHour { get; set; }

        public string OperatorName { get; set; }
        public string BatchDescription { get; set; }
        public string ProductDescription { get; set; }
        public string OpDescription { get; set; }
        public string MachineDescription { get; set; }
        
       
    }
}
