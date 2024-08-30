using Implementation.Helper;
using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Model.DWM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.DWM
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MobileAppReadingController : ControllerBase
    {
        private readonly IMobileAppReadingService _mobileAppReadingService;


        public MobileAppReadingController(IMobileAppReadingService mobileAppReadingService)
        {
            _mobileAppReadingService = mobileAppReadingService;

        }
        [HttpGet]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMobileReadingsLength()
        {
            return Ok(await _mobileAppReadingService.GetMobileAppReadingsLength());
        }

        [HttpGet]
        [ProducesResponseType(typeof(MobileAppReadingDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMobileReadings(int pageNumber, int pageSize)
        {
            return Ok(await _mobileAppReadingService.GetMobileAppReading(pageNumber, pageSize));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> InsertMobileAppReading(string year , string month , string ? kebele, string? village)
        {
            return Ok(await _mobileAppReadingService.InsertMobileAppReading(month,year,kebele, village));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ClearScript()
        {
            return Ok(await _mobileAppReadingService.ClearScript());
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> CalculateAverage()
        {

            return Ok(await _mobileAppReadingService.CalculateAverage());
        }
    }
}
