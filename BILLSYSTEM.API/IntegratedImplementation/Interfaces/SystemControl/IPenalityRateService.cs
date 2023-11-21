using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IPenalityRateService
    {
        Task<List<PenalityRateDto>> GetPenalityRates();

        Task<PenalityRateDto> GetPenalityRateForUpdate(int recordNo);
        Task<ResponseMessage> AddPenalityRate(PenalityRateDto addPenalityRate);
        Task<ResponseMessage> UpdatePenalityRate(PenalityRateDto updatePenalityRate);
        Task<ResponseMessage> DeletePenalityRate(int PenalityRateId);

    }
}
