using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class StudyPeriodSettingVM
    {
        public int ID { get; set; }


        public string Name { get; set; }

        public string EnName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "DriveLevelId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int DriveLevelId { get; set; }

        [Display(Name = "DriveLevelName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DriveLevelName { get; set; }

        [Display(Name = "DriveLevelEnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DriveLevelEnName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "VisualStudyCount", ResourceType = typeof(AutoDriveResources.Resources))]
        public int VisualStudyCount { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "VisualStudyCost", ResourceType = typeof(AutoDriveResources.Resources))]
        public double VisualStudyCost { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "VisualStudyTotal", ResourceType = typeof(AutoDriveResources.Resources))]
        public double VisualStudyTotal { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "PracticalCount", ResourceType = typeof(AutoDriveResources.Resources))]
        public int PracticalCount { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "PracticalCost", ResourceType = typeof(AutoDriveResources.Resources))]
        public double PracticalCost { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "PracticalTotal", ResourceType = typeof(AutoDriveResources.Resources))]
        public double PracticalTotal { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "LevelTotal", ResourceType = typeof(AutoDriveResources.Resources))]
        public double LevelTotal { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "LevelStatus", ResourceType = typeof(AutoDriveResources.Resources))]
        public Status LevelStatus { get; set; }

      
        [Display(Name = "LevelStatusName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LevelStatusName { get; set; }

        [Display(Name = "LevelStatusEnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LevelStatusEnName { get; set; }

    }
}
