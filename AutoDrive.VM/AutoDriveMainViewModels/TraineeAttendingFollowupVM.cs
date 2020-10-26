using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class TraineeAttendingFollowupVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "TraineeAttendanceId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int TraineeAttendanceId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "AttendanceDatetime", ResourceType = typeof(AutoDriveResources.Resources))]
        public string AttendanceDatetime { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "LeaveDateTime", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LeaveDateTime { get; set; }
      

        public string ArTraineeAttendance { get; set; }
        public string EnTraineeAttendance { get; set; }
        public string Day_ofWeek { get; set; }


        [Display(Name = "TraineeId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int TraineeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "AttendanceDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string AttendanceDate { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "PracticalOrVisual", ResourceType = typeof(AutoDriveResources.Resources))]
        public string PracticalOrVisual { get; set; }

        [Display(Name = "PracticalOrVisual", ResourceType = typeof(AutoDriveResources.Resources))]
        public string PracticalOrVisualEn { get; set; }

        [Display(Name = "TraineeEvaluationId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int TraineeEvaluationId { get; set; }


        public string ArAttendanceOrAbsence { get; set; }
        public string EnAttendanceOrAbsence { get; set; }


        [Display(Name = "Name", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string ArName { get; set; }

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string EnName { get; set; }

        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "TraineeGuid", ResourceType = typeof(AutoDriveResources.Resources))]
        public Guid? TraineeGuid { get; set; }

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




        [Display(Name = "CourseStartDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string CourseStartDate { get; set; }

        [Display(Name = "CourseEndDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string CourseEndDate { get; set; }


        public string Visualdays { get; set; }
        public string Practicaldays { get; set; }

        [Display(Name = "DaysWork", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DaysWork { get; set; }


    }
}
