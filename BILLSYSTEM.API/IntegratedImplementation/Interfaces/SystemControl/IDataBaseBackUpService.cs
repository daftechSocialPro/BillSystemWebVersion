using Implementation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Interfaces.SystemControl
{
    public interface IDataBaseBackUpService
    {

        Task<ResponseMessage> DatabaseBackup(string DbName, string Path);
        Task<ResponseMessage> OpenFolder();

        Task<ResponseMessage> GetDatabaseBakcupPath();
    }
}
