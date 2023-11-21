using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MeterSizeController : ControllerBase
    {

        private readonly IMeterSizeService _meterSizeService;

       
        public MeterSizeController(IMeterSizeService meterSizeService) {
            _meterSizeService = meterSizeService;
                
                }

        [HttpGet]
        [ProducesResponseType(typeof(MeterSizeDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeterSize()
        {
            return Ok(await _meterSizeService.GetMeterSize());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddMeterSize(MeterSizeDto addMeterSize)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _meterSizeService.AddMeterSize(addMeterSize));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMeterSize(MeterSizeDto updateMeterSize)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _meterSizeService.UpdateMeterSize(updateMeterSize));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMeterSize(int meterSizeId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _meterSizeService.DeleteMeterSize(meterSizeId));
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
