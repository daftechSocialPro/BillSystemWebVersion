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
    public class GeneralInterfaceController : ControllerBase
    {

        private readonly IGeneralInterfaceService _GeneralInterfaceService;


        public GeneralInterfaceController(IGeneralInterfaceService GeneralInterfaceService)
        {
            _GeneralInterfaceService = GeneralInterfaceService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(GeneralInterfaceDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGeneralInterface(string ObjectCategory)
        {
            return Ok(await _GeneralInterfaceService.GetGeneralInterface(ObjectCategory));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddGeneralInterface(GeneralInterfaceDto addGeneralInterface)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _GeneralInterfaceService.AddGeneralInterface(addGeneralInterface));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateGeneralInterface(GeneralInterfaceDto updateGeneralInterface)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _GeneralInterfaceService.UpdateGeneralInterface(updateGeneralInterface));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteGeneralInterface(int GeneralInterfaceId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _GeneralInterfaceService.DeleteGeneralInterface(GeneralInterfaceId));
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
