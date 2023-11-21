using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IMeterSizeRentService
    {
        Task<List<MeterSizeRentDto>> GetMeterSizeRents();

        Task <MeterSizeRentDto>GetMeterSizeRentForUpdate(int recordNo);
        Task<ResponseMessage> AddMeterSizeRent(MeterSizeRentDto addMeterSizeRent);
        Task<ResponseMessage> UpdateMeterSizeRent(MeterSizeRentDto updateMeterSizeRent);
        Task<ResponseMessage> DeleteMeterSizeRent(int MeterSizeRentId);


    }
}
