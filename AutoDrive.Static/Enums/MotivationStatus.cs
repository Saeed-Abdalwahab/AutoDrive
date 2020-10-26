using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum MotivationStatus
    {
        [Display(Name = "underRevision", ResourceType = typeof(AutoDriveResources.Resources))]
        underRevision = 1,
        [Display(Name = "ApprovalReward", ResourceType = typeof(AutoDriveResources.Resources))]
        Approval= 2
    }
}
