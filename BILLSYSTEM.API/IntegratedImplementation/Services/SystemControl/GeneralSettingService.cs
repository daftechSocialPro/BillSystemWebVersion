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
    public class GeneralSettingService:IGeneralSettingService
    {

        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public GeneralSettingService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<List<GeneralSettingDto>> GetGeneralSetting(string settingCategory)
        {
            var employeeHistories = await _dbContext.GeneralSettings.Where(x=>x.InputCategory.ToLower()== settingCategory).AsNoTracking()
                                .ProjectTo<GeneralSettingDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return employeeHistories;
        }
        public async Task<ResponseMessage> AddGeneralSetting(GeneralSettingDto addGeneralSetting)
        {
            try
            {

                GeneralSetting GeneralSetting = new GeneralSetting()
                {
                    recordno = Guid.NewGuid(),
                    InputValues = addGeneralSetting.InputValues,
                    InputCategory = addGeneralSetting.InputCategory,
         

                };
                await _dbContext.GeneralSettings.AddAsync(GeneralSetting);
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
                    Message = "Cannot insert duplicate key for Input Field Name ",
                    Success = false
                };
            }


        }
        public async Task<ResponseMessage> UpdateGeneralSetting(GeneralSettingDto  updateGeneralSetting)
        {
            try
            {
                var currentGeneralSetting = await _dbContext.GeneralSettings.FirstOrDefaultAsync(x => x.recordno.Equals(updateGeneralSetting.recordno));

                if (currentGeneralSetting != null)
                {


                    currentGeneralSetting.InputValues = updateGeneralSetting.InputValues;

                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find GeneralSetting" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field Name ",
                    Success = false
                };
            }

        }
        public async Task<ResponseMessage> DeleteGeneralSetting(Guid GeneralSettingId)
        {

            var currentGeneralSetting = await _dbContext.GeneralSettings.FindAsync(GeneralSettingId);

            if (currentGeneralSetting != null)
            {

                _dbContext.Remove(currentGeneralSetting);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find GeneralSetting" };
        }

    }
}
