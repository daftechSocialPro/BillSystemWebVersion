using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("billempDuties")]
    public class BillEmpDuties
    {
        [Key]
        public int recordno { get; set; }
        public string empID { get; set; }
        public string duties { get; set; }

    }
}
