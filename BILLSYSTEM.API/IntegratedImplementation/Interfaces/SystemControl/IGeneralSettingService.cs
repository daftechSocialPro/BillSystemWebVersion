using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IGeneralSettingService
    {

        Task<List<GeneralSettingDto>> GetGeneralSetting(string settingCategory);
        Task<ResponseMessage> AddGeneralSetting(GeneralSettingDto addGeneralSetting);
        Task<ResponseMessage> UpdateGeneralSetting(GeneralSettingDto updateGeneralSetting);
        Task<ResponseMessage> DeleteGeneralSetting(Guid GeneralSettingId);
    }
}
