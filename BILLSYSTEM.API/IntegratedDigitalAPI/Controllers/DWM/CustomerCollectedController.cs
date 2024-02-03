using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.DWM
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerCollectedController : ControllerBase
    {

        private readonly ICustomerCollectedService _customerCollectedService;
        public CustomerCollectedController(ICustomerCollectedService customerCollectedService) {
        _customerCollectedService = customerCollectedService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerCollectedDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillMobileData(string contractNo)
        {
            return Ok(await _customerCollectedService.GetBillMobileData(contractNo));
        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerCollectedDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillMobileDataByEntryDate(string entryDate, string userName )
        {
            return Ok(await _customerCollectedService.GetBillMobileDataByEntryDate(entryDate,userName));
        }
      

    }
}
