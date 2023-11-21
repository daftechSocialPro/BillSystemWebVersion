using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IMeterOriginService
    {

        Task<List<MeterOriginGetDto>> GetMeterOrigin();
        Task<ResponseMessage> AddMeterOrigin(MeterOriginPostDto addMeterOrigin);
        Task<ResponseMessage> UpdateMeterOrigin(MeterOriginGetDto updateMeterOrigin);
        Task<ResponseMessage> DeleteMeterOrigin(Guid MeterOriginId);
    }
}
