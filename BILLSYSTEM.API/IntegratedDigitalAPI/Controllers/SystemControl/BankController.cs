using IntegratedImplementation.DTOS.Configuration;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SystemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankController : ControllerBase
    {

        private readonly IBankService _bankService;


        public BankController(IBankService bankService)
        {
            _bankService = bankService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(SelectListDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBanks()
        {
            return Ok(await _bankService.GetBanks());
        }
    }
}
