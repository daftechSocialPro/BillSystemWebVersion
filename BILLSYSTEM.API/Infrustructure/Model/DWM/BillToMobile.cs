using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.DWM
{

    [Table("billToMobile")]
    public class BillToMobileView
    {
        [Key]
        public string custID { get; set; }
        public string? custCategoryCode { get; set; }
        public string? MeterSizeCode { get; set; }
        public string? Ketena { get; set; }
        public string? Village { get; set; }
        public string? BookNo { get; set; }
        public string? sdPaid { get; set; }
        public string? MeterStatus { get; set; }
        public string? ReaderName { get; set; }
        public string? Mobile { get; set; }
        public string? ContractNo { get; set; }
        public string? customerName { get; set; }
        public string? Kebele { get; set; }
        public string? HouseNo { get; set; }
        public int? OrdinaryNo { get; set; }
        public int? regFiscalYear { get; set; }
        public int? regMonthIndex { get; set; }
        public string? CustomerCategory { get; set; }
        public string? meterno { get; set; }
        public string? MeterSize { get; set; }
        public double? MeterAltitude { get; set; }
        public double? MeterLongitude { get; set; }
        public string? BillOfficerId { get; set; }
        public int? Previous { get; set; }
        public int? CurrentReading { get; set; }
        public double? avgReading { get; set; }
        public int? fiscalYear { get; set; }
        public int? monthIndex { get; set; }
        public double? PrevTotalCost { get; set; }
        public string? Month { get; set; }
        public string? monthnamelocal { get; set; }
        public int? prevNoMth { get; set; }

    }
}
