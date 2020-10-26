using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class TraineeVM
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Name", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string ArName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string EnName { get; set; }

        [Display(Name = "TraineeGuid", ResourceType = typeof(AutoDriveResources.Resources))]
        public Guid? TraineeGuid { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Nationality", ResourceType = typeof(AutoDriveResources.Resources))]
        public int NationalityId { get; set; }

        [Display(Name = "Religion", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? ReligionId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "DateOfBirth", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DateOfBirth { get; set; }

        [Display(Name = "NationalId", ResourceType = typeof(AutoDriveResources.Resources))]
        public long? NationalId { get; set; }

        [Display(Name = "NationalType", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? NationalTypeId { get; set; }

        [Display(Name = "Residence", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Residence { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "FileNo", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string FileNo { get; set; }

        [Display(Name = "FileOpenDate", ResourceType = typeof(AutoDriveResources.Resources))]
        [Column(TypeName = "date")]
        public string FileOpenDate { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Gender", ResourceType = typeof(AutoDriveResources.Resources))]
        public Gender Gender { get; set; }
        public Gender GenderId { get; set; }

        [Display(Name = "SpeechLanguageId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? SpeechLanguageId { get; set; }

        [Display(Name = "Photopath", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Photopath { get; set; }


        public string Ar_msg { get; set; }
        public string En_msg { get; set; }

    }
}
