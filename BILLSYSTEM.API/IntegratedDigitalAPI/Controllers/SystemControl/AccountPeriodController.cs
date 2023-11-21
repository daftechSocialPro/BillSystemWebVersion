using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedImplementation.Services.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountPeriodController : ControllerBase
    {

        private readonly IAccountPeriodService _AccountPeriodService;


        public AccountPeriodController(IAccountPeriodService AccountPeriodService)
        {
            _AccountPeriodService = AccountPeriodService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountPeriodDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAccountPeriod()
        {
            return Ok(await _AccountPeriodService.GetAccountPeriod());
        }


        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAccountPeriod(AccountPeriodDto updateAccountPeriod)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _AccountPeriodService.UpdateAccountPeriod(updateAccountPeriod));
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
