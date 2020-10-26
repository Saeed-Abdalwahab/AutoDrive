using AutoDrive.Static.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
    public class VacationDeductionVM
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public string DaysDate { get; set; }
        public int DaysNumber { get; set; }
        public int HolidayTypeId { get; set; }
        public double DeductionValue { get; set; }
        public int DeductionFromMonth { get; set; }
        public int DeductionFromYear { get; set; }
        public string DeptName { get; set; }
        public string EmpName { get; set; }
        public string HolidayName { get; set; }
        public int FirstMonth { get; set; }
        public int FirstYear { get; set; }
        public string MonthName { get; set; }
    }
}
