using Implementation.Helper;
using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IntegratedDigitalAPI.Controllers.DWM
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QRCodeGenerateController : ControllerBase
    {

        private readonly IQRCodeGenerateService _qrCodeGenerateService;


        public QRCodeGenerateController(IQRCodeGenerateService qrCodeGenerateService)
        {
            _qrCodeGenerateService = qrCodeGenerateService;

        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetQRCodeGenerate(QRCodeDto[] qRCodes  )
        {
            var result = await _qrCodeGenerateService.GeneratePdf(qRCodes);

           

            Response.Headers.Add("Content-Disposition", "attachment;filename=qr_codes.pdf");
            Response.Headers.Add("Content-Length", result.data.Length.ToString());


            //Response.Headers.Add("Content-Disposition", "attachment;filename=qr_codes.pdf");
            //Response.Headers.Add("Content-Length", result.data.Length.ToString());

            Response.ContentType = "application/pdf";
            await Response.Body.WriteAsync(result.data, 0, result.data.Length);

            // Return the PDF file
            return Ok(result.data);
        }


        
    }
}
