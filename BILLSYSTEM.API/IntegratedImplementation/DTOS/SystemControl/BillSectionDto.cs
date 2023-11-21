using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public record BillSectionDto
    {
        public string empID { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string position { get; set; }
    }
}
