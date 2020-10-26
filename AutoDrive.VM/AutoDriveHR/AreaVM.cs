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
  public  class AreaVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(Resources))]
        [Remote("NameExist", "Area",AdditionalFields ="ID",ErrorMessageResourceType =typeof(Messages),ErrorMessageResourceName = "NameAlreadyExist")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(Resources))]
        [Remote("ENNameExist", "Area", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]
        public string EnName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Country", ResourceType = typeof(AutoDriveResources.Resources))]

        public int CountryId { get; set; }
        [Display(Name = "CountryName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string CountryName { get; set; }
        [Display(Name = "CountryEnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string CountryEnName { get; set; }
    }
}
