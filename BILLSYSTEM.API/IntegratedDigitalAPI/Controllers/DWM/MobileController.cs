using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Model.DWM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.DWM
{
    [Route("API/[action]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        

             private readonly IDWMMobileService _DWMMobileService;


        public MobileController(IDWMMobileService DWMMobileService)
        {
            _DWMMobileService = DWMMobileService;

        }

        [HttpPost]
        [ProducesResponseType(typeof(MobileUsersDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAdmin(IEnumerable<MobileUsers> readerCridential)
        {
            return Ok(await _DWMMobileService.Login(readerCridential));
        }
    }
}
