using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionPlanning.Model
{
    public class Contractor
    {
       public string ContractorCode { get; set; }
       public int MinimumTransferBatch { get; set; }
       public double TransferBatchExecutionTime { get; set; }
       public int BatchCapacity { get; set; }
       public int CalendarCode { get; set; }
       public double Speed { get; set; }
       public short TimeType { get; set; }
    }
}
