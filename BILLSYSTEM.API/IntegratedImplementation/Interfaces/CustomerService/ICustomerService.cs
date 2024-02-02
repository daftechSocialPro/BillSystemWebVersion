using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.CustomerService
{
    public interface ICustomerService
    {

        public Task<List<CustomerGetDto>> GetCustomers();

        public Task <CustomerGetDto> GetSingleCustomer(string contractNo);

        public Task<ResponseMessage> AddCustomer(CustomerPostDto customerPost);
        public Task<ResponseMessage> UpdateCustomer(CustomerDto updateCustomer);


        public Task<ResponseMessage> DeleteCustomer (string contractNo);


        public Task<int> GetContractNumber(string kebele, string ketena);

    }
}
