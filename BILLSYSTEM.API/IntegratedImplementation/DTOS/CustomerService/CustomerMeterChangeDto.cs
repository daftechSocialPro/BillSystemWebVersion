using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.CustomerService
{
    public class CustomerMeterChangeGetDto
    {
        public int? FiscalYear { get; set; }
        public int? monthIndex { get; set; }
        public DateTime? enterDate { get; set; }
        public string? reason { get; set; }

    }
    public class CustomerMeterChangePostDto 
    {
        public int? FiscalYear { get; set; }
        public int? monthIndex { get; set; }
        public string? curMeterNo { get; set; }
        public string? curMeterSizeCode { get; set; }
        public string? curMeterType { get; set; }
        public int? curMeterDigit { get; set; }
        public string? curMeterOrigin { get; set; }
        public string? curMeterModel { get; set; }
        public DateTime? curInstallationDate { get; set; }
        public int? curStartReading { get; set; }
        public int? cnpaidCons { get; set; }
        public string? reason { get; set; }
    }
}
