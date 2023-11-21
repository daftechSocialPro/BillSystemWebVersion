using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedImplementation.Services.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillEmpDutiesController : ControllerBase
    {
        private readonly IBillEmpDutiesService _billEmpDutiesService;


        public BillEmpDutiesController(IBillEmpDutiesService billEmpDutiesService)
        {
            _billEmpDutiesService = billEmpDutiesService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(BillEmpDutiesDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillDuties()
        {
            return Ok(await _billEmpDutiesService.GetBillEmpDuties());
        }

        [HttpGet]
        [ProducesResponseType(typeof(BillEmpDutiesDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillEmpDutyForUpdate(int recordNo)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _billEmpDutiesService.GetBillEmpDutyForUpdate(recordNo));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBillEmpDuties(BillEmpDutiesDto addBillEmpDuties)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _billEmpDutiesService.AddBillEmpDuties(addBillEmpDuties));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBillEmpDuties(BillEmpDutiesDto updateBillEmpDuties)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _billEmpDutiesService.UpdateBillEmpDuties(updateBillEmpDuties));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBillEmpDuties(int recordno)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _billEmpDutiesService.DeleteBillEmpDuties(recordno));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
