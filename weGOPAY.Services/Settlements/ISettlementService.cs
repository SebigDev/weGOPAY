using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Models.Settlements;

namespace weGOPAY.weGOPAY.Services.Settlements
{
   public  interface ISettlementService :IDisposable
    {
        Task<bool> VerifyTransaction(long transId);

        Task<TransactionSettlementDto> GenerateSettlement(long transId);
    }
}
