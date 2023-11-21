using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public record MeterSizeRentDto
    {
        public int recordno { get; set; }

        public string RentGroupCode { get; set; }

        public string MeterSizeCode { get; set; }

        public string MeterSize { get; set; }
        public double MeterRent { get; set; }

        public string custCategoryCode { get; set; }
    }
}
