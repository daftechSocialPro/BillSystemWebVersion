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
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        private readonly IConfiguration _configuration;

        public MobileAppReadingService(DBCustomerContext dbContext, IMapper mapper, IGeneralConfigService generalConfig, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _generalConfig = generalConfig;
            _configuration = configuration;
        }

        public async Task<ReadingAverageCount> GetMobileAppReadingsLength()
        {

            var counts = new ReadingAverageCount
            {
                ReadingCount = await _dbContext.MobileAppReadings.CountAsync(),
                AverageNotCalculatedCount = await _dbContext.MobileAppReadings.CountAsync(x => !x.isAverageCaluclated)
            };

            return counts;
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
        public async Task<ResponseMessage> InsertMobileAppReading(string monthIndex, string year, string? Kebele, string? village)
        {
            try
            {
                // Set command timeout for the database context
                _dbContext.Database.SetCommandTimeout(0);

                // Build the query using parameterized SQL
                var query = _dbContext.Set<BillToMobileView>()
                    .FromSqlRaw("SELECT * FROM DB_CUSTOMER.dbo.billToMobile WHERE fiscalYear = {0} AND monthIndex = {1}", year, monthIndex);

                // Apply optional filters
                if (!string.IsNullOrEmpty(Kebele))
                {
                    query = query.Where(x => x.Kebele == Kebele);
                }
                if (!string.IsNullOrEmpty(village))
                {
                    query = query.Where(x => x.Village == village);
                }

                // Fetch data asynchronously
                var readings = await query.ToListAsync();

                // Map and deduplicate
                var mobileAppReadings = _mapper.Map<List<MobileAppReading>>(readings)
                    .GroupBy(x => x.custId)
                    .Select(g => g.First())
                    .ToList();



                // Add mobile app readings to the context and save changes
                _dbContext.MobileAppReadings.AddRange(mobileAppReadings);
                await _dbContext.SaveChangesAsync();

                // Return a response message
                return new ResponseMessage
                {
                    Message = $"{mobileAppReadings.Count} Customers Generated for year {year} and month {monthIndex} Successfully!!!",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                // Log exception details
                return new ResponseMessage
                {
                    Message = $"Error: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<float> GetAverageConsumptionAsync(DBCustomerContext dbContext, string custId, int? fiscalYear, int? monthIndex)
        {
            try
            {
                var avgCons = await dbContext.billGenerate
                    .Where(bg => bg.CustID == custId
                                 && (!fiscalYear.HasValue || bg.FiscalYear <= fiscalYear.Value)
                                 && (!monthIndex.HasValue || bg.MonthIndex <= monthIndex.Value)
                                 && bg.BillStatus != "VOID"
                                 && bg.ReadingCons > 0)
                    .AverageAsync(bg => (float?)bg.ReadingCons) ?? 0; // Handle null case by defaulting to 0

                return avgCons;
            }
            catch (Exception ex)
            {
                // Log exception details and handle errors as needed
                throw new Exception("Error fetching average consumption", ex);
            }
        }



        public async Task<ResponseMessage> ClearScript()
        {

            string manual = "DELETE FROM MobileAppReading";
            int response = _dbContext.Database.ExecuteSqlRaw(manual);
            return new ResponseMessage { Success = true, Message = $"{response} Data has been Successfully Cleared from MobileAppReading" };
        }

        public async Task<ResponseMessage> CalculateAverage()
        {

            try
            {

                var mobileAppreadings = await _dbContext.MobileAppReadings.Where(x => !x.isAverageCaluclated).ToListAsync();
                var count = 0;

                foreach (var reading in mobileAppreadings)
                {

                    var avgCons = await _dbContext.billGenerate
                   .Where(bg => bg.CustID == reading.custId
                                && (!reading.fiscalYear.HasValue || bg.FiscalYear <= reading.fiscalYear.Value)
                                && (!reading.monthindex.HasValue || bg.MonthIndex <= reading.monthindex.Value)
                                && bg.BillStatus != "VOID"
                                && bg.ReadingCons > 0)
                   .AverageAsync(bg => (float?)bg.ReadingCons) ?? 0;

                    reading.AvgReading = avgCons;
                    reading.isAverageCaluclated = true;


                    await _dbContext.SaveChangesAsync();

                    count++;

                }

                return new ResponseMessage
                {
                    Success = true,
                    Message = $"{count} avagerage calculated successfully."
                };



            }
            catch (Exception ex)
            {

                return new ResponseMessage
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
