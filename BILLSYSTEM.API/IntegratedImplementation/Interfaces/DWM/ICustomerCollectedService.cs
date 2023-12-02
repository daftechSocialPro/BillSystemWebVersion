using IntegratedImplementation.DTOS.DWM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.DWM
{
    public interface ICustomerCollectedService
    {
       Task<IQueryable<CustomerCollectedDto>> GetBillMobileData(string contractNo);
       Task<IQueryable<CustomerCollectedDto>> GetBillMobileDataByEntryDate(string entryDate, string userName);
    }
}
