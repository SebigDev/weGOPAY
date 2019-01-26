using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace weGOPAY.weGOPAY.Core.Models.Response
{
    public class TransactionResponse
    {
        public int Currency { get; set; }
        public decimal Balance { get; set; }
    }
}
