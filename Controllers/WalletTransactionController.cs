using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weGOPAY.weGOPAY.Core.Models.WalletTransactions;
using weGOPAY.weGOPAY.Services.WalletTransactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace weGOPAY.Controllers
{
    [Route("api/[controller]")]
    public class WalletTransactionController : ControllerBase
    {
        private readonly IWalletTransactionService _walletTransactionService;
        public WalletTransactionController(IWalletTransactionService walletTransactionService)
        {
            _walletTransactionService = walletTransactionService;
        }


        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(long))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> MakeTransactionRequest([FromBody] RequestWalletTransactionDto model)
        {
            try
            {
                var nWallet = await _walletTransactionService.MakeTransactionRequest(model);
                return Ok(nWallet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(long))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> MakeTransactionResponse([FromBody] ResponseWalletTransactionDto model, long requestUserId)
        {
            try
            {
                await _walletTransactionService.MakeTransactionResponse(model, requestUserId);
                return Ok("Transaction Response was Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<RequestWalletTransactionDto>))]
        [ProducesResponseType(typeof(IEnumerable<RequestWalletTransactionDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllWalletTransactionRequests()
        {
            try
            {
                var all = await _walletTransactionService.GetAllTransactionRequest();
                return Ok(all);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("[action]")]
        [Produces(typeof(bool))]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InitiateTransaction([FromBody] ResponseWalletTransactionDto model, long id)
        {
            try
            {
                var response = await _walletTransactionService.InitiateTransaction(model, id);
                if(response == true)
                {
                    return Ok("Transaction Response Initiated");
                }
                return Ok("No Transaction Request to Initiate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
