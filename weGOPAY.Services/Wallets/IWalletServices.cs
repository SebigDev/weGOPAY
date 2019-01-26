using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Models.Wallets;

namespace weGOPAY.weGOPAY.Services.Wallets
{
    public interface IWalletServices : IDisposable
    {
        Task<long> CreateWallet(CreateWalletDto model, long idUser);
        Task UpdateWallet(UpdateWalletDto model, long idUser);
        Task<IEnumerable<WalletDto>> GetAllWallets();
        Task<WalletDto> GetWalletByUserId(string id);
        Task<WalletDto> GetWalletById(long id);

        Task<WalletDto> GetWalletByStatus(string status);


    }
}
