using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{

    [Table("odb_Meter SizeRent")]
    public class MeterSizeRent
    {
        [Key]
        public int recordno { get; set; }

        public string RentGroupCode { get; set; }

        public string MeterSizeCode { get; set; }

        public double MeterRent { get; set; }

        public string custCategoryCode { get; set; }
    }
}
