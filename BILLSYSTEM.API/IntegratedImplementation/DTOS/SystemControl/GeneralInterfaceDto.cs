using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public class GeneralInterfaceDto
    {
        public int recordno { get; set; }
        public string ObjectNameEN { get; set; }
        public string ObjectNameLocalam { get; set; }
        public string ObjectNameLocalen { get; set; }
        public string ObjectCategory { get; set; }
    }
}
