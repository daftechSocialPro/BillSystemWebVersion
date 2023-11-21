using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedInfrustructure.Model.SCS
{

    [Table("odb_Meter TariffRate")]
    public  class ConsumptionTariff
    {

        [Key]
        public int recordno { get; set; }
        public string RateGroupCode { get; set; }
        public string consLevels { get; set; }
        public string consRanges { get; set; }
        public int Consumption { get; set; }
        public double TariffRate { get; set; }
        public string custCategoryCode { get; set; }



    }
}
