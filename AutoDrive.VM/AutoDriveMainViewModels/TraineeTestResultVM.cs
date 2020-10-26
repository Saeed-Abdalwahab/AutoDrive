using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class TraineeTestResultVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int TraineeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int TraineeEvaluationId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public string TestDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int TestId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int TestResultId { get; set; }


        [Display(Name = "TraineeGuid", ResourceType = typeof(AutoDriveResources.Resources))]
        public Guid? TraineeGuid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string ArName { get; set; }

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string EnName { get; set; }

        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Code { get; set; }


        [Display(Name = "FileNo", ResourceType = typeof(AutoDriveResources.Resources))]
        public string FileNo { get; set; }


        [Display(Name = "StudyPeriodSettingName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string StudyPeriodSettingName { get; set; }

        [Display(Name = "StudyPeriodSettingName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string StudyPeriodSettingEnName { get; set; }


        [Display(Name = "VisualStudyCount", ResourceType = typeof(AutoDriveResources.Resources))]
        public int VisualStudyCount { get; set; }



        [Display(Name = "PracticalCount", ResourceType = typeof(AutoDriveResources.Resources))]
        public int PracticalCount { get; set; }


        public string ArTestId { get; set; }
        public string EnTestId { get; set; }

        public string ArTestResultId { get; set; }
        public string EnTestResultId { get; set; }


        public string ArDay { get; set; }
        public string EnDay { get; set; }
    }
}
