using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.CSS;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
