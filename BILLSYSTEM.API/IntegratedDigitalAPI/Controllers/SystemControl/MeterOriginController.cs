using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MeterOriginController : ControllerBase
    {
        private readonly IMeterOriginService _MeterOriginService;


        public MeterOriginController(IMeterOriginService MeterOriginService)
        {
            _MeterOriginService = MeterOriginService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(MeterOriginGetDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeterOrigin()
        {
            return Ok(await _MeterOriginService.GetMeterOrigin());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddMeterOrigin(MeterOriginPostDto addMeterOrigin)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _MeterOriginService.AddMeterOrigin(addMeterOrigin));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMeterOrigin(MeterOriginGetDto updateMeterOrigin)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _MeterOriginService.UpdateMeterOrigin(updateMeterOrigin));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMeterOrigin(Guid MeterOriginId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _MeterOriginService.DeleteMeterOrigin(MeterOriginId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
