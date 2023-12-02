using Implementation.Helper;
using IntegratedImplementation.DTOS.DWM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegratedImplementation.Services.DWM.QRCodeGenerateService;

namespace IntegratedImplementation.Interfaces.DWM
{
    public interface IQRCodeGenerateService
    {
        Task<qrCodeResponse> GeneratePdf(QRCodeDto[] qRCodes);
    }
}
