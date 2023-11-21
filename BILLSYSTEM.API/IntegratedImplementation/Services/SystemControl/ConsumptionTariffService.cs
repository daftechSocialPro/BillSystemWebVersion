using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.SystemControl
{
    public class ConsumptionTariffService : IConsumptionTariffService
    {


        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public ConsumptionTariffService(DBGeneralContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ConsumptionTariffDto>> GetConsumptionTariffs()
        {

            var results = await (from consumption in _dbContext.ConsumptionTariffs
                                 join customerCategory in _dbContext.CustomerCategories on consumption.custCategoryCode equals customerCategory.custCategoryCode
                                 join consLevel in _dbContext.ConsumptionLevels on consumption.consLevels equals consLevel.recordno.ToString()

                                 select new ConsumptionTariffDto
                                 {
                                     recordno = consumption.recordno,
                                     RateGroupCode = consumption.RateGroupCode,
                                     consRanges = consumption.consRanges,
                                     TariffRate = consumption.TariffRate,
                                     Consumption = consumption.Consumption,
                                     custCategoryCode = customerCategory.custCategoryName,
                                     consLevels=consLevel.levelNameLocal 

                                 }).ToListAsync();

     

            return results;
        }

        public async Task<ConsumptionTariffDto> GetConsumptionTariffForUpdate(int recordNo)
        {

            var result = await _dbContext.ConsumptionTariffs.Where(x => x.recordno == recordNo).AsNoTracking().ProjectTo<ConsumptionTariffDto>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();


            return result;
        }
        public async Task<ResponseMessage> AddConsumptionTariff(ConsumptionTariffDto addConsumptionTariff)
        {
            try
            {

                ConsumptionTariff meterSize = new ConsumptionTariff()
                {
                    recordno = addConsumptionTariff.recordno,
                    TariffRate = addConsumptionTariff.TariffRate,
                    Consumption = addConsumptionTariff.Consumption,
                    consRanges = addConsumptionTariff.consRanges,
                    RateGroupCode = addConsumptionTariff.RateGroupCode,
                    consLevels = addConsumptionTariff.consLevels,
                    custCategoryCode = addConsumptionTariff.custCategoryCode

                };
                await _dbContext.ConsumptionTariffs.AddAsync(meterSize);
                await _dbContext.SaveChangesAsync();

                return new ResponseMessage
                {

                    Message = "Added Successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = ex.Message,
                    Success = false
                };

            }


        }
        public async Task<ResponseMessage> UpdateConsumptionTariff(ConsumptionTariffDto updateConsumptionTariff)
        {
            try
            {
                var currentConsumptionTariff = await _dbContext.ConsumptionTariffs.FirstOrDefaultAsync(x => x.recordno.Equals(updateConsumptionTariff.recordno));

                if (currentConsumptionTariff != null)
                {
                    currentConsumptionTariff.recordno = updateConsumptionTariff.recordno;
                    currentConsumptionTariff.TariffRate = updateConsumptionTariff.TariffRate;
                    currentConsumptionTariff.Consumption = updateConsumptionTariff.Consumption; ;
                    currentConsumptionTariff.consRanges = updateConsumptionTariff.consRanges;
                    currentConsumptionTariff.RateGroupCode = updateConsumptionTariff.RateGroupCode;
                    currentConsumptionTariff.consLevels = updateConsumptionTariff.consLevels;
                    currentConsumptionTariff.custCategoryCode = updateConsumptionTariff.custCategoryCode;


                 
                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Consumption Tariff" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field code ",
                    Success = false
                };

            }

        }
        public async Task<ResponseMessage> DeleteConsumptionTariff(int ConsumptionTariffId)
        {

            var currentConsumptionTariff = await _dbContext.ConsumptionTariffs.FindAsync(ConsumptionTariffId);

            if (currentConsumptionTariff != null)
            {

                _dbContext.Remove(currentConsumptionTariff);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Consumption Tariff" };
        }
    }
}
