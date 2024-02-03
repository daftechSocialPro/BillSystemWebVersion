using AutoMapper;
using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.Configuration;
using IntegratedImplementation.Interfaces.DWM;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.DWM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.DWM
{
    public class DWMMobileService :IDWMMobileService
    {

        private readonly DBCustomerContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IGeneralConfigService _generalConfig;
     
        public DWMMobileService(DBCustomerContext dbContext, IMapper mapper, IGeneralConfigService generalConfig)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _generalConfig = generalConfig;
         
        }


        public async Task<List<ProfileResponseDto>> Login(IEnumerable<MobileUsers> readerCridential)
        {

                List<ProfileResponseDto> responses = new List<ProfileResponseDto>();
            ProfileResponseDto response = new ProfileResponseDto();
                try
                {
                    if (readerCridential.Count() > 0)
                    {
                    MobileUsers incomingUser = readerCridential.FirstOrDefault();
                        List<MobileUsers> usersFromDb = _dbContext.MobileUsers.Where(e => e.userName == incomingUser.userName && e.passWord == incomingUser.passWord && e.role != "Reader" && (e.imei1 == incomingUser.imei1 || e.imei2 == incomingUser.imei2)).ToList();
                        if (usersFromDb.Count() > 0)
                        {
                            response.userName = usersFromDb.FirstOrDefault().userName;
                            response.passWord = usersFromDb.FirstOrDefault().passWord;
                            response.fullName = usersFromDb.FirstOrDefault().fullName;
                            response.role = usersFromDb.FirstOrDefault().role;
                            response.image = "";
                            response.phone = usersFromDb.FirstOrDefault().phone;
                            response.Kebele = usersFromDb.FirstOrDefault().Kebele;
                            response.Ketena = usersFromDb.FirstOrDefault().Ketena;
                            response.IsSuccess = "1";
                            response.Reason = "Update Successfull";
                            responses.Add(response);
                            return responses;
                        }
                        else
                        {
                            response.Id = -1;
                            response.userName = "";
                            response.passWord = "";
                            response.fullName = "";
                            response.role = "";
                            response.image = "";
                            response.phone = "";
                            response.Kebele = "";
                            response.Ketena = "";
                            response.IsSuccess = "0";
                            response.Reason = "Un Authorized User Please Contact System Administrator";
                            responses.Add(response);
                            return responses;
                        }
                    }
                    else
                    {
                        response.Id = -1;
                        response.userName = "";
                        response.passWord = "";
                        response.fullName = "";
                        response.role = "";
                        response.image = "";
                        response.phone = "";
                        response.Kebele = "";
                        response.Ketena = "";
                        response.IsSuccess = "0";
                        response.Reason = "Sending Parameter Miss-Match NULL Value Found";
                        responses.Add(response);
                        return responses;
                    }


                }
                catch (Exception e)
                {
                    response.Id = -1;
                    response.userName = "";
                    response.passWord = "";
                    response.fullName = "";
                    response.role = "";
                    response.image = "";
                    response.phone = "";
                    response.Kebele = "";
                    response.Ketena = "";
                    response.IsSuccess = "0";
                    response.Reason = e.ToString();
                    responses.Add(response);
                    return responses;

                }




            }

        

    private string imageTobasse64(string imageUrl)
    {
        try
        {

                return "";
            }
        catch (Exception)
        {

            return "";
        }
    }
}
}
