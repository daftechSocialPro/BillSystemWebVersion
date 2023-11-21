using IntegratedInfrustructure.Model.HRM;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IntegratedInfrustructure.Data.EnumList;

namespace IntegratedImplementation.DTOS.HRM
{
    public record EmployeePostDto
    {

     
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public IFormFile? ImagePath { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string Gender { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public DateTime EmploymentDate { get; set; }

        public DateTime? TerminatedDate { get; set; }

        public string? EmploymentStatus { get; set; } 

        public string EmploymentPosition { get; set; } = null!;

        public string CreatedById { get; set; } = null!;


    }

    public class EmployeeGetDto
    {
       
        public Guid Id { get; set; }
        public string? EmployeeCode { get; set; } 

        public string ? EmployeeName { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IFormFile? Image{ get; set; } = null!;
        public string? ImagePath { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string Gender { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        public DateTime EmploymentDate { get; set; }

        public DateTime? TerminatedDate { get; set; }

        public string? EmploymentStatus { get; set; }

        public string EmploymentPosition { get; set; } = null!;

  

    }

    public class EmployeeImagePostDto
    {
        public Guid Id { get; set; }
        public IFormFile? Image { get; set; } = null!;
        public string? ImagePath { get; set; } = null!;

    }


}
