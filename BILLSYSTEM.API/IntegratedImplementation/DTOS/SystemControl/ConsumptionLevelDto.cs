using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public record ConsumptionLevelDto
    {
        public int recordno { get; set; }
        public string levelNameEN { get; set; }
        public string levelNameLocal { get; set; }
    }
}
