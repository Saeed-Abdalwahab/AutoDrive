using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum SalariesStatus
    {
        [Display(Name = "underRevision", ResourceType = typeof(AutoDriveResources.Resources))]
        underRevision = 1,
        [Display(Name = "ApprovalSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        Approval = 2,
        [Display(Name = "AllStatus", ResourceType = typeof(AutoDriveResources.Resources))]
        All =3
    }
}
