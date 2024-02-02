using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedImplementation.Services.CustomerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.CustomerService
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChangeActionController : ControllerBase
    {
        private readonly IChangeActionService _changeActionService;


        public ChangeActionController(IChangeActionService changeActionService)
        {
            _changeActionService = changeActionService;

        }
        [HttpGet]
        [ProducesResponseType(typeof(ChangeActionGetDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerStatus()
        {
            return Ok(await _changeActionService.GetCustomerStatus());
        }
    }
}
