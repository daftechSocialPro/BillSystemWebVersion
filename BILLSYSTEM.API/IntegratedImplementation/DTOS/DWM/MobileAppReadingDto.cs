using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.DWM
{
    public record MobileAppReadingDto
    {
        public string custId { get; set; }
        public string ReaderName { get; set; }
        public string Mobile { get; set; }

        public string ContractNo { get; set; }

        public string customerName { get; set; }

        public int? PrevReading { get; set; }

        public int? fiscalYear { get; set; }

        public int? monthindex { get; set; }
        public double? PrevTotalCost { get; set; }

        public string month { get; set; }

        public string monthnamelocal { get; set; }
    }

    public record ReadingAverageCount
    {
        public int ReadingCount { get; set; }

        public int AverageNotCalculatedCount { get; set; }
    }


}
