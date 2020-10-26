using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class TraineeEvaluationVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Trainee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? TraineeId { get; set; }
        public string TraineeName { get; set; }
        public string TraineeEnName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? LicenceTypeId { get; set; }
        public string LicenceTypeName { get; set; }
        public string LicenceTypeEnName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "LicenceCategory", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? LicenceCategoryId { get; set; }
        public string LicenceCategoryName { get; set; }
        public string LicenceCategoryEnName { get; set; }


        [Display(Name = "StudyPeriodSetting", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? StudyPeriodSettingId { get; set; }

        [Display(Name = "StudyPeriodSettingName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string StudyPeriodSettingName { get; set; }
        [Display(Name = "StudyPeriodSettingEnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string StudyPeriodSettingEnName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EvaluationEmployee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? EvaluationEmployeeId { get; set; }
        public string EvaluationEmployeeName { get; set; }
        public string EvaluationEmployeeEnName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EvaluationDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string EvaluationDate { get; set; }


        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DateOfBirth { get; set; }


        [Display(Name = "TraineeAge", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime? _TraineeAge { get; set; }
        public string TraineeAge { get; set; }

        [Display(Name = "TraineePhotopath", ResourceType = typeof(AutoDriveResources.Resources))]
        public string TraineePhotopath { get; set; }

        public string Ar_msg { get; set; }
        public string En_msg { get; set; }

    }
}
