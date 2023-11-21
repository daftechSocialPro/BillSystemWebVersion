using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public class PenalityRateDto
    {
        public int recordno { get; set; }
        public string NoOfMonth { get; set; }
        public double PenalityAmount { get; set; }
        public string CustCategoryCode { get; set; }
        public string PenalityGroupCode { get; set; }

    }
}
