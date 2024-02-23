using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.Interfaces.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.CustomerService
{
    public class CustomerMeterChangeService : ICustomerMeterChangeService
    {
        public Task<List<CustomerMeterChangeGetDto>> GetCustomerMeter(string CustId)
        {
            throw new NotImplementedException();
        }
    }
}
