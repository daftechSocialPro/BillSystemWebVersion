using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IAccountPeriodService
    {
        Task<AccountPeriodDto> GetAccountPeriod();
        Task<ResponseMessage> UpdateAccountPeriod(AccountPeriodDto updateAccountPeriod);
    }
}
