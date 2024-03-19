using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.DWM
{
    [Table("DWMUsers")]
    public class MobileUsers
    {
        [Key]
        public int? Id { get; set; }
        public string? userName { get; set; }
        public string? passWord { get; set; }
        public string? fullName { get; set; }
        public string? Kebele { get; set; }
        public string? Ketena { get; set; }
        public string? phone { get; set; }
        public string? imei1 { get; set; }
        public string? imei2 { get; set; }
        public string? imagePath { get; set; }
        public string? role { get; set; }
        public string? IsSuccess { get; set; }
        public string? Reason { get; set; }
        public string? fiscalYear { get; set; }
        public string? monthIndex { get; set; }
        public string? isRemoved { get; set; }

    }
}
