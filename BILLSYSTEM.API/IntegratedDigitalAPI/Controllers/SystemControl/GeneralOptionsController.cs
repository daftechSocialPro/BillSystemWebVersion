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
    public class GeneralOptionsController : ControllerBase
    {

        private readonly IGeneralOptionsService _generalOptionsService;


        public GeneralOptionsController(IGeneralOptionsService generalOptionsService)
        {
            _generalOptionsService = generalOptionsService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(GeneralOptions), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGeneralOptions()
        {
            return Ok(await _generalOptionsService.GetGenralOptions());
        }
    }
}
