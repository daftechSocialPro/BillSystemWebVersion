using IntegratedInfrustructure.Model.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegratedInfrustructure.Model.Configuration;
using IntegratedInfrustructure.Model.HRM;
using IntegratedInfrustructure.Model.SRC;
using IntegratedInfrustructure.Model.SCS;

namespace IntegratedInfrustructure.Data
{

    public class DBGeneralContext  : DbContext
    {

        public DBGeneralContext(DbContextOptions<DBGeneralContext> options)
        : base(options)
        {
        }


        public DbSet<User> Users { get; set; }

        #region configuration

        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<GeneralCodes> GeneralCodes { get; set; }

        #endregion

        #region HRM


        
        public DbSet<HrmSetting> HrmSettings { get; set; }

        #endregion

        #region SCS

        public DbSet<CustomerCategory> CustomerCategories { get; set; }
        public DbSet<MeterSize> MeterSizes { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<GeneralInterface> GeneralInterfaces { get; set; }
        public DbSet<ConsumptionLevel> ConsumptionLevels { get; set; }     

        public DbSet<MeterSizeRent> MeterSizeRents { get; set; }   
        public DbSet<ConsumptionTariff> ConsumptionTariffs { get; set; }  
        public DbSet<Kebeles> Kebeless { get; set; }
        public DbSet<Ketena> Ketenas { get; set; }      



        public DbSet<GeneralOptions> GeneralOptions { get; set; } 
        public DbSet<BillSection> BillSections { get; set; }
        public DbSet<BillEmpDuties> BillEmpDuties { get; set; }




        public DbSet<FiscalMonth> FiscalMonths { get; set; }      
     
        public DbSet<PenalityRate> PenalityRates { get; set; }     
        public DbSet<AccountPeriod> AccountPeriods { get; set; }    
        

        public DbSet<DetailPermission> DetailPermissions { get; set; }


        public DbSet<UserPermission> UserPermissions { get; set; }  

     


        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<GeneralCodes>()
               .HasIndex(b => b.GeneralCodeType).IsUnique();

            modelBuilder.Entity<UserPermission>()
               .HasNoKey();

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(r => new { r.UserId, r.RoleId });
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            });


        }
    }
}

