using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
    public class EmployeeVacationVM
    {
       
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int EmployeeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "HolidayType", ResourceType = typeof(AutoDriveResources.Resources))]
        public int HolidayTypeId { get; set; }[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "FromDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime FromDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ToDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime ToDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "NODays", ResourceType = typeof(AutoDriveResources.Resources))]
        public int NODays { get; set; }
        [Display(Name = "Notes", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Notes { get; set; }
        public string EmpName { get; set; }
        public string HolidayTypeName { get; set; }
        public bool check { get; set; }
    }
}
