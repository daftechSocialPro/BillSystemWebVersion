using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public class GeneralSettingDto
    {
        public Guid recordno { get; set; }
        public string InputValues { get; set; }
        public string InputCategory { get; set; }
    }
   
}
