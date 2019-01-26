using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;

namespace weGOPAY.weGOPAY.Core.Models.WalletTransactions
{
    public class RequestWalletTransactionDto
    {

        public string UserId { get; set; }

        public CurrencyEnum RequestCurrency { get; set; }

        public CurrencyEnum ResponseCurrency { get; set; }

        public decimal RequestAmount { get; set; }

        public TransactionStatus TransactionStatus { get; set; }
    }
}
