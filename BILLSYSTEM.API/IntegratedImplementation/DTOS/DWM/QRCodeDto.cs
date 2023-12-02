using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.DWM
{
    public record QRCodeDto
    {

        public string CustomerName { get; set; }
        public string CustId { get; set; }
    }
}