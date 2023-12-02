using Implementation.Helper;
using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.DWM
{
    public interface IMobileUsersService
    {
        Task<List<MobileUsersDto>> GetMobileUsers();
        Task<ResponseMessage> AddMobileUsers(MobileUsersDto addMobileUsers);
        Task<ResponseMessage> UpdateMobileUsers(MobileUsersDto updateMobileUsers);
        Task<ResponseMessage> DeleteMobileUsers(int MobileUsersId);
    }
}
