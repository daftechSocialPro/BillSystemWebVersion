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
    public class PenalityRateService : IPenalityRateService
    {

        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public PenalityRateService(DBGeneralContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<PenalityRateDto>> GetPenalityRates()
        {
            var results = await (from penality in _dbContext.PenalityRates
                                 join customerCategory in _dbContext.CustomerCategories on penality.CustCategoryCode equals customerCategory.custCategoryCode
                                 join groupCode in _dbContext.MeterSizeRents on penality.PenalityGroupCode equals groupCode.RentGroupCode
                                 select new PenalityRateDto
                                 {
                                     recordno = penality.recordno,
                                     NoOfMonth = penality.NoOfMonth,
                                     PenalityAmount = penality.PenalityAmount,
                                     CustCategoryCode = customerCategory.custCategoryName,
                                     PenalityGroupCode = penality.PenalityGroupCode
                                 }).Distinct().ToListAsync();

            return results;
          
        }


        public async Task<PenalityRateDto> GetPenalityRateForUpdate(int recordNo)
        {


            var result = await _dbContext.PenalityRates.Where(x => x.recordno == recordNo).AsNoTracking().ProjectTo<PenalityRateDto>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();


            return result;
        }
        public async Task<ResponseMessage> AddPenalityRate(PenalityRateDto addPenalityRate)
        {
            try
            {

                PenalityRate Penality = new PenalityRate()
                {
                    recordno = addPenalityRate.recordno,
                    NoOfMonth = addPenalityRate.NoOfMonth,
                    PenalityAmount = addPenalityRate.PenalityAmount,
                    CustCategoryCode = addPenalityRate.CustCategoryCode,
                    PenalityGroupCode = addPenalityRate.PenalityGroupCode,

                };
                await _dbContext.PenalityRates.AddAsync(Penality);
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
        public async Task<ResponseMessage> UpdatePenalityRate(PenalityRateDto updatePenalityRate)
        {
            try
            {
                var currentPenalityRate = await _dbContext.PenalityRates.FirstOrDefaultAsync(x => x.recordno.Equals(updatePenalityRate.recordno));

                if (currentPenalityRate != null)
                   

                {
                    currentPenalityRate.recordno = updatePenalityRate.recordno;
                    currentPenalityRate.NoOfMonth = updatePenalityRate.NoOfMonth;
                    currentPenalityRate.PenalityAmount = updatePenalityRate.PenalityAmount; ;
                    currentPenalityRate.CustCategoryCode = updatePenalityRate.CustCategoryCode;
                    currentPenalityRate.PenalityGroupCode = updatePenalityRate.PenalityGroupCode;


                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Penality Rate" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field penality code ",
                    Success = false
                };

            }

        }
        public async Task<ResponseMessage> DeletePenalityRate(int PenalityRateId)
        {

            var currentPenalityRate = await _dbContext.PenalityRates.FindAsync(PenalityRateId);

            if (currentPenalityRate != null)
            {

                _dbContext.Remove(currentPenalityRate);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Penality Rate" };
        }
    }
}
