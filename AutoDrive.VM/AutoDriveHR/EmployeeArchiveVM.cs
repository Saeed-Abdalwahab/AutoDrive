using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveHR
{
    public class EmployeeArchiveVM
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public int ArchiveSettingHRsId { get; set; }

        [Display(Name = "Number", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Number { get; set; }

        [Display(Name = "Notes", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Notes { get; set; }

        [Display(Name = "Date", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime? Date { get; set; }
        public string DateString { get; set; }

        [Display(Name = "ImageName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ImageName { get; set; }

        public string ImageUrl { get; set; }
        public string Imagepath { get; set; }
    }
}
