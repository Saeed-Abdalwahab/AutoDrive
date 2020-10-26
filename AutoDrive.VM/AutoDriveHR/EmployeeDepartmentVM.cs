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
   public class EmployeeDepartmentVM
    {
        public int ID { get; set; }
        public int? EmployeeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Department", ResourceType = typeof(Resources))]

        public int DepartmentId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Startdate", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime StartDate { get; set; }

        [Display(Name = "Enddate", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? EndDate { get; set; }
        [Display(Name = "DepartmentName", ResourceType = typeof(Resources))]

        public string DepartmentName { get; set; }  
        [Display(Name = "DepartmentName", ResourceType = typeof(Resources))]
        public string DepartmentEnName { get; set; }

    }
}
