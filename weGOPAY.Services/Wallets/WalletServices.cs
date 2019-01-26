using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;
using weGOPAY.weGOPAY.Core.Extensions;
using weGOPAY.weGOPAY.Core.Models.Wallets;
using weGOPAY.weGOPAY.Data;

namespace weGOPAY.weGOPAY.Services.Wallets
{
    public class WalletServices : IWalletServices
    {
        private readonly weGOPAYDbContext _weGOPAYDbContext;
       
       

        public WalletServices(weGOPAYDbContext weGOPAYDbContext)
        {
            _weGOPAYDbContext = weGOPAYDbContext;

       
        }

        public async Task<long> CreateWallet(CreateWalletDto model, long idUser)
        {
            try
            {
                var user = await _weGOPAYDbContext.User.Where(s => s.Id == idUser).FirstOrDefaultAsync();
                var userId = user.UserId;

                var nWallet = new Wallet
                {
                    UserId = userId,
                    Status = model.Status.Description(),
                    NairaBalance = GetNairaBalance(model.Currency, model.Amount, userId),
                    EuroBalance = GetEuroBalance(model.Currency, model.Amount, userId),
                    PoundBalance = GetPoundBalance(model.Currency, model.Amount, userId),
                    USDBalance = GetDollarBalance(model.Currency, model.Amount, userId),
                    YenBalance = GetYenBalance(model.Currency, model.Amount, userId),

                    WalletCreationDate = DateTime.UtcNow
 
                };
                 await _weGOPAYDbContext.Wallet.AddAsync(nWallet);
                await _weGOPAYDbContext.SaveChangesAsync();
                return nWallet.Id;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task UpdateWallet(UpdateWalletDto model, long idUser)
        {
            try
            {

                var user = await _weGOPAYDbContext.User.Where(s => s.Id == idUser).FirstOrDefaultAsync();
                var userId = user.UserId;
                var nUpdate = await _weGOPAYDbContext.Wallet.FindAsync(model.Id);
                if (nUpdate != null)
                {
                    nUpdate.Id = model.Id;
                    nUpdate.WalletCreationDate = DateTime.UtcNow;
                    nUpdate.NairaBalance = GetNairaBalance(model.Currency, model.Amount, userId);
                    nUpdate.EuroBalance = GetEuroBalance(model.Currency, model.Amount, userId);
                    nUpdate.PoundBalance = GetPoundBalance(model.Currency, model.Amount, userId);
                    nUpdate.USDBalance = GetDollarBalance(model.Currency, model.Amount, userId);
                    nUpdate.YenBalance = GetYenBalance(model.Currency, model.Amount, userId);
                };
                _weGOPAYDbContext.Entry(nUpdate).State = EntityState.Modified;
                await _weGOPAYDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<WalletDto> GetWalletById(long id)
        {
            try
            {
                var model = await _weGOPAYDbContext.Wallet.Where(a => a.Id == id).FirstOrDefaultAsync();
                if (model == null) return null;
                var nAcct = new WalletDto
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    Status = model.Status,
                    NairaBalance = model.NairaBalance,
                    USDBalance = model.USDBalance,
                    EuroBalance = model.EuroBalance,
                    PoundBalance = model.PoundBalance,
                    YenBalance = model.YenBalance,
                    WalletCreationDate = model.WalletCreationDate
                };
                return nAcct;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<WalletDto> GetWalletByStatus(string status)
        {
            var model = await _weGOPAYDbContext.Wallet.Where(a => a.Status == status).FirstOrDefaultAsync();
            if (model == null) return null;
            var nAcct = new WalletDto
            {
                Id = model.Id,
                UserId = model.UserId,
                Status = model.Status,
                NairaBalance = model.NairaBalance,
                USDBalance = model.USDBalance,
                EuroBalance = model.EuroBalance,
                PoundBalance = model.PoundBalance,
                YenBalance = model.YenBalance,
                WalletCreationDate = model.WalletCreationDate
            };
            return nAcct;
        }

        public async Task<WalletDto> GetWalletByUserId(string userId)
        {
            var model = await _weGOPAYDbContext.Wallet.Where(a => a.UserId == userId).FirstOrDefaultAsync();
            if (model == null) return null;
            var nAcct = new WalletDto
            {
                Id = model.Id,
                UserId = model.UserId,
                Status = model.Status,
                NairaBalance = model.NairaBalance,
                USDBalance = model.USDBalance,
                EuroBalance = model.EuroBalance,
                PoundBalance = model.PoundBalance,
                YenBalance = model.YenBalance,
                WalletCreationDate = model.WalletCreationDate
            };
            return nAcct;
        }

        public async Task<IEnumerable<WalletDto>> GetAllWallets()
        {
            try
            {
                var WalletDto = new List<WalletDto>();
                var allWallets = await _weGOPAYDbContext.Wallet.ToListAsync();
               
                WalletDto.AddRange(allWallets.OrderBy(s => s.WalletCreationDate).Select(model => new WalletDto()
                {
                    Id = model.Id,
                    UserId = model.UserId,
                    Status = model.Status,
                    NairaBalance = model.NairaBalance,
                    USDBalance = model.USDBalance,
                    EuroBalance = model.EuroBalance,
                    PoundBalance = model.PoundBalance,
                    YenBalance = model.YenBalance,
                    WalletCreationDate = model.WalletCreationDate
                }).ToList());
                return WalletDto;
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        #region Wallet Balance

        public decimal GetNairaBalance(int currency, decimal amount, string userId)
        {
            var current = _weGOPAYDbContext.Wallet.Where(x =>x.UserId == userId).FirstOrDefaultAsync().Result;
            decimal currNaira = current != null ? current.NairaBalance : 0.00000M;
            if (currency == (int)CurrencyEnum.Naira)
            {
                var balance = currNaira + amount;
                return balance;
            }
            return currNaira;

        }

        public decimal GetEuroBalance(int currency, decimal amount, string userId)
        {
            var current = _weGOPAYDbContext.Wallet.Where(x => x.UserId == userId).FirstOrDefaultAsync().Result;
            decimal currEuro = current != null ? current.EuroBalance : 0.0000M;
            if (currency == (int)CurrencyEnum.Euro)
            {
                var balance = currEuro + amount;
                return balance;
            }
            return currEuro;

        }

        public decimal GetDollarBalance(int currency, decimal amount, string userId)
        {
            var current = _weGOPAYDbContext.Wallet.Where(x => x.UserId == userId).FirstOrDefaultAsync().Result;
            decimal currDollar = current != null ? current.USDBalance: 0.00000M;
            if (currency == (int)CurrencyEnum.USDollar)
            {
                var balance = currDollar + amount;
                return balance;
            }
            return currDollar;
        }

        public decimal GetPoundBalance(int currency, decimal amount, string userId)
        {
            var current = _weGOPAYDbContext.Wallet.Where(x => x.UserId == userId).FirstOrDefaultAsync().Result;
            decimal currPound = current != null ? current.PoundBalance: 0.00000M;
            if (currency == (int)CurrencyEnum.BritishPound)
            {
                var balance = currPound + amount;
                return balance;
            }
            return currPound;
        }

        public decimal GetYenBalance(int currency, decimal amount, string userId)
        {
            var current = _weGOPAYDbContext.Wallet.Where(x => x.UserId == userId).FirstOrDefaultAsync().Result;
            decimal currYen = current != null ? current.YenBalance : 0.00000M;
            if (currency == (int)CurrencyEnum.Yen)
            {
                var balance = currYen + amount;
                return balance;
            }
            return currYen;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WalletServices() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

       


        #endregion
    }
}
