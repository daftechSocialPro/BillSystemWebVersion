using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.CSS;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace IntegratedImplementation.Services.CustomerService
{
    public class CustomerMeterStatusService : ICustomerMeterStatusService
    {
        private readonly DBCustomerContext _dbCustomerContext;
        private readonly DBGeneralContext _dbGeneralContext;
        private readonly IMapper _mapper;


        public CustomerMeterStatusService(DBCustomerContext dbCustomerContext, DBGeneralContext dbGeneralContext
           , IMapper mapper)
        {
            _dbCustomerContext = dbCustomerContext;
            _dbGeneralContext = dbGeneralContext;
            _mapper = mapper;
        }

        public async Task<ResponseMessage> ChangeCustomerStatus(CustomerMeterStatusPostDto customerStatus)
        {
            try
            {
         
                var meterStatus = new CustomerMeterStatus()
                {
                    custID = customerStatus.CustId,
                    recordno = Guid.NewGuid(),
                    enterDate = DateTime.Now,
                    disDate = customerStatus.DisDate.ToString(),
                    reason = customerStatus.Reason,
                    monthIndex = customerStatus.MonthIndex,
                    FiscalYear = customerStatus.FiscalYear,
                    typeOfAction = customerStatus.TypeOfAction,
                    enterBy =customerStatus.enterBy


                };

                await _dbCustomerContext.CustomerMeterStatus.AddAsync(meterStatus);
                await _dbCustomerContext.SaveChangesAsync();

                return new ResponseMessage()
                {
                    Success=true,
                    Message=""
                    
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

        public async Task<List<CustomerMeterStatusGetDto>> GetCustomerStatus(string CustId)
        {

            var customerStatus = await _dbCustomerContext.CustomerMeterStatus.Where(x => x.custID == CustId).
                Select(x => new CustomerMeterStatusGetDto

                {

                    FiscalYear = x.FiscalYear,
                    monthIndex = x.monthIndex,
                    typeOfAction = x.typeOfAction,
                    reason = x.reason,
                    enterDate = x.enterDate


                }).OrderByDescending(x=>x.enterDate).ToListAsync();
            return customerStatus;
        }
    }
}
