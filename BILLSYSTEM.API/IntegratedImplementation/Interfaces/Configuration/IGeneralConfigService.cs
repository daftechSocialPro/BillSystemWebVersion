using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegratedInfrustructure.Data.EnumList;

namespace IntegratedImplementation.Interfaces.Configuration
{
    public interface IGeneralConfigService
    {
        Task<string> GenerateCode(GeneralCodeType GeneralCodeType);
        Task<string> UploadFiles(IFormFile formFile, string Name, string FolderName);
        Task<string> GetFiles(string path);

        string Encrypt(string plainText);

        string Encrypt(string text, string encryptionKey);
        string Decrypt(string encryptedText, string encryptionKey);


    }
}
