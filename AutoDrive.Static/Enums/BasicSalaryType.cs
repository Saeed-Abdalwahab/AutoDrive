using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static
{
    public enum BasicSalaryType
    {
        [Display(Name = "Basic", ResourceType = typeof(AutoDriveResources.Resources))]
        Basic =1,
        [Display(Name = "All", ResourceType = typeof(AutoDriveResources.Resources))]
        All =2
    }
}
