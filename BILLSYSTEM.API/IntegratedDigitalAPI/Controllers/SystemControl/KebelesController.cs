using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedImplementation.Services.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KebelesController : ControllerBase
    {
        private readonly IKebelesService _KebelesService;


        public KebelesController(IKebelesService KebelesService)
        {
            _KebelesService = KebelesService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(Kebeles), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetKebeles()
        {
            return Ok(await _KebelesService.GetKebeles());
        }


        [HttpGet]
        [ProducesResponseType(typeof(Kebeles), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetKetenasKebeles(string ketenaCode)
        {
            return Ok(await _KebelesService.GetKetenaKebeles(ketenaCode));
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddKebeles(KebelesDto addKebeles)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _KebelesService.AddKebeles(addKebeles));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateKebeles(KebelesDto updateKebeles)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _KebelesService.UpdateKebeles(updateKebeles));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteKebeles(int KebelesId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _KebelesService.DeleteKebeles(KebelesId));
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
