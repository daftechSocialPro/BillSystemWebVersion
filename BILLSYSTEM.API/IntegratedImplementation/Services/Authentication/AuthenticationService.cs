using Implementation.DTOS.Authentication;

using Implementation.Helper;
using Implementation.Interfaces.Authentication;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using static IntegratedInfrustructure.Data.EnumList;

namespace Implementation.Services.Authentication
{

    public class AuthenticationService : IAuthenticationService
    {

        private RoleManager<IdentityRole> _roleManager;
        private readonly DBGeneralContext _dbContext;

        private readonly IConfiguration _configuration;

        public AuthenticationService(

            DBGeneralContext dbContext,
            IConfiguration configuration

             )
        {

            _configuration = configuration;
            _dbContext = dbContext;

        }


        public async Task<ResponseMessage> Login(LoginDto login)
        {

            try
            {




                var user = await _dbContext.Users.Where(x => x.userName.ToLower() == login.UserName.ToLower()).FirstOrDefaultAsync();

                if (user != null)
                {
                    string password = user.password;
                    if (login.IsEncryptChecked)
                    {
                        password = Decrypt(user.password);
                    }


                    if (     login.Password ==password )
                    {




                        var TokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                            {
                        new Claim("userId", user.userId.ToString()),
                        new Claim("employeeId", user.empId.ToString()),
                        new Claim("fullName", $"{user.userName}"),
                        new Claim("photo",""),
                            }),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1225290901686999272364748849994004994049404940")), SecurityAlgorithms.HmacSha256Signature)
                        };

                        var TokenHandler = new JwtSecurityTokenHandler();
                        var SecurityToken = TokenHandler.CreateToken(TokenDescriptor);
                        var token = TokenHandler.WriteToken(SecurityToken);
                        return new ResponseMessage()
                        {
                            Success = true,
                            Message = "Login Success",
                            Data = token
                        };
                    }
                    else
                    {

                        return new ResponseMessage()
                        {
                            Success = false,
                            Message = "Invalid User Name or Password"
                        };
                    }



                }
                else
                    return new ResponseMessage()
                    {
                        Success = false,
                        Message = "Invalid User Name or Password"
                    };
            }
            catch (Exception ex)
            {
                return new ResponseMessage()
                {
                    Success = false,
                    Message = "Invalid User Name or Password"
                };

            }


        }

        public async Task<List<UserListDto>> GetUserList()
        {
            var userList = await _dbContext.Users.ToListAsync();
            var userLists = new List<UserListDto>();

            foreach (var user in userList)
            {



                var userListt = new UserListDto()
                {
                    Id = user.userId,
                    EmployeeId = user.empId,
                    UserName = user.userName,
                    Name = user.userName,
                    Status = user.userStatus.ToString(),
                    ImagePath = "",
                    Email = user.userName,


                };


                userLists.Add(userListt);

            }



            return userLists;
        }

        public string Decrypt(string cipherText)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes("@1B2c3D4e5F6g7H8");
            byte[] saltValueBytes = Encoding.ASCII.GetBytes("s@1tValue");
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            // byte[] PassPhrase = Convert.FromBase64String("*&Daftech*Pas5pr@se");

            PasswordDeriveBytes password = new PasswordDeriveBytes(
                "*&Daftech*Pas5pr@se",
                saltValueBytes,
                "SHA1",
               10);

            byte[] keyBytes = password.GetBytes(256 / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

            byte[] plainTextBytes;

            using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    plainTextBytes = new byte[cipherTextBytes.Length];
                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }

            string plainText = Encoding.UTF8.GetString(plainTextBytes);
            plainText = plainText.TrimEnd('\0');
            return plainText;
        }
       
        //public async Task<ResponseMessage> AddUser(AddUSerDto addUSer)
        //{
        //        return new ResponseMessage { Success = true, Message = "Succesfully Added User", Data = applicationUser.UserName };



        //}

        //public async Task<List<RoleDropDown>> GetRoleCategory()
        //{
        //    var roleCategory = await _roleManager.Roles.Select(x => new RoleDropDown
        //    {
        //        Id = x.Id.ToString(),
        //        Name = x.NormalizedName,
        //    }).ToListAsync();

        //    return roleCategory;
        //}
        //public async Task<List<RoleDropDown>> GetNotAssignedRole(string userId)
        //{
        //    var currentuser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
        //    if (currentuser != null)
        //    {
        //        var currentRoles = await _userManager.GetRolesAsync(currentuser);
        //        if (currentRoles.Any())
        //        {
        //            var notAssignedRoles = await _roleManager.Roles.
        //                          Where(x => 
        //                          !currentRoles.Contains(x.Name)).Select(x => new RoleDropDown
        //                          {
        //                              Id = x.Id,
        //                              Name = x.Name
        //                          }).ToListAsync();

        //            return notAssignedRoles;
        //        }
        //        else
        //        {
        //            var notAssignedRoles = await _roleManager.Roles
        //                        .Select(x => new RoleDropDown
        //                          {
        //                              Id = x.Id,
        //                              Name = x.Name
        //                          }).ToListAsync();

        //            return notAssignedRoles;

        //        }


        //    }

        //    throw new FileNotFoundException();
        //}

        //public async Task<List<RoleDropDown>> GetAssignedRoles(string userId)
        //{
        //    var currentuser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
        //    if (currentuser != null)
        //    {
        //        var currentRoles = await _userManager.GetRolesAsync(currentuser);
        //        if (currentRoles.Any())
        //        {
        //            var notAssignedRoles = await _roleManager.Roles.
        //                              Where(x => 
        //                              currentRoles.Contains(x.Name)).Select(x => new RoleDropDown
        //                              {
        //                                  Id = x.Id,
        //                                  Name = x.Name
        //                              }).ToListAsync();

        //            return notAssignedRoles;
        //        }

        //        return new List<RoleDropDown>();

        //    }

        //    throw new FileNotFoundException();
        //}

        //public async Task<ResponseMessage> AssignRole(UserRoleDto userRole)
        //{
        //    var currentUser = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id==userRole.UserId);

        //    foreach (var role in userRole.RoleName)
        //    {

        //        if (currentUser != null)
        //        {
        //            var roleExists = await _roleManager.RoleExistsAsync(role);

        //            if (roleExists)
        //            {
        //                await _userManager.AddToRoleAsync(currentUser, role);

        //            }
        //            else
        //            {
        //                return new ResponseMessage { Success = false, Message = "Role does not exist" };
        //            }
        //        }
        //        else
        //        {
        //            return new ResponseMessage { Success = false, Message = "User Not Found" };
        //        }
        //    }


        //    return new ResponseMessage { Success = true, Message = "Successfully Added Role" };
        //}


        //public async Task<ResponseMessage> RevokeRole(UserRoleDto userRole)
        //{
        //    var curentUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(userRole.UserId));

        //    if (curentUser != null)
        //    {
        //        foreach (var role in userRole.RoleName) { 
        //            await _userManager.RemoveFromRoleAsync(curentUser, role);
        //    }
        //        return new ResponseMessage { Success = true, Message = "Succesfully Revoked Roles" };
        //    }
        //    return new ResponseMessage { Success = false, Message = "User Not Found" };

        //}

        //public async Task<ResponseMessage> ChangeStatusOfUser(string userId)
        //{
        //    var curentUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));

        //    if (curentUser != null)
        //    {
        //        curentUser.RowStatus = curentUser.RowStatus == RowStatus.ACTIVE ? RowStatus.INACTIVE : RowStatus.ACTIVE;
        //        await _dbContext.SaveChangesAsync();
        //        return new ResponseMessage { Success = true, Message = "Succesfully Changed Status of User", Data = curentUser.Id };
        //    }
        //    return new ResponseMessage { Success = false, Message = "User Not Found" };
        //}

        //public async Task<ResponseMessage> ChangePassword(ChangePasswordDto model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.UserId);

        //    if (user == null)
        //    {
        //        return new ResponseMessage
        //        {

        //            Success = false,
        //            Message = "User not found."
        //        };
        //    }

        //    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return new ResponseMessage
        //        {
        //            Success = false,
        //            Message = result.Errors.ToString()
        //        };
        //    }

        //    return new ResponseMessage { Message = "Password changed successfully.", Success = true };
        //}
    }
}
