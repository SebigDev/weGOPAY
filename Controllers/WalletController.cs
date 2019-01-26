using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weGOPAY.weGOPAY.Core.Models.Wallets;
using weGOPAY.weGOPAY.Services.Wallets;

namespace weGOPAY.Controllers
{
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletServices _WalletService;

        public WalletController(IWalletServices WalletServices)
        {
            _WalletService = WalletServices;
        }

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(WalletDto))]
        [ProducesResponseType(typeof(IEnumerable<WalletDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllWallets()
        {
            try
            {
                var allWallets = await _WalletService.GetAllWallets();
                return Ok(allWallets);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(WalletDto))]
        [ProducesResponseType(typeof(WalletDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetWalletById([FromQuery] long id)
        {
            try
            {
                var Wallet = await _WalletService.GetWalletById(id);
                if (Wallet == null) return NotFound();
                return Ok(Wallet);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(WalletDto))]
        [ProducesResponseType(typeof(WalletDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetWalletByUserId([FromQuery] string userId)
        {
            try
            {
                var Wallet = await _WalletService.GetWalletByUserId(userId);
                if (Wallet == null) return NotFound();
                return Ok(Wallet);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(WalletDto))]
        [ProducesResponseType(typeof(WalletDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetWalletByStatus([FromQuery] string status)
        {
            try
            {
                var Wallet = await _WalletService.GetWalletByStatus(status);
                if (Wallet == null) return NotFound();
                return Ok(Wallet);

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
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDto createWallet, long idUser)
        {
            try
            {
                var nWallet = await _WalletService.CreateWallet(createWallet, idUser);
                return Ok(nWallet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(typeof(WalletDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateWallet([FromBody] UpdateWalletDto updateWallet, long idUser)
        {
            try
            {
                await _WalletService.UpdateWallet(updateWallet, idUser);
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
