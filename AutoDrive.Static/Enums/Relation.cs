using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum Relation
    {
        [Display(Name = "husband", ResourceType = typeof(AutoDriveResources.Resources))]
        husband = 1,

        [Display(Name = "son", ResourceType = typeof(AutoDriveResources.Resources))]
        son = 2,
    }
}
