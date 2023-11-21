using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.DTOS.SystemControl
{
    public class MeterOriginGetDto
    {
        public Guid Id { get; set; }
       
        public string Name { get; set; }
    }

    public class MeterOriginPostDto
    {
        public string Name { get; set; }

        public string CreatedById { get; set; }
    }
}
