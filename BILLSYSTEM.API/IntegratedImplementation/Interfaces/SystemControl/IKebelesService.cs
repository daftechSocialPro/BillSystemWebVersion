using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IKebelesService
    {

        Task<List<KebelesDto>> GetKebeles();


        Task<ResponseMessage> AddKebeles(KebelesDto addKebeles);
        Task<ResponseMessage> UpdateKebeles(KebelesDto updateKebeles);
        Task<ResponseMessage> DeleteKebeles(int KebelesId);

    }
}
