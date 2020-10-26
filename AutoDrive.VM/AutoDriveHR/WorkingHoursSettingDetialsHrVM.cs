using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
   public  class WorkingHoursSettingDetialsHrVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "WorkingHours", ResourceType = typeof(AutoDriveResources.Resources))]
        public int WorkingHoursSettingHRId { get; set; }
       // [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "FromTime", ResourceType = typeof(AutoDriveResources.Resources))]
        public string FromTime { get; set; }
       // [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "ToTime", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ToTime { get; set; }
       // [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "Day", ResourceType = typeof(AutoDriveResources.Resources))]
        public Days DayName { get; set; }
        public string DisplayArDayName { get; set; }
        public string DisplayEnDayName { get; set; }
        public string Day { get; set; }
        public string WorkingHoursName { get; set; }
    }
}
