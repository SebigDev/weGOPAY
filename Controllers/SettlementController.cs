using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weGOPAY.weGOPAY.Core.Models.Settlements;
using weGOPAY.weGOPAY.Services.Settlements;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace weGOPAY.Controllers
{
    [Route("api/[controller]")]
    public class SettlementController : Controller
    {
        private readonly ISettlementService _settlement;
        public SettlementController(ISettlementService settlementService)
        {
            _settlement = settlementService;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(TransactionSettlementDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> VerifyTransaction(long transactionId)
        {
            try
            {
                var verify = await _settlement.VerifyTransaction(transactionId);
                if(verify == true)
                {
                    var generateSettlement = await _settlement.GenerateSettlement(transactionId);
                    return Ok(generateSettlement);
                }
                return BadRequest("Could Not Verify Transaction and No statement Could be Generated");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
