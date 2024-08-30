using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.DWM
{
    public class CustomerCollectedService : ICustomerCollectedService
    {
        private readonly DBCustomerContext _dbCustomerContext;
        public CustomerCollectedService(DBCustomerContext dbCustomerContext) {

            _dbCustomerContext = dbCustomerContext;
        }
        public async Task<IQueryable<CustomerCollectedDto>> GetBillMobileData(string contractNo)
        {
            var query = from b in _dbCustomerContext.BillMobileData
                        join u in _dbCustomerContext.MobileUsers on b.readingBY equals u.userName
                        join c in _dbCustomerContext.Customers on b.custId equals c.custID
                        where c.ContractNo == contractNo
                        select new CustomerCollectedDto
                        {
                            CustomerName = b.customerName,
                            MeterNo = b.meterno,
                            CustId = b.custId,
                            ReadingPrev = b.readingPrev,
                            ReadingCurrent = b.readingCurrent,
                            ReadingAvg =  b.readingAvg,
                            ReadingImage = b.Reading_Image,
                            Consumption = b.readingCurrent - b.readingPrev,
                            ReadingReasonCode = b.readingReasonCode,
                            FullName = u.fullName,
                            UserName = u.userName,
                            EntryDT = b.EntryDT,
                            ReadingDT = DateTime.Parse(b.readingDT),
                            Latitude = b.xCoord / 1000000.0,
                            Longitude = b.yCoord / 1000000.0,
                            ContractNo = c.ContractNo
                        };

            return query;
        }
        public async Task<List<CustomerCollectedDto>> GetBillMobileDataByEntryDate(string entryDate , string userName)
        {
            var query = (from b in _dbCustomerContext.BillMobileData
                         join u in _dbCustomerContext.MobileUsers on b.readingBY equals u.userName
                         join c in _dbCustomerContext.Customers on b.custId equals c.custID
                         where c.ReaderName == userName
                         select new CustomerCollectedDto
                         {
                             CustomerName = b.customerName,
                             MeterNo = b.meterno,
                             CustId = b.custId,
                             ReadingPrev = (double)b.readingPrev,
                             ReadingCurrent = (double)b.readingCurrent,
                             ReadingAvg = (double)b.readingAvg,
                             Consumption = (double)(b.readingCurrent - b.readingPrev),
                             ReadingImage = b.Reading_Image,
                             ReadingReasonCode = b.readingReasonCode,
                             FullName = u.fullName,
                             UserName = u.userName,
                             EntryDT = b.EntryDT,
                             ReadingDT = DateTime.Parse(b.readingDT),
                             Latitude = b.xCoord != null ? b.xCoord / 1000000.0 : 0.0,
                             Longitude = b.yCoord != null ? b.yCoord / 1000000.0 : 0.0,
                             ContractNo = c.ContractNo
                         });

            if (entryDate != null && entryDate != "undefined")
            {
                
                query = query.Where(u => u.EntryDT.Equals(DateTime.Parse(entryDate)));
            }

            // Force immediate execution of the query
            var result = query.ToList();

            return result;
        }
    }
}
