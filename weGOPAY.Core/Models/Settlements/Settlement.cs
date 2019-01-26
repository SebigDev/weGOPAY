using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Models.Settlements
{
    public class Settlement
    {
        public long  Id { get; set; }
        public long TransactionId { get; set; }
        public int SettlementStatus { get; set; }
        public string ReferenceId { get; set; }
        public DateTime DateOfSettlement { get; set; }
    }
}
