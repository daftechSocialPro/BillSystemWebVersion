using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.CustomerService
{
    public record CustomerHomeDto
    {
        public string CustomerCategory { get; set; }
        public int ActiveCustomers { get; set; }

        public int DisconnectedCustomers { get; set; }

        public int TerminatedCustomers { get; set; }
    }
}
