using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM
{
    public class EmployeeAttendanceVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int EmployeeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "AttendDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime AttendanceDate { get; set; }
        [Display(Name = "AttendanceTime", ResourceType = typeof(AutoDriveResources.Resources))]
        public string AttendanceTime { get; set; }
        [Display(Name = "AttendanceType", ResourceType = typeof(AutoDriveResources.Resources))]
        public AttendanceType AttendanceType { get; set; }
        [Display(Name = "EmpCode", ResourceType = typeof(AutoDriveResources.Resources))]
        public long? Code { get; set; }
        [Display(Name = "EmpDept", ResourceType = typeof(AutoDriveResources.Resources))]
        public string DeptartmentName { get; set; }
        public string EmployeeName { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }

    }
}
