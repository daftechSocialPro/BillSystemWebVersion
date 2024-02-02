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
    public class CustomerMeterStatuspostDto
    {
        public string? reason { get; set;}
        public string? enterBy { get; set; }
        public DateTime? enterDate { get; set; }
        public string? modifyBy { get; set; }
        public DateTime? modifyDate { get; set; }
    }
}
