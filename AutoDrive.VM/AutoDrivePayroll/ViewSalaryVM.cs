using AutoDrive.Static.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
    public class ViewSalaryVM
    {
        [Display(Name = "MonthSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public Months Month { get; set; }
        [Display(Name = "YearSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public int Year { get; set; }
        [Display(Name = "SalaryStatus", ResourceType = typeof(AutoDriveResources.Resources))]
        public SalariesStatus SalaryStatus { get; set; }
        public String ManagementName { get; set; }
        public string JobDegree { get; set; }
        public string JobLevel{ get; set; }
        public string currentMonth { get; set; }
        public string PerviousMonth { get; set; }
        public String Employeename { get; set; }
        public string StatusName { get; set; }
        public int EmpID { get; set; }
        public int EmployeeMoneyID { get; set; }
        public bool check { get; set; }
        public int LevelSort { get; set; }
        public int DegreeSort { get; set; }

    }
}
