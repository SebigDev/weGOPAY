using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Models.Wallets
{
    public class UpdateWalletDto
    {

        public long Id { get; set; }

        public int Currency { get; set; }
        
        public decimal Amount { get; set; }
        public DateTime WalletCreationDate { get; set; }
    }
}
