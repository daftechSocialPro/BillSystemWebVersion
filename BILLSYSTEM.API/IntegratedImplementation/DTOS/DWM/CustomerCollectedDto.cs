using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.DWM
{
    public record CustomerCollectedDto
    {
            public string CustomerName { get; set; }
            public string MeterNo { get; set; }
            public string CustId { get; set; }
            public double? ReadingPrev { get; set; }
            public double? ReadingCurrent { get; set; }
            public double? ReadingAvg { get; set; }
            public string ReadingImage { get; set; }
            public double? Consumption { get; set; }
            public string ReadingReasonCode { get; set; }
            public string FullName { get; set; }
            public string UserName { get; set; }
            public DateTime? EntryDT { get; set; }
            public DateTime? ReadingDT { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public string ContractNo { get; set; }
        
    }
}
