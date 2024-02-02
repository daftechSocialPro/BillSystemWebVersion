using IntegratedImplementation.DTOS.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.CustomerService
{
    public interface IChangeActionService
    {
        public Task<List<ChangeActionGetDto>> GetCustomerStatus();

    }
}
