using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IntegratedImplementation.Services.DWM
{
    public class DWMDashboardService : IDWMDashboardService
    {

        private readonly DBGeneralContext _generalContext;
        private readonly DBCustomerContext _customerContext;

        public DWMDashboardService(DBGeneralContext generalContext, DBCustomerContext customerContext)
        {

            _generalContext = generalContext;
            _customerContext = customerContext;

        }

        public async Task<DWMDashboardDto> GetDashbordDetail(int year, int month)
        {
            var dashboardDetail = new DWMDashboardDto();


            dashboardDetail.TotalCustomers = await _customerContext.Customers.CountAsync(x => x.MeterStatus == "Active");
            dashboardDetail.GpsEncoded = await _customerContext.Customers.CountAsync(x => x.MeterStatus == "Active" && x.MeterAltitude != 0 && x.MeterLongitude != 0);
            dashboardDetail.Pending = await _customerContext.Customers
            .Where(c => !_customerContext.BillMobileData.Any(m => m.custId == c.custID && m.monthIndex == month && m.fiscalYear == year))
            .Where(c => c.MeterStatus == "Active")
            .CountAsync();

            dashboardDetail.Readed = await _customerContext.BillMobileData.CountAsync(x => x.fiscalYear == year && x.monthIndex == month);

            dashboardDetail.ReadingTypeRatio = await _customerContext.BillMobileData
            .GroupBy(m => 1)
            .Select(g => new ReadingTypeRatioDto
            {
                Id = 1,
                AboveAVG = g.Sum(m => (m.readingCurrent - m.readingPrev) > (m.readingAvg * 1.5) ? 1 : 0),
                BelowAVG = g.Sum(m => (m.readingCurrent - m.readingPrev) < (m.readingAvg * 0.5) ? 1 : 0),
                Normal = g.Sum(m => (m.readingCurrent - m.readingPrev) >= (m.readingAvg * 0.5) && (m.readingCurrent - m.readingPrev) <= (m.readingAvg * 1.5) ? 1 : 0),
                ZeroReading = g.Sum(m => m.readingCurrent == m.readingPrev ? 1 : 0),
                ReasonOfCode = g.Sum(m => !string.IsNullOrEmpty(m.readingReasonCode) ? 1 : 0),
                TotalReading = g.Count()
            })
            .FirstOrDefaultAsync();

            dashboardDetail.AnnuallyConsumption = new List<AnnuallyConsumptionDto>();


            var billMobiled = _customerContext.BillMobileData.Where(m => m.fiscalYear == year).ToList();
            var monthIndexes = billMobiled.Select(m => m.monthIndex).Distinct().ToList();
            var months = _generalContext.FiscalMonths.Where(x => monthIndexes.Contains(x.monthIndex)).ToList();

            dashboardDetail.AnnuallyConsumption = billMobiled
                .GroupBy(m => new { m.monthIndex, m.fiscalYear })
                .Select(g => new AnnuallyConsumptionDto
                {
                    Consumption = g.Sum(m => m.readingCurrent - m.readingPrev),
                    Month_Name = months.FirstOrDefault(x => x.monthIndex == g.Key.monthIndex)?.monthnamelocal,
                    FiscalYear = g.Key.fiscalYear
                })
                .OrderBy(g => g.Month_Name)
                .ToList();


            return dashboardDetail;

        }

    

    }
}
