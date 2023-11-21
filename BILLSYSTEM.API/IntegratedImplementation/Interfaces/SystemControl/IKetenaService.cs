using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IKetenaService
    {
        Task<List<KetenaDto>> GetKetena();
        Task<ResponseMessage> AddKetena(KetenaDto addKetena);

        Task<ResponseMessage> UpdateKetena(KetenaDto updateKetena);
        Task<ResponseMessage> DeleteKetena(int KetenaId);
    }
}
