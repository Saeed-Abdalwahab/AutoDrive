using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
    public class MotivationEmployeeVM
    {
        public int ID { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MotivationDateRequired")]

        [Display(Name = "MotivationDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime MotivationDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MotivationTypeRequired")]

        [Display(Name = "MotivationType", ResourceType = typeof(AutoDriveResources.Resources))]

        public int MotivationTypeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmployeeRequired")]

        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int EmployeeId { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InMonthRequired")]

        [Display(Name = "InMonth", ResourceType = typeof(AutoDriveResources.Resources))]
        public Months InMonth { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "InYearRequired")]

        [Display(Name = "InYear", ResourceType = typeof(AutoDriveResources.Resources))]
        public int InYear { get; set; }
        
        [Display(Name = "Note", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Note { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MotivationStatusRequired")]

        [Display(Name = "MotivationStatus", ResourceType = typeof(AutoDriveResources.Resources))]
        public MotivationStatus MotivationStatus { get; set; }
       // [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "WithSalaryOrFormRequired")]

        [Display(Name = "WithSalaryOrForm", ResourceType = typeof(AutoDriveResources.Resources))]
        public RewardType WithSalaryOrForm { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ValueRequired")]

        [Display(Name = "Value", ResourceType = typeof(AutoDriveResources.Resources))]
        public double Value { get; set; }

        public string MotivationTypeName { get; set; }
        public string EmployeeName { get; set; }
        public string InMonthName { get; set; }
        public string MotivationStatusName { get; set; }
        public string WithsalaryOrFromName { get; set; }
        public string InyearName { get; set; }
        public bool check { get; set; }
    }
}
