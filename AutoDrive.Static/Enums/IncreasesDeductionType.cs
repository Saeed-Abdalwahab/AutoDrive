using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum IncreasesDeductionType
    {
        [Display(Name = "Increase", ResourceType = typeof(AutoDriveResources.Resources))]
        Increase =0,
        [Display(Name = "Deduction1", ResourceType = typeof(AutoDriveResources.Resources))]
        Deduction =1
    }
}
