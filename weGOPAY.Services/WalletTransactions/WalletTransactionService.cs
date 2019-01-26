using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Enums;
using weGOPAY.weGOPAY.Core.Models.WalletTransactions;
using weGOPAY.weGOPAY.Data;
using weGOPAY.weGOPAY.Services.Wallets;

namespace weGOPAY.weGOPAY.Services.WalletTransactions
{
    public class WalletTransactionService : IWalletTransactionService
    {
        private readonly weGOPAYDbContext _weGOPAYDbContext;


        public WalletTransactionService(IWalletServices walletServices, weGOPAYDbContext weGOPAYDbContext)
        {
            _weGOPAYDbContext = weGOPAYDbContext;
        }

        public async Task<long> MakeTransactionRequest(RequestWalletTransactionDto model)
        {
            #region Initiating Currency Transaction Request
            try
            {
                
                WalletTransaction nMakeTransaction = new WalletTransaction();

                var checkTransaction = await _weGOPAYDbContext.Wallet.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                if ((int)model.RequestCurrency == (int)CurrencyEnum.Naira && model.RequestAmount <= checkTransaction.NairaBalance)
                {
                    nMakeTransaction = new WalletTransaction
                    {
                        UserId = model.UserId,
                        Amount = model.RequestAmount,
                        RequestCurrency = (int)CurrencyEnum.Naira,
                        ResponseCurrency = (int)model.ResponseCurrency,
                        TransactionStatus = (int)TransactionStatus.Requested,

                    };

                }
                if ((int)model.RequestCurrency == (int)CurrencyEnum.USDollar && model.RequestAmount <= checkTransaction.NairaBalance)
                {
                    nMakeTransaction = new WalletTransaction
                    {
                        UserId = model.UserId,
                        Amount = model.RequestAmount,
                        RequestCurrency = (int)CurrencyEnum.USDollar,
                        ResponseCurrency = (int)model.ResponseCurrency,
                        TransactionStatus = (int)TransactionStatus.Requested,

                    };

                }
                if ((int)model.RequestCurrency == (int)CurrencyEnum.BritishPound && model.RequestAmount <= checkTransaction.NairaBalance)
                {
                    nMakeTransaction = new WalletTransaction
                    {
                        UserId = model.UserId,
                        Amount = model.RequestAmount,
                        RequestCurrency = (int)CurrencyEnum.BritishPound,
                        ResponseCurrency = (int)model.ResponseCurrency,
                        TransactionStatus = (int)TransactionStatus.Requested,

                    };

                }
                if ((int)model.RequestCurrency == (int)CurrencyEnum.Euro && model.RequestAmount <= checkTransaction.NairaBalance)
                {
                    nMakeTransaction = new WalletTransaction
                    {
                        UserId = model.UserId,
                        Amount = model.RequestAmount,
                        RequestCurrency = (int)CurrencyEnum.Euro,
                        ResponseCurrency = (int)model.ResponseCurrency,
                        TransactionStatus = (int)TransactionStatus.Requested,

                    };

                }
                if ((int)model.RequestCurrency == (int)CurrencyEnum.Yen && model.RequestAmount <= checkTransaction.NairaBalance)
                {
                    nMakeTransaction = new WalletTransaction
                    {
                        UserId = model.UserId,
                        Amount = model.RequestAmount,
                        RequestCurrency = (int)CurrencyEnum.Yen,
                        ResponseCurrency = (int)model.ResponseCurrency,
                        TransactionStatus = (int)TransactionStatus.Requested,

                    };
                }
                await _weGOPAYDbContext.WalletTransaction.AddAsync(nMakeTransaction);
                await _weGOPAYDbContext.SaveChangesAsync();
                return nMakeTransaction.Id;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
        }

        public async Task MakeTransactionResponse(ResponseWalletTransactionDto model, long requestId)
        {

            #region Transaction Response Completion
            try
            {
                #region Wallets/WalletTransaction Queries

                //Checks the Response Client wallet
                var getResponseWallet = await _weGOPAYDbContext.Wallet.Where(s => s.UserId == model.UserId).FirstOrDefaultAsync();

                //Checks the requested wallet transaction
                var nRequest = await _weGOPAYDbContext.WalletTransaction.Where(x => x.Id == requestId).FirstOrDefaultAsync();

                //Checks the Request Client wallet
                var getRequestWallet = await _weGOPAYDbContext.Wallet.Where(s => s.UserId == nRequest.UserId).FirstOrDefaultAsync();

                #endregion



                #region Naira to Dollar Transaction
                using (var transaction = _weGOPAYDbContext.Database.BeginTransaction())
                {
                    if (nRequest.RequestCurrency == (int)CurrencyEnum.Naira && getRequestWallet.NairaBalance >= nRequest.Amount)
                    {

                        if (getRequestWallet != null)
                        {
                            if (nRequest.ResponseCurrency == (int)CurrencyEnum.USDollar)
                            {
                                var currentNairaBalance = getRequestWallet.NairaBalance - nRequest.Amount;
                                var currentDollarBalance = getRequestWallet.USDBalance + ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);

                                //Updating the Request Client

                                getRequestWallet.NairaBalance = currentNairaBalance;
                                getRequestWallet.USDBalance = currentDollarBalance;
                              
                                _weGOPAYDbContext.Entry(getRequestWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();

                            }


                        }
                        if (getResponseWallet != null)
                        {
                            if (nRequest.RequestCurrency == (int)CurrencyEnum.Naira)
                            {
                                var currentDollarBalance = getResponseWallet.USDBalance - ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);
                                var currentNairaBalance = getResponseWallet.NairaBalance + nRequest.Amount;

                                //Updating the response Client

                                getResponseWallet.USDBalance = currentDollarBalance;
                                getResponseWallet.NairaBalance = currentNairaBalance;
                      
                                _weGOPAYDbContext.Entry(getResponseWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();
                            }

                        }
                    }

                    transaction.Commit();
                }

                #endregion

                #region Dollar to Naira Transaction
                using (var transaction = _weGOPAYDbContext.Database.BeginTransaction())
                {
                    if (nRequest.RequestCurrency == (int)CurrencyEnum.USDollar && getRequestWallet.USDBalance >= nRequest.Amount)
                    {

                        if (getRequestWallet != null)
                        {
                            if (nRequest.ResponseCurrency == (int)CurrencyEnum.Naira)
                            {
                                var  currentDollarBalance = getRequestWallet.USDBalance - nRequest.Amount;
                                var currentNairaBalance = getRequestWallet.NairaBalance + ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);

                                //Updating the Request Client

                                getRequestWallet.NairaBalance = currentNairaBalance;
                                getRequestWallet.USDBalance = currentDollarBalance;

                                _weGOPAYDbContext.Entry(getRequestWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();

                            }


                        }
                        if (getResponseWallet != null)
                        {
                            if (nRequest.RequestCurrency == (int)CurrencyEnum.Naira)
                            {
                                var currentNairaBalance = getResponseWallet.NairaBalance - ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);
                                var currentDollarBalance = getResponseWallet.USDBalance + nRequest.Amount;

                                //Updating the response Client

                                getResponseWallet.USDBalance = currentDollarBalance;
                                getResponseWallet.NairaBalance = currentNairaBalance;

                                _weGOPAYDbContext.Entry(getResponseWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();
                            }

                        }
                    }

                    transaction.Commit();
                }

                #endregion

                #region Dollar to Pounds Transaction
                using (var transaction = _weGOPAYDbContext.Database.BeginTransaction())
                {
                    if (nRequest.RequestCurrency == (int)CurrencyEnum.USDollar && getRequestWallet.USDBalance >= nRequest.Amount)
                    {

                        if (getRequestWallet != null)
                        {
                            if (nRequest.ResponseCurrency == (int)CurrencyEnum.BritishPound)
                            {
                                var currentPoundBalance = getRequestWallet.PoundBalance - nRequest.Amount;
                                var currentDollarBalance  = getRequestWallet.USDBalance + ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);

                                //Updating the Request Client

                                getRequestWallet.PoundBalance = currentPoundBalance;
                                getRequestWallet.USDBalance = currentDollarBalance;

                                _weGOPAYDbContext.Entry(getRequestWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();

                            }


                        }
                        if (getResponseWallet != null)
                        {
                            if (nRequest.RequestCurrency == (int)CurrencyEnum.USDollar)
                            {
                                var currentPoundBalance = getResponseWallet.NairaBalance - ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);
                                var currentDollarBalance = getResponseWallet.USDBalance + nRequest.Amount;

                                //Updating the response Client

                                getResponseWallet.PoundBalance = currentPoundBalance;
                                getResponseWallet.USDBalance = currentDollarBalance;

                                _weGOPAYDbContext.Entry(getResponseWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();
                            }

                        }
                    }

                    transaction.Commit();
                }

                #endregion

                #region Pounds to Dollar Transaction
                using (var transaction = _weGOPAYDbContext.Database.BeginTransaction())
                {
                    if (nRequest.RequestCurrency == (int)CurrencyEnum.BritishPound && getRequestWallet.USDBalance >= nRequest.Amount)
                    {

                        if (getRequestWallet != null)
                        {
                            if (nRequest.ResponseCurrency == (int)CurrencyEnum.USDollar)
                            {
                                var currentPoundBalance = getRequestWallet.PoundBalance - nRequest.Amount;
                                var  currentDollarBalance = getRequestWallet.USDBalance + ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);

                                //Updating the Request Client

                                getRequestWallet.PoundBalance = currentPoundBalance;
                                getRequestWallet.USDBalance = currentDollarBalance;

                                _weGOPAYDbContext.Entry(getRequestWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();

                            }
                        }
                        if (getResponseWallet != null)
                        {
                            if (nRequest.RequestCurrency == (int)CurrencyEnum.BritishPound)
                            {
                                var currentDollarBalance = getResponseWallet.USDBalance - ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);
                                var currentPoundBalance  = getResponseWallet.PoundBalance + nRequest.Amount;

                                //Updating the response Client

                                getResponseWallet.PoundBalance = currentPoundBalance;
                                getResponseWallet.USDBalance = currentDollarBalance;

                                _weGOPAYDbContext.Entry(getResponseWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();
                            }

                        }
                    }

                    transaction.Commit();
                }

                #endregion

                #region Pounds to Euro Transaction
                using (var transaction = _weGOPAYDbContext.Database.BeginTransaction())
                {
                    if (nRequest.RequestCurrency == (int)CurrencyEnum.BritishPound && getRequestWallet.USDBalance >= nRequest.Amount)
                    {

                        if (getRequestWallet != null)
                        {
                            if (nRequest.ResponseCurrency == (int)CurrencyEnum.Euro)
                            {
                                var currentPoundBalance = getRequestWallet.PoundBalance - nRequest.Amount;
                                var currentEuroBalance = getRequestWallet.EuroBalance + ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);

                                //Updating the Request Client

                                getRequestWallet.PoundBalance = currentPoundBalance;
                                getRequestWallet.USDBalance = currentEuroBalance;

                                _weGOPAYDbContext.Entry(getRequestWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();

                            }
                        }
                        if (getResponseWallet != null)
                        {
                            if (nRequest.RequestCurrency == (int)CurrencyEnum.BritishPound)
                            {
                                var currentEuroBalance = getResponseWallet.EuroBalance - ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);
                                var currentPoundBalance = getResponseWallet.PoundBalance + nRequest.Amount;

                                //Updating the response Client

                                getResponseWallet.PoundBalance = currentPoundBalance;
                                getResponseWallet.EuroBalance = currentEuroBalance;

                                _weGOPAYDbContext.Entry(getResponseWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();
                            }

                        }
                    }

                    transaction.Commit();
                }

                #endregion

                #region Euro to Pounds Transaction
                using (var transaction = _weGOPAYDbContext.Database.BeginTransaction())
                {
                    if (nRequest.RequestCurrency == (int)CurrencyEnum.Euro && getRequestWallet.USDBalance >= nRequest.Amount)
                    {

                        if (getRequestWallet != null)
                        {
                            if (nRequest.ResponseCurrency == (int)CurrencyEnum.BritishPound)
                            {
                                var currentEuroBalance = getRequestWallet.EuroBalance - nRequest.Amount;
                                var currentPoundBalance  = getRequestWallet.PoundBalance + ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);

                                //Updating the Request Client

                                getRequestWallet.PoundBalance = currentPoundBalance;
                                getRequestWallet.USDBalance = currentEuroBalance;

                                _weGOPAYDbContext.Entry(getRequestWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();

                            }
                        }
                        if (getResponseWallet != null)
                        {
                            if (nRequest.RequestCurrency == (int)CurrencyEnum.BritishPound)
                            {
                                var currentPoundBalance  = getResponseWallet.PoundBalance - ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);
                                var currentEuroBalance = getResponseWallet.EuroBalance + nRequest.Amount;

                                //Updating the response Client

                                getResponseWallet.PoundBalance = currentPoundBalance;
                                getResponseWallet.EuroBalance = currentEuroBalance;

                                _weGOPAYDbContext.Entry(getResponseWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();
                            }

                        }
                    }

                    transaction.Commit();
                }

                #endregion

                #region Naira to Pounds Transaction
                using (var transaction = _weGOPAYDbContext.Database.BeginTransaction())
                {
                    if (nRequest.RequestCurrency == (int)CurrencyEnum.Naira && getRequestWallet.NairaBalance >= nRequest.Amount)
                    {

                        if (getRequestWallet != null)
                        {
                            if (nRequest.ResponseCurrency == (int)CurrencyEnum.BritishPound)
                            {
                                var currentNairaBalance = getRequestWallet.NairaBalance - nRequest.Amount;
                                var currentPoundBalance = getRequestWallet.PoundBalance + ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);

                                //Updating the Request Client

                                getRequestWallet.NairaBalance = currentNairaBalance;
                                getRequestWallet.PoundBalance = currentPoundBalance;

                                _weGOPAYDbContext.Entry(getRequestWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();

                            }


                        }
                        if (getResponseWallet != null)
                        {
                            if (nRequest.RequestCurrency == (int)CurrencyEnum.Naira)
                            {
                                var currentPoundBalance = getResponseWallet.PoundBalance - ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);
                                var currentNairaBalance = getResponseWallet.NairaBalance + nRequest.Amount;

                                //Updating the response Client

                                getResponseWallet.PoundBalance = currentPoundBalance;
                                getResponseWallet.NairaBalance = currentNairaBalance;

                                _weGOPAYDbContext.Entry(getResponseWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();
                            }

                        }
                    }

                    transaction.Commit();
                }

                #endregion

                #region Pounds to Naira Transaction
                using (var transaction = _weGOPAYDbContext.Database.BeginTransaction())
                {
                    if (nRequest.RequestCurrency == (int)CurrencyEnum.BritishPound && getRequestWallet.NairaBalance >= nRequest.Amount)
                    {

                        if (getRequestWallet != null)
                        {
                            if (nRequest.ResponseCurrency == (int)CurrencyEnum.Naira)
                            {
                                var currentPoundBalance = getRequestWallet.PoundBalance - nRequest.Amount;
                                var currentNairaBalance  = getRequestWallet.NairaBalance + ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);

                                //Updating the Request Client

                                getRequestWallet.NairaBalance = currentNairaBalance;
                                getRequestWallet.PoundBalance = currentPoundBalance;

                                _weGOPAYDbContext.Entry(getRequestWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();

                            }


                        }
                        if (getResponseWallet != null)
                        {
                            if (nRequest.RequestCurrency == (int)CurrencyEnum.Naira)
                            {
                                var currentNairaBalance = getResponseWallet.NairaBalance - ConvertedAmount(nRequest.RequestCurrency, nRequest.ResponseCurrency, nRequest.Amount);
                                var currentPoundBalance  = getResponseWallet.PoundBalance + nRequest.Amount;

                                //Updating the response Client

                                getResponseWallet.PoundBalance = currentPoundBalance;
                                getResponseWallet.NairaBalance = currentNairaBalance;

                                _weGOPAYDbContext.Entry(getResponseWallet).State = EntityState.Modified;
                                await _weGOPAYDbContext.SaveChangesAsync();
                            }

                        }
                    }

                    transaction.Commit();
                }

                #endregion




                //Update the wallet transactions
                #region Update transaction Status to Responded
                if(nRequest != null)
                {
                    nRequest.TransactionStatus = (int)TransactionStatus.Responded;
                }
                _weGOPAYDbContext.Entry(nRequest).State = EntityState.Modified;
                await _weGOPAYDbContext.SaveChangesAsync();

                #endregion

            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            #endregion
        }


        public async Task<IEnumerable<WalletTransactionDto>> GetAllTransactionRequest()
        {
            try
            {
                var allWalletTrxnDto = new List<WalletTransactionDto>();
                var allWallettransactions = await _weGOPAYDbContext.WalletTransaction.ToListAsync();
                
                allWalletTrxnDto.AddRange(allWallettransactions.OrderBy(s => s.TransactionDate).Select(x => new WalletTransactionDto
                {
                    UserId = x.UserId,
                    RequestAmount = x.Amount,
                    TransactionStatus = x.TransactionStatus,
                    RequestCurrency = x.RequestCurrency,
                    ResponseCurrency = x.ResponseCurrency
                }).ToList());
                return allWalletTrxnDto;
             
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<WalletTransactionDto> GetTransactionRequestById(long id)
        {
            try
            {
                var walletTransDto = new WalletTransactionDto();
                var transaction = await _weGOPAYDbContext.WalletTransaction.Where(s => s.Id == id).FirstOrDefaultAsync();
                if (transaction == null)
                    return null;
                else{
                    walletTransDto.UserId = transaction.UserId;
                    walletTransDto.Id = transaction.Id;
                    walletTransDto.RequestAmount = transaction.Amount;
                    walletTransDto.RequestCurrency = transaction.RequestCurrency;
                    walletTransDto.ResponseCurrency = transaction.ResponseCurrency;
                    walletTransDto.TransactionStatus = transaction.TransactionStatus;

                    return walletTransDto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InitiateTransaction(ResponseWalletTransactionDto response, long id)
        {
            var initiatedResponse = await _weGOPAYDbContext.WalletTransaction.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(initiatedResponse != null && initiatedResponse.TransactionStatus == (int)TransactionStatus.Requested)
            {
                //Update the wallet Status to Processing
                {
                    initiatedResponse.TransactionStatus = (int)TransactionStatus.Processing;
                }
                _weGOPAYDbContext.Entry(initiatedResponse).State = EntityState.Modified;
                await _weGOPAYDbContext.SaveChangesAsync();
            }
            return false;
        }



        #region Conversion Helper
        public decimal ConvertedAmount(int reqCurr, int resCurr, decimal amount)
        {
            if(reqCurr == (int)CurrencyEnum.Naira)
            {
                if(resCurr == (int)CurrencyEnum.USDollar)
                {
                    var con = (amount)/(decimal)(362.27);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.Yen)
                {
                    var con = (amount)/(decimal)(3.31);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.BritishPound)
                {
                    var con = (amount)/(decimal)(467.94);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.Euro)
                {
                    var con = (amount)/(decimal)(410.88);
                    return con;

                }

                return amount;
            }

            if (reqCurr == (int)CurrencyEnum.USDollar)
            {
                if (resCurr == (int)CurrencyEnum.Naira)
                {
                    var con = (amount)*(decimal)(362.27);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.Yen)
                {
                    var con = (amount)*(decimal)(109.4471);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.BritishPound)
                {
                    var con = (amount)*(decimal)(0.7742);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.Euro)
                {
                    var con = (amount)*(decimal)(0.8817);
                    return con;

                }

                return amount;
            }

            if (reqCurr == (int)CurrencyEnum.BritishPound)
            {
                if (resCurr == (int)CurrencyEnum.Naira)
                {
                    var con = (amount)*(decimal)(467.94);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.Yen)
                {
                    var con = (amount)*(decimal)(141.3716);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.USDollar)
                {
                    var con = (amount)*(decimal)(1.2917);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.Euro)
                {
                    var con = (amount) * (decimal)(1.1389);
                    return con;

                }

                return amount;
            }

            if (reqCurr == (int)CurrencyEnum.Euro)
            {
                if (resCurr == (int)CurrencyEnum.Naira)
                {
                    var con = (amount) * (decimal)(410.88);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.Yen)
                {
                    var con = (amount) * (decimal)(124.1329);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.USDollar)
                {
                    var con = (amount) * (decimal)(1.1342);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.BritishPound)
                {
                    var con = (amount) * (decimal)(0.8781);
                    return con;

                }

                return amount;
            }

            if (reqCurr == (int)CurrencyEnum.Yen)
            {
                if (resCurr == (int)CurrencyEnum.Naira)
                {
                    var con = (amount) * (decimal)(3.31);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.BritishPound)
                {
                    var con = (amount) * (decimal)(0.00707);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.USDollar)
                {
                    var con = (amount) * (decimal)(0.00914);
                    return con;

                }
                if (resCurr == (int)CurrencyEnum.Euro)
                {
                    var con = (amount) * (decimal)(0.00806);
                    return con;

                }

                return amount;
            }

            return amount;

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
        // ~WalletTransactionService() {
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
