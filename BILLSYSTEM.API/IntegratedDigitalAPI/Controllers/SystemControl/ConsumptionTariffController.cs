using Implementation.Helper;
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
    public class ConsumptionTariffController : ControllerBase
    {
        private readonly IConsumptionTariffService _ConsumptionTariffService;


        public ConsumptionTariffController(IConsumptionTariffService ConsumptionTariffService)
        {
            _ConsumptionTariffService = ConsumptionTariffService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(ConsumptionTariff), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetConsumptionTariffs()
        {
            return Ok(await _ConsumptionTariffService.GetConsumptionTariffs());
        }

        [HttpGet]
        [ProducesResponseType(typeof(ConsumptionTariff), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetConsumptionTariffForUpdate(int recordNo)
        {
            return Ok(await _ConsumptionTariffService.GetConsumptionTariffForUpdate(recordNo));
        }




        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddConsumptionTariff(ConsumptionTariffDto addMeterSize)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _ConsumptionTariffService.AddConsumptionTariff(addMeterSize));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateConsumptionTariff(ConsumptionTariffDto updateConsumptionTariff)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _ConsumptionTariffService.UpdateConsumptionTariff(updateConsumptionTariff));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteConsumptionTariff(int ConsumptionTariffId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _ConsumptionTariffService.DeleteConsumptionTariff(ConsumptionTariffId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
