using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegratedInfrustructure.Data.EnumList;

namespace IntegratedInfrustructure.Model.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public Guid EmployeeId { get; set; }
        public RowStatus RowStatus { get; set; }
     
    }

    [Table("odb_users")]
    public class User
    {

         [Key]
        public int userId { get; set; }         
        public string? userName { get; set; }        
        public string? password { get; set; }
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

  
    
}
