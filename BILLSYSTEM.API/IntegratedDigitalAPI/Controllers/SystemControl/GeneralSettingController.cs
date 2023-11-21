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
    public class GeneralSettingController : ControllerBase
    {

        private readonly IGeneralSettingService _GeneralSettingService;


        public GeneralSettingController(IGeneralSettingService GeneralSettingService)
        {
            _GeneralSettingService = GeneralSettingService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(GeneralSettingDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGeneralSetting(string settingCategory)
        {
            return Ok(await _GeneralSettingService.GetGeneralSetting(settingCategory));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddGeneralSetting(GeneralSettingDto addGeneralSetting)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _GeneralSettingService.AddGeneralSetting(addGeneralSetting));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateGeneralSetting(GeneralSettingDto updateGeneralSetting)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _GeneralSettingService.UpdateGeneralSetting(updateGeneralSetting));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteGeneralSetting(Guid GeneralSettingId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _GeneralSettingService.DeleteGeneralSetting(GeneralSettingId));
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
