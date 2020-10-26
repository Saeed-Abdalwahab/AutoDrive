using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class CourseReservationVM
    {
        public int ID { get; set; }


        public int TraineeId { get; set; }
        public int CodeId { get; set; }
        public int TraineeEvaluationId { get; set; }


        [Display(Name = "Name", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string ArName { get; set; }

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string EnName { get; set; }

        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Code { get; set; }


        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceTypeName { get; set; }
        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceTypeEnName { get; set; }


        [Display(Name = "LicenceCategory", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceCategoryName { get; set; }
        [Display(Name = "LicenceCategory", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceCategoryEnName { get; set; }

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



        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "CourseStartDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string CourseStartDate { get; set; }

        [Display(Name = "CourseEndDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string CourseEndDate { get; set; }

       
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "CourseCost", ResourceType = typeof(AutoDriveResources.Resources))]
        public double CourseCost { get; set; }


        public string CourseReservation_Msg { get; set; }

    }
}
