using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum Status
    {



        [Display(Name = "Active" ,ResourceType = typeof(AutoDriveResources.Messages))]
        Active = 1,

        [Display(Name = "NotActive", ResourceType = typeof(AutoDriveResources.Messages))]
        NotActive = 2

    }
}





    

