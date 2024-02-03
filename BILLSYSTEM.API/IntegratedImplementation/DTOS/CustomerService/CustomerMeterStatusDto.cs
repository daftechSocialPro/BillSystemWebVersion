using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.CustomerService
{
    public class CustomerMeterStatusGetDto
    {


        public int FiscalYear { get; set; }
        public int monthIndex { get; set; }

        public string? typeOfAction { get;set; }
        public string? reason { get; set; }

        public DateTime? enterDate { get; set; }
    

    }
public class CustomerMeterStatusPostDto
        {
            public int FiscalYear { get; set; }
            public int MonthIndex { get; set; }
            public string TypeOfAction { get; set; }
            public string Reason { get; set; }
            public DateTime DisDate { get; set; }
            public string CustId { get; set; }
        }
    
}
