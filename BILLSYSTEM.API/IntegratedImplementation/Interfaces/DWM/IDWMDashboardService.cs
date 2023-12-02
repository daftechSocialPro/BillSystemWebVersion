using IntegratedImplementation.DTOS.DWM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.DWM
{
    public interface IDWMDashboardService
    {
        Task<DWMDashboardDto> GetDashbordDetail(int  year , int month );
    }
}
