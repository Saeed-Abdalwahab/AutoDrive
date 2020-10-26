using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
  public  enum Gender
    {
        [Display(Name = "Male", ResourceType = typeof(AutoDriveResources.Messages))]
        Male = 1,
        [Display(Name = "Female", ResourceType = typeof(AutoDriveResources.Messages))]
        female = 2
    }
}
