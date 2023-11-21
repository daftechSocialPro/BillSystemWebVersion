using IntegratedInfrustructure.Model.HRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Data
{
    public class DBHumanContext : DbContext
    {

        public DBHumanContext(DbContextOptions<DBHumanContext> options)
        : base(options)
        {
        }

        public DbSet<EmployeeList> Employees { get; set; }
    }
}
