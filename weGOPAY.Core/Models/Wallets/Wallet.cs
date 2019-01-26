using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;

namespace weGOPAY.weGOPAY.Core.Models.Wallets
{
    public class Wallet
    { 
        public long Id { get; set; }

        public string UserId { get; set; }
       // public int Currency { get; set; }

        public decimal NairaBalance { get;  set; }

        public decimal USDBalance { get; set; }

        public decimal EuroBalance { get; set; }
        public decimal PoundBalance { get; set; }

        public decimal YenBalance { get; set; }
      

        public string Status { get; set; }


        public DateTime WalletCreationDate { get; set; }
    }
}
