using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.CustomerService
{
    public record CustomerToBillOfficerDto
    {
        public List<string> CustomerContractNos { get; set; }

        public string BillOfficerId { get; set; }
    }
}
