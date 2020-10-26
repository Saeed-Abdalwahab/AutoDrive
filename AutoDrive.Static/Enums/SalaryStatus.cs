using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum SalaryStatus
    {
        [Display(Name = "underRevision", ResourceType = typeof(AutoDriveResources.Resources))]
        underRevision=1,
        [Display(Name = "ApprovalAmount", ResourceType = typeof(AutoDriveResources.Resources))]
        ApprovalAmount =2,
        [Display(Name = "CancelEvent", ResourceType = typeof(AutoDriveResources.Resources))]
        CancelEvent =3,
        [Display(Name = "ReceivableReceived", ResourceType = typeof(AutoDriveResources.Resources))]
        ReceivableReceived =4
    }
}
