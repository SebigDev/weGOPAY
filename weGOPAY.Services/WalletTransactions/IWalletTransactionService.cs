using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Models.WalletTransactions;

namespace weGOPAY.weGOPAY.Services.WalletTransactions
{
    public interface IWalletTransactionService : IDisposable
    {
        Task<long> MakeTransactionRequest(RequestWalletTransactionDto model);
        Task MakeTransactionResponse(ResponseWalletTransactionDto model, long requestId);
        Task<IEnumerable<WalletTransactionDto>> GetAllTransactionRequest();
        Task<WalletTransactionDto> GetTransactionRequestById(long id);

        Task<bool> InitiateTransaction(ResponseWalletTransactionDto response, long id);
    }
}
