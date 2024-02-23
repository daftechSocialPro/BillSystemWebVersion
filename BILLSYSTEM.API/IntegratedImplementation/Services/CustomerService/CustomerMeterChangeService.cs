using AutoMapper;
using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.CSS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.CustomerService
{
    public class CustomerMeterChangeService : ICustomerMeterChangeService
    {
        private readonly DBCustomerContext _dbCustomerContext;
        private readonly DBGeneralContext _dbGeneralContext;
        private readonly IMapper _mapper;


        public CustomerMeterChangeService(DBCustomerContext dbCustomerContext, DBGeneralContext dbGeneralContext
           , IMapper mapper)
        {
            _dbCustomerContext = dbCustomerContext;
            _dbGeneralContext = dbGeneralContext;
            _mapper = mapper;
        }
        public async Task<ResponseMessage> ChangeMeter(CustomerMeterChangePostDto meterChange)
        {
            try
            {
                var customerMeter = new CustomerMeterChange()
                {
                    recordno = Guid.NewGuid(),
                    enterDate = DateTime.Now,
                    curInstallationDate = DateTime.Now,
                    curMeterDigit = meterChange.curMeterDigit,
                    curMeterModel = meterChange.curMeterModel,
                    curMeterNo = meterChange.curMeterNo,
                    curMeterOrigin = meterChange.curMeterOrigin,
                    curMeterSizeCode = meterChange.curMeterSizeCode,
                    curMeterType = meterChange.curMeterType,
                    curStartReading = meterChange.curStartReading,
                    FiscalYear = meterChange.FiscalYear,
                    monthIndex = meterChange.monthIndex,
                    reason = meterChange.reason,
                    custID = meterChange.custID,


                };

                await _dbCustomerContext.CustomerMeterChange.AddAsync(customerMeter);
                await _dbCustomerContext.SaveChangesAsync();

                var custCustomer = await _dbCustomerContext.Customers.Where(x => x.custID == meterChange.custID).FirstOrDefaultAsync();
                if(custCustomer != null)
                {
                    custCustomer.MeterDigit= meterChange.curMeterDigit;

                    custCustomer.MeterModel = meterChange.curMeterModel;
                    custCustomer.meterno = meterChange.curMeterNo;
                    custCustomer.MeterCountryOrigin = meterChange.curMeterOrigin;
                    custCustomer.MeterType = meterChange.curMeterType;
                    custCustomer.MeterStartReading = meterChange.curStartReading;
                    custCustomer.MeterSizeCode = meterChange.curMeterSizeCode;

                }

                return new ResponseMessage()
                {
                    Success = true,
                    Message = ""

                };

            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    Success = false,
                    Message = ""

                };

            }
        }

        public async Task<List<CustomerMeterChangeGetDto>> GetCustomerMeter(string CustId)
        {

            var meterChange = await _dbCustomerContext.CustomerMeterChange.Where(x => x.custID == CustId).
                Select(x => new CustomerMeterChangeGetDto

                {

                    FiscalYear = x.FiscalYear,
                    monthIndex = x.monthIndex,
                    reason = x.reason,
                    enterDate = x.enterDate


                }).OrderByDescending(x => x.enterDate).ToListAsync();
            return meterChange;
        }
    }
}
