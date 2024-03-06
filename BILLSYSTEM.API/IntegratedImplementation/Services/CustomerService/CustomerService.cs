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
using IntegratedInfrustructure.Model.CSS;
using Microsoft.Azure.Management.Compute.Fluent.Models;

namespace IntegratedImplementation.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {

        private readonly DBCustomerContext _dbCustomerContext;
        private readonly DBGeneralContext _dbGeneralContext;

        private readonly IMapper _mapper;
       

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
                      join CustomerCategory in _dbGeneralContext.CustomerCategories on cus.custCategoryCode equals CustomerCategory.custCategoryCode
                      join meterSizeCode in _dbGeneralContext.MeterSizes on cus.MeterSizeCode equals meterSizeCode.meterSizeCode

                      select new CustomerGetDto
                      {
                          regFiscalYear = cus.regFiscalYear,
                          regMonthIndex = cus.regMonthIndex,
                          ContractNo = cus.ContractNo,
                          customerName = cus.customerName,
                          custId = cus.custId,
                          custCategoryCode = CustomerCategory.custCategoryName,
                          MeterSizeCode = meterSizeCode.meterSizeName,
                          meterno = cus.meterno       

                      }).ToList();



        


            return customerResults;
;
        }

       


        public async Task<ResponseMessage> AddCustomer(CustomerPostDto customerPost)
        {
            try
            {
                var customerLast = await _dbCustomerContext.Customers.
           Select(x => x.custID).ToListAsync();
                List<long> contactNumbers = new List<long>();
                foreach (var c in customerLast)
                {
                    if (c != null)
                        contactNumbers.Add(long.Parse(c));
                }
                var contactNumber = contactNumbers.Max() + 1;

                var InstallationDate = customerPost.InstallationDate;
                InstallationDate = InstallationDate.AddTicks(-InstallationDate.Ticks % TimeSpan.TicksPerSecond);
                Customer customers = new Customer()
                {
                    custID = contactNumber.ToString(),
                    customerName = customerPost.FullName,
                    Telephone = customerPost.PhoneNumber,
                    Ketena = customerPost.Ketena,
                    Kebele = customerPost.Kebele,
                    Village = customerPost.Village,
                    MeterStartReading = customerPost.StartReading,
                    // map number 
                    // bill officer
                    HouseNo = customerPost.HouseNumber,
                    ReaderName = customerPost.ReaderName,
                    sdPaid = customerPost.SweragePaid,
                    custCategoryCode = customerPost.CustomerCategory,
                    ContractNo = customerPost.ContractNo,
                    OrdinaryNo = customerPost.OrdinaryNo,
                    meterno = customerPost.MeterNo,
                    MeterSizeCode = customerPost.MeterSize,
                    InstallationDate = InstallationDate,
                    regMonthIndex = customerPost.MonthIndex,
                    regFiscalYear = customerPost.FiscalYear,
                    enterDate = InstallationDate,
                   

                };


            


                await _dbCustomerContext.Customers.AddAsync(customers);
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

        public async Task<ResponseMessage> DeleteCustomer(string contractNo)
        {
            try
            {
               
                var currentCustomer = await _dbCustomerContext.Customers.AsNoTracking().Where(x=>x.ContractNo==contractNo).FirstOrDefaultAsync();

                if (currentCustomer != null)
                {

                    _dbCustomerContext.Remove(currentCustomer);
                    await _dbCustomerContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Deleted", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Customer" };
            }catch(Exception ex)
            {
                return new ResponseMessage { Success = false, Message = "Unable To Find Customer" };
            }


        }

        public async  Task<CustomerGetDto> GetSingleCustomer(string contractNo)
        {

            var customers =
                await _dbCustomerContext.Customers.Where(x=>x.ContractNo==contractNo).AsNoTracking()
                                    .ProjectTo<CustomerGetDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            var customerResults =
                     (from cus in customers
                      join CustomerCategory in _dbGeneralContext.CustomerCategories on cus.custCategoryCode equals CustomerCategory.custCategoryCode
                      join meterSizeCode in _dbGeneralContext.MeterSizes on cus.MeterSizeCode equals meterSizeCode.meterSizeCode

                      select new CustomerGetDto
                      {
                          regFiscalYear = cus.regFiscalYear,
                          regMonthIndex = cus.regMonthIndex,
                          ContractNo = cus.ContractNo,
                          customerName = cus.customerName,
                          custId = cus.custId,
                          custCategoryCode = CustomerCategory.custCategoryName,
                          MeterSizeCode = meterSizeCode.meterSizeName,
                          meterno = cus.meterno

                      }).FirstOrDefault();





            return customerResults;
            ;

        }
        public async Task<ResponseMessage> UpdateCustomer(CustomerDto updateCustomer)
        {
            try
            {
                var currentcustomer = await _dbCustomerContext.Customers.FirstOrDefaultAsync(x => x.ContractNo.Equals(updateCustomer.ContractNo));

                if (currentcustomer != null)
                {


                    currentcustomer.custID = updateCustomer.custID;
                    currentcustomer.regFiscalYear = updateCustomer.regFiscalYear;
                    currentcustomer.regMonthIndex = updateCustomer.regMonthIndex;
                    currentcustomer.customerName = updateCustomer.customerName;
                    currentcustomer.Ketena = updateCustomer.Ketena;
                    currentcustomer.Kebele = updateCustomer.Kebele;
                    currentcustomer.HouseNo = updateCustomer.HouseNo;
                    currentcustomer.Village = updateCustomer.Village;
                    currentcustomer.Telephone = updateCustomer.Telephone;
                    currentcustomer.Mobile = updateCustomer.Mobile;
                    currentcustomer.BookNo = updateCustomer.BookNo;
                    currentcustomer.AccountNo = updateCustomer.AccountNo;
                    currentcustomer.ContractNo = updateCustomer.ContractNo;
                    currentcustomer.ReaderName = updateCustomer.readerName;
                    currentcustomer.OrdinaryNo = updateCustomer.OrdinaryNo;
                   // currentcustomer.BillCycle = updateCustomer.BillCycle;
                    currentcustomer.custCategoryCode = updateCustomer.custCategoryCode;
                    currentcustomer.meterno = updateCustomer.meterno;
                    currentcustomer.MeterSizeCode = updateCustomer.MeterSizeCode;
                    currentcustomer.MeterDigit = updateCustomer.MeterDigit;
                    currentcustomer.MeterType = updateCustomer.MeterType;
                    currentcustomer.MeterModel = updateCustomer.MeterModel;
                    currentcustomer.MeterCountryOrigin = updateCustomer.MeterCountryOrigin;
                    currentcustomer.InstallationDate = updateCustomer.InstallationDate;
                    currentcustomer.MeterStartReading = updateCustomer.MeterStartReading;
                    currentcustomer.sdPaid = updateCustomer.sdPaid;
                    currentcustomer.MeterClass = updateCustomer.MeterClass;
                    currentcustomer.WaterSource = updateCustomer.WaterSource;
                    currentcustomer.MeterStatus = updateCustomer.MeterStatus;
                    currentcustomer.RegDate = updateCustomer.RegDate;
                    currentcustomer.PaymentMode = updateCustomer.PaymentMode;
                    currentcustomer.BankAccount = updateCustomer.BankAccount;
                    currentcustomer.BillOfficerId = updateCustomer.BillOfficerId;
                   

                    //currentcustomer.custCategoryName = updatecustomer.custCategoryName;


        await _dbCustomerContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Customer " };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field contract NO ",
                    Success = false
                };

            }

        }
        public async Task<int> GetContractNumber(string kebele, string ketena)
        {
            
            var customerLast = await _dbCustomerContext.Customers
                .Where(x => x.Kebele == kebele && x.Ketena == ketena).Select(x => x.ContractNo).ToListAsync();
            List<int> contactNumbers = new List<int>();
            foreach(var c in customerLast)
            {
                contactNumbers.Add(Int32.Parse(c));
            }
            var contactNumber = contactNumbers.Max() + 1;

            return contactNumber;
        }

        //    public  async  Task<int> GetContractNumber(string kebele, string ketena)
        //    {
        //        var customerLast = _dbCustomerContext.Customers
        //    .Where(x => x.Kebele == kebele && x.Ketena == ketena && int.TryParse(x.ContractNo, out  ContractNo))
        //    .Max(x => int.Parse(x.ContractNo));
        //        var contractNo= customerLast + 1;

        //        return contractNo;
        //    }
        //
        }
    }
