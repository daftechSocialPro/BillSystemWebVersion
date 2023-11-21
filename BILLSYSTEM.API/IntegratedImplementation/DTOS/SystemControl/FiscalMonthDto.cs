using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public class FiscalMonthDto
    {
       
        public int monthIndex { get; set; }
        public int monthCode { get; set; }
        public string monthnameEn { get; set; }
        public string monthnamelocal { get; set; }
    }
}
