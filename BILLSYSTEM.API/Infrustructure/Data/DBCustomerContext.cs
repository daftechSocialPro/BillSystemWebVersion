using IntegratedInfrustructure.Model.DWM;
using IntegratedInfrustructure.Model.CSS;
using IntegratedInfrustructure.Model.HRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Data
{
    public class DBCustomerContext : DbContext
    {

        public DBCustomerContext(DbContextOptions<DBCustomerContext> options)
        : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } 
        public DbSet<CustomerMeterStatus> CustomerMeterStatus { get; set; }        


        public DbSet<MobileUsers> MobileUsers { get; set; }
        public DbSet<MobileAppReading> MobileAppReadings { get; set; }
        public DbSet<BillToMobileView> BillToMobile { get; set; }
        public DbSet<BillMobileData> BillMobileData { get; set; }



        



    }
}
