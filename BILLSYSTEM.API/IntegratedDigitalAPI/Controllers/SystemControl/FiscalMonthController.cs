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
    public class FiscalMonthController : ControllerBase
    {

        private readonly IFiscalMonthService _FiscalMonthService;


        public FiscalMonthController(IFiscalMonthService FiscalMonthService)
        {
            _FiscalMonthService = FiscalMonthService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(FiscalMonthDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFiscalMonth()
        {
            return Ok(await _FiscalMonthService.GetFiscalMonth());
        }


        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateFiscalMonth(FiscalMonthDto updateFiscalMonth)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _FiscalMonthService.UpdateFiscalMonth(updateFiscalMonth));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteFiscalMonth(int FiscalMonthId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _FiscalMonthService.DeleteFiscalMonth(FiscalMonthId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
