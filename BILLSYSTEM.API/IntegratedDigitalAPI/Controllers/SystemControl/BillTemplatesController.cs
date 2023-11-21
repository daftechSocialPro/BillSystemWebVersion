using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.SytemControl
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillTemplatesController : ControllerBase
    {
        private readonly IBillTemplateService _billTemplateService;


        public BillTemplatesController(IBillTemplateService billTemplateService)
        {
            _billTemplateService = billTemplateService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(File), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBillTemplates(string imageName)
        {
            return Ok(await _billTemplateService.GetBillTemplate(imageName));
        }
    }
}
