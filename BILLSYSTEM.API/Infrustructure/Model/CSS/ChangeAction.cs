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
    public class ChangeAction
    {
        [Key]
        public int customerName { get; set; }
        public int? Kebele { get; set; }
        public int meterno { get; set; }
        public string? MeterSizeCode { get; set; }
        public string? custCategoryCode { get; set; }
        public string? ContractNo { get; set; }
        public string? OrdinaryNo { get; set; }
        public int? FiscalYear { get; set; }
        public int monthIndex { get; set; }
        public string? custID { get; set; }
        public string? disDate { get; set; }
        public string? typeOfAction { get; set; }
        public string? actionBy { get; set; }
        public string? reason { get; set;}
        public string? enterBy { get; set;}
        public DateTime? enterDate { get; set;}
        public string? modifyBy { get; set; }
        public DateTime? modifyDate { get; set;}
        
    }
}
