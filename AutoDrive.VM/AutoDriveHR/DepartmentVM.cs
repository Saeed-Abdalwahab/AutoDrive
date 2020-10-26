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
   public class DepartmentVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("NameExist", "Department", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]

        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("ENNameExist", "Department", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]

        public string EnName { get; set; }
        [Display(Name = "Management", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? ParentId { get; set; }
        [Display(Name = "ManagementName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string ParentName { get; set; }
        [Display(Name = "ManagementEnName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string ParentEnName { get; set; }
    }
}
