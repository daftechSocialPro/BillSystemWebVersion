using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{

    [Table("odb_BillOfficers")]
    public  class BillSection
    {
        [Key]
        public string empID { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string position { get; set; }


    }
}
