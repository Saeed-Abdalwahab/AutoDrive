using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutoDrive.VM.AutoDriveHR
{
   public class JobFunctionVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(Resources))]
        [Remote("NameExist", "JobFunction", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]


        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(Resources))]
        [Remote("ENNameExist", "JobFunction", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]


        public string EnName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "JobName", ResourceType = typeof(AutoDriveResources.Resources))]

        public int JobNameId { get; set; }
        [Display(Name = "JobName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string JobNameName { get; set; }
        [Display(Name = "JobName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string JobNameEnName { get; set; }
    }
}
