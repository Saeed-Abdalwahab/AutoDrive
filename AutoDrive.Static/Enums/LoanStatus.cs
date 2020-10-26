using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutoDrive.Static.Enums
{
    public enum LoanStatus
    {
        [Display(Name = "underRevision", ResourceType = typeof(AutoDriveResources.Resources))]
        underRevision = 1,
        [Display(Name = "ApprovalLoan", ResourceType = typeof(AutoDriveResources.Resources))]
        Approval = 2,
    }
}
