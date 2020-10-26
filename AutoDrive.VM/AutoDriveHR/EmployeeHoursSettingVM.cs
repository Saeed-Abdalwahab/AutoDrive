using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
    public class EmployeeHoursSettingVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? EmployeeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "WorkingHours", ResourceType = typeof(AutoDriveResources.Resources))]
        public int WorkingHoursSettingHRId { get; set; }
        [Display(Name = "FromDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime? FromDate { get; set; }
        [Display(Name = "ToDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime? ToDate { get; set; }
        public string EmpName { get; set; }
        public string WorkingHoursName { get; set; }
        public string from { get; set; }
        public string to { get; set; }
    }
}
