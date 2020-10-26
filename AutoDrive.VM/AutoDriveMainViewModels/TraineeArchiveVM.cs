using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class TraineeArchiveVM
    {
        public int ID { get; set; }

        public int ArchiveSettingDriveId { get; set; }

        public int TraineeId { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Note { get; set; }

        [Display(Name = "Number", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Number { get; set; }

        [Display(Name = "Date", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Date { get; set; }

        [Display(Name = "ImageName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ImageName { get; set; }


        //---------------------------------------------------
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("NameExist", "ArchiveSettingDrive", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]
        public string Name { get; set; }


        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [Remote("ENNameExist", "ArchiveSettingDrive", AdditionalFields = "ID", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NameAlreadyExist")]
        public string EnName { get; set; }


        [Display(Name = "Item", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? ParentId { get; set; }


        [Display(Name = "ItemName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ParentName { get; set; }


        [Display(Name = "ItemEnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ParentEnName { get; set; }


        [Display(Name = "TraineeGuid", ResourceType = typeof(AutoDriveResources.Resources))]
        public Guid? TraineeGuid { get; set; }


        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public int CodeId { get; set; }

        [Display(Name = "Name", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string Ar_Name { get; set; }

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string En_Name { get; set; }

        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Code { get; set; }

        [Display(Name = "FileNo", ResourceType = typeof(AutoDriveResources.Resources))]
        public string FileNo { get; set; }




        public string Ar_msg { get; set; }
        public string En_msg { get; set; }


        public string ImgVirtualPath { get; set; }
    }
}
