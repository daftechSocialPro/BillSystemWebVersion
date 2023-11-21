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
    public class ConsumptionLevelService:IConsumptionLevelService
    {


        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public ConsumptionLevelService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<List<ConsumptionLevelDto>> GetConsumptionLevel()
        {
            var employeeHistories = await _dbContext.ConsumptionLevels.AsNoTracking()
                                .ProjectTo<ConsumptionLevelDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return employeeHistories;
        }
        public async Task<ResponseMessage> AddConsumptionLevel(ConsumptionLevelDto addConsumptionLevel)
        {
            try
            {

                ConsumptionLevel consumptionLevel = new ConsumptionLevel()
                {
                    recordno = addConsumptionLevel.recordno,
                    levelNameEN = addConsumptionLevel.levelNameEN,
                    levelNameLocal = addConsumptionLevel.levelNameLocal,



                };
                await _dbContext.ConsumptionLevels.AddAsync(consumptionLevel);
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

                    Message = "Cannot insert duplicate key for Input Field Consumption level ",
                    Success = false
                };

            }


        }

        public async Task<ResponseMessage> UpdateConsumptionLevel(ConsumptionLevelDto updateConsumptionLevel)
        {
            try
            {
                var currentConsumptionLevel = await _dbContext.ConsumptionLevels.FirstOrDefaultAsync(x => x.recordno.Equals(updateConsumptionLevel.recordno));

                if (currentConsumptionLevel != null)
                {

                    currentConsumptionLevel.levelNameLocal = updateConsumptionLevel.levelNameLocal;
                    currentConsumptionLevel.levelNameEN = updateConsumptionLevel.levelNameEN;



                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Customer Category" };
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
        public async Task<ResponseMessage> DeleteConsumptionLevel(int ConsumptionLevelId)
        {

            var currentConsumptionLevel = await _dbContext.ConsumptionLevels.FindAsync(ConsumptionLevelId);

            if (currentConsumptionLevel != null)
            {

                _dbContext.Remove(currentConsumptionLevel);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Customer Category" };
        }
    }
}
