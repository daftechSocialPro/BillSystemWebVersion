using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("odb_months")]

    public class FiscalMonth
    {
        [Key]
        public int monthIndex { get; set; }
        public int monthCode { get; set; }
        public string monthnameEn { get; set; }
        public string monthnamelocal { get; set; }
    }
}
