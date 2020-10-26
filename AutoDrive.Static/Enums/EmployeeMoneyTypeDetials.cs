using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum EmployeeMoneyTypeDetials
    {
        [Display(Name = "Reward", ResourceType = typeof(AutoDriveResources.Resources))]
        Reward =3,
        [Display(Name = "Violation", ResourceType = typeof(AutoDriveResources.Resources))]
        Violation =4,
        [Display(Name = "Salary", ResourceType = typeof(AutoDriveResources.Resources))]
        Basic =6,
        [Display(Name = "ChangedSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        Changed =7,
        [Display(Name = "Loan", ResourceType = typeof(AutoDriveResources.Resources))]
        Loan =8,
        [Display(Name = "Holiday", ResourceType = typeof(AutoDriveResources.Resources))]
        Holiday =9
    }
}
