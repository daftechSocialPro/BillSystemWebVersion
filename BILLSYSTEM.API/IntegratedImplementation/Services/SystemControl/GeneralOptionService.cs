using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.SystemControl
{
    public class GeneralOptionService:IGeneralOptionsService
    {
        private readonly DBGeneralContext _dbContext;

        public GeneralOptionService(DBGeneralContext dbContext)
        {
            _dbContext=dbContext;
        }
        public async Task<GeneralOptions> GetGenralOptions()
        {

          
                var options = await _dbContext.GeneralOptions.FirstOrDefaultAsync();
                return options;
            
            
        }
    }
}
