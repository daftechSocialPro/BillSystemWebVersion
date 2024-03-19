using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.CSS
{
    [Table("meterChanges")]
    public class CustomerMeterChange
    {
        [Key]
        public Guid recordno { get; set; }
        public string? custID { get; set; }
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
        public int? unpaidCons { get; set; }
        public string? meterNo { get; set; }
        public DateTime? changeDate { get; set; }
        public string? reason { get; set; }
        public string? enterBy { get; set; }
        public DateTime? enterDate { get; set; }
        public string? modifyBy { get; set; }
        public DateTime? modifyDate { get; set; }
        public string? remark { get; set; }
        public string? dataSynched { get; set; }

    }
}
