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
    public class IncreasesDeductionTypeVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedName")]

        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedENName")]

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string EnName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedIncreasingDeductionType")]

        [Display(Name = "IncreasingORdeduction", ResourceType = typeof(AutoDriveResources.Resources))]
        public IncreasesDeductionType IncreasesOrDeductions { get; set; }
        public string IncreasesOrDeductionsName { get; set; }
    }
}
