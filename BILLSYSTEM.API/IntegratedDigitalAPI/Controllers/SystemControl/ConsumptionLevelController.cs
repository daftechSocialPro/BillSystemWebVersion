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
    public class ConsumptionLevelController : ControllerBase
    {

        private readonly IConsumptionLevelService _ConsumptionLevelService;


        public ConsumptionLevelController(IConsumptionLevelService ConsumptionLevelService)
        {
            _ConsumptionLevelService = ConsumptionLevelService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ConsumptionLevelDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetConsumptionLevel()
        {
            return Ok(await _ConsumptionLevelService.GetConsumptionLevel());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddConsumptionLevel(ConsumptionLevelDto addConsumptionLevel)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _ConsumptionLevelService.AddConsumptionLevel(addConsumptionLevel));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateConsumptionLevel(ConsumptionLevelDto updateConsumptionLevel)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _ConsumptionLevelService.UpdateConsumptionLevel(updateConsumptionLevel));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteConsumptionLevel(int ConsumptionLevelId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _ConsumptionLevelService.DeleteConsumptionLevel(ConsumptionLevelId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
