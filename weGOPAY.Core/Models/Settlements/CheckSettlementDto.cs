using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;

namespace weGOPAY.weGOPAY.Core.Models.Settlements
{
    public class CheckSettlementDto
    {
        public TransactionStatus SettlementStatus { get; set; }

        public long TransactionId { get; set; }
        public string ReferenceId { get; set; }
    }
}
