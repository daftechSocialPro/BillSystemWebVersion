using Implementation.Helper;
using IntegratedImplementation.Interfaces.SystemControl;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;



namespace IntegratedImplementation.Services.SystemControl
{
    public class DatabaseBackUpService : IDataBaseBackUpService
    {

        private readonly IConfiguration _configuration;

        public DatabaseBackUpService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ResponseMessage> OpenFolder()
        {


            string fullPath = Path.Combine("wwwroot", "DatabaseBackup");
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), fullPath);

            try
            {
                if (Directory.Exists(folderPath))
                {
                    // Open the folder using the default file explorer
                    System.Diagnostics.Process.Start(folderPath);

                    return new ResponseMessage
                    {
                        Success = true,
                        Message = "Back Up Folder has been opened !!!"
                    };
                            }
                else
                {
                    return new ResponseMessage
                    {
                        Success = false,
                        Message = "No Such Directory!!!"
                    };
                   
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    Success = false,
                    Message = ex.Message
                };

            }
            // Check if the folder exists
            
        }

        public async Task<ResponseMessage> GetDatabaseBakcupPath()
        {
            string fullPath = Path.Combine("wwwroot", "DatabaseBackup");
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), fullPath);

            return new ResponseMessage { Success = true, Message = folderPath };
        }
        public async Task<ResponseMessage> DatabaseBackup(string databaseName, string backupPath)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("SqlConnectionGeneral");

                string backupFilePath = BuildBackupPathWithFileName(databaseName, backupPath);
                GrantPermissionsToBackupFolder(backupPath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string backupQuery = $"BACKUP DATABASE [{databaseName}] TO DISK='{backupFilePath}'";

                    using (SqlCommand sqlCommand = new SqlCommand(backupQuery, connection))
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                }
                return new ResponseMessage
                {
                    Success = true,
                    Message = "Database backup completed successfully."
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        private string BuildBackupPathWithFileName(string databaseName,string path)
        {
       
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd");
            string fileName = $"{databaseName}-{timestamp}.bak";
            string fullPath = Path.Combine("wwwroot", "DatabaseBackup", fileName);
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), fullPath);

            return pathToSave;
        }
        private void GrantPermissionsToBackupFolder(string backupPath)
        {


            DirectoryInfo directoryInfo = new DirectoryInfo(backupPath);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create(); // Create the directory if it doesn't exist
            }

            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

            // Add FullControl permissions for "NT Service\MSSQLSERVER"
            directorySecurity.AddAccessRule(new FileSystemAccessRule(
                @"NT Service\MSSQLSERVER",  // SQL Server service account
                FileSystemRights.FullControl,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                AccessControlType.Allow
            ));

            directoryInfo.SetAccessControl(directorySecurity);

          
        }

    }


}


