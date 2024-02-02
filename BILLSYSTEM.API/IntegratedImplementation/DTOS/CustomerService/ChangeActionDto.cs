using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.CustomerService
{
    public class ChangeActionGetDto
    {
        public int customerName { get; set; }
        public int? Kebele { get; set; }
        public int meterno { get; set; }
        public string? MeterSizeCode { get; set; }
        public string? custCategoryCode { get; set; }
        public string? ContractNo { get; set; }
        public string? OrdinaryNo { get; set; }

    }
    public class ChangeActionpostDto
    {
        public string? reason { get; set;}
        public string? enterBy { get; set; }
        public DateTime? enterDate { get; set; }
        public string? modifyBy { get; set; }
        public DateTime? modifyDate { get; set; }
    }
}
