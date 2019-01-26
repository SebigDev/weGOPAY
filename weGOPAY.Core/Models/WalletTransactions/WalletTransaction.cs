using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;

namespace weGOPAY.weGOPAY.Core.Models.WalletTransactions
{
    public class WalletTransaction
    {
        public WalletTransaction()
        {
            TransactionDate = DateTime.UtcNow;
        }
        public long Id { get; set; }

        public string UserId { get; set; }

        public int RequestCurrency { get; set; }

        public decimal Amount { get; set; }

        public int ResponseCurrency { get; set; }

        public int TransactionStatus { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
