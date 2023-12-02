﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntegratedImplementation.DTOS.CustomerService;
using IntegratedImplementation.DTOS.HRM;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.CustomerService;
using IntegratedInfrustructure.Data;
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
;
        }
    }
}
