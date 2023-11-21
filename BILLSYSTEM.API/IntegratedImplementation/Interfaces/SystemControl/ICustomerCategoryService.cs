using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface ICustomerCategoryService
    {

        Task<List<CustomerCategoryDto>> GetCustomerCategory();
        Task<ResponseMessage> AddCustomerCategory(CustomerCategoryDto addCustomerCategory);
        Task<ResponseMessage> UpdateCustomerCategory(CustomerCategoryDto updateCustomerCategory);
        Task<ResponseMessage> DeleteCustomerCategory(int CustomerCategoryId);
    }
}
