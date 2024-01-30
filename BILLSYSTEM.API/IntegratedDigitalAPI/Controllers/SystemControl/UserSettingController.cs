using Implementation.Helper;
using IntegratedImplementation.DTOS.Configuration;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedImplementation.Services.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SystemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSettingController : ControllerBase
    {
        private readonly IUserSettingService _userSettingService;
        public UserSettingController(IUserSettingService userSettingService) { 
        
        _userSettingService = userSettingService;
        
        }


        [HttpGet]
        [ProducesResponseType(typeof(UserServiceDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUserSettings()
        {
            return Ok(await _userSettingService.GetUserSettingList());
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserServiceDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeeForUserSetting()
        {
            return Ok(await _userSettingService.GetEmployeesforUserSetting());
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserServiceDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeesforUserSettingUpdate()
        {
            return Ok(await _userSettingService.GetEmployeesforUserSettingUpdate());
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSystemUser(int userId)
        {
            return Ok(await _userSettingService.DeleteSystemUser(userId));
        }






        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateSystemUser(UserServicePostDto userPost)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userSettingService.CreateSystemUser(userPost));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSystemUser(UserServicePostDto userUpdate)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userSettingService.UpdateSystemUser(userUpdate));
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(SelectListDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAppModules()
        {
            return Ok(await _userSettingService.GetAppModules());
        }

        [HttpGet]
        [ProducesResponseType(typeof(SelectListDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAppTabsByModule(string appModule)
        {
            return Ok(await _userSettingService.GetAppTabsByModule(appModule));
        }


        [HttpGet]
        [ProducesResponseType(typeof(SelectListDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserPermissions(int  userId)
        {

            
            return Ok(await _userSettingService.GetUserPermissions(userId.ToString()));
        }


        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserPermission(List<UserPermission> userPermissions)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userSettingService.UpdateUserPermission(userPermissions));
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
