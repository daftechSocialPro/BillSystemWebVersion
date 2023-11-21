using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IBillEmpDutiesService
    {
        Task<List<BillEmpDutiesDto>> GetBillEmpDuties();
        Task<BillEmpDutiesDto> GetBillEmpDutyForUpdate(int recordNo);
        Task<ResponseMessage> AddBillEmpDuties(BillEmpDutiesDto addBillEmpDuties);
        Task<ResponseMessage> UpdateBillEmpDuties(BillEmpDutiesDto updateBillEmpDuties);
        Task<ResponseMessage> DeleteBillEmpDuties(int recordno);
    }
}
