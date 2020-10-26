using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
    public class EmployeeFamilyVM
    {
        public int ID { get; set; }

        public int EmployeeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(Messages))]
        public string ArName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(Messages))]
        public string EnName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Gender", ResourceType = typeof(Messages))]
        public Gender Gender { get; set; }
        public string Genderid { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Relation", ResourceType = typeof(Messages))]
        public Relation Relation { get; set; }
        public string Relationid { get; set; }

        [Display(Name = "Note", ResourceType = typeof(Messages))]
        public string Note { get; set; }
    }
}
