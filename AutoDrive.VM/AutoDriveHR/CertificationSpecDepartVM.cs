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
  public  class CertificationSpecDepartVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("NameExist", "CertificationSpecDepart", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]


        [StringLength(50)]
        public string Name { get; set; }
        [Display(Name = "CertificationSpecific", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? CertificationSpecificID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("ENNameExist", "CertificationSpecDepart", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]

        [StringLength(50)]
        public string EnName { get; set; }
        [Display(Name = "CertificationSpecificeName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationSpecificName { get; set; }
        [Display(Name = "CertificationSpecificeEnName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CertificationSpecificEnName { get; set; }
    }
}
