using IntegratedInfrustructure.Model.Authentication;
using IntegratedInfrustructure.Model.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static IntegratedInfrustructure.Data.EnumList;

namespace IntegratedInfrustructure.Model.HRM
{

    [Table("tbl_EmployeeBasic")]
    public class EmployeeList
    {

        [Key]
        public Guid recordno { get; set; }
        public int? RegFiscalYear { get; set; } 
        public int? RegMonthIndex { get; set; }
        public string? EmpID { get; set; } = null!;
        public string? Title { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? MiddleName { get; set; } = null!;
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? MartalStatus { get; set; }
        public string? BirthDateETH { get; set; }
        public DateTime? BirthDateGRE { get; set; }

        public string? BirthPlaceCountry { get; set; }
        public string? BirthPaceCity { get; set; }
        public string? EthnicGroup { get; set; }
        public string? PositionCode { get; set; }
        public string? CurrentPosition { get; set; }
        public string? JobGrade { get; set; }
        public string? JobTitle { get; set; }
        public string? EducationCode { get; set; }
        public string? EducationLevel { get; set; }
        public string? FieldOfStudy { get; set; }
        public double? Salary { get; set; }
        public string? EmploymentType { get; set; }
        public string? EmployementDT { get; set; }
        public string? BranchId { get; set; }
        public string? DeptId { get; set; }
        public string? DivisionId { get; set; }
        public string? SectionId { get; set; }
        public string? HomeRegion { get; set; }
        public string? HomeZone { get; set; }
        public string? HomeWereda { get; set; }
        public string? HomeCity { get; set; }
        public string? HomeSubCity { get; set; }
        public string? HomeKebele { get; set; }
        public string? HomeHouseno { get; set; }
        public string? HomeVillage { get; set; }
        public string? HomeTelephone { get; set; }
        public string? Mobile { get; set; }
        public string? EmployeeStatus { get; set; }
        public string? Remarks { get; set; }



    }


}
