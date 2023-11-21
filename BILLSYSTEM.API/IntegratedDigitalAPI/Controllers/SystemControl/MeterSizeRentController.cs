using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedImplementation.Services.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MeterSizeRentController : ControllerBase
    {
        private readonly IMeterSizeRentService _meterSizeRentService;


        public MeterSizeRentController(IMeterSizeRentService meterSizeRentService)
        {
            _meterSizeRentService = meterSizeRentService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(MeterSizeRent), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeterSizeRents()
        {
            return Ok(await _meterSizeRentService.GetMeterSizeRents());
        }

        [HttpGet]
        [ProducesResponseType(typeof(MeterSizeRent), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeterSizeRentForUpdate(int recordNo)
        {
            return Ok(await _meterSizeRentService.GetMeterSizeRentForUpdate(recordNo));
        }


        

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddMeterSizeRent(MeterSizeRentDto addMeterSize)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _meterSizeRentService.AddMeterSizeRent(addMeterSize));
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMeterSizeRent(MeterSizeRentDto updateMeterSizeRent)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _meterSizeRentService.UpdateMeterSizeRent(updateMeterSizeRent));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMeterSizeRent(int MeterSizeRentId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _meterSizeRentService.DeleteMeterSizeRent(MeterSizeRentId));
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
