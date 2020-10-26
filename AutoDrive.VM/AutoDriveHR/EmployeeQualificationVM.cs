using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
   public class EmployeeQualificationVM
    {
        public int ID { get; set; }
        [Display(Name = "CetificationType", ResourceType = typeof(AutoDriveResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int CetificationTypeId { get; set; }
        [Display(Name = "CetificationType", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationTypeName { get; set; }
        [Display(Name = "CetificationType", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationTypeEnName { get; set; }

        [Display(Name = "CertificationSpecific", ResourceType = typeof(AutoDriveResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int CertificationSpecificId { get; set; }
        [Display(Name = "CertificationSpecific", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationSpecificName { get; set; }
        [Display(Name = "CertificationSpecific", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationSpecificEnName { get; set; }
        [Display(Name = "CertificationSpecDepart", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? CertificationSpecDepartId { get; set; }
        [Display(Name = "CertificationSpecDepart", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationSpecDepartName { get; set; }
        [Display(Name = "CertificationSpecDepart", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationSpecDepartEnName { get; set; }


        [Display(Name = "CertificationGrade", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? CertificationGradeId { get; set; }
        [Display(Name = "CertificationGrade", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationGradeName { get; set; }
        [Display(Name = "CertificationGrade", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationGradeENName { get; set; }



        [Display(Name = "QualificationDiscribtion", ResourceType = typeof(AutoDriveResources.Resources))]

        public string QualificationDiscribtion { get; set; }

        [Display(Name = "GraduationYear", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? GraduationYear { get; set; }
        [Display(Name = "GraduationMonth", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? GraduationMonth { get; set; }

        public int? EmployeeId { get; set; }
    }
}
