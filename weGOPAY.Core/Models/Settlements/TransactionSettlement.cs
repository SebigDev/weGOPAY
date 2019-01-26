using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Models.Users;
using weGOPAY.weGOPAY.Core.Models.Wallets;
using weGOPAY.weGOPAY.Core.Models.WalletTransactions;

namespace weGOPAY.weGOPAY.Core.Models.Settlements
{
    public class TransactionSettlementDto
    {
        public UserDto User { get; set; }

        //public Wallet Wallet { get; set; }
        public WalletTransactionDto WalletTransaction { get; set; }
        public SettlementDto Settlement { get; set; }

    }
}
