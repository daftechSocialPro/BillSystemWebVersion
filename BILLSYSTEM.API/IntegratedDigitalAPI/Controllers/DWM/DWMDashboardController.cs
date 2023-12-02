using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedImplementation.Services.DWM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.DWM
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DWMDashboardController : ControllerBase
    {
        private readonly IDWMDashboardService _dashboardService;

        public DWMDashboardController(IDWMDashboardService dashboardService)
        {
            _dashboardService = dashboardService;

        }
        [HttpGet]
        [ProducesResponseType(typeof(DWMDashboardDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDashboardDetail(int year, int month )
        {
            return Ok(await _dashboardService.GetDashbordDetail(year,month));
        }
    }
}
