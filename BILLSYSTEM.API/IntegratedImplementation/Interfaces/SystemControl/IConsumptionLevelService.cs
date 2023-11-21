using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IConsumptionLevelService
    {
        Task<List<ConsumptionLevelDto>> GetConsumptionLevel();
        Task<ResponseMessage> AddConsumptionLevel(ConsumptionLevelDto addConsumptionLevel);
        Task<ResponseMessage> UpdateConsumptionLevel(ConsumptionLevelDto updateConsumptionLevel);
        Task<ResponseMessage> DeleteConsumptionLevel(int ConsumptionLevelId);
    }
}
