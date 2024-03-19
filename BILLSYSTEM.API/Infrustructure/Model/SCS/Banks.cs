
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{

    [Table("odb_Banks")]
    public class Banks
    {

        [Key]

        public int recordno { get; set; }

        public string? BankCode { get; set; }

        public string? BankName { get; set; }
    }
}
