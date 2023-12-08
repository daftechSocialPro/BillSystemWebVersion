using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedImplementation.Services.SystemControl;
using IntegratedInfrustructure.Model.CSS;
using IntegratedInfrustructure.Model.SCS;
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


        [HttpGet]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerForUpdate(int ContractNo)
        {
            return Ok(await _customerService.GetCustomerForUpdate(ContractNo));
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddCustomer(CustomerDto Customer)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _customerService.AddCustomer(Customer));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustomer(CustomerDto updateCustomer)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _customerService.UpdateCustomer(updateCustomer));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _customerService.DeleteCustomer(CustomerId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
