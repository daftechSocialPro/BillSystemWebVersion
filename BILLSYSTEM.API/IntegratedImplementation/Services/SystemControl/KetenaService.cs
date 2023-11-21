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
    public class KetenaService : IKetenaService
    {
        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public KetenaService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<List<KetenaDto>> GetKetena()
        {
            var employeeHistories = await _dbContext.Ketenas.AsNoTracking()
                                .ProjectTo<KetenaDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return employeeHistories;
        }
        public async Task<ResponseMessage> AddKetena(KetenaDto addKetena)
        {
            try
            {

                Ketena Ketena = new Ketena()
                {
                   
                    ketenaName = addKetena.ketenaName,
                    ketenaCode = addKetena.ketenaCode,

                };
                await _dbContext.Ketenas.AddAsync(Ketena);
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
        public async Task<ResponseMessage> UpdateKetena(KetenaDto updateKetena)
        {
            try
            {
                var currentKetena = await _dbContext.Ketenas.FirstOrDefaultAsync(x => x.ketenaCode.Equals(updateKetena.ketenaCode));

                if (currentKetena != null)
                {

                    currentKetena.ketenaName = updateKetena.ketenaName;
                    currentKetena.ketenaCode = updateKetena.ketenaCode;



                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Ketena" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field ketena ",
                    Success = false
                };

            }

        }
        public async Task<ResponseMessage> DeleteKetena(int KetenaId)
        {

            var currentKetena = await _dbContext.Ketenas.FindAsync(KetenaId);

            if (currentKetena != null)
            {

                _dbContext.Remove(currentKetena);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Ketena" };
        }


    }

}

