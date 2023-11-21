using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public  interface IConsumptionTariffService
    {
        Task<List<ConsumptionTariffDto>> GetConsumptionTariffs();

        Task<ConsumptionTariffDto> GetConsumptionTariffForUpdate(int recordNo);
        Task<ResponseMessage> AddConsumptionTariff(ConsumptionTariffDto addConsumptionTariff);
        Task<ResponseMessage> UpdateConsumptionTariff(ConsumptionTariffDto updateConsumptionTariff);
        Task<ResponseMessage> DeleteConsumptionTariff(int ConsumptionTariffId);

    }
}
