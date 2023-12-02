using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using Implementation.Helper;
using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.DTOS.HRM;
using IntegratedImplementation.Interfaces.Configuration;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.DWM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.DWM
{
    public class MobileAppReadingService : IMobileAppReadingService
    {

        private readonly DBCustomerContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IGeneralConfigService _generalConfig;

        public MobileAppReadingService(DBCustomerContext dbContext, IMapper mapper, IGeneralConfigService generalConfig)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _generalConfig = generalConfig;
        }

        public async Task<int> GetMobileAppReadingsLength()
        {
            return await _dbContext.MobileAppReadings.CountAsync();
        }
        public async Task<List<MobileAppReadingDto>> GetMobileAppReading(int pageNumber, int pageSize)
        {
            var query = _dbContext.MobileAppReadings
       .ProjectTo<MobileAppReadingDto>(_mapper.ConfigurationProvider)
       .Skip((pageNumber - 1) * pageSize)
       .Take(pageSize);

            var MobileAppReadings = await query.ToListAsync();

         
            return MobileAppReadings;
        }
        public async Task<ResponseMessage> InsertMobileAppReading(string monthIndex, string year, string? Kebele)
        {
            try
            {
                var readings = await _dbContext.Set<BillToMobileView>()
            .FromSqlRaw("SELECT * FROM DB_CUSTOMER.dbo.billToMobile WHERE fiscalYear = {0} AND monthIndex = {1}", year, monthIndex)
            .ToListAsync();

                if (Kebele != null)
                {
                    readings = readings.Where(x => x.Kebele == Kebele).ToList();
                }

                var mobileAppReadings = _mapper.Map<List<MobileAppReading>>(readings);

                mobileAppReadings = mobileAppReadings
        .DistinctBy(x => x.custId)
        .ToList();

                _dbContext.MobileAppReadings.AddRange(mobileAppReadings);
                _dbContext.SaveChanges();

                return new ResponseMessage
                {

                    Message = $"{mobileAppReadings.Count()} Customers Generates for year {year} and month {monthIndex} Successfully!!!",
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


        public async Task<ResponseMessage> ClearScript()
        {

            string manual = "DELETE FROM MobileAppReading";
            int response = _dbContext.Database.ExecuteSqlRaw(manual);
            return new ResponseMessage { Success = true, Message = $"{response} Data has been Successfully Cleared from MobileAppReading" };
        }
    }
}
