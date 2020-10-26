using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace AutoDrive.Static.Enums
{
    public enum Months
    {
        [Display(Name = "Jan", ResourceType = typeof(AutoDriveResources.Resources))]
        Jan =1,
        [Display(Name = "Feb", ResourceType = typeof(AutoDriveResources.Resources))]
        Feb =2,
        [Display(Name = "March", ResourceType = typeof(AutoDriveResources.Resources))]
        March =3,
        [Display(Name = "April", ResourceType = typeof(AutoDriveResources.Resources))]
        April =4,
        [Display(Name = "May", ResourceType = typeof(AutoDriveResources.Resources))]
        May =5,
        [Display(Name = "June", ResourceType = typeof(AutoDriveResources.Resources))]
        June =6,
        [Display(Name = "July", ResourceType = typeof(AutoDriveResources.Resources))]
        July =7,
        [Display(Name = "Augst", ResourceType = typeof(AutoDriveResources.Resources))]
        Augst =8,
        [Display(Name = "Sept", ResourceType = typeof(AutoDriveResources.Resources))]
        Sept =9,
        [Display(Name = "Oct", ResourceType = typeof(AutoDriveResources.Resources))]
        Oct =10,
        [Display(Name = "Nov", ResourceType = typeof(AutoDriveResources.Resources))]
        Nov =11,
        [Display(Name = "Dec", ResourceType = typeof(AutoDriveResources.Resources))]
        Dec =12
    }
}
