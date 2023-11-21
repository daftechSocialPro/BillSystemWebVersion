using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.SystemControl
{
    public class BillTemplateService:IBillTemplateService
    {


        public async Task<IActionResult> GetBillTemplate(string imageName)
        {

            var contentType = "image/png"; 
            return ImageHelper.LoadImage("BillTemplates", imageName, contentType);
        }
   
        public static class ImageHelper
        {
            public static IActionResult LoadImage(string folderName, string imageName, string contentType)
            {
                var imagePath = Path.Combine(folderName, imageName);
                var imageFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);

                if (System.IO.File.Exists(imageFilePath))
                {
                    var imageBytes = System.IO.File.ReadAllBytes(imageFilePath);
                    return new FileContentResult(imageBytes, contentType);
                }

                return new NotFoundResult();
            }
        }
    }
}
