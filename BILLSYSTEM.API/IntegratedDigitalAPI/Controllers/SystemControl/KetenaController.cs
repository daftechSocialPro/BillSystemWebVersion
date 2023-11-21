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
    public class KetenaController : ControllerBase
    {

        private readonly IKetenaService _KetenaService;


        public KetenaController(IKetenaService KetenaService)
        {
            _KetenaService = KetenaService;

        }

        [HttpGet]
        [ProducesResponseType(typeof(KetenaDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetKetena()
        {
            return Ok(await _KetenaService.GetKetena());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddKetena(KetenaDto addKetena)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _KetenaService.AddKetena(addKetena));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateKetena(KetenaDto updateKetena)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _KetenaService.UpdateKetena(updateKetena));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteKetena(int KetenaId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _KetenaService.DeleteKetena(KetenaId));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
