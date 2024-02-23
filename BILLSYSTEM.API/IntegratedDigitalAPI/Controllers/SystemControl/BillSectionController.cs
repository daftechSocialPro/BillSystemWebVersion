using Implementation.Helper;
using IntegratedImplementation.DTOS.HRM;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillSectionController : ControllerBase
    {
        private readonly IBillSectionService _BillSectionService;


        public BillSectionController(IBillSectionService BillSectionService)
        {
            _BillSectionService = BillSectionService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(BillSectionDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillSection()
        {
            return Ok(await _BillSectionService.GetBillSections());
        }


        [HttpGet]
        [ProducesResponseType(typeof(BillSectionDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillOfficersForTransfer()
        {
            return Ok(await _BillSectionService.GetBillOfficersToTransfer());
        }
        [HttpGet]
        [ProducesResponseType(typeof(BillSectionDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillOfficers()
        {
            return Ok(await _BillSectionService.GetBillOfficers());
        }
        


        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBillSection(BillSectionDto addBillSection)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _BillSectionService.AddBillSection(addBillSection));
            }
            else
            {
                return BadRequest();
            }
        }
    
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBillSection(string empId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _BillSectionService.DeleteBillSection(empId));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(BillSectionDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillOfficerHavingNoDuty()
        {
            return Ok(await _BillSectionService.GetBillOfficerHavingNoDuty());
        }


        [HttpGet]
        [ProducesResponseType(typeof(DetailPermission), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllDetailPermission()
        {
            return Ok(await _BillSectionService.GetDetailPermissions());
        }


    }
}
