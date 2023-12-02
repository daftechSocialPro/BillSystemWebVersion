using Implementation.Helper;
using IntegratedImplementation.DTOS.DWM;
using IntegratedInfrustructure.Model.DWM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.DWM
{
    public interface IMobileAppReadingService
    {

        Task<int> GetMobileAppReadingsLength();
        Task<List<MobileAppReadingDto>> GetMobileAppReading(int pageNumber, int pageSize);
        Task<ResponseMessage> InsertMobileAppReading(string monthIndex, string year, string? Kebele);
        Task<ResponseMessage> ClearScript();
    }
}
