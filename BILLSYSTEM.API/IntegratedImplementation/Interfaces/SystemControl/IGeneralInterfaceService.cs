using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IGeneralInterfaceService
    {

        Task<List<GeneralInterfaceDto>> GetGeneralInterface(string ObjectCategory);
        Task<ResponseMessage> AddGeneralInterface(GeneralInterfaceDto addGeneralInterface);
        Task<ResponseMessage> UpdateGeneralInterface(GeneralInterfaceDto updateGeneralInterface);
        Task<ResponseMessage> DeleteGeneralInterface(int GeneralInterfaceId);

    }
}
