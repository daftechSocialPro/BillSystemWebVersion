using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.CustomerService
{
    public record CustomerBatchDto
    {

        public List<string> SelectedCustomerIds { get; set; }

        public string ChangeByName { get; set; }

        public string ChangedValue { get; set; }
    }
}
