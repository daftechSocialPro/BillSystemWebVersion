using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.Configuration;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.Configuration;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.Authentication;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.SystemControl
{
    public class UserSettingService : IUserSettingService
    {

        private readonly DBGeneralContext _generalContext;
        private readonly DBHumanContext _humanContext;
        private readonly IGeneralConfigService _generalConfigService;
        private IMapper _mapper;

        public UserSettingService(
            DBGeneralContext generalContext,
            IMapper mapper,
            DBHumanContext humanContext,
            IGeneralConfigService generalConfigService)
        {
            _generalContext = generalContext;
            _mapper = mapper;
            _humanContext = humanContext;
            _generalConfigService = generalConfigService;
        }



        public async Task<List<SelectListDto>> GetEmployeesforUserSetting()
        {

            var employeess = _humanContext.Employees.ToList();
            var employees = (from employee in employeess
                             where !_generalContext.Users.Any(user => user.empId == employee.EmpID)
                             select new SelectListDto
                             {
                                 EmpId = employee.EmpID,
                                 Name = $"{employee?.FirstName} {employee?.MiddleName}",
                             }).ToList();


            return employees;



        }

        public async Task<List<SelectListDto>> GetEmployeesforUserSettingUpdate()
        {

            var employeess = _humanContext.Employees.ToList();
            var employees = (from employee in employeess
                             where _generalContext.Users.Any(user => user.empId == employee.EmpID)
                             select new SelectListDto
                             {
                                 EmpId = employee.EmpID,
                                 Name = $"{employee?.FirstName} {employee?.MiddleName}",
                             }).ToList();


            return employees;



        }

        public async Task<ResponseMessage> CreateSystemUser(UserServicePostDto userPost)
        {
            try
            {
                var encryptedPassword = _generalConfigService.Encrypt(userPost.password);

                var User = new User
                {

                    userName = userPost.userName,
                    password = encryptedPassword,
                    empId = userPost.empId,
                    userLevel = userPost.userLevel,
                    userStatus = userPost.userStatus,
                    SysetmAdmin = userPost.SysetmAdmin,
                    CustomerService = userPost.CustomerService,
                    BillProduce = userPost.BillProduce,
                    TechnicalService = userPost.TechnicalService,
                    StockControl = userPost.StockControl,
                    HRM = userPost.HRM,
                    AllowKetenas = userPost.AllowKetenas,
                    Online = userPost.Online,



                };


                await _generalContext.Users.AddAsync(User);
                await _generalContext.SaveChangesAsync();
                return new ResponseMessage
                {

                    Message = "Added Successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = ex.Message,
                    Success = false
                };

            }
        }



        public async Task<List<UserServiceDto>> GetUserSettingList()
        {
            var result = await _generalContext.Users.ProjectTo<UserServiceDto>(_mapper.ConfigurationProvider).ToListAsync();

            return result;

        }

        public async Task<ResponseMessage> UpdateSystemUser(UserServicePostDto userUpdate)
        {
            try
            {
                var currentUser = await _generalContext.Users.FindAsync(userUpdate.userId);

                if (currentUser != null)
                {

                    currentUser.userName = userUpdate.userName;

                    currentUser.userLevel = userUpdate.userLevel;
                    currentUser.userStatus = userUpdate.userStatus;
                    currentUser.SysetmAdmin = userUpdate.SysetmAdmin;
                    currentUser.CustomerService = userUpdate.CustomerService;
                    currentUser.BillProduce = userUpdate.BillProduce;
                    currentUser.TechnicalService = userUpdate.TechnicalService;
                    currentUser.StockControl = userUpdate.StockControl;
                    currentUser.HRM = userUpdate.HRM;
                    currentUser.password = userUpdate.password;

                    currentUser.AllowKetenas = userUpdate.AllowKetenas;
                    currentUser.Online = userUpdate.Online;

                    if (userUpdate.password != null)
                    {
                        var encryptedPassword = _generalConfigService.Encrypt(userUpdate.password);
                        currentUser.password = encryptedPassword;
                    }



                    await _generalContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find The User" };

            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {
                    Message = ex.Message,
                    Success = false
                };

            }

        }

        public async Task<ResponseMessage> DeleteSystemUser(int userId)
        {

            var currentBillSection = await _generalContext.Users.FindAsync(userId);

            if (currentBillSection != null)
            {

                _generalContext.Remove(currentBillSection);
                await _generalContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find User" };
        }

        public async Task<List<SelectListDto>> GetAppModules()
        {
            var modules = await _generalContext.DetailPermissions.Select(x => new SelectListDto
            {
                Name = x.AppModule

            }).GroupBy(x => x.Name)
            .Select(group => group.First())
            .ToListAsync();

            return modules;
        }

        public async Task<List<SelectListDto>> GetAppTabsByModule(string appModule)
        {

            var tabs = await _generalContext.DetailPermissions.Where(x => x.AppModule == appModule).Select(x => new SelectListDto
            {
                Name = x.AppTabs

            }).GroupBy(x => x.Name)
         .Select(group => group.First())
         .ToListAsync();


            return tabs;



        }

        public async Task<List<UserPermission>> GetUserPermissions(string userId)
        {
            var permissions = await _generalContext.UserPermissions.Where(x => x.UserId == userId).ToListAsync();

            return permissions;
        }

        public async Task<ResponseMessage> UpdateUserPermission(List<UserPermission> userPermissions)
        {
            
            try
            {
                if (userPermissions != null)
                {
                    var userId = userPermissions[0].UserId;
                    var permissions = await _generalContext.UserPermissions.Where(x => x.UserId == userId).ToListAsync();


                    var permissionsToRemove = permissions.Except(userPermissions, new UserPermissionEqualityComparer());
                    var permissionsToAdd = userPermissions.Except(permissions, new UserPermissionEqualityComparer());

                    if (permissionsToRemove != null)
                    {

                        foreach (var permission in permissionsToRemove)
                        {
                            await RemoveUserPermission(permission);
                        }
                    }

                       

                    foreach (var permission in permissionsToAdd)
                    {

                        permission.UserId = userId;
                        await AddUserPermission(permission);

                    }
                    return new ResponseMessage
                    {

                        Success = true,
                        Message = "Userpermission Updated Successfully !!!"

                    };


                }
                else
                {
                    return new ResponseMessage
                    {

                        Success = false,
                        Message = "Userpermission is empty !!!"

                    };
                }
            }
            catch(Exception ex)
            {
                return new ResponseMessage
                {

                    Success = false,
                    Message = ex.Message

                };
            }
        
        }

        public async Task AddUserPermission(UserPermission userPermission)
        {
            string insertQuery = "INSERT INTO odb_zUserPermission (UserId, ButtonId) VALUES (@userId, @buttonId)";

            await _generalContext.Database.ExecuteSqlRawAsync(insertQuery,
                new SqlParameter("@userId", userPermission.UserId),
                new SqlParameter("@buttonId", userPermission.ButtonId));
        }
        public async Task RemoveUserPermission(UserPermission userPermission)
        {
           
                string deleteQuery = $"DELETE FROM odb_zUserPermission WHERE UserId = @userId AND ButtonId = @buttonId";

                await _generalContext.Database.ExecuteSqlRawAsync(deleteQuery,
                    new SqlParameter("@userId", userPermission.UserId),
                    new SqlParameter("@buttonId", userPermission.ButtonId));
            
           
            
        }
    }




    public class UserPermissionEqualityComparer : IEqualityComparer<UserPermission>
    {
        public bool Equals(UserPermission x, UserPermission y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.ButtonId == y.ButtonId && x.UserId == y.UserId;
        }

        public int GetHashCode(UserPermission obj)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + obj.ButtonId.GetHashCode();
                hash = hash * 23 + obj.UserId.GetHashCode();
                return hash;
            }
        }
    }



}





