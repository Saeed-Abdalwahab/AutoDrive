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
  public  class CertificationSpecificVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("NameExist", "CertificationSpecific", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]

        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("ENNameExist", "CertificationSpecific", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]

        public string EnName { get; set; }
        [Display(Name = "CetificationType", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? CertificationTypeID { get; set; }
        [Display(Name = "CertificationTypeName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationTypeName { get; set; }
        [Display(Name = "CertificationTypeEnName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationTypeEnName { get; set; }
    }
}
