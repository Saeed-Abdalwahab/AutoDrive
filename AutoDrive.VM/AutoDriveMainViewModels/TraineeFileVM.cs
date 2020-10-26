using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class TraineeFileVM
    {
        [Key]
        public int ID { get; set; }

        public Guid? TraineeGuid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string ArName { get; set; }

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string EnName { get; set; }

        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "FileNo", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string FileNo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "FileOpenDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string FileOpenDate { get; set; }

        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? LicenceTypeId { get; set; }
        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceTypeName { get; set; }
        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceTypeEnName { get; set; }


        [Display(Name = "LicenceCategory", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? LicenceCategoryId { get; set; }
        [Display(Name = "LicenceCategory", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceCategoryName { get; set; }
        [Display(Name = "LicenceCategory", ResourceType = typeof(AutoDriveResources.Resources))]
        public string LicenceCategoryEnName { get; set; }


        public string objAlreadyFound { get; set; }
        public string objDeleted { get; set; }
    }
}
