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
using static IntegratedInfrustructure.Data.EnumList;

namespace IntegratedImplementation.Services.SystemControl
{
    public class MeterSizeService : IMeterSizeService
    {

        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public MeterSizeService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper =mapper;
            _dbContext = dbContext;
        }


        public async Task<List<MeterSizeDto>> GetMeterSize()
        {
            var employeeHistories = await _dbContext.MeterSizes.AsNoTracking()
                                .ProjectTo<MeterSizeDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return employeeHistories;
        }
        public async Task<ResponseMessage> AddMeterSize(MeterSizeDto addMeterSize)
        {
            try
            {

                MeterSize meterSize = new MeterSize()
                {
                    recordno = addMeterSize.recordno,
                    meterSizeName = addMeterSize.meterSizeName,
                    meterSizeCode = addMeterSize.meterSizeCode,
                   
                };
                await _dbContext.MeterSizes.AddAsync(meterSize);
                await _dbContext.SaveChangesAsync();

                return new ResponseMessage
                {

                    Message = "Added Successfully",
                    Success = true
                };
            }
            catch (Exception ex) {
                return new ResponseMessage
                {

                    Message = ex.Message ,
                    Success = false
                };

            }


        }
        public async Task<ResponseMessage> UpdateMeterSize(MeterSizeDto updateMeterSize)
        {
            try
            {
                var currentMeterSize = await _dbContext.MeterSizes.FirstOrDefaultAsync(x => x.recordno.Equals(updateMeterSize.recordno));

                if (currentMeterSize != null)
                {

                    currentMeterSize.meterSizeName = updateMeterSize.meterSizeName;
                    currentMeterSize.meterSizeCode = updateMeterSize.meterSizeCode;



                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Meter Size" };
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
        public async Task<ResponseMessage> DeleteMeterSize(int MeterSizeId)
        {

            var currentMeterSize = await _dbContext.MeterSizes.FindAsync(MeterSizeId);

            if (currentMeterSize != null)
            {

                _dbContext.Remove(currentMeterSize);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Meter Size" };
        }

    }
}
