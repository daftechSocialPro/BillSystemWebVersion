using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.DWM
{
    public record MobileUsersDto
    {
        public string? Id { get; set; }
        public string? userName { get; set; }
        public string? passWord { get; set; }
        public string? fullName { get; set; }
        public string? Kebele { get; set; }
        public string? Ketena { get; set; }
        public string? phone { get; set; }
        public string? imei1 { get; set; }
        public string? imei2 { get; set; }
        public IFormFile? Image { get; set; }
        public string? imagePath { get; set; }
        public string? role { get; set; }
        public string? IsSuccess { get; set; }
        public string? Reason { get; set; }
        public string? fiscalYear { get; set; }
        public string? monthIndex { get; set; }
        public string? isRemoved { get; set; }
    }
    public record ImportResponse
    {
        public string ContractNo { get; set; }
        public string meterno { get; set; }
        public string custID { get; set; }
        public string IsSuccess { get; set; }
        public string Reason { get; set; }
    }

    public record bill_mobileImport
    {
        public string? customerName { get; set; }
        public string? Kebele { get; set; }
        public string? HouseNo { get; set; }
        public string? ContractNo { get; set; }
        public string? OrdinaryNo { get; set; }
        public string? meterno { get; set; }
        public string? custID { get; set; }
        public string? custCategoryCode { get; set; }
        public string? MeterSizeCode { get; set; }
        public string? sdPaid { get; set; }
        public string? CustomerCategory { get; set; }
        public string? MeterSize { get; set; }
        public string? Ketena { get; set; }
        public string? Village { get; set; }
        public string? BookNo { get; set; }
        public string?  fiscalYear { get; set; }
        public string? monthIndex { get; set; }
        public string? readingCurrent { get; set; }
        public string? readingReasonCode { get; set; }
        public string? readingBY { get; set; }
        public string? readingDT { get; set; }
        public string? readingPrev { get; set; }
        public string? readingCons { get; set; }
        public string? xCoord { get; set; }
        public string? yCoord { get; set; }
        public string? ReaderName { get; set; }
        public string? BillOfficerId { get; set; }
        public string? readingAvg { get; set; }
        public string? ReadingImage { get; set; }
    }

 
}
