using AutoDrive.DAL.AutoDriveDB.HR;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
    public class HonestyVM
    {
        public int ID { get; set; }

       
        public int EmployeeId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ItemId", ResourceType = typeof(Messages))]
        public int ItemId { get; set; }

        [Display(Name = "HonestyDate", ResourceType = typeof(Messages))]
        public DateTime? HonestyDate { get; set; }
        public string HonestyDatestring { get; set; }

        [Display(Name = "honestyEndDate", ResourceType = typeof(Messages))]
        public DateTime? honestyEndDate { get; set; }

        public string ItemName { get; set; }

        public List<Item> Items { get; set; }

        public string ItemCode { get; set; }

        public string ImageNme { get; set; }

        public string ImageUrl { get; set; }

    }
}
