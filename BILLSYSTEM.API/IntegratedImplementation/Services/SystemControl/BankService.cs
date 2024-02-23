using IntegratedImplementation.DTOS.Configuration;
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
    public class BankService : IBankService
    {

        private readonly DBGeneralContext _generalContext;
        public BankService(DBGeneralContext generalContext) {
            _generalContext = generalContext;

        }
        public async Task<List<SelectListDto>> GetBanks()
        {
            return await _generalContext.Banks.Select(x => new SelectListDto
            {
                Name = x.BankName,
                EmpId = x.BankCode,
            }).ToListAsync();
        }
    }
}
