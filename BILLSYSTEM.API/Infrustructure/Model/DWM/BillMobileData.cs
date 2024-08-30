using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.DWM
{


    [Table("billMobileData")]
    public class BillMobileData
    {
        [Key]
            public Guid recordno { get; set; }
            public int? fiscalYear { get; set; }
            public int? monthIndex { get; set; }
            public string? custId { get; set; }
            public string? meterno { get; set; }
            public int? readingPrev { get; set; }
            public int? readingCurrent { get; set; }
            public int? readingCons { get; set; }
            public int? readingAvg { get; set; }
            public string? readingReasonCode { get; set; }
            public string? readingBY { get; set; }
            public string? readingDT { get; set; }
            public double? xCoord { get; set; }
            public double? yCoord { get; set; }
            public DateTime?  EntryDT { get; set; }
            public DateTime? ModifyDT { get; set; }
            public string? customerName { get; set; }
            public string? Reading_Image { get; set; }
        
    }
}
