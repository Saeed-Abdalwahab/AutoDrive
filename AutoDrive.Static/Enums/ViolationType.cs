using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum ViolationType
    {
        [Display(Name = "FromMoney", ResourceType = typeof(AutoDriveResources.Resources))]
        FromMoney =1,
        [Display(Name = "Moral", ResourceType = typeof(AutoDriveResources.Resources))]
        Moral=2
    }
}
