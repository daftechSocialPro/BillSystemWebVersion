using Implementation.Helper;
using IntegratedImplementation.DTOS.Configuration;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IBillSectionService
    {
        Task<List<BillSectionDto>> GetBillSections();
        Task<ResponseMessage> AddBillSection(BillSectionDto addBillSection);
        Task<ResponseMessage> DeleteBillSection(string empID);
        Task<List<BillSectionDto>> GetBillOfficerHavingNoDuty();
        Task<List<DetailPermission>> GetDetailPermissions();

        Task<List<SelectListDto>> GetBillOfficersToTransfer();

    }
}
