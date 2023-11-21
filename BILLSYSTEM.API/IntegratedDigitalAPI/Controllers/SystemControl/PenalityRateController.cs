using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PenalityRateController : ControllerBase
    {

        private readonly IPenalityRateService _PenalityRateService;


        public PenalityRateController(IPenalityRateService PenalityRateService)
        {
            _PenalityRateService = PenalityRateService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(PenalityRate), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPenalityRates()
        {
            return Ok(await _PenalityRateService.GetPenalityRates());
        }

        [HttpGet]
        [ProducesResponseType(typeof(PenalityRate), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPenalityRateForUpdate(int recordNo)
        {
            return Ok(await _PenalityRateService.GetPenalityRateForUpdate(recordNo));
        }




        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddPenalityRate(PenalityRateDto PenalityRate)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _PenalityRateService.AddPenalityRate(PenalityRate));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePenalityRate(PenalityRateDto updatePenalityRate)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _PenalityRateService.UpdatePenalityRate(updatePenalityRate));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePenalityRate(int PenalityRateId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _PenalityRateService.DeletePenalityRate(PenalityRateId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
