using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum AttendanceType
    {
        [Display(Name = "Attendance", ResourceType = typeof(AutoDriveResources.Resources))]
        Attendance =1,
        [Display(Name = "Departure", ResourceType = typeof(AutoDriveResources.Resources))]
        Departure =2
    }
}
