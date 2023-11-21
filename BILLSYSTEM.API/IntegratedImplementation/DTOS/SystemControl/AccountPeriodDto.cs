using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public class AccountPeriodDto
    {
        public int recordno { get; set; }
        public string UserID { get; set; }
        public int FiscalYear { get; set; }
        public int MonthIndex { get; set; }
    }
}
