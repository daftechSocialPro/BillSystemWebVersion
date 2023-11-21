using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("odb_PenalityRates")]
    public class PenalityRate
    {
        
        [Key]
        public int recordno { get; set; }
        public string NoOfMonth { get; set; }
        public double PenalityAmount { get; set; }
        public string CustCategoryCode { get; set; }
        public string PenalityGroupCode { get; set; }
     
    }
}
