using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
 public   class EmployeeLicenceDataVM
    {
        public int ID { get; set; }
        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int LicenceTypeHRId { get; set; }
        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]

        public string LicenceTypeHRName { get; set; }
        [Display(Name = "LicenceType", ResourceType = typeof(AutoDriveResources.Resources))]

        public string LicenceTypeHREnName { get; set; }
        [Display(Name = "LicenceKind", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? LicenceKindHRId { get; set; }
        [Display(Name = "LicenceKind", ResourceType = typeof(AutoDriveResources.Resources))]

        public string LicenceKindHRName { get; set; }
        [Display(Name = "LicenceKind", ResourceType = typeof(AutoDriveResources.Resources))]

        public string LicenceKindHREnName { get; set; }

        [StringLength(250)]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "LicenseNumber", ResourceType = typeof(AutoDriveResources.Resources))]

        public string LicenseNumber { get; set; }

        [Display(Name = "ReleaseDate", ResourceType = typeof(AutoDriveResources.Resources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Enddate", ResourceType = typeof(AutoDriveResources.Resources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? EndDate { get; set; }
        [Display(Name = "SourceArea", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? SourceAreaId { get; set; }
        [Display(Name = "SourceArea", ResourceType = typeof(AutoDriveResources.Resources))]

        public string SourceAreaEnName { get; set; }
        [Display(Name = "SourceArea", ResourceType = typeof(AutoDriveResources.Resources))]

        public string SourceAreaName { get; set; }
        public int EmployeeId { get; set; }

    }
}
