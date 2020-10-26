using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
    public class EmployeeViolationVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int EmployeeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "ViolationsType", ResourceType = typeof(AutoDriveResources.Resources))]
        public int ViolationTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "MotivationStatus", ResourceType = typeof(AutoDriveResources.Resources))]

        public ViolationStatus ViolationStatus { get; set; }

        [Display(Name = "FromMonth", ResourceType = typeof(AutoDriveResources.Resources))]

        public Months FromMonth { get; set; }
        [Display(Name = "FromYear", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? FromYear { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "ViolationDate", ResourceType = typeof(AutoDriveResources.Resources))]

        public DateTime ViolationDate { get; set; }
         [Display(Name = "Value", ResourceType = typeof(AutoDriveResources.Resources))]

        public double? ViolationValue { get; set; }
        public string  EmpName { get; set; }
        public string ViolationTypeName{ get; set; }
        public string FromMonthName { get; set; }
        public string  Val { get; set; }
        public string FromYearName { get; set; }
        public string  Status { get; set; }
        public bool check { get; set; }
    }
}
