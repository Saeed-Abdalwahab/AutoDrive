using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum RewardType
    {
        [Display(Name = "WithSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        WithSalary =1,
        [Display(Name = "Form", ResourceType = typeof(AutoDriveResources.Resources))]
        Form =2
    }
}
