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
    public class AccountPeriodService : IAccountPeriodService
    {
        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public AccountPeriodService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<AccountPeriodDto> GetAccountPeriod()
        {
            var accountPeriods = await _dbContext.AccountPeriods.AsNoTracking()
                                .ProjectTo<AccountPeriodDto>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();
            return accountPeriods;
        }


        public async Task<ResponseMessage> UpdateAccountPeriod(AccountPeriodDto updateAccountPeriod)
        {
            try
            {
                var currentAccountPeriod = await _dbContext.AccountPeriods.FirstOrDefaultAsync(x => x.UserID.Equals(updateAccountPeriod.UserID));

                if (currentAccountPeriod != null)
                {

                    currentAccountPeriod.MonthIndex = updateAccountPeriod.MonthIndex;
                    currentAccountPeriod.FiscalYear = updateAccountPeriod.FiscalYear;



                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Account Period" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field Account Period ",
                    Success = false
                };

            }

        }




     }

 }

