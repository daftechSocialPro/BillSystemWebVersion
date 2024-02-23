using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.CustomerService
{
    public interface ICustomerMeterChangeService
    {
        public Task<List<CustomerMeterChangeGetDto>> GetCustomerMeter(string CustId);
        public Task<ResponseMessage> ChangeMeter (CustomerMeterChangePostDto meterChange);
    }
}
