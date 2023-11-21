using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("odb_Kebeles")]
    public class Kebeles
    {
        [Key]
        public int kebeleCode { get; set; }
        public string kebeleName { get; set; }

        public string ketenaCode { get; set; }
       
    }
}
