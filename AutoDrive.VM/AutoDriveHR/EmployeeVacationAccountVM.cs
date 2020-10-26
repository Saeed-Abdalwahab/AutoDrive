using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
    public class EmployeeVacationAccountVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int EmployeeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "HolidayType", ResourceType = typeof(AutoDriveResources.Resources))]
        public int HolidayTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "TheYear", ResourceType = typeof(AutoDriveResources.Resources))]
        public int Year { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "DaysNumberVacations", ResourceType = typeof(AutoDriveResources.Resources))]
        public int DaysNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "FromDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ToDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime EndDate { get; set; }
        public string EmpName { get; set; }
        public string HolidayTypeName { get; set; }
    }
}
