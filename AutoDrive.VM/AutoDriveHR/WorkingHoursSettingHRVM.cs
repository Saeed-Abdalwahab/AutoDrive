using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
    public class WorkingHoursSettingHRVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ArName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string EnName { get; set; }
        [Display(Name = "FromDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime? FromDate { get; set; }
        [Display(Name = "ToDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime? ToDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string WorkingHoursName { get; set; }
    }
}
