using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.DTOS.DWM;
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

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateBasicData(CustomerDto customerPost)
        {
            return Ok(await _customerService.AddCustomer(customerPost));
        }


        [HttpGet]
        [ProducesResponseType(typeof(CustomerGetDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSingleCustomer(string contractNo)
        {
            return Ok(await _customerService.GetSingleCustomer(contractNo) );
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustomer(string contractNo)
        {
            return Ok(await _customerService.DeleteCustomer(contractNo));
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

        [HttpGet]
        [ProducesResponseType(typeof(CustomerGetDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetContractNumber(string kebele, string ketena)
        {
            return Ok(await _customerService.GetContractNumber(kebele,ketena));
        }



    }
}
