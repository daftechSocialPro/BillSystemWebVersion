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
    public class ChangeActionService : IChangeActionService
    {
        private readonly DBCustomerContext _dbCustomerContext;
        private readonly DBGeneralContext _dbGeneralContext;

        private readonly IMapper _mapper;
        private object _dbContext;

        public ChangeActionService(DBCustomerContext dbCustomerContext, DBGeneralContext dbGeneralContext
           , IMapper mapper)
        {
            _dbCustomerContext = dbCustomerContext;
            _dbGeneralContext = dbGeneralContext;
            _mapper = mapper;
        }
        public async Task<List<ChangeActionGetDto>> GetCustomerStatus()
              
        {
            var changeAction = await _dbCustomerContext.ChangeActions.AsNoTracking()
                                    .ProjectTo<ChangeActionGetDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            var Results = (
                from action in changeAction
                join CustomerCategory in _dbGeneralContext.CustomerCategories on action.custCategoryCode equals CustomerCategory.custCategoryCode
                join meterSizeCode in _dbGeneralContext.MeterSizes on action.MeterSizeCode equals meterSizeCode.meterSizeCode
                select new ChangeActionGetDto
                {
                    customerName = action.customerName,
                    Kebele = action.Kebele,
                    meterno = action.meterno,
                    MeterSizeCode = meterSizeCode.meterSizeName,
                    custCategoryCode = CustomerCategory.custCategoryName,
                    ContractNo = action.ContractNo,
                    OrdinaryNo = action.OrdinaryNo,
                })
                .ToList();

            return Results;
        }

        //    public async Task<List<ChangeActionGetDto>> GetCustomerStatus()
        //    {

        //        var customerResults = await 
        //                 ( from action in _dbCustomerContext.ChangeActions
        //                  join CustomerCategory in _dbGeneralContext.CustomerCategories on action.custCategoryCode equals CustomerCategory.custCategoryCode
        //                  join meterSizeCode in _dbGeneralContext.MeterSizes on action.MeterSizeCode equals meterSizeCode.meterSizeCode


        //                  select new ChangeActionGetDto
        //                  {
        //                      customerName = action.customerName,
        //                      Kebele = action.Kebele,
        //                      meterno = action.meterno,
        //                      MeterSizeCode = meterSizeCode.meterSizeName,
        //                      custCategoryCode = CustomerCategory.custCategoryName,
        //                      ContractNo = action.ContractNo,
        //                      OrdinaryNo = action.OrdinaryNo,

        //                  


        //    }
        //
        }
    }
