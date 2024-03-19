using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.Configuration;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.DWM;
using IntegratedInfrustructure.Model.SCS;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.DWM
{
    public class MobileUsersService:IMobileUsersService
    {


        private readonly DBCustomerContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IGeneralConfigService _generalConfig;

        public MobileUsersService(DBCustomerContext dbContext, IMapper mapper, IGeneralConfigService generalConfig)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _generalConfig = generalConfig;
        }


        public async Task<List<MobileUsersDto>> GetMobileUsers()
        {

          
            var mobileUsers = await _dbContext.MobileUsers.AsNoTracking()
                                .ProjectTo<MobileUsersDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return mobileUsers;
        }
        public async Task<ResponseMessage> AddMobileUsers(MobileUsersDto addMobileUsers)
        {
            try
            {
                Random random = new Random();
                int min = 100; // Minimum value
                int max = 10000; // Maximum value

                int randomNumber = random.Next(min, max + 1);

                var path = "";

                if (addMobileUsers.Image != null)
                   path = _generalConfig.UploadFiles(addMobileUsers.Image, randomNumber.ToString(), "MobileUsers").Result.ToString();

                MobileUsers mobileUsers = new MobileUsers()
                {
                    //Id = randomNumber,
                    userName = addMobileUsers.userName,
                    passWord = addMobileUsers.passWord,
                    role = addMobileUsers.role,
                    imagePath = path,
                    fullName = addMobileUsers.fullName,
                    phone = addMobileUsers.phone,
                    imei1 = addMobileUsers.imei1,
                    imei2 = addMobileUsers.imei2,
                    isRemoved="0"

                };
                await _dbContext.MobileUsers.AddAsync(mobileUsers);
                await _dbContext.SaveChangesAsync();

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

                    Message = "Cannot insert duplicate key for Input Field Consumption level ",
                    Success = false
                };

            }


        }

        public async Task<ResponseMessage> UpdateMobileUsers(MobileUsersDto updateMobileUsers)
        {
            try
            {
                var currentMobileUsers = await _dbContext.MobileUsers.FindAsync( Int32.Parse(updateMobileUsers.Id));

                var path = "";

                if (updateMobileUsers.Image != null)
                    path = _generalConfig.UploadFiles(updateMobileUsers.Image, updateMobileUsers.Id.ToString(), "MobileUsers").Result.ToString();

                if (currentMobileUsers != null)
                {

                    currentMobileUsers.userName = updateMobileUsers.userName;
                    currentMobileUsers.passWord = updateMobileUsers.passWord;
                    currentMobileUsers.role = updateMobileUsers.role;
                   
                    currentMobileUsers.fullName = updateMobileUsers.fullName;
                    currentMobileUsers.phone = updateMobileUsers.phone;
                    currentMobileUsers.imei1 = updateMobileUsers.imei1;
                    currentMobileUsers.imei2 = updateMobileUsers.imei2;

                    if(updateMobileUsers.Image != null)
                    {
                        currentMobileUsers.imagePath = path;
                    }


                    await _dbContext.SaveChangesAsync();
                    return new ResponseMessage { Message = "Successfully Updated", Success = true };
                }
                return new ResponseMessage { Success = false, Message = "Unable To Find Customer Category" };
            }
            catch (Exception ex)
            {
                return new ResponseMessage
                {

                    Message = "Cannot insert duplicate key for Input Field code ",
                    Success = false
                };

            }

        }
        public async Task<ResponseMessage> DeleteMobileUsers(int MobileUsersId)
        {

            var currentMobileUsers = await _dbContext.MobileUsers.FindAsync(MobileUsersId);

            if (currentMobileUsers != null)
            {

                _dbContext.Remove(currentMobileUsers);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Customer Category" };
        }
    }
}
