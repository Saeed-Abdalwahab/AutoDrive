using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
    public class VacationDeductionFiltersVM
    {
        [Display(Name = "MonthSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public Months Month { get; set; }
        [Display(Name = "YearSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public int Year { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "HolidayType", ResourceType = typeof(AutoDriveResources.Resources))]
        public int HolidayTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "ViewVacation", ResourceType = typeof(AutoDriveResources.Resources))]
        public int ViewVacationId { get; set; }
        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? EmployeeId { get; set; }
        public string EmpName { get; set; }
    }
}
