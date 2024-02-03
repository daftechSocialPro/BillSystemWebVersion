using IntegratedImplementation.DTOS.DWM;
using IntegratedInfrustructure.Model.DWM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.DWM
{
    public interface IDWMMobileService
    {
        Task<List<ProfileResponseDto>> Login(IEnumerable<MobileUsers> readerCridential);
    }
}
