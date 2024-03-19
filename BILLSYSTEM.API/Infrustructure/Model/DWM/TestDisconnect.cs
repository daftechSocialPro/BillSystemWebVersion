
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.DWM
{

    public class TEST_DISCONNECT
    {
        [Key]
        public string custID { get; set; }
        public string customerName { get; set; }
        public string ContractNo { get; set; }
        public string Kebele { get; set; }
        public Nullable<int> OrdinaryNo { get; set; }
        public string meterno { get; set; }
        public Nullable<double> MeterLongitude { get; set; }
        public Nullable<double> MeterAltitude { get; set; }
        public string ReaderName { get; set; }
        public string typeOfAction { get; set; }
        public string reason { get; set; }
        public Nullable<System.DateTime> enterDate { get; set; }
        public string enterBy { get; set; }
        public Nullable<int> FiscalYear { get; set; }
        public Nullable<int> monthIndex { get; set; }
    }
}
