using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.Configuration;
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
    public class BillSectionService : IBillSectionService
    {
        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public BillSectionService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<List<BillSectionDto>> GetBillSections()
        {
            var billSections = await _dbContext.BillSections.AsNoTracking()
                                .ProjectTo<BillSectionDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return billSections;
        }
        public async Task<ResponseMessage> AddBillSection(BillSectionDto addBillSection)
        {
            try
            {

                BillSection billSection = new BillSection()
                {

                    empID = addBillSection.empID,
                    name = addBillSection.name,
                    gender=addBillSection.gender,
                    position =addBillSection.position


                };
                await _dbContext.BillSections.AddAsync(billSection);
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
      public async Task<ResponseMessage> DeleteBillSection(string empId)
        {

            var currentBillSection = await _dbContext.BillSections.FindAsync(empId);

            if (currentBillSection != null)
            {

                _dbContext.Remove(currentBillSection);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Bill Section" };
        }

        public async Task<List<BillSectionDto>> GetBillOfficerHavingNoDuty()
        {
            //var users = _userManager.Users.Select(x => x.EmployeeId).ToList();
            var billEmpDuties = await _dbContext.BillEmpDuties.ToListAsync();

            var billOfficer = await _dbContext.BillSections
               .Where(e => !billEmpDuties.Select(x => x.empID).Contains(e.empID))
                .ProjectTo<BillSectionDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return billOfficer;
        }
    }
}
