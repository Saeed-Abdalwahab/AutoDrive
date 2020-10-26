using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
    public class SearchedEmployeeSalaryVM
    {
        public string Month { get; set; }
        public int Year { get; set; }
        public string SalaryStatus { get; set; }
        public Dictionary<string, string> IncreaseList{get;set;}
        public Dictionary<string, string> DeductionList { get; set; }
        public double IncreaseTotal { get; set; }
        public double DeductionTotal { get; set;}
        public double Total { get; set; }
        public string DeptName { get; set; }
        public string EmpName { get; set; }
        public string JobName { get; set; }
        public string JobDegreeName { get; set; }
        public string JobLevelName { get; set; }
        public string EmployeeCode { get; set; }
    }
}
