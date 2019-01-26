using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Models.WalletTransactions
{
    public class WalletTransactionDto
    {
        public long Id { get; set; }

        public string UserId { get; set; }

        public int RequestCurrency { get; set; }

        public decimal RequestAmount { get; set; }

        public decimal ResponseAmount { get; set; }

        public int ResponseCurrency { get; set; }

        public int TransactionStatus { get; set; }
    }
}
