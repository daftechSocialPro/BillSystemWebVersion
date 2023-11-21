using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("odb_Interface")]
    public class GeneralInterface
    {
        [Key]
        
        public int recordno { get; set; }
        public string ObjectNameEN { get; set; }
        public string ObjectNameLocalam { get; set; }
        public string ObjectNameLocalen { get; set; }
        public string ObjectCategory { get; set; }
       
    }
}
