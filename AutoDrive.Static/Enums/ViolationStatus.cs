using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum ViolationStatus
    {
        [Display(Name = "underRevision", ResourceType = typeof(AutoDriveResources.Resources))]
        underRevision = 1,
        [Display(Name = "ApprovalViolation", ResourceType = typeof(AutoDriveResources.Resources))]
        Approval = 2
    }
}
