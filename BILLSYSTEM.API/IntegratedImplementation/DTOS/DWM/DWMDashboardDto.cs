using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.DWM
{
    public class DWMDashboardDto
    {

        public int TotalCustomers { get; set; }

        public int GpsEncoded { get; set; }

        public int Readed { get; set; }

        public int Pending { get; set; }

        public ReadingTypeRatioDto ReadingTypeRatio { get; set; }

        public List<AnnuallyConsumptionDto> AnnuallyConsumption { get; set;}

    }

    public class ReadingTypeRatioDto
    {
        public int Id { get; set; }
        public int AboveAVG { get; set; }
        public int BelowAVG { get; set; }
        public int Normal { get; set; }
        public int ZeroReading { get; set; }
        public int ReasonOfCode { get; set; }
        public int TotalReading { get; set; }
    }


    public class AnnuallyConsumptionDto
    {
        public int? Consumption { get; set; }
        public string Month_Name { get; set; }
        public int? FiscalYear { get; set; }

    }
}
