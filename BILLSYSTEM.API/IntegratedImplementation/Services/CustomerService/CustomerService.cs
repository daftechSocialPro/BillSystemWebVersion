using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.DTOS.HRM;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.CSS;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {

        private readonly DBCustomerContext _dbCustomerContext;
        private readonly DBGeneralContext _dbGeneralContext;

        private readonly IMapper _mapper;
        private object _dbContext;

        public CustomerService(DBCustomerContext dbCustomerContext, DBGeneralContext dbGeneralContext
           , IMapper mapper)
        {
            _dbCustomerContext = dbCustomerContext;
            _dbGeneralContext = dbGeneralContext;
            _mapper = mapper;
        }
        public async Task<List<CustomerGetDto>> GetCustomers()
        {

            var customers =
                await _dbCustomerContext.Customers.AsNoTracking()
                                    .ProjectTo<CustomerGetDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            var customerResults =
                     (from cus in customers
                         join customerCategory in _dbGeneralContext.CustomerCategories on cus.custCategoryCode equals customerCategory.custCategoryCode
                        join meterSizeCode in _dbGeneralContext.MeterSizes on cus.MeterSizeCode equals meterSizeCode.meterSizeCode

                        select new CustomerGetDto
                        {
                            regFiscalYear = cus.regFiscalYear,
                            regMonthIndex = cus.regMonthIndex,
                            ContractNo = cus.ContractNo,
                            customerName = cus.customerName,
                            custCategoryCode = customerCategory.custCategoryName,                            
                            MeterSizeCode = meterSizeCode.meterSizeName,
                            meterno =cus.meterno

                        }).ToList();


            return customerResults;

         
            }
        public async Task<CustomerDto> GetCustomerForUpdate(int ContractNo)
        {


            var result = await _dbCustomerContext.Customers.Where(x => x.ContractNo.Equals(ContractNo)).AsNoTracking().ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();


            return result;
        }

        public async Task<ResponseMessage> AddCustomer(CustomerDto addCustomer)
        {
            try
            {

                Customer customer = new Customer()
                {

                    custID = addCustomer.custID,
                    regFiscalYear = addCustomer.regFiscalYear,
                    regMonthIndex = addCustomer.regMonthIndex,
                    customerName = addCustomer.customerName,
                    Ketena = addCustomer.Ketena,
                    Kebele = addCustomer.Kebele,
                    HouseNo = addCustomer.HouseNo,
                    Village = addCustomer.Village,
                    Telephone = addCustomer.Telephone,
                    Mobile = addCustomer.Mobile,
                    BookNo = addCustomer.BookNo,
                    AccountNo = addCustomer.AccountNo,
                    ContractNo = addCustomer.ContractNo,
                    OrdinaryNo = addCustomer.OrdinaryNo,
                    custCategoryCode = addCustomer.custCategoryCode,
                    meterno = addCustomer.meterno,
                    MeterSizeCode = addCustomer.MeterSizeCode,
                    MeterType = addCustomer.MeterType,
                    MeterDigit = addCustomer.MeterDigit,
                    MeterCountryOrigin = addCustomer.MeterCountryOrigin,
                    MeterModel = addCustomer.MeterModel,
                    InstallationDate = addCustomer.InstallationDate,
                    MeterStartReading = addCustomer.MeterStartReading,
                    MeterAltitude = addCustomer.MeterAltitude,
                    MeterLongitude = addCustomer.MeterLongitude,
                    sdPaid = addCustomer.sdPaid,
                    MeterClass = addCustomer.MeterClass,
                    WaterSource = addCustomer.WaterSource,
                    MeterStatus = addCustomer.MeterStatus,
                    RegDate = addCustomer.RegDate,
                    ReaderName = addCustomer.ReaderName,
                    PaymentPlace = addCustomer.PaymentPlace,
                    PaymentDuration = addCustomer.PaymentDuration,
                    PaymentMode = addCustomer.PaymentMode,
                    BillSalesGroup = addCustomer.BillSalesGroup,
                    OnlineGroup = addCustomer.OnlineGroup,
                    BankCode = addCustomer.BankCode,
                    BankAccount = addCustomer.BankAccount,
                    TrasnferBY = addCustomer.TrasnferBY,
                    BillOfficerId = addCustomer.BillOfficerId,
                    TransferFY = addCustomer.TransferFY,
                    TransferMI = addCustomer.TransferMI,
                    TransferDT = addCustomer.TransferDT,
                    Field01 = addCustomer.Field01,
                    Field02 = addCustomer.Field02,
                    Field03 = addCustomer.Field03,
                    Field04 = addCustomer.Field04,
                    enterBy = addCustomer.enterBy,
                    enterDate = addCustomer.enterDate,
                    modifyBy = addCustomer.modifyBy,
                    modifyDate = addCustomer.modifyDate,
                    Remarks = addCustomer.Remarks,
                    DataSynched = addCustomer.DataSynched,
                };



                await _dbCustomerContext.Customers.AddAsync(customer);
                await _dbCustomerContext.SaveChangesAsync();

                return new ResponseMessage
                {

                    Message = "Added Successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = ex.Message,
                    Success = false
                };

            }


        }
    }
}
