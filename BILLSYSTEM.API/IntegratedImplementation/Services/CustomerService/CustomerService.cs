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
using IntegratedInfrustructure.Model.CSS;

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
                          meterno = cus.meterno

                      }).ToList();





            return customerResults;
            ;
        }


        public async Task<ResponseMessage> AddCustomer(CustomerPostDto customerPost)
        {
            try
            {

                Customer customers = new Customer()
                {
                    custID  = "90000002905",
                    customerName = customerPost.FullName,
                    Telephone = customerPost.PhoneNumber,
                    Ketena = customerPost.Ketena,
                    Kebele = customerPost.Kebele,
                    Village = customerPost.Village,
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
                    InstallationDate = customerPost.InstallationDate,
                    regMonthIndex =customerPost.MonthIndex,
                    regFiscalYear =customerPost.FiscalYear

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
            var currentCustomer =  await _dbCustomerContext.Customers.FirstAsync(x=>x.ContractNo==contractNo);

            if (currentCustomer != null)
            {

                _dbCustomerContext.Remove(currentCustomer);
                await _dbCustomerContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Customer" };


        }

        public async  Task<CustomerGetDto> GetSingleCustomer(string contractNo)
        {

            var customers =
                await _dbCustomerContext.Customers.Where(x=>x.ContractNo==contractNo).AsNoTracking()
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
                          meterno = cus.meterno

                      }).FirstOrDefault();





            return customerResults;
            ;

        }

        



    }
}
