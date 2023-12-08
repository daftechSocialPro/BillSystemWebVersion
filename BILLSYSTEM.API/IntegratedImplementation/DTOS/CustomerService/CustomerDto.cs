using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.CustomerService
{
    public record CustomerDto
    {

        public string custID { get; set; }
        public int regFiscalYear { get; set; }
        public int regMonthIndex { get; set; }
        public string customerName { get; set; }
        public string Ketena { get; set; }
        public string Kebele { get; set; }
        public string HouseNo { get; set; }
        public string Village { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string BookNo { get; set; }
        public string AccountNo { get; set; }
        public string ContractNo { get; set; }
        public int OrdinaryNo { get; set; }
        public string custCategoryCode { get; set; }
        public string meterno { get; set; }
        public string MeterSizeCode { get; set; }
        public string MeterType { get; set; }
        public int MeterDigit { get; set; }
        public string MeterCountryOrigin { get; set; }
        public string MeterModel { get; set; }
        public DateTime InstallationDate { get; set; }
        public int MeterStartReading { get; set; }
        public float MeterAltitude { get; set; }
        public float MeterLongitude { get; set; }
        public string sdPaid { get; set; }
        public string MeterClass { get; set; }
        public string WaterSource { get; set; }
        public string MeterStatus { get; set; }
        public string RegDate { get; set; }
        public string ReaderName { get; set; }
        public string PaymentPlace { get; set; }
        public string PaymentDuration { get; set; }
        public string PaymentMode { get; set; }
        public string BillSalesGroup { get; set; }
        public string OnlineGroup { get; set; }
        public string BankCode { get; set; }
        public string BankAccount { get; set; }
        public string TrasnferBY { get; set; }
        public string BillOfficerId { get; set; }
        public int TransferFY { get; set; }
        public int TransferMI { get; set; }
        public string TransferDT { get; set; }
        public string Field01 { get; set; }
        public string Field02 { get; set; }
        public float Field03 { get; set; }
        public float Field04 { get; set; }
        public string enterBy { get; set; }
        public DateTime enterDate { get; set; }
        public string modifyBy { get; set; }
        public DateTime modifyDate { get; set; }
        public string Remarks { get; set; }
        public string DataSynched { get; set; }

        
    }

    public record CustomerGetDto
    {

        public int? regFiscalYear { get; set; }
        public int? regMonthIndex { get; set; }
        public string custId { get; set; }
        public string customerName { get; set; }
        public string ContractNo { get; set; }
        public string custCategoryCode { get; set; }
        public string meterno { get; set; }
        public string MeterSizeCode { get; set; }
    }

    public record CustomerPostDto
    {
       
        public string FullName { get; set; }     
        public string PhoneNumber { get; set; }
        public string Ketena { get; set; }            
        public string Kebele { get; set; }      
        public string Village { get; set; }   
        public string ReaderName { get; set; }
        public string MapNumber { get; set; }     
        public string HouseNumber { get; set; }     
        public string BillCycle { get; set; }
        public string CustomerCategory { get; set; }      
        public string ContractNo { get; set; }
        public int? OrdinaryNo { get; set; }       
        public string MeterNo { get; set; }
        public string MeterSize { get; set; }      
        public DateTime InstallationDate { get; set; }       
        public bool? UpdateInitial { get; set; }      
        public int StartReading { get; set; }

        public string SweragePaid { get; set; }

        public int MonthIndex { get; set; }
        public int FiscalYear { get; set; }
    }
}
