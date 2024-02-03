using Implementation.DTOS.Authentication;
using Implementation.Helper;
using IntegratedImplementation.DTOS.Configuration;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedInfrustructure.Model.SCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IUserSettingService
    {

        Task<List<UserServiceDto>> GetUserSettingList();
        Task<List<SelectListDto>> GetEmployeesforUserSettingUpdate();

        Task<ResponseMessage> CreateSystemUser(UserServicePostDto userPost);
        Task<ResponseMessage> UpdateSystemUser(UserServicePostDto userUpdate);
        Task<List<SelectListDto>> GetEmployeesforUserSetting();
        Task<ResponseMessage> DeleteSystemUser(int userId);


        Task<List<SelectListDto>> GetAppModules();
        Task<List<SelectListDto>> GetAppTabsByModule(string appModule);

        Task<List<UserPermission>> GetUserPermissions(string userId);

    }
}
