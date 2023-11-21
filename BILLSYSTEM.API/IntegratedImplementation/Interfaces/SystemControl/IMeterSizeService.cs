using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IMeterSizeService
    {

        Task<List<MeterSizeDto>> GetMeterSize();
        Task<ResponseMessage> AddMeterSize(MeterSizeDto addMeterSize);
        Task<ResponseMessage> UpdateMeterSize(MeterSizeDto updateMeterSize);
        Task<ResponseMessage> DeleteMeterSize(int MeterSizeId);
    }
}
