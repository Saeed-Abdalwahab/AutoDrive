using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
    public class ItemIndexVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Name", ResourceType = typeof(Messages))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(Resources))]
        public string EnName { get; set; }

        
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ItemType", ResourceType = typeof(Messages))]
        public ItemTypes ItemType { get; set; }

        [Display(Name = "SerialNumber", ResourceType = typeof(Messages))]
        public string SerialNumber { get; set; }

        [Display(Name = "ItemCode", ResourceType = typeof(Messages))]
        public string ItemCode { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Messages))]
        public string ItemPathImage { get; set; }
        public int? ItemParentId { get; set; }
    }
}
