using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public class BillEmpDutiesDto
    {
        public int? recordno { get; set; }
        public string empID { get; set; }
        public string name { get; set; }
        public string duties { get; set; }
    }
}
