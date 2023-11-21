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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IntegratedImplementation.Services.SystemControl
{
    public class BillEmpDutiesService: IBillEmpDutiesService
    {   
        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;
        public BillEmpDutiesService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<List<BillEmpDutiesDto>> GetBillEmpDuties()
        {

            var billEmpDuties = await (from billDuty in _dbContext.BillEmpDuties
                                      join billsection in _dbContext.BillSections on billDuty.empID equals billsection.empID

                                      select new BillEmpDutiesDto
                                      {
                                          recordno = billDuty.recordno,
                                          empID = billDuty.empID,
                                          duties = billDuty.duties,
                                          name = billsection.name,

                                      }).ToListAsync();

            return billEmpDuties;
        }

        public async Task<BillEmpDutiesDto> GetBillEmpDutyForUpdate(int recordNo)
        {

            var result = await _dbContext.BillEmpDuties.Where(x => x.recordno == recordNo).AsNoTracking().ProjectTo<BillEmpDutiesDto>(_mapper.ConfigurationProvider)
                                .FirstOrDefaultAsync();


            return result;
        }
        public async Task<ResponseMessage> AddBillEmpDuties(BillEmpDutiesDto addBillEmpDuties)
        {
            try
            {

                BillEmpDuties billEmpDuties = new BillEmpDuties()
                {
                    recordno = this.generateUniqueNum(),
                    empID = addBillEmpDuties.empID,
                    duties = addBillEmpDuties.duties

                };
                await _dbContext.BillEmpDuties.AddAsync(billEmpDuties);
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

        public async Task<ResponseMessage> UpdateBillEmpDuties(BillEmpDutiesDto updateBillEmpDuties)
        {
            try
            {
                var currentBillEmpDuties = await _dbContext.BillEmpDuties.FirstOrDefaultAsync(x => x.recordno.Equals(updateBillEmpDuties.recordno));

                if (currentBillEmpDuties != null)
                {

                   // currentBillEmpDuties.empID = updateBillEmpDuties.empID;
                    currentBillEmpDuties.duties = updateBillEmpDuties.duties;



                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Employee Dutie" };
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
        public async Task<ResponseMessage> DeleteBillEmpDuties(int recordno)
        {

            var currentBillEmpDuties = await _dbContext.BillEmpDuties.FindAsync(recordno);

            if (currentBillEmpDuties != null)
            {

                _dbContext.Remove(currentBillEmpDuties);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Employee" };
        }

        public int generateUniqueNum()
        {

            int randomNumber;

            do
            {
                randomNumber = new Random().Next(100000, 999999);
            }
            while (_dbContext.BillEmpDuties.Any(x => x.recordno == randomNumber));

            return randomNumber;
        }
    }
}
