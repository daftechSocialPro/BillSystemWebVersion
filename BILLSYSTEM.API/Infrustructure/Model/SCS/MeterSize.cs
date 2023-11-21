using IntegratedInfrustructure.Model.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("odb_Meter Size")]
    public class MeterSize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int recordno { get; set; }
        public string meterSizeCode { get; set;}
        public string meterSizeName { get; set; }
    }
}
