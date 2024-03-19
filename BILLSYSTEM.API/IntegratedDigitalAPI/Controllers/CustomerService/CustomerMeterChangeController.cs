using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.Interfaces.CustomerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.CustomerService
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerMeterChangeController : ControllerBase
    {
        private readonly ICustomerMeterChangeService _changeMeterService;
        public CustomerMeterChangeController(ICustomerMeterChangeService changeMeterService)
        {
            _changeMeterService = changeMeterService;

        }
        [HttpGet]
        [ProducesResponseType(typeof(CustomerMeterChangeGetDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerMeterChange(string custId)
        {
            return Ok(await _changeMeterService.GetCustomerMeter(custId));
        }



        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeMeter(CustomerMeterChangePostDto meterChange)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _changeMeterService.ChangeMeter(meterChange));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
