using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.Static;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM
{ 
   public class IncreasingDeductionSettingVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedName")]

        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(250)]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedENName")]

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string EnName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredValue")]

        [Display(Name = "Value", ResourceType = typeof(AutoDriveResources.Resources))]

        public double Value { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredRatio")]

        [Display(Name = "Ratio", ResourceType = typeof(AutoDriveResources.Resources))]

        public PayingType RatioOrValue { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredBasicSalary")]

        [Display(Name = "BasicSalary", ResourceType = typeof(AutoDriveResources.Resources))]

        public BasicSalaryType BasicSalaryOrAll { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RequiredDeduction")]

        [Display(Name = "Deduction", ResourceType = typeof(AutoDriveResources.Resources))]

        public int IncreasesDeductionsTypeId { get; set; }

        public string IncreasesDeductionsType { get; set; }
        public string RatioORValueName { get; set; }
        public string BasicSalaryORALLName { get; set; }
        public int IncreaseORDeduction { get; set; }
      
    }
}
