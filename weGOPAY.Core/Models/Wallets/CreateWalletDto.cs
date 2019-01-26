using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;

namespace weGOPAY.weGOPAY.Core.Models.Wallets
{
    public class CreateWalletDto
    {

        public string UserId { get; set; }
        public int Currency { get; set; }

        public decimal Amount { get; set; }

        public Status Status { get; set; }
    }
}
