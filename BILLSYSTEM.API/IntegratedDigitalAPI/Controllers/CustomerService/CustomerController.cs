using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.CustomerService
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;


        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerGetDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomeres()
        {
            return Ok(await _customerService.GetCustomers());
        }
    }
}
