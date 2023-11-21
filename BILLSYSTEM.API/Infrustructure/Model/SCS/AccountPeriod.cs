using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("odb_User Periods")]
    public class AccountPeriod
    {
        [Key]
        public int recordno { get; set; }
        public string UserID { get; set; }
        public int FiscalYear { get; set; }
        public int MonthIndex { get; set; }
    }
}
