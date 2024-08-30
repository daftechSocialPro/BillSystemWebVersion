using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace IntegratedInfrustructure.Model.CSS
{

    public class BillGenerate
    {
        [Key]
        [Column("recordno")]
        public Guid RecordNo { get; set; }

        [Column("fiscalYear")]
        public int? FiscalYear { get; set; }

        [Column("monthIndex")]
        public int? MonthIndex { get; set; }

        [Column("custID")]
        [StringLength(20)]
        public string? CustID { get; set; }

        [Column("meterno")]
        [StringLength(50)]
        public string? MeterNo { get; set; }

        [Column("unpaidCons")]
        public int? UnpaidCons { get; set; }

        [Column("readingPrev")]
        public int? ReadingPrev { get; set; }

        [Column("readingCurrent")]
        public int? ReadingCurrent { get; set; }

        [Column("readingCons")]
        public int? ReadingCons { get; set; }

        [Column("readingReasonCode")]
        [StringLength(10)]
        public string? ReadingReasonCode { get; set; }

        [Column("readingBY")]
        [StringLength(10)]
        public string? ReadingBy { get; set; }

        [Column("readingDT")]
        [StringLength(20)]
        public string? ReadingDate { get; set; }

        [Column("rentCode")]
        [StringLength(10)]
        public string? RentCode { get; set; }

        [Column("tariffCode")]
        [StringLength(10)]
        public string? TariffCode { get; set; }

        [Column("meterRent")]
        public float? MeterRent { get; set; }

        [Column("currTariff")]
        public float? CurrTariff { get; set; }

        [Column("block_1Cons")]
        public int? Block1Cons { get; set; }

        [Column("block_1")]
        public float? Block1 { get; set; }

        [Column("block_2Cons")]
        public int? Block2Cons { get; set; }

        [Column("block_2")]
        public float? Block2 { get; set; }

        [Column("block_3Cons")]
        public int? Block3Cons { get; set; }

        [Column("block_3")]
        public float? Block3 { get; set; }

        [Column("block_4Cons")]
        public int? Block4Cons { get; set; }

        [Column("block_4")]
        public float? Block4 { get; set; }

        [Column("block_5Cons")]
        public int? Block5Cons { get; set; }

        [Column("block_5")]
        public float? Block5 { get; set; }

        [Column("block_6Cons")]
        public int? Block6Cons { get; set; }

        [Column("block_6")]
        public float? Block6 { get; set; }

        [Column("block_7Cons")]
        public int? Block7Cons { get; set; }

        [Column("block_7")]
        public float? Block7 { get; set; }

        [Column("block_8Cons")]
        public int? Block8Cons { get; set; }

        [Column("block_8")]
        public float? Block8 { get; set; }

        [Column("block_9Cons")]
        public int? Block9Cons { get; set; }

        [Column("block_9")]
        public float? Block9 { get; set; }

        [Column("prevNoMth")]
        public int? PrevNoMth { get; set; }

        [Column("prevMRate")]
        public float? PrevMRate { get; set; }

        [Column("prevTariff")]
        public float? PrevTariff { get; set; }

        [Column("prevPenality")]
        public float? PrevPenality { get; set; }

        [Column("prevSewerageD")]
        public float? PrevSewerageD { get; set; }

        [Column("prevServiceC")]
        public float? PrevServiceC { get; set; }

        [Column("prevMiscCost")]
        public float? PrevMiscCost { get; set; }

        [Column("penality")]
        public float? Penality { get; set; }

        [Column("sewerageAmt")]
        public float? SewerageAmt { get; set; }

        [Column("serviceCharge")]
        public float? ServiceCharge { get; set; }

        [Column("MiscCost")]
        public float? MiscCost { get; set; }

        [Column("MiscCostName")]
        [StringLength(100)]
        public string? MiscCostName { get; set; }

        [Column("ServiceCName")]
        [StringLength(100)]
        public string? ServiceCName { get; set; }

        [Column("totalCost")]
        public float? TotalCost { get; set; }

        [Column("invoiceNo")]
        [StringLength(20)]
        public string? InvoiceNo { get; set; }

        [Column("producerID")]
        [StringLength(10)]
        public string? ProducerID { get; set; }

        [Column("produceDate")]
        [StringLength(10)]
        public string? ProduceDate { get; set; }

        [Column("printBy")]
        [StringLength(10)]
        public string? PrintBy { get; set; }

        [Column("printDate")]
        [StringLength(10)]
        public string? PrintDate { get; set; }

        [Column("printNo")]
        public int? PrintNo { get; set; }

        [Column("voidBy")]
        [StringLength(10)]
        public string? VoidBy { get; set; }

        [Column("voidDate")]
        [StringLength(10)]
        public string? VoidDate { get; set; }

        [Column("voidInvestigate")]
        [StringLength(3)]
        public string? VoidInvestigate { get; set; }

        [Column("voidReason")]
        [StringLength(200)]
        public string? VoidReason { get; set; }

        [Column("soldDate")]
        [StringLength(10)]
        public string? SoldDate { get; set; }

        [Column("soldID")]
        [StringLength(10)]
        public string? SoldID { get; set; }

        [Column("UnsoldReason")]
        [StringLength(300)]
        public string? UnsoldReason { get; set; }

        [Column("TransferBy")]
        [StringLength(10)]
        public string? TransferBy { get; set; }

        [Column("TransferDT")]
        [StringLength(20)]
        public string? TransferDT { get; set; }

        [Column("TransferTo")]
        [StringLength(10)]
        public string? TransferTo { get; set; }

        [Column("TransferType")]
        [StringLength(20)]
        public string? TransferType { get; set; }

        [Column("billStatus")]
        [StringLength(20)]
        public string? BillStatus { get; set; }

        [Column("readyGenerate")]
        [StringLength(3)]
        public string? ReadyGenerate { get; set; }

        [Column("readyPrint")]
        [StringLength(3)]
        public string? ReadyPrint { get; set; }

        [Column("TransferYN")]
        [StringLength(3)]
        public string? TransferYN { get; set; }

        [Column("readySold")]
        [StringLength(3)]
        public string? ReadySold { get; set; }

        [Column("readyNR")]
        [StringLength(3)]
        public string? ReadyNR { get; set; }

        [Column("entryBy")]
        [StringLength(50)]
        public string? EntryBy { get; set; }

        [Column("entryDate")]
        public DateTime? EntryDate { get; set; }

        [Column("modifyBy")]
        [StringLength(50)]
        public string? ModifyBy { get; set; }

        [Column("modifyDate")]
        public DateTime? ModifyDate { get; set; }

        [Column("remarks")]
        [StringLength(50)]
        public string? Remarks { get; set; }

        [Column("ComputerName")]
        [StringLength(250)]
        public string? ComputerName { get; set; }

        [Column("CompIPAddress")]
        [StringLength(50)]
        public string? CompIPAddress { get; set; }

        [Column("CompMacAddress")]
        [StringLength(250)]
        public string? CompMacAddress { get; set; }

        [Column("UpdateYM")]
        [StringLength(50)]
        public string? UpdateYM { get; set; }

        [Column("ZeroConsCnt")]
        public int? ZeroConsCnt { get; set; }

        [Column("NullReadingCnt")]
        public int? NullReadingCnt { get; set; }

        [Column("AvgCons")]
        public float? AvgCons { get; set; }

        [Column("DataSynched")]
        [StringLength(1)]
        public string? DataSynched { get; set; }
    }


}
