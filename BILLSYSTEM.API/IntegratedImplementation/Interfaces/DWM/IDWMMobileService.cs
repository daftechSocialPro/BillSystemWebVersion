using IntegratedImplementation.DTOS.DWM;
using IntegratedInfrustructure.Model.DWM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.DWM
{
    public interface IDWMMobileService
    {
        List<ProfileResponseDto> Login(List<MobileUsers> readerCridential);
        List<ProfileResponseDto> LoginAdmin(List<MobileUsers> readerCridential);
        List<ImportExportDto> ImportData(MobileUsers readerCridential);
        List<TEST_DISCONNECT> ImportDisconnected(MobileUsers readerCredential);
        List<ImportResponse> ExportCustomers(bill_mobileImport bill);
        List<ImportExportDto> ImportGPS(List<bill_mobileImport> bills);

    }
}
