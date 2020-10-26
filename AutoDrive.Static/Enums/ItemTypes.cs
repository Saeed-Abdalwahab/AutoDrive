using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum ItemTypes
    {
        [Display(Name = "ClassMain", ResourceType = typeof(AutoDriveResources.Resources))]
        ClassMain = 1,

        [Display(Name = "Class", ResourceType = typeof(AutoDriveResources.Resources))]
        Class = 2,
    }
}
