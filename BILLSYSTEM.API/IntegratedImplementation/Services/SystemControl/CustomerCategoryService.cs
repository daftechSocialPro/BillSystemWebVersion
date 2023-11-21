using AutoMapper;
using AutoMapper.QueryableExtensions;
using Implementation.Helper;
using IntegratedImplementation.DTOS.SystemControl;
using IntegratedImplementation.Interfaces.SystemControl;
using IntegratedInfrustructure.Data;
using IntegratedInfrustructure.Model.SRC;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedImplementation.Services.SystemControl
{
    public class CustomerCategoryService : ICustomerCategoryService
    {


        private readonly DBGeneralContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerCategoryService(DBGeneralContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<List<CustomerCategoryDto>> GetCustomerCategory()
        {
            var employeeHistories = await _dbContext.CustomerCategories.AsNoTracking()
                                .ProjectTo<CustomerCategoryDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
            return employeeHistories;
        }
        public async Task<ResponseMessage> AddCustomerCategory(CustomerCategoryDto addCustomerCategory)
        {
            try
            {

                CustomerCategory CustomerCategory = new CustomerCategory()
                {
                    recordno = addCustomerCategory.recordno,
                    custCategoryCode = addCustomerCategory.custCategoryCode,
                    custCategoryName = addCustomerCategory.custCategoryName,        



                };
                await _dbContext.CustomerCategories.AddAsync(CustomerCategory);
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

                    Message = "Cannot insert duplicate key for Input Field code ",
                    Success = false
                };

            }


        }
        public async Task<ResponseMessage> UpdateCustomerCategory(CustomerCategoryDto updateCustomerCategory)
        {
            try
            {
                var currentCustomerCategory = await _dbContext.CustomerCategories.FirstOrDefaultAsync(x => x.recordno.Equals(updateCustomerCategory.recordno));

                if (currentCustomerCategory != null)
                {

                    currentCustomerCategory.custCategoryCode = updateCustomerCategory.custCategoryCode;
                    currentCustomerCategory.custCategoryName = updateCustomerCategory.custCategoryName;



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
        public async Task<ResponseMessage> DeleteCustomerCategory(int CustomerCategoryId)
        {

            var currentCustomerCategory = await _dbContext.CustomerCategories.FindAsync(CustomerCategoryId);

            if (currentCustomerCategory != null)
            {

                _dbContext.Remove(currentCustomerCategory);
                await _dbContext.SaveChangesAsync();
                return new ResponseMessage { Message = "Successfully Deleted", Success = true };
            }
            return new ResponseMessage { Success = false, Message = "Unable To Find Customer Category" };
        }

    }
}

