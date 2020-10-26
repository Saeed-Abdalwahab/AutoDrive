using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class LicenceCategoryVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Name", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string EnName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "LicenceTypeId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int LicenceTypeId { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        //[Display(Name = "LicenceTypeName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceTypeName { get; set; }

        public string LicenceTypeEnName { get; set; }

    }
}
