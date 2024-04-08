using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.DWM
{
    public record ImportExportDto
    {
      
            public string custID { get; set; }
            public string custCategoryCode { get; set; }
            public string MeterSizeCode { get; set; }
            public string Ketena { get; set; }
            public string Village { get; set; }
            public string BookNo { get; set; }
            public string sdPaid { get; set; }
            public string MeterStatus { get; set; }
            public string ReaderName { get; set; }
            public string Mobile { get; set; }
            public string ContractNo { get; set; }
            public string customerName { get; set; }
            public string Kebele { get; set; }
            public string HouseNo { get; set; }
            public string OrdinaryNo { get; set; }
            public Nullable<int> regFiscalYear { get; set; }
            public Nullable<int> regMonthIndex { get; set; }
            public string CustomerCategory { get; set; }
            public string meterno { get; set; }
            public string MeterSize { get; set; }
            public Nullable<double> MeterAltitude { get; set; }
            public Nullable<double> MeterLongitude { get; set; }
            public string BillOfficerId { get; set; }
            public Nullable<int> PrevReading { get; set; }
            public Nullable<int> CurrentReading { get; set; }
            public Nullable<int> fiscalYear { get; set; }
            public Nullable<int> monthIndex { get; set; }
            public Nullable<double> prevTotalCost { get; set; }
            public string Month { get; set; }
            public string monthnamelocal { get; set; }
            public Nullable<double> avgReading { get; set; }
            public string IsSuccess { get; set; }
            public string Reason { get; set; }
            public Nullable<int> prevNoMth { get; set; }
        
    }


    
}
