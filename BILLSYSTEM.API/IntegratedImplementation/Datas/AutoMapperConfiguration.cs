using AutoMapper;
using IntegratedImplementation.DTOS.HRM;
using IntegratedInfrustructure.Model.HRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegratedInfrustructure.Data.EnumList;

using IntegratedInfrustructure.Data;
using IntegratedImplementation.DTOS.Configuration;

using IntegratedInfrustructure.Model.SCS;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedInfrustructure.Model.SRC;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedInfrustructure.Model.DWM;
using IntegratedImplementation.DTOS.DWM;
using IntegratedInfrustructure.Model.CSS;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedInfrustructure.Model.Authentication;

namespace IntegratedImplementation.Datas
{
    public class AutoMapperConfigurations : Profile
    {

        public AutoMapperConfigurations()
        {

            //CreateMap<EmployeeList, EmployeeGetDto>()
            //    .ForMember(a => a.Id, e => e.MapFrom(mfg => mfg.Id))
            //    .ForMember(a => a.EmployeeName, e => e.MapFrom(mfg => $"{mfg.FullName}"))
            //    .ForMember(a => a.Gender, e => e.MapFrom(mfg => mfg.Gender.ToString()))
            //    .ForMember(a => a.EmploymentPosition, e => e.MapFrom(mfg => mfg.EmploymentPosition.ToString()))
            //    .ForMember(a => a.EmploymentStatus, e => e.MapFrom(mfg => mfg.EmploymentStatus.ToString()));

            CreateMap<EmployeeList, SelectListDto>()
               .ForMember(a => a.EmpId, e => e.MapFrom(mfg => mfg.EmpID))
               .ForMember(a => a.Name, e => e.MapFrom(mfg => $"{mfg.FirstName} {mfg.MiddleName} {mfg.LastName}"));

            CreateMap<MeterSize, MeterSizeDto>();
            CreateMap<CustomerCategory, CustomerCategoryDto>();
            CreateMap<GeneralSetting, GeneralSettingDto>();
            CreateMap<GeneralInterface, GeneralInterfaceDto>();
            CreateMap<ConsumptionLevel, ConsumptionLevelDto>();
            CreateMap<MeterSizeRent, MeterSizeRentDto>();
            CreateMap<ConsumptionTariff, ConsumptionTariffDto>();
            CreateMap<Kebeles, KebelesDto>();
            CreateMap<Ketena, KetenaDto>();

            CreateMap<BillSection, BillSectionDto>();
            CreateMap<BillEmpDuties, BillEmpDutiesDto>();


            CreateMap<FiscalMonth, FiscalMonthDto>();
            CreateMap<PenalityRate, PenalityRateDto>();
            CreateMap<AccountPeriod, AccountPeriodDto>();

            CreateMap<User, UserServiceDto>();

            #region DWM
            CreateMap<MobileUsers, MobileUsersDto>();
            CreateMap<MobileAppReading, MobileAppReadingDto>();
            CreateMap<BillToMobileView, MobileAppReading>()
            .ForMember(a => a.PrevReading, e => e.MapFrom(mfg => mfg.Previous))
            .ForMember(a => a.AvgReading, e => e.MapFrom(mfg => mfg.avgReading))
            .ForMember(a => a.OrdinaryNo, e => e.MapFrom(mfg => mfg.OrdinaryNo.ToString()))
            ;
            
            #endregion



            #region cusomer-service

            CreateMap<Customer, CustomerGetDto>()

               .ForMember(a => a.ContractNo, e => e.MapFrom(mfg => mfg.ContractNo))
               .ForMember(a => a.customerName, e => e.MapFrom(mfg => mfg.customerName))
               .ForMember(a => a.regMonthIndex, e => e.MapFrom(mfg => mfg.regMonthIndex))
               .ForMember(a => a.regFiscalYear, e => e.MapFrom(mfg => mfg.regFiscalYear))
               .ForMember(a => a.custCategoryCode, e => e.MapFrom(mfg => mfg.custCategoryCode))
               .ForMember(a => a.MeterSizeCode, e => e.MapFrom(mfg => mfg.MeterSizeCode))
               .ForMember(a => a.meterno, e => e.MapFrom(mfg => mfg.meterno));

              


            #endregion




        }
    }
}
