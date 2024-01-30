using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{
    [Table("odb_zButtonAccess")]
    public class DetailPermission
    {
        [Key]
        public string recId { get; set; }

        public string ? AppModule { get; set; }

        public string ? AppTabs { get; set; }

        public string ?ButtonName { get; set; }

        public string? ButtonCaption { get; set; } 

    }
}
