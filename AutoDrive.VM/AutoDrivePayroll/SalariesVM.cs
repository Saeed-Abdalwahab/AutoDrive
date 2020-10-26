using AutoDrive.Static.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
    public class SalariesVM
    {
        [Display(Name = "MonthSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public Months Month { get; set; }
        [Display(Name = "YearSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public int Year { get; set; }
    }
}
