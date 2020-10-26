using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class ReceiptVoucherVM
    {
        public int ID { get; set; }


        public string ArNameLevelDateCodeId { get; set; }
        public string EnNameLevelDateCodeId { get; set; }
        public int TraineeEvaluationId { get; set; }


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

        [Display(Name = "StudyPeriodSettingEnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string StudyPeriodSettingEnName { get; set; }

        [Display(Name = "LevelTotal", ResourceType = typeof(AutoDriveResources.Resources))]
        public double LevelTotal { get; set; }

        [Display(Name = "VisualStudyCount", ResourceType = typeof(AutoDriveResources.Resources))]
        public int VisualStudyCount { get; set; }

        [Display(Name = "PracticalCount", ResourceType = typeof(AutoDriveResources.Resources))]
        public int PracticalCount { get; set; }



        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "PaidValue", ResourceType = typeof(AutoDriveResources.Resources))]
        public double PaidValue { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "PaidDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string PaidDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ReceiptVoucherCode", ResourceType = typeof(AutoDriveResources.Resources))]
        public int ReceiptVoucherCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ReceiptVoucherDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ReceiptVoucherDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "TraineeId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int TraineeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "CourseReservationId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int CourseReservationId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EmployeeSafeId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? EmployeeSafeId { get; set; }


        public string ReceiptVoucher_Msg { get; set; }
    }
}
