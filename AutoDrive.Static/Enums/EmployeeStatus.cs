using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
 public   enum EmployeeStatus
    {
        [Display(Name = "Inservice", ResourceType = typeof(AutoDriveResources.Resources))]

        InService = 1,
        [Display(Name = "EndOfService", ResourceType = typeof(AutoDriveResources.Resources))]
        EndOfService = 3
    }
}
