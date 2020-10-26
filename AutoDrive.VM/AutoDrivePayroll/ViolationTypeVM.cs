using AutoDrive.Static;
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
    public class ViolationTypeVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string EnName { get; set; }
    
        [Display(Name = "DaysNumber", ResourceType = typeof(AutoDriveResources.Resources))]
        public double? DaysNumber { get; set; }
        [Display(Name = "FromBaseSalaryOrOverall", ResourceType = typeof(AutoDriveResources.Resources))]
        public BasicORAllType FromBaseSalaryOrOverall { get; set; }
        [Display(Name = "FromMoneyOrMoral", ResourceType = typeof(AutoDriveResources.Resources))]
        public ViolationType FromMoneyOrMoral { get; set; }
        public string FromMoneyORMoralName { get; set; }
        public string BasicSalaryOROverall { get; set; }
        public string  DaysNum { get; set; }
    }
}
