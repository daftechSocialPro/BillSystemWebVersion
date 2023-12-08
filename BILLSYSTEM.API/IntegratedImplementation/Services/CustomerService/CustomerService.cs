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
using iTextSharp.text.pdf;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.events.IndexEvents;

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
                            custId = cus.custId,
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
        public async Task<ResponseMessage> UpdateCustomer(CustomerDto updateCustomer)
        {
            try
            {
                var currentCustomer = await _dbCustomerContext.Customers.FirstOrDefaultAsync(x => x.ContractNo.Equals(updateCustomer.ContractNo));

                if (currentCustomer != null)
                {

                    currentCustomer.regFiscalYear = updateCustomer.regFiscalYear;
                   currentCustomer. regMonthIndex = updateCustomer.regMonthIndex;
                   currentCustomer. customerName = updateCustomer.customerName;
                   currentCustomer. Ketena = updateCustomer.Ketena;
                   currentCustomer. Kebele = updateCustomer.Kebele;
                    currentCustomer.HouseNo = updateCustomer.HouseNo;
                    currentCustomer.Village = updateCustomer.Village;
                    currentCustomer.Telephone = updateCustomer.Telephone;
                  currentCustomer. Mobile = updateCustomer.Mobile;
                   currentCustomer. BookNo = updateCustomer.BookNo;
                   currentCustomer. AccountNo = updateCustomer.AccountNo;
                   currentCustomer. ContractNo = updateCustomer.ContractNo;
                   currentCustomer. OrdinaryNo = updateCustomer.OrdinaryNo;
                   currentCustomer. custCategoryCode = updateCustomer.custCategoryCode;
                   currentCustomer. meterno = updateCustomer.meterno;
                   currentCustomer. MeterSizeCode = updateCustomer.MeterSizeCode;
                   currentCustomer. MeterType = updateCustomer.MeterType;
                   currentCustomer. MeterDigit = updateCustomer.MeterDigit;
                   currentCustomer. MeterCountryOrigin = updateCustomer.MeterCountryOrigin;
                   currentCustomer. MeterModel = updateCustomer.MeterModel;
                    currentCustomer.InstallationDate = updateCustomer.InstallationDate;
                    currentCustomer.MeterStartReading = updateCustomer.MeterStartReading;
                   currentCustomer. MeterAltitude = updateCustomer.MeterAltitude;
                   currentCustomer. MeterLongitude = updateCustomer.MeterLongitude;
                   currentCustomer. sdPaid = updateCustomer.sdPaid;
                   currentCustomer. MeterClass = updateCustomer.MeterClass;
                   currentCustomer. WaterSource = updateCustomer.WaterSource;
                   currentCustomer. MeterStatus = updateCustomer.MeterStatus;
                   currentCustomer. RegDate = updateCustomer.RegDate;
                   currentCustomer. ReaderName = updateCustomer.ReaderName;
                   currentCustomer. PaymentPlace = updateCustomer.PaymentPlace;
                   currentCustomer .PaymentDuration = updateCustomer.PaymentDuration;
                   currentCustomer. PaymentMode = updateCustomer.PaymentMode;
                    currentCustomer.BillSalesGroup = updateCustomer.BillSalesGroup;
                    currentCustomer.OnlineGroup = updateCustomer.OnlineGroup;
                   currentCustomer. BankCode = updateCustomer.BankCode;
                   currentCustomer. BankAccount = updateCustomer.BankAccount;
                    currentCustomer.TrasnferBY = updateCustomer.TrasnferBY;
                    currentCustomer.BillOfficerId = updateCustomer.BillOfficerId;
                    currentCustomer.TransferFY = updateCustomer.TransferFY;
                    currentCustomer.TransferMI = updateCustomer.TransferMI;
                     currentCustomer.TransferDT = updateCustomer.TransferDT;
                    currentCustomer.Field01 = updateCustomer.Field01;
                     currentCustomer.Field02 = updateCustomer.Field02;
                     currentCustomer.Field03 = updateCustomer.Field03;
                    currentCustomer.Field04 = updateCustomer.Field04;
                    currentCustomer.enterBy = updateCustomer.enterBy;
                    currentCustomer.enterDate = updateCustomer.enterDate;
                    currentCustomer.modifyBy = updateCustomer.modifyBy;
                    currentCustomer.modifyDate = updateCustomer.modifyDate;
                    currentCustomer.Remarks = updateCustomer.Remarks;
                    currentCustomer.DataSynched = updateCustomer.DataSynched;


                    await _dbCustomerContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find the  Customer" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field Contract number ",
                    Success = false
                };

            }

        }
        public async Task<ResponseMessage> DeleteCustomer(int CustomerId)
        {

            var currentCustomer = await _dbCustomerContext.Customers.FindAsync(CustomerId);

            if (currentCustomer != null)
            {

                _dbCustomerContext.Remove(currentCustomer);
                await _dbCustomerContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find this customer" };
        }
    }
}
