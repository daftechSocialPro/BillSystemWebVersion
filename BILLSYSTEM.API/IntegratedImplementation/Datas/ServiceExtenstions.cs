using Implementation.Interfaces.Authentication;
using Implementation.Services.Authentication;
using IntegratedImplementation.Interfaces.Configuration;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedImplementation.Interfaces.HRM;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedImplementation.Services.Configuration;
using IntegratedImplementation.Services.DWM;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedImplementation.Interfaces.HRM;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedImplementation.Services.Configuration;
using IntegratedImplementation.Services.CustomerService;
using IntegratedImplementation.Services.HRM;
using IntegratedImplementation.Services.SystemControl;
using Microsoft.Extensions.DependencyInjection;

namespace IntegratedImplementation.Datas
{
    public static class ServiceExtenstions
    {
        public static IServiceCollection AddCoreBusiness(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //hrm services 
         
            services.AddScoped<IGeneralConfigService, GeneralConfigService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            #region System-control

            services.AddScoped<IMeterSizeService, MeterSizeService>();
            services.AddScoped<ICustomerCategoryService, CustomerCategoryService>();
            services.AddScoped<IGeneralSettingService, GeneralSettingService>();
            services.AddScoped<IGeneralInterfaceService, GeneralInterfaceService>();
            services.AddScoped<IConsumptionLevelService, ConsumptionLevelService>();
            services.AddScoped<IMeterSizeRentService, MeterSizeRentService>();
            services.AddScoped<IConsumptionTariffService, ConsumptionTariffService>();
            services.AddScoped<IKetenaService, KetenaService>();
            services.AddScoped<IKebelesService, KebelesService>();
            services.AddScoped<IGeneralOptionsService, GeneralOptionService>();

            services.AddScoped<IBillSectionService, BillSectionService>();
            services.AddScoped<IBillEmpDutiesService, BillEmpDutiesService>();

            services.AddScoped<IFiscalMonthService, FiscalMonthService>();
            services.AddScoped<IPenalityRateService, PenalityRateService>();
            services.AddScoped<IAccountPeriodService, AccountPeriodService>();

            services.AddScoped<IDataBaseBackUpService, DatabaseBackUpService>();
            services.AddScoped<IBillTemplateService, BillTemplateService>();


            //services.AddScoped<IVillageService, VillageService>();
            //services.AddScoped<IMeterOriginService, MeterOriginService>();




            #endregion


            #region dwm

            services.AddScoped<IMobileUsersService, MobileUsersService>();
            services.AddScoped<IMobileAppReadingService, MobileAppReadingService>();
            services.AddScoped<IQRCodeGenerateService, QRCodeGenerateService>();
            services.AddScoped<IDWMDashboardService, DWMDashboardService>();
            services.AddScoped<ICustomerCollectedService, CustomerCollectedService>();
            services.AddScoped<IDWMReportService, DWMReportService>();
            services.AddScoped<IDWMMobileService, DWMMobileService>();

            #endregion

            #region Customer-Service
            services.AddScoped<ICustomerService, CustomerService>();
            #endregion


            return services;
        }
    }
}
