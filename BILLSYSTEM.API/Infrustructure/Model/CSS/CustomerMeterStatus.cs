using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.CSS
{
    [Table ("meterDisconnet")]
    public class CustomerMeterStatus
    {
        [Key]
        public Guid recordno { get; set; }
        public int FiscalYear { get; set; }
        public int monthIndex { get; set; }
        public string? custID { get; set; }
        public string? disDate { get; set; }
        public string? typeOfAction { get; set; }
        public string? actionBy { get; set; }
        public string? reason { get; set; }
        public string? enterBy { get; set; }
        public DateTime enterDate { get; set; }
        public string? modifyBy { get; set; }
        public DateTime modifyDate { get; set; }
        public string? remark { get; set; }

    }
}
