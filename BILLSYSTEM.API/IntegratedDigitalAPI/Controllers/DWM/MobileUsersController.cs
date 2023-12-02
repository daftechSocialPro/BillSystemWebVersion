using Implementation.Helper;
using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.DWM
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MobileUsersController : ControllerBase
    {
        private readonly IMobileUsersService _MobileUsersService;


        public MobileUsersController(IMobileUsersService MobileUsersService)
        {
            _MobileUsersService = MobileUsersService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(MobileUsersDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMobileUsers()
        {
            return Ok(await _MobileUsersService.GetMobileUsers());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddMobileUsers([FromForm]  MobileUsersDto addMobileUsers)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _MobileUsersService.AddMobileUsers(addMobileUsers));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMobileUsers([FromForm]MobileUsersDto updateMobileUsers)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _MobileUsersService.UpdateMobileUsers(updateMobileUsers));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMobileUsers(int MobileUsersId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _MobileUsersService.DeleteMobileUsers(MobileUsersId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
