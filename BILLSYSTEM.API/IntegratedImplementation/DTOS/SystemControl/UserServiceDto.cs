using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public record UserServiceDto
    {
        public int? userId { get; set; }
        public string? userName { get; set; }      
        public string? empId { get; set; }
        public string? userLevel { get; set; }
        public string? userStatus { get; set; }
        public string? SysetmAdmin { get; set; }
        public string? CustomerService { get; set; }
        public string? BillProduce { get; set; }
        public string? TechnicalService { get; set; }
        public string? StockControl { get; set; }
        public string? HRM { get; set; }
        public string? AllowKetenas { get; set; }
        public string? Online { get; set; }



    }
    public record UserServicePostDto:UserServiceDto
    {   
        public string? password { get; set; }
        
      
        public string? AllowKetenas { get; set; }
        public string? Online { get; set; }

    }
}
