using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedInfrustructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.SystemControl
{
    public class FiscalMonthService : IFiscalMonthService
    {

        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public FiscalMonthService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<List<FiscalMonthDto>> GetFiscalMonth()
        {
            var employeeHistories = await _dbContext.FiscalMonths.AsNoTracking()
                                .ProjectTo<FiscalMonthDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return employeeHistories;
        }

        public async Task<ResponseMessage> UpdateFiscalMonth(FiscalMonthDto updateFiscalMonth)
        {
            try
            {
                var currentFiscalMonth = await _dbContext.FiscalMonths.FirstOrDefaultAsync(x => x.monthCode.Equals(updateFiscalMonth.monthCode));

                if (currentFiscalMonth != null)
                {
                    currentFiscalMonth.monthIndex = updateFiscalMonth.monthIndex;
                    currentFiscalMonth.monthCode = updateFiscalMonth.monthCode;
                    currentFiscalMonth.monthnameEn = updateFiscalMonth.monthnameEn;
                    currentFiscalMonth.monthnamelocal = updateFiscalMonth.monthnamelocal;



                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Fiscal Month" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field Fiscal Month Index ",
                    Success = false
                };

            }

        }
        public async Task<ResponseMessage> DeleteFiscalMonth(int FiscalMonthId)
        {

            var currentFiscalMonth = await _dbContext.FiscalMonths.FindAsync(FiscalMonthId);

            if (currentFiscalMonth != null)
            {

                _dbContext.Remove(currentFiscalMonth);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Fiscal Month" };
        }
    }
}
