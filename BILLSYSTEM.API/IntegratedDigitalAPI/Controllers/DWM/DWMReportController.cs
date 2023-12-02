using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.DWM
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DWMReportController : ControllerBase
    {

        private readonly IDWMReportService _dwmReportService;

        public DWMReportController(IDWMReportService dwmReportService)
        {
            _dwmReportService = dwmReportService;

        }
        [HttpGet]
        [ProducesResponseType(typeof(DWMReadingLogReportDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReadingLogReport(int monthIndex, int year)
        {
            return Ok(await _dwmReportService.GetReadingLogReport(monthIndex, year));
        }

        [HttpGet]
        [ProducesResponseType(typeof(DWMPendingLogReportDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPendingLogReport(int monthIndex, int year)
        {
            return Ok(await _dwmReportService.GetPendingLogReport(year, monthIndex));
        }

        [HttpGet]
        [ProducesResponseType(typeof(DWMReadingAccuracyReportDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReadingAccuracyReportDto(int monthIndex, int year)
        {
            return Ok(await _dwmReportService.GetReadingAccuracyReport(year, monthIndex));
        }

        [HttpGet]
        [ProducesResponseType(typeof(DWMReadingEfficencyReportDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReadingEfficencyReport(int monthIndex, int year)
        {
            return Ok(await _dwmReportService.GetReadingEfficencyReport(year, monthIndex));
        }
        [HttpGet]
        [ProducesResponseType(typeof(DWMReadingEfficencyReportDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReadingConsumptionReport(int monthIndex, int year)
        {
            return Ok(await _dwmReportService.GetReadingConsumptionReport(year, monthIndex));
        }
    }
}
