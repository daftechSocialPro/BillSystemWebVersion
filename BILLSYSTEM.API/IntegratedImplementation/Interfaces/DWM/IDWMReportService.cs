using IntegratedImplementation.DTOS.DWM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.DWM
{
    public interface IDWMReportService
    {

        Task<IQueryable<DWMReadingLogReportDto>> GetReadingLogReport (int monthIndex, int FiscalYear);
        Task<List<DWMPendingLogReportDto>> GetPendingLogReport(int fiscalYear, int monthIndex);
        Task<List<DWMReadingAccuracyReportDto>> GetReadingAccuracyReport(int fiscalYear, int monthIndex);

        Task<List<DWMReadingEfficencyReportDto>> GetReadingEfficencyReport(int fiscalYear, int monthIndex);
        Task<List<DWMReadingConsumptionReportDto>> GetReadingConsumptionReport(int fiscalYear, int monthIndex);
    }
}
