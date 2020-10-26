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
    public class InquiryEmployeeSalaryVM
    {

        [Display(Name = "MonthSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public Months Month { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "YearSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public int Year { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int EmployeeID { get; set; }
    }
}
