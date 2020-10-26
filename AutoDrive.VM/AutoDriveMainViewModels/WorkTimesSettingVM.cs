using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class WorkTimesSettingVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "DisplayDayName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DisplayDayName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "DisplayDayNameEn", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DisplayDayNameEn { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "DayName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DayName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "FromHour", ResourceType = typeof(AutoDriveResources.Resources))]
        public string FromHour { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ToHour", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ToHour { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "NOWorkHours", ResourceType = typeof(AutoDriveResources.Resources))]
        public double? NOWorkHours { get; set; }

        public string Mess { get; set; }
        public bool sat { get; set; } = false;
        public bool sun { get; set; } = false;
        public bool mon { get; set; } = false;
        public bool tue { get; set; } = false;
        public bool wed { get; set; } = false;
        public bool thu { get; set; } = false;
        public bool fri { get; set; } = false;
    }
}
