using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerCategoryController : ControllerBase
    {

        private readonly ICustomerCategoryService _CustomerCategoryService;


        public CustomerCategoryController(ICustomerCategoryService CustomerCategoryService)
        {
            _CustomerCategoryService = CustomerCategoryService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(CustomerCategoryDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCustomerCategory()
        {
            return Ok(await _CustomerCategoryService.GetCustomerCategory());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddCustomerCategory(CustomerCategoryDto addCustomerCategory)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _CustomerCategoryService.AddCustomerCategory(addCustomerCategory));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustomerCategory(CustomerCategoryDto updateCustomerCategory)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _CustomerCategoryService.UpdateCustomerCategory(updateCustomerCategory));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCustomerCategory(int CustomerCategoryId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _CustomerCategoryService.DeleteCustomerCategory(CustomerCategoryId));
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
