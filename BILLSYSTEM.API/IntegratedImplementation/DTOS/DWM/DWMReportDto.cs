using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.DWM
{
    public record DWMReadingLogReportDto
    {
        public string ReaderName { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string MeterNo { get; set; }

        public double? Previous { get; set; }
        public double? Current { get; set; }
        public double? Consumption { get; set; }
        public double? Average { get; set; }
        public string Status { get; set; }

        public int? MonthIndex { get; set; }

        public int? FiscalYear { get; set; }

    }

    public record DWMPendingLogReportDto
    {
        public string ReaderName { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string MeterNo { get; set; }
        public string ContractNo { get; set; }



    }
    public record DWMReadingAccuracyReportDto
    {
        public string ReaderName { get; set; }
        public string UserName { get; set; }
        public int AboveAVG { get; set; }
        public int BelowAVG { get; set; }
        public int Normal { get; set; }



    }

    public record DWMReadingEfficencyReportDto
    {
        public string ReaderName { get; set; }
        public string UserName { get; set; }
        public int Readed { get; set; }
        public int TotalCustomers { get; set; }
    }
    public record DWMReadingConsumptionReportDto
    {
        public string ReaderName { get; set; }
        public string UserName { get; set; }
        public double? Consumption { get; set; }
       
    }
}
