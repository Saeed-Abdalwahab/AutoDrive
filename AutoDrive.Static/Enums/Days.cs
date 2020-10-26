using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum Days
    {
        [Display(Name = "Sat", ResourceType = typeof(AutoDriveResources.Resources))]
        Sat = 6,
        [Display(Name = "Sun", ResourceType = typeof(AutoDriveResources.Resources))]
        Sun = 7,
        [Display(Name = "Mon", ResourceType = typeof(AutoDriveResources.Resources))]
        Mon =1,
        [Display(Name = "Tue", ResourceType = typeof(AutoDriveResources.Resources))]
        Tue =2,
        [Display(Name = "Wed", ResourceType = typeof(AutoDriveResources.Resources))]
        Wed =3,
        [Display(Name = "Thu", ResourceType = typeof(AutoDriveResources.Resources))]
        Thu =4,
        [Display(Name = "Fri", ResourceType = typeof(AutoDriveResources.Resources))]
        Fri =5
    }
}
