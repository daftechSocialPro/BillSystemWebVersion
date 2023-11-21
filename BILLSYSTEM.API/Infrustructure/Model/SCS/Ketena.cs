using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("odb_Ketena")]
    public class Ketena
    {
        [Key]
        public int ketenaCode { get; set; }     
        public string ketenaName { get; set; }
    }
}
