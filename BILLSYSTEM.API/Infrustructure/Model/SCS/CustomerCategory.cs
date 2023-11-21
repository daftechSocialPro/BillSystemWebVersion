using IntegratedInfrustructure.Model.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SRC
{

    [Table("odb_Customer Categories")]
    public class CustomerCategory 
    {
        [Key]
        public int recordno { get; set; }
        public string custCategoryCode { get; set; }
        public string custCategoryName { get; set; }
    }
}
