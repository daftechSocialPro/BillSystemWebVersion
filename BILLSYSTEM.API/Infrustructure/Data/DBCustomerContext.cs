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



    }
}
