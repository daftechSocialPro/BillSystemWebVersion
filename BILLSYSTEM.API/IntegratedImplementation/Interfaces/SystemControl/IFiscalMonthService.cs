using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IFiscalMonthService
    {
        Task<List<FiscalMonthDto>> GetFiscalMonth();
        Task<ResponseMessage> UpdateFiscalMonth(FiscalMonthDto updateFiscalMonth);
        Task<ResponseMessage> DeleteFiscalMonth(int FiscalMonthId);
    }
}
