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
    public class GeneralInterfaceService : IGeneralInterfaceService
    {

        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public GeneralInterfaceService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<List<GeneralInterfaceDto>> GetGeneralInterface(string ObjectCategory)
        {
            var employeeHistories = await _dbContext.GeneralInterfaces.Where(x => x.ObjectCategory.ToLower() == ObjectCategory.ToLower()).AsNoTracking()
                                            .ProjectTo<GeneralInterfaceDto>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return employeeHistories;
        }
        public async Task<ResponseMessage> AddGeneralInterface(GeneralInterfaceDto addGeneralInterface)
        {
            try
            {

                GeneralInterface generalInterface = new GeneralInterface()
                {
            
                    recordno = addGeneralInterface.recordno,
                    ObjectNameEN = addGeneralInterface.ObjectNameEN,
                    ObjectNameLocalen = addGeneralInterface.ObjectNameLocalen,
                    ObjectNameLocalam = addGeneralInterface.ObjectNameLocalam,
                    ObjectCategory = addGeneralInterface.ObjectCategory,


                };
                await _dbContext.GeneralInterfaces.AddAsync(generalInterface);
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
                    Message = "Cannot insert duplicate key for Input Field record no ",
                    Success = false
                };
            }


        }
        public async Task<ResponseMessage> UpdateGeneralInterface(GeneralInterfaceDto updateGeneralInterface)
        {
            try
            {
                var currentGeneralInterface = await _dbContext.GeneralInterfaces.FirstOrDefaultAsync(x => x.recordno.Equals(updateGeneralInterface.recordno));

                if (currentGeneralInterface != null)
                {
                                                         
                    currentGeneralInterface.ObjectNameLocalen = updateGeneralInterface.ObjectNameLocalen;
                    currentGeneralInterface.ObjectNameLocalam = updateGeneralInterface.ObjectNameLocalam;

                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = $"Unable To Find {currentGeneralInterface.ObjectCategory}" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate Input field recordNo ",
                    Success = false
                };
            }

        }
        public async Task<ResponseMessage> DeleteGeneralInterface(int GeneralInterfaceId)
        {

            var currentGeneralInterface = await _dbContext.GeneralInterfaces.FindAsync(GeneralInterfaceId);

            if (currentGeneralInterface != null)
            {

                _dbContext.Remove(currentGeneralInterface);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find General Interface" };
        }

    }
}
