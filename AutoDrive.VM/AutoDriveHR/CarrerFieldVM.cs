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
   public class CarrerFieldVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("NameExist", "CarrerField", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]

        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("ENNameExist", "CarrerField", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]

        [StringLength(50)]
        public string EnName { get; set; }

    }
}
