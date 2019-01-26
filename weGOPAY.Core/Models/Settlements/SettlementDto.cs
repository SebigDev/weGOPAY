using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Models.Settlements
{
    public class SettlementDto
    {
        public long TransactionId { get; set; }
        public string SettlementStatus { get; set; }
        public string ReferenceId { get; set; }
        public DateTime DateOfSettlement { get; set; }
    }
}
