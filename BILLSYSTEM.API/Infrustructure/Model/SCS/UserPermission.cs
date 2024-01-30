using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{

    [Table("odb_zUserPermission")] 
    public class UserPermission
    {

        public string UserId { get; set; }

        public string ButtonId { get; set; }
    }

}
