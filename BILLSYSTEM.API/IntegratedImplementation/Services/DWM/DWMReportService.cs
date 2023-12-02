using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.DWM;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.DWM
{
    public class DWMReportService : IDWMReportService
    {
        private readonly DBGeneralContext _generalContext;
        private readonly DBCustomerContext _customerContext;

        public DWMReportService(DBGeneralContext generalContext, DBCustomerContext customerContext)
        {

            _generalContext = generalContext;
            _customerContext = customerContext;

        }

        public async Task<IQueryable<DWMReadingLogReportDto>> GetReadingLogReport(int monthIndex, int fiscalYear)
        {
            var query = from billMobileData in _customerContext.BillMobileData
                        join dwmUser in _customerContext.MobileUsers on billMobileData.readingBY equals dwmUser.userName
                        where billMobileData.monthIndex == monthIndex && billMobileData.fiscalYear == fiscalYear
                        select new DWMReadingLogReportDto
                        {

                            ReaderName = dwmUser.fullName,
                            UserName = dwmUser.userName,
                            CustomerName = billMobileData.customerName,
                            MeterNo = billMobileData.meterno,
                            Previous = billMobileData.readingPrev,
                            Current = billMobileData.readingCurrent,
                            Consumption = billMobileData.readingCons,
                            Average = billMobileData.readingAvg,
                            MonthIndex = billMobileData.monthIndex,
                            FiscalYear = billMobileData.fiscalYear


                        };

            return query;
        }

        public async Task<List<DWMPendingLogReportDto>> GetPendingLogReport(int fiscalYear, int monthIndex)
        {
            var result = await (from customers in _customerContext.Customers
                                join dwmUser in _customerContext.MobileUsers on customers.ReaderName equals dwmUser.userName
                                where customers.MeterStatus == "Active" &&
                                          !_customerContext.BillMobileData.Any(b => b.custId == customers.custID && b.monthIndex == monthIndex && b.fiscalYear == fiscalYear)

                                select new DWMPendingLogReportDto
                                {

                                    ReaderName = dwmUser.fullName,
                                    CustomerName = customers.customerName,
                                    ContractNo = customers.ContractNo,
                                    UserName = dwmUser.userName,
                                    MeterNo = customers.meterno

                                }
                ).ToListAsync();


            return result;
        }

        public async Task<List<DWMReadingAccuracyReportDto>> GetReadingAccuracyReport(int fiscalYear, int monthIndex)
        {
            var result = await (from dwmUsers in _customerContext.MobileUsers
                                join customers in _customerContext.BillMobileData on dwmUsers.userName equals customers.readingBY
                                where customers.readingBY == dwmUsers.userName && customers.fiscalYear == fiscalYear && customers.monthIndex == monthIndex
                                group customers by new { dwmUsers.userName, dwmUsers.fullName } into g
                                select new DWMReadingAccuracyReportDto
                                {
                                    UserName = g.Key.userName,
                                    ReaderName = g.Key.fullName,

                                    AboveAVG = _customerContext.BillMobileData
                         .Count(b => b.readingBY == g.Key.userName && b.fiscalYear == fiscalYear && b.monthIndex == monthIndex && (b.readingCurrent - b.readingPrev) > (b.readingAvg * 1.5)),
                                    BelowAVG = _customerContext.BillMobileData
                         .Count(b => b.readingBY == g.Key.userName && b.fiscalYear == fiscalYear && b.monthIndex == monthIndex && (b.readingCurrent - b.readingPrev) < (b.readingAvg * 0.5)),
                                    Normal = _customerContext.BillMobileData
                         .Count(b => b.readingBY == g.Key.userName && b.fiscalYear == fiscalYear && b.monthIndex == monthIndex && (b.readingCurrent - b.readingPrev) >= (b.readingAvg * 0.5) && (b.readingCurrent - b.readingPrev) <= (b.readingAvg * 1.5))

                                }).ToListAsync();

            return result;
        }

        public async Task<List<DWMReadingEfficencyReportDto>> GetReadingEfficencyReport(int fiscalYear, int monthIndex)
        {
            var query = from user in _customerContext.MobileUsers
                        join data in _customerContext.BillMobileData on user.userName equals data.readingBY into gj
                        from subData in gj.DefaultIfEmpty()
                        where subData != null && subData.monthIndex == monthIndex && subData.fiscalYear == fiscalYear
                        group new { user, subData } by new { user.userName, user.fullName, subData.monthIndex, subData.fiscalYear } into g
                        select new DWMReadingEfficencyReportDto
                        {
                            TotalCustomers = _customerContext.Customers.Count(x => x.MeterStatus == "Active" && x.ReaderName == g.Key.userName),
                            Readed = _customerContext.BillMobileData.Count(x => x.monthIndex == g.Key.monthIndex && x.fiscalYear == g.Key.fiscalYear && x.readingBY == g.Key.userName),
                            UserName = g.Key.userName,
                            ReaderName = g.Key.fullName,

                        };

            return query.ToList();
        }


        public async Task<List<DWMReadingConsumptionReportDto>> GetReadingConsumptionReport(int fiscalYear, int monthIndex)
        {
            var results = _customerContext.MobileUsers
  .Select(u => new DWMReadingConsumptionReportDto
  {
      Consumption = _customerContext.BillMobileData.Where(x => x.readingBY == u.userName && x.fiscalYear == fiscalYear && x.monthIndex == monthIndex).Sum(x => x.readingCurrent - x.readingPrev),
      ReaderName = u.fullName,
      UserName = u.userName,

  })
  .OrderByDescending(u => u.Consumption)
  .ToList();

            return results;
        }


    }
}
