using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.Configuration;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.CSS;
using IntegratedInfrustructure.Model.DWM;
using Microsoft.Azure.Management.Graph.RBAC.Fluent.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.DWM
{
    public class DWMMobileService : IDWMMobileService
    {

        private readonly DBCustomerContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IGeneralConfigService _generalConfig;

        private readonly IConfiguration _configuration;

        public DWMMobileService(DBCustomerContext dbContext, IMapper mapper, IGeneralConfigService generalConfig, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _generalConfig = generalConfig;
            _configuration = configuration;
        }

        public List<ProfileResponseDto> Login(List<MobileUsers> readerCridential)
        {

            List<ProfileResponseDto> responses = new List<ProfileResponseDto>();
            ProfileResponseDto response = new ProfileResponseDto();
            try
            {
                if (readerCridential.Count() > 0)
                {
                    MobileUsers incomingUser = readerCridential.FirstOrDefault();
                    MobileUsers usersFromDb = _dbContext.MobileUsers.Where(e => e.userName == incomingUser.userName && e.passWord == incomingUser.passWord && e.role == "Reader").FirstOrDefault();
                    if (usersFromDb != null)
                    {
                        response.userName = usersFromDb.userName;
                        response.passWord = usersFromDb.passWord;
                        response.fullName = usersFromDb.fullName;
                        response.role = usersFromDb.role;
                        response.image = imageTobasse64("~/UI_Assets/img/" + usersFromDb.imagePath);
                        response.phone = usersFromDb.phone;
                        response.Kebele = usersFromDb.Kebele;
                        response.Ketena = usersFromDb.Ketena;
                        response.IsSuccess = "1";
                        response.Reason = "Update Successfull";
                        responses.Add(response);
                        return responses;
                    }
                    else
                    {
                        response.Id = -1;
                        response.userName = "";
                        response.passWord = "";
                        response.fullName = "";
                        response.role = "";
                        response.image = "";
                        response.phone = "";
                        response.Kebele = "";
                        response.Ketena = "";
                        response.IsSuccess = "0";
                        response.Reason = "Un Authorized User Please Contact System Administrator";
                        responses.Add(response);
                        return responses;
                    }
                }
                else
                {
                    response.Id = -1;
                    response.userName = "";
                    response.passWord = "";
                    response.fullName = "";
                    response.role = "";
                    response.image = "";
                    response.phone = "";
                    response.Kebele = "";
                    response.Ketena = "";
                    response.IsSuccess = "0";
                    response.Reason = "Sending Parameter Miss-Match NULL Value Found";
                    responses.Add(response);
                    return responses;
                }


            }
            catch (Exception e)
            {
                response.Id = -1;
                response.userName = "";
                response.passWord = "";
                response.fullName = "";
                response.role = "";
                response.image = "";
                response.phone = "";
                response.Kebele = "";
                response.Ketena = "";
                response.IsSuccess = "0";
                response.Reason = e.ToString();
                responses.Add(response);
                return responses;

            }




        }

        public List<ProfileResponseDto> LoginAdmin(List<MobileUsers> readerCridential)
        {

            List<ProfileResponseDto> responses = new List<ProfileResponseDto>();
            ProfileResponseDto response = new ProfileResponseDto();
            try
            {
                if (readerCridential.Count() > 0)
                {
                    MobileUsers incomingUser = readerCridential.FirstOrDefault();
                    MobileUsers usersFromDb = _dbContext.MobileUsers.Where(e => e.userName == incomingUser.userName && e.passWord == incomingUser.passWord && e.role != "Reader"

                    ).FirstOrDefault();
                    if (usersFromDb != null)
                    {
                        response.userName = usersFromDb.userName;
                        response.passWord = usersFromDb.passWord;
                        response.fullName = usersFromDb.fullName;
                        response.role = usersFromDb.role;
                        response.image = "";
                        response.phone = usersFromDb.phone;
                        response.Kebele = usersFromDb.Kebele;
                        response.Ketena = usersFromDb.Ketena;
                        response.IsSuccess = "1";
                        response.Reason = "Update Successfull";
                        responses.Add(response);
                        return responses;
                    }
                    else
                    {
                        response.Id = -1;
                        response.userName = "";
                        response.passWord = "";
                        response.fullName = "";
                        response.role = "";
                        response.image = "";
                        response.phone = "";
                        response.Kebele = "";
                        response.Ketena = "";
                        response.IsSuccess = "0";
                        response.Reason = "Un Authorized User Please Contact System Administrator";
                        responses.Add(response);
                        return responses;
                    }
                }
                else
                {
                    response.Id = -1;
                    response.userName = "";
                    response.passWord = "";
                    response.fullName = "";
                    response.role = "";
                    response.image = "";
                    response.phone = "";
                    response.Kebele = "";
                    response.Ketena = "";
                    response.IsSuccess = "0";
                    response.Reason = "Sending Parameter Miss-Match NULL Value Found";
                    responses.Add(response);
                    return responses;
                }


            }
            catch (Exception e)
            {
                response.Id = -1;
                response.userName = "";
                response.passWord = "";
                response.fullName = "";
                response.role = "";
                response.image = "";
                response.phone = "";
                response.Kebele = "";
                response.Ketena = "";
                response.IsSuccess = "0";
                response.Reason = e.ToString();
                responses.Add(response);
                return responses;

            }




        }

        private string imageTobasse64(string imageUrl)
        {
            try
            {

                return "";
            }
            catch (Exception)
            {

                return "";
            }
        }

        public List<ImportExportDto> ImportData(MobileUsers readerCredentials)
        {
            List<ImportExportDto> results = new List<ImportExportDto>();

            try
            {
                MobileUsers incomingUser = readerCredentials;
                List<MobileUsers> usersFromDb = _dbContext.MobileUsers.Where(e => e.userName == incomingUser.userName && e.passWord == incomingUser.passWord
                //(e.imei1 == incomingUser.imei1 || e.imei2 == incomingUser.imei2)
                ).ToList();

                if (usersFromDb.Count() > 0)
                {


                    return results = _dbContext.MobileAppReadings
                          .Where(x => x.fiscalYear.ToString() == incomingUser.fiscalYear && x.monthindex.ToString() == incomingUser.monthIndex && x.ReaderName == incomingUser.userName)
                          .Select(x => new ImportExportDto
                          {
                              custID = x.custId,
                              custCategoryCode = x.custCategoryCode,
                              MeterSizeCode = x.MeterSizeCode,
                              Ketena = x.Ketena,
                              Village = x.Village,
                              BookNo = x.BookNo,
                              sdPaid = x.sdPaid,
                              MeterStatus = x.MeterStatus,
                              ReaderName = x.ReaderName,
                              Mobile = x.Mobile!=null?x.Mobile:"",
                              ContractNo = x.ContractNo,
                              customerName = x.customerName,
                              Kebele = x.Kebele,
                              HouseNo = x.HouseNo,
                              OrdinaryNo = x.OrdinaryNo,
                              regFiscalYear = x.regFiscalYear,
                              regMonthIndex = x.regMonthIndex,
                              CustomerCategory = x.CustomerCategory,
                              meterno = x.meterno,
                              MeterSize = x.MeterSize,
                              MeterAltitude = x.MeterAltitude,
                              MeterLongitude = x.MeterLongitude,
                              BillOfficerId = x.BillOfficerId,
                              PrevReading = x.PrevReading,
                              CurrentReading = x.CurrentReading,
                              fiscalYear = x.fiscalYear,
                              monthIndex = x.monthindex,
                              prevTotalCost = x.PrevTotalCost!=null?x.PrevTotalCost:0.0,
                              Month = x.month,
                              monthnamelocal = x.monthnamelocal,
                              avgReading = x.AvgReading,
                              IsSuccess = "1",
                              Reason = "Success",
                              prevNoMth = x.prevNoMth
                          }).AsNoTracking().ToList();
                }
                else
                {
                    results.Add(new ImportExportDto
                    {
                        IsSuccess = "0",
                        Reason = "Unauthorized User. Please contact the system administrator."
                    });
                }
            }
            catch (Exception e)
            {
                results.Add(new ImportExportDto
                {
                    IsSuccess = "0",
                    Reason = e.Message // Log the exception properly or handle it according to your application's logging strategy
                });
            }

            return results;
        }
             

        public List<TEST_DISCONNECT> ImportDisconnected(MobileUsers readerCredential)
        {
            List<TEST_DISCONNECT> results = new List<TEST_DISCONNECT>();

            try
            {
                MobileUsers incomingUser = readerCredential;
                List<MobileUsers> usersFromDb = _dbContext.MobileUsers.Where(e => e.userName == incomingUser.userName && e.passWord == incomingUser.passWord && (e.imei1 == incomingUser.imei1 || e.imei2 == incomingUser.imei2)).ToList();

                if (usersFromDb.Count() > 0)
                {
                    results = _dbContext.TEST_DISCONNECTS.ToList(); ;
                }
                else
                {
                    results.Add(new TEST_DISCONNECT
                    {

                    });
                }
            }
            catch (Exception e)
            {
                results.Add(new TEST_DISCONNECT
                {
                });
            }

            return results;

        }

        public List<ImportResponse> ExportCustomers(bill_mobileImport bill)
        {
            List<ImportResponse> importResponses = new List<ImportResponse>();

            try
            {
                if (bill != null)
                {


                    BillMobileData singleBill = new BillMobileData();
                    ImportResponse singleResponse = new ImportResponse();
                    singleBill.recordno = Guid.NewGuid();
                    singleBill.custId = bill.custID != null ? bill.custID : "";
                    singleBill.fiscalYear = int.Parse(bill.fiscalYear ?? "0");
                    singleBill.monthIndex = int.Parse(bill.monthIndex ?? "0");
                    singleBill.meterno = bill.meterno ?? "";
                    singleBill.readingPrev = (int)Math.Round(decimal.Parse(bill.readingPrev ?? "0"));
                    singleBill.readingCurrent = (int)Math.Round(decimal.Parse(bill.readingCurrent ?? "0"));
                    singleBill.readingCons = (int)Math.Round(decimal.Parse(bill.readingCons ?? "0"));
                    singleBill.readingReasonCode = bill.readingReasonCode ?? "";
                    singleBill.readingBY = bill.readingBY ?? "";
                    singleBill.readingDT = bill.readingDT ?? "";
                    singleBill.xCoord = double.Parse(bill.xCoord ?? "0");
                    singleBill.yCoord = double.Parse(bill.yCoord ?? "0");
                    singleBill.customerName = bill.customerName ?? "";
                    singleBill.readingAvg = (int)Math.Round(decimal.Parse(bill.readingAvg ?? "0"));

                    singleBill.EntryDT = DateTime.Now;
                    singleBill.Reading_Image = bill.ReadingImage != null ? bill.ReadingImage : "";
                    _dbContext.BillMobileData.Add(singleBill);

                    Customer eachCust = new Customer();
                    eachCust = _dbContext.Customers.Where(e => e.custID == bill.custID).FirstOrDefault();
                    if (eachCust != null)
                    {
                        eachCust.MeterAltitude = bill.xCoord != null ? double.Parse(bill.xCoord) : 0;
                        eachCust.MeterLongitude = bill.yCoord != null ? double.Parse(bill.yCoord) : 0;
                        _dbContext.Entry(eachCust).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }

                    singleResponse.ContractNo = bill.ContractNo ?? "";
                    singleResponse.meterno = bill.meterno ?? "";
                    singleResponse.custID = bill.custID ?? "";
                    singleResponse.IsSuccess = "1";
                    singleResponse.Reason = "Success";
                    importResponses.Add(singleResponse);

                }

                return importResponses;
            }
            catch (Exception e)
            {
                ImportResponse singleResponse = new ImportResponse();
                singleResponse.ContractNo = "";
                singleResponse.meterno = "";
                singleResponse.custID = "";
                singleResponse.IsSuccess = "0";
                singleResponse.Reason = e.ToString();
                importResponses.Add(singleResponse);
                return importResponses;
            }

        }

        public List<ImportExportDto> ImportGPS(List<bill_mobileImport> bills)
        {
            List<ImportExportDto> importResponses = new List<ImportExportDto>();

            try
            {
                foreach (var bill in bills)
                {

                    ImportExportDto singleResponse = new ImportExportDto();
                    Customer eachCust = new Customer();
                    eachCust = _dbContext.Customers.Where(e => e.custID == bill.custID).FirstOrDefault();
                    eachCust.MeterAltitude = bill.xCoord != null ? double.Parse(bill.xCoord) : 0;
                    eachCust.MeterLongitude = bill.yCoord != null ? double.Parse(bill.yCoord) : 0;
                    _dbContext.Entry(eachCust).State = EntityState.Modified;
                    singleResponse.ContractNo = bill.ContractNo;
                    singleResponse.meterno = bill.meterno;
                    singleResponse.custID = bill.custID;
                    singleResponse.IsSuccess = "1";
                    singleResponse.Reason = "Success";
                    importResponses.Add(singleResponse);

                }
                _dbContext.SaveChanges();
                return importResponses;
            }
            catch (Exception e)
            {
                ImportExportDto singleResponse = new ImportExportDto();
                singleResponse.ContractNo = "";
                singleResponse.meterno = "";
                singleResponse.custID = "";
                singleResponse.IsSuccess = "0";
                singleResponse.Reason = e.ToString();
                importResponses.Add(singleResponse);
                return importResponses;
            }
        }
    }
}
