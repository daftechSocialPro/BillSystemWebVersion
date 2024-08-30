using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Model.DWM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        public IActionResult LoginAdmin(List<MobileUsers> readerCridential)
        {
            return Ok(_DWMMobileService.LoginAdmin(readerCridential));
        }


        [HttpPost]
        [ProducesResponseType(typeof(MobileUsersDto), (int)HttpStatusCode.OK)]
        public IActionResult Login(List<MobileUsers> readerCridential)
        {
            return Ok(_DWMMobileService.Login(readerCridential));
        }

        [HttpPost]
        [ProducesResponseType(typeof(MobileUsersDto), (int)HttpStatusCode.OK)]
        public IActionResult ExportCustomers(MobileUsers readerCridential)
        {
           return Ok(_DWMMobileService.ImportData(readerCridential));
        }


        //[HttpPost]
        //[ProducesResponseType(typeof(MobileUsersDto), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> ExportCustomers(MobileUsers readerCridential)
        //{
        //    var results = _DWMMobileService.ImportData(readerCridential);
        //    var json = JsonConvert.SerializeObject(results);
        //    var bytes = System.Text.Encoding.UTF8.GetBytes(json);
        //    var stream = new System.IO.MemoryStream(bytes);
        //    return File(stream, "application/json", "exported_data.json");
        //}


        [HttpPost]
        [ProducesResponseType(typeof(MobileUsersDto), (int)HttpStatusCode.OK)]
        public IActionResult ImportDisconnected(MobileUsers readerCridential)
        {
            return Ok(_DWMMobileService.ImportDisconnected(readerCridential));


        }

        [HttpPost]
        [ProducesResponseType(typeof(ImportResponse), (int)HttpStatusCode.OK)]
        public IActionResult ImportCustomers(bill_mobileImport readerCridential)
        {
            return Ok(_DWMMobileService.ExportCustomers(readerCridential));


        }

        [HttpPost]
        [ProducesResponseType(typeof(ImportResponse), (int)HttpStatusCode.OK)]
        public IActionResult ImportGPS(List<bill_mobileImport> readerCridential)
        {
            return Ok(_DWMMobileService.ImportGPS(readerCridential));


        }




    }
}
