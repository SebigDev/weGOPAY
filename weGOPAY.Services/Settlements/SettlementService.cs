using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weGOPAY.weGOPAY.Core.Models.Settlements;
using weGOPAY.weGOPAY.Data;
using weGOPAY.weGOPAY.Core.Enums;
using weGOPAY.weGOPAY.Core.Models.WalletTransactions;
using weGOPAY.weGOPAY.Services.Users;
using weGOPAY.weGOPAY.Services.WalletTransactions;
using weGOPAY.weGOPAY.Core.Extensions;
using weGOPAY.weGOPAY.Core.Models.Users;

namespace weGOPAY.weGOPAY.Services.Settlements
{
    public class SettlementService : ISettlementService
    {
        private readonly weGOPAYDbContext _weGOPAYDbContext;
        private readonly IUserServices _userService;
        private readonly IWalletTransactionService _walletTransactionService;
        public SettlementService(weGOPAYDbContext weGOPAYDbContext,
            IUserServices userServices, 
            IWalletTransactionService walletTransactionService
            )
        {
            _weGOPAYDbContext = weGOPAYDbContext;
            _userService = userServices;
            _walletTransactionService = walletTransactionService;
        }
       
        public async Task<bool> VerifyTransaction(long transId)
        {
            try
            {
                var trans = await _walletTransactionService.GetTransactionRequestById(transId);

                if (trans.TransactionStatus == (int)TransactionStatus.Responded)
                {
                    await GenerateSettlement(transId);

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TransactionSettlementDto> GenerateSettlement(long transId)
        {
            try
            {
                var trans = await _walletTransactionService.GetTransactionRequestById(transId);
                var user = await _userService.GetUserByUserId(trans.UserId);



                var nSettle = new SettlementDto
                {
                    TransactionId = transId,
                    SettlementStatus = SettlementStatus.Success.Description(),
                    ReferenceId = Guid.NewGuid().ToString().Trim(),
                    DateOfSettlement = DateTime.UtcNow,
                };
                var walletTransaction = new WalletTransactionDto
                {
                    Id = transId,
                    RequestAmount = trans.RequestAmount,
                    RequestCurrency = trans.RequestCurrency,
                    ResponseCurrency = trans.RequestCurrency,
                    TransactionStatus = (int)TransactionStatus.Responded,
                    UserId = trans.UserId

                };
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Fullname = user.Fullname,
                    EmailAddress = user.EmailAddress,
                    CountryOfOrigin = user.CountryOfOrigin,
                    Gender = user.Gender,
                    CountryOfResidence = user.CountryOfResidence,
                    Status = Status.True.Description(),

                };

                var transactionSettlement = new TransactionSettlementDto
                {
                    User = userDto,
                    Settlement = nSettle,
                    WalletTransaction = walletTransaction,
                };
                
                return transactionSettlement;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


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
        // ~Settlement() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
             GC.SuppressFinalize(this);
        }
        #endregion
    }
}
