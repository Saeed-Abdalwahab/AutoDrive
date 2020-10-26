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
   public class EmployeeContractDurationVM
    {

        public int ID { get; set; }
        [Display(Name = "Startdate", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime FromDate { get; set; }
        [Display(Name = "Enddate", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime EndDate { get; set; }
        [Display(Name = "Duration", ResourceType = typeof(Resources))]

        public double? Duration { get; set; }

        public int EmployeeId { get; set; }
        [Display(Name = "EmployeeStatus", ResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        public int EmployeeStatusId { get; set; }
        [Display(Name = "EmployeeStatus", ResourceType = typeof(Resources))]

        public string EmployeeStatusName { get; set; }
        [Display(Name = "EmployeeStatus", ResourceType = typeof(Resources))]

        public string EmployeeStatusEnName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "EmployeeStatus", ResourceType = typeof(Resources))]
        public int EmployeeStatusKindId { get; set; }
        [Display(Name = "EmployeeStatus", ResourceType = typeof(Resources))]

        public string EmployeeStatusKindName { get; set; }
        [Display(Name = "EmployeeStatus", ResourceType = typeof(Resources))]

        public string EmployeeStatusKindEnName { get; set; }
    }
}
