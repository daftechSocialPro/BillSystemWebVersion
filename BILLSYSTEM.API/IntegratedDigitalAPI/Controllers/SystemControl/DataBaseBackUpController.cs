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
    public class DataBaseBackUpController : ControllerBase
    {
        private readonly IDataBaseBackUpService _dataBaseBackUpService;


        public DataBaseBackUpController(IDataBaseBackUpService dataBaseBackUpService)
        {
            _dataBaseBackUpService = dataBaseBackUpService;

        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> BackUp(string dbName , string path)
        {
            return Ok(await _dataBaseBackUpService.DatabaseBackup(dbName,path));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> OpenFolder()
        {
            return Ok(await _dataBaseBackUpService.OpenFolder());
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDatabaseBakcupPath()
        {
            return Ok(await _dataBaseBackUpService.GetDatabaseBakcupPath());
        }
    }
}
