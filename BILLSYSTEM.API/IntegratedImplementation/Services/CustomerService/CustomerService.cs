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

            //var c = customers.Where(x => x.customerName == "kirubel").ToList();
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
                          meterno = cus.meterno,
                          MeterStartReading= cus.MeterStartReading,
                          Kebele = cus.Kebele,
                          BillOfficerId = cus.BillOfficerId,
                          OrdinaryNo = cus.OrdinaryNo,
                          //InstallationDate = cus.InstallationDate,



                         





                      }).ToList();



        


            return customerResults;
;
        }

       


        public async Task<ResponseMessage> AddCustomer(CustomerDto customerPost)
        {
            try
            {
                var customerLast = await _dbCustomerContext.Customers.
                                    Select(x => x.custID).ToListAsync();
                List<long> custIds = new List<long>();
                foreach (var c in customerLast)
                {
                    if (c != null)
                        custIds.Add(long.Parse(c));
                }
                var custId = custIds.Max() + 1;

                var InstallationDate = customerPost.InstallationDate;
                //InstallationDate = InstallationDate.AddTicks(-InstallationDate.Ticks % TimeSpan.TicksPerSecond);
                Customer customers = new Customer()
                {
                    custID = custId.ToString(),
                    customerName = customerPost.customerName,
                    Telephone = customerPost.Telephone,
                    Ketena = customerPost.Ketena,
                    Kebele = customerPost.Kebele,
                    Village = customerPost.Village,
                    MeterStartReading = customerPost.MeterStartReading,
                    // map number 
                    // bill officer
                    HouseNo = customerPost.HouseNo,
                    ReaderName = customerPost.readerName,
                    sdPaid = customerPost.sdPaid,
                    custCategoryCode = customerPost.custCategoryCode,
                    ContractNo = customerPost.ContractNo,
                    OrdinaryNo = customerPost.OrdinaryNo,
                    meterno = customerPost.meterno,
                    MeterSizeCode = customerPost.MeterSizeCode,
                    InstallationDate = InstallationDate,
                    regMonthIndex = customerPost.regMonthIndex,
                    regFiscalYear = customerPost.regFiscalYear,
                    enterDate = DateTime.Now,

                    PaymentMode = customerPost.PaymentMode,
                    //MapNumber = customerPost.MapNumber,
                    //BillCycle= customerPost.BillCycle,
                    AccountNo= customerPost.AccountNo,
                    MeterDigit= customerPost.MeterDigit,
                    MeterType= customerPost.MeterType,
                    MeterCountryOrigin= customerPost.MeterCountryOrigin,
                    MeterModel= customerPost.MeterModel,
                   RegDate= DateTime.Now.ToString(),
                    
                    //Reason = customerPost.Reason

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
                          custCategoryCode = CustomerCategory.custCategoryCode,
                          MeterSizeCode = meterSizeCode.meterSizeCode,
                          meterno = cus.meterno,
                          Kebele = cus.Kebele,
                          Ketena = cus.Ketena,
                          ReaderName = cus.ReaderName,
                          sdPaid = cus.sdPaid,
                          Telephone = cus.Telephone,
                          Village = cus.Village,
                          MeterStartReading = cus.MeterStartReading,
                          HouseNo = cus.HouseNo,
                          OrdinaryNo = cus.OrdinaryNo,
                          InstallationDate = cus.InstallationDate,
                          PaymentMode = cus.PaymentMode,
                          //MapNumber = customerPost.MapNumber,
                          //BillCycle= customerPost.BillCycle,
                          AccountNo = cus.AccountNo,
                          MeterDigit = cus.MeterDigit,
                          MeterType = cus.MeterType,
                          MeterCountryOrigin = cus.MeterCountryOrigin,
                          MeterModel = cus.MeterModel,


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


                    //currentcustomer.custID = updateCustomer.custID;
                    currentcustomer.regFiscalYear = updateCustomer.regFiscalYear;
                    currentcustomer.regMonthIndex = updateCustomer.regMonthIndex;
                    currentcustomer.customerName = updateCustomer.customerName;
                    currentcustomer.Ketena = updateCustomer.Ketena;
                    currentcustomer.Kebele = updateCustomer.Kebele;
                    currentcustomer.HouseNo = updateCustomer.HouseNo;
                    currentcustomer.Village = updateCustomer.Village;
                    currentcustomer.Telephone = updateCustomer.Telephone;
                    //currentcustomer.Mobile = updateCustomer.Mobile;
                    //currentcustomer.BookNo = updateCustomer.BookNo;
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
                    //currentcustomer.WaterSource = updateCustomer.WaterSource;
                    //currentcustomer.MeterStatus = updateCustomer.MeterStatus;
                    //currentcustomer.RegDate = updateCustomer.RegDate;
                    currentcustomer.PaymentMode = updateCustomer.PaymentMode;
                    //currentcustomer.BankAccount = updateCustomer.BankAccount;
                    //currentcustomer.BillOfficerId = updateCustomer.BillOfficerId;


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

            if(customerLast.Count() == 0)
            {
                return 00000000;
            }
            List<int> contactNumbers = new List<int>();

            foreach(var c in customerLast)
            {
                contactNumbers.Add(Int32.Parse(c));
            }
            var contactNumber = contactNumbers.Max() + 1;

            return contactNumber;
        }

        public async Task<List<CustomerHomeDto>> GetCustomerHomeData()
        {

            var cusomers = await _dbCustomerContext.Customers.ToListAsync();

            var customerCategories = await _dbGeneralContext.CustomerCategories.ToListAsync();

            List<CustomerHomeDto> customerHomeDatas = new List<CustomerHomeDto>();

            foreach (var cc in customerCategories)
            {

                if (cc.custCategoryCode == "0") continue;
                var customerHomeData = new CustomerHomeDto
                {
                    CustomerCategory  = cc.custCategoryName,
                    ActiveCustomers = cusomers.Count(x=>x.MeterStatus == "ACTIVE" && x.custCategoryCode == cc.custCategoryCode ),
                    DisconnectedCustomers = cusomers.Count(x=>x.MeterStatus == "DISCONNECT" && x.custCategoryCode == cc.custCategoryCode  ),
                    TerminatedCustomers = cusomers.Count(x=>x.MeterStatus == "TERMINATED" && x.custCategoryCode == cc.custCategoryCode  )
                };

                customerHomeDatas.Add(customerHomeData);    

            }


            return customerHomeDatas;
        }

        public async Task<ResponseMessage> AssignBillOfficerToCustomer(CustomerToBillOfficerDto customerData)
        {
            
            try
            {
                var count = 0;
                foreach(var contractNo in customerData.CustomerContractNos)
                {

                    var customer = await _dbCustomerContext.Customers.Where(x => x.ContractNo == contractNo).FirstOrDefaultAsync();
                    if (customer!= null)
                    {
                        customer.BillOfficerId = customerData.BillOfficerId;
                        
                        await _dbCustomerContext.SaveChangesAsync();
                        count++;
                    }
                }

                return new ResponseMessage
                {
                    Success = true, 
                    Message  = $"{count} Customers tranferd to BillOfficerId {customerData.BillOfficerId} !!!"
                };


            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    Success = false,
                    Message = ex.Message
                };

            }
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
