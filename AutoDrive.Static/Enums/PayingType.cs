using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static
{
   public enum PayingType
    {
        [Display(Name = "TheRatio", ResourceType = typeof(AutoDriveResources.Resources))]
        Ratio =1
        ,
        [Display(Name = "TheValue", ResourceType = typeof(AutoDriveResources.Resources))]
        Value =2
    }
}
