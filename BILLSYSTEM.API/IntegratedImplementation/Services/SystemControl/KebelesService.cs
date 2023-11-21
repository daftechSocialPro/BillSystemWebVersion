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
    public class KebelesService : IKebelesService
    {

        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public KebelesService(DBGeneralContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<KebelesDto>> GetKebeles()
        {

            var results = await _dbContext.Kebeless.AsNoTracking().ProjectTo<KebelesDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync();

            return results;
        }

        

        public async Task<ResponseMessage> AddKebeles(KebelesDto addKebeles)
        {
            try
            {

                Kebeles kebele = new Kebeles()
                {
                    kebeleCode = addKebeles.kebeleCode,
                    kebeleName = addKebeles.kebeleName,
                    ketenaCode = addKebeles.ketenaCode,

                };
                await _dbContext.Kebeless.AddAsync(kebele);
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
        public async Task<ResponseMessage> UpdateKebeles(KebelesDto updateKebeles)
        {
            try
            {
                var currentKebeles = await _dbContext.Kebeless.FirstOrDefaultAsync(x => x.kebeleCode.Equals(updateKebeles.kebeleCode));

                if (currentKebeles != null)
                {
                    currentKebeles.kebeleName = updateKebeles.kebeleName;
                    currentKebeles.kebeleCode = updateKebeles.kebeleCode; ;
                    currentKebeles.ketenaCode = updateKebeles.ketenaCode;


                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Kebeles" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field Kebele Code ",
                    Success = false
                };

            }

        }
        public async Task<ResponseMessage> DeleteKebeles(int KebelesId)
        {

            var currentKebeles = await _dbContext.Kebeless.FindAsync(KebelesId);

            if (currentKebeles != null)
            {

                _dbContext.Remove(currentKebeles);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Kebele" };
        }
    }
}


    

