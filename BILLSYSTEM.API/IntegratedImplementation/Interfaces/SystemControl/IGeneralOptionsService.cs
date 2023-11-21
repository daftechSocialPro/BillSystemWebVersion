using IntegratedImplementation.DTOS.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IGeneralOptionsService
    {
        Task<GeneralOptions> GetGenralOptions();
    }
}
