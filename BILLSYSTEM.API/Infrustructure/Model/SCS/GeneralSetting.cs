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


    [Table("odb_Input Values")]
    public class GeneralSetting
    {
        [Key]
        public Guid recordno { get; set; }
        public string InputValues { get; set; }
        public string InputCategory { get; set; }
    }
}
