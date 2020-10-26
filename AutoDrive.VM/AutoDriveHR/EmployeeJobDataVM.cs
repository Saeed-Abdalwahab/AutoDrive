using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
  public  class EmployeeJobDataVM
    {
        public int ID { get; set; }

        public int EmployeeId { get; set; }
        [Display(Name = "JobDegree", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int JobDegreeId { get; set; }
        [Display(Name = "JobDegree", ResourceType = typeof(Resources))]

        public string JobDegreeName { get; set; }
        [Display(Name = "JobDegree", ResourceType = typeof(Resources))]

        public string JobDegreeEnName { get; set; }

        [Display(Name = "CarrerField", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int CarrerFieldId { get; set; }
        [Display(Name = "CarrerField", ResourceType = typeof(Resources))]

        public string CarrerFieldName { get; set; }
        [Display(Name = "CarrerField", ResourceType = typeof(Resources))]

        public string CarrerFieldEnName { get; set; }

        [Display(Name = "JobLevel", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int JobLevelId { get; set; }
        [Display(Name = "JobLevel", ResourceType = typeof(Resources))]

        public string JobLevelName { get; set; }
        [Display(Name = "JobLevel", ResourceType = typeof(Resources))]

        public string JobLevelEnName { get; set; }

        [Display(Name = "JobName", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int JobNameId { get; set; }
        [Display(Name = "JobName", ResourceType = typeof(Resources))]

        public string JobNameName { get; set; }
        [Display(Name = "JobName", ResourceType = typeof(Resources))]

        public string JobNameEnName { get; set; }
        [Display(Name = "JobFunction", ResourceType = typeof(Resources))]
        public int? JobFunctionId { get; set; }
        [Display(Name = "JobFunction", ResourceType = typeof(Resources))]

        public string JobFunctionName { get; set; }
        [Display(Name = "JobFunction", ResourceType = typeof(Resources))]

        public string JobFunctionEnName { get; set; }

        [Display(Name = "Startdate", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime StartDate { get; set; }
        [Display(Name = "Enddate", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
    }
}
