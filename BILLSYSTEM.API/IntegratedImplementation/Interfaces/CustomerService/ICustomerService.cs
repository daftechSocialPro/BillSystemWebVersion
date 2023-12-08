using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.CustomerService
{
    public interface ICustomerService
    {

        Task<List<CustomerGetDto>> GetCustomers();

        Task<CustomerDto> GetCustomerForUpdate(int ContractNo);
        Task<ResponseMessage> AddCustomer(CustomerDto addCustomer);
        Task<ResponseMessage> UpdateCustomer(CustomerDto updateCustomer);
        Task<ResponseMessage> DeleteCustomer(int CustomerId);
    }
}
