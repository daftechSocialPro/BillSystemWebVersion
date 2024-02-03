using Implementation.Helper;
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
    public class CustomerMeterStatusController : ControllerBase
    {
        private readonly ICustomerMeterStatusService _changeActionService;


        public CustomerMeterStatusController(ICustomerMeterStatusService changeActionService)
        {
            _changeActionService = changeActionService;

        }
        [HttpGet]
        [ProducesResponseType(typeof(CustomerMeterStatusGetDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerMeterStatus(string custId )
        {
            return Ok(await _changeActionService.GetCustomerStatus(custId));
        }

    

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeCustomerStatus(CustomerMeterStatusPostDto meterStatus)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _changeActionService.ChangeCustomerStatus(meterStatus));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
