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
    public class MeterSizeRentService : IMeterSizeRentService
    {

        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public MeterSizeRentService(DBGeneralContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<MeterSizeRentDto>> GetMeterSizeRents()
        {

            var results = await (from meterRent in _dbContext.MeterSizeRents
                                 join customerCategory in _dbContext.CustomerCategories on meterRent.custCategoryCode equals customerCategory.custCategoryCode
                                 join meterSizeCode in _dbContext.MeterSizes on meterRent.MeterSizeCode equals meterSizeCode.meterSizeCode

                                 select new MeterSizeRentDto
                                 {
                                     recordno = meterRent.recordno,
                                     MeterRent = meterRent.MeterRent,
                                     RentGroupCode = meterRent.RentGroupCode,
                                     custCategoryCode = customerCategory.custCategoryName,
                                     MeterSize = meterSizeCode.meterSizeName,
                                     MeterSizeCode = meterSizeCode.meterSizeCode,

                                 }).ToListAsync();

            return results;
        }

        public async Task<MeterSizeRentDto> GetMeterSizeRentForUpdate(int recordNo)
        {

            var result = await _dbContext.MeterSizeRents.Where(x=>x.recordno==recordNo).AsNoTracking().ProjectTo<MeterSizeRentDto>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();
            

            return result;
        }
        public async Task<ResponseMessage> AddMeterSizeRent(MeterSizeRentDto addMeterSizeRent)
        {
            try
            {

                MeterSizeRent meterSize = new MeterSizeRent()
                {
                    recordno = addMeterSizeRent.recordno,
                    RentGroupCode = addMeterSizeRent.RentGroupCode,
                    MeterSizeCode = addMeterSizeRent.MeterSizeCode,
                    MeterRent = addMeterSizeRent.MeterRent,
                    custCategoryCode = addMeterSizeRent.custCategoryCode,

                };
                await _dbContext.MeterSizeRents.AddAsync(meterSize);
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
        public async Task<ResponseMessage> UpdateMeterSizeRent(MeterSizeRentDto updateMeterSizeRent)
        {
            try
            {
                var currentMeterSizeRent = await _dbContext.MeterSizeRents.FirstOrDefaultAsync(x => x.recordno.Equals(updateMeterSizeRent.recordno));

                if (currentMeterSizeRent != null)
                {
                    currentMeterSizeRent.recordno = updateMeterSizeRent.recordno;
                    currentMeterSizeRent.RentGroupCode = updateMeterSizeRent.RentGroupCode;
                    currentMeterSizeRent.MeterSizeCode = updateMeterSizeRent.MeterSizeCode; ;
                    currentMeterSizeRent.MeterRent = updateMeterSizeRent.MeterRent;
                    currentMeterSizeRent.custCategoryCode = updateMeterSizeRent.custCategoryCode;


                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find meter rent" };
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
        public async Task<ResponseMessage> DeleteMeterSizeRent(int MeterSizeRentId)
        {

            var currentMeterSizeRent = await _dbContext.MeterSizeRents.FindAsync(MeterSizeRentId);

            if (currentMeterSizeRent != null)
            {

                _dbContext.Remove(currentMeterSizeRent);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find meter size rent" };
        }
    }
    }