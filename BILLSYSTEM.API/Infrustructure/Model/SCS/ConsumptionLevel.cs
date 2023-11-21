using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{

    [Table("odb_BillLevels")]
    public class ConsumptionLevel
    {
        [Key]
        public int recordno { get; set; }

        public string levelNameEN { get; set; }

        public string levelNameLocal { get; set; }
    }
}
