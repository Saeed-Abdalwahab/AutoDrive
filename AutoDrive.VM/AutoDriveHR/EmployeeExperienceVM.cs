using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
   public class EmployeeExperienceVM
    {
        public int ID { get; set; }
        [Display(Name = "FromMonth", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int FromMonth { get; set; }
        [Display(Name = "FromYear", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        public int FromYear { get; set; }
        [Display(Name = "ToMonth", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        public int ToMonth { get; set; }
        [Display(Name = "ToYear", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        public int ToYear { get; set; }
        [Display(Name = "CompanyName", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        public string CompanyName { get; set; }
        [Display(Name = "JobSpecification", ResourceType = typeof(Resources))]

        public string JobSpecification { get; set; }
        [Display(Name = "CompanyAddress", ResourceType = typeof(Resources))]

        public string CompanyAddress { get; set; }

        public int? EmployeeId { get; set; }

    }
}
