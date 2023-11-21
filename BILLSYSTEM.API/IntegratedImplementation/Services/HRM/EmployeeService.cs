using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.Configuration;
using IntegratedImplementation.DTOS.HRM;
using IntegratedImplementation.Interfaces.Configuration;
using IntegratedImplementation.Interfaces.HRM;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.Authentication;
using IntegratedInfrustructure.Model.HRM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static IntegratedInfrustructure.Data.EnumList;

namespace IntegratedImplementation.Services.HRM
{
    public class EmployeeService : IEmployeeService
    {

        private readonly DBHumanContext _dbContext;
        private readonly DBGeneralContext _dbGeneralContext;
        //private readonly IGeneralConfigService _generalConfig;
        //private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public EmployeeService(DBHumanContext dbContext,DBGeneralContext dBGeneralContext
           , IMapper mapper)
        {
            _dbContext = dbContext;
            _dbGeneralContext = dBGeneralContext;
            _mapper = mapper;
        }

        public async Task<ResponseMessage> AddEmployee(EmployeePostDto addEmployee)
        {
            var id = Guid.NewGuid();
            var path = "";

            //if (addEmployee.ImagePath != null)
            //    path = _generalConfig.UploadFiles(addEmployee.ImagePath, id.ToString(), "Employee").Result.ToString();


         

            //var code = await _generalConfig.GenerateCode(GeneralCodeType.EMPLOYEEPREFIX);
            addEmployee.EmploymentStatus = EmploymentStatus.ACTIVE.ToString();
            EmployeeList employee = new EmployeeList
            {

            };
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();



            return new ResponseMessage
            {

                Message = "Added Successfully",
                Success = true
            };
        }

        public async Task<ResponseMessage> UpdateEmployee(EmployeeGetDto addEmployee)
        {

            var path = "";

            //if (addEmployee.Image != null)
            //    path = _generalConfig.UploadFiles(addEmployee.Image, addEmployee.Id.ToString(), "Employee").Result.ToString();




            

            var employee = _dbContext.Employees.Find(addEmployee.Id);

            if (employee != null)
            {

                

                await _dbContext.SaveChangesAsync();

                return new ResponseMessage
                {

                    Message = "Updated Successfully",
                    Success = true
                };

            }
            else
            {
                return new ResponseMessage
                {

                    Message = "No employee Found",
                    Success = false
                };
            }
   



        }

        public async Task<List<EmployeeGetDto>> GetEmployees()
        {
            var employeeList = await _dbContext.Employees.AsNoTracking()
                                    .ProjectTo<EmployeeGetDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync();
            return employeeList;
        }

        public async Task<EmployeeGetDto> GetEmployee(Guid employeeId)
        {
            var employee = await _dbContext.Employees

                .Where(x => x.recordno == employeeId)
                .AsNoTracking()
                .ProjectTo<EmployeeGetDto>(_mapper.ConfigurationProvider).FirstAsync();

            return employee;
        }



        public async Task<List<SelectListDto>> GetEmployeeNoUser()
        {
            //var users = _userManager.Users.Select(x => x.EmployeeId).ToList();
            var billsections = await _dbGeneralContext.BillSections.ToListAsync();
            
            var employees = await _dbContext.Employees
               .Where(e => !billsections.Select(x=>x.empID).Contains(e.EmpID))
                .ProjectTo<SelectListDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return employees;
        }
        

      public async  Task<List<SelectListDto>> GetEmployeeSelectList()
        {

            

            var employees = await _dbContext.Employees.ProjectTo<SelectListDto>(_mapper.ConfigurationProvider).ToListAsync();
            
            return employees;
        }

        public async Task<ResponseMessage> changeEmployeeImage(EmployeeImagePostDto addEmployee)
        {

            var path = "";

            //if (addEmployee.Image != null)
            //    path = _generalConfig.UploadFiles(addEmployee.Image, addEmployee.Id.ToString(), "Employee").Result.ToString();
            
            var employee = _dbContext.Employees.Find(addEmployee.Id);
            if (employee != null)
            {
                

                await _dbContext.SaveChangesAsync();

                return new ResponseMessage
                {

                    Message = "Image Updated Successfully",
                    Success = true
                };

            }
            else
            {
                return new ResponseMessage
                {

                    Message = "No employee Found",
                    Success = false
                };
            }

        }
    }
}
