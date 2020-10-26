using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
   public  class UnderPaymentEmployeeLoanVM
    {
        public string EmpName { get; set; }
        public string JobDegree { get; set; }
        public string JobLevel { get; set; }
        public string DeptName { get; set; }
        public string LoanValue { get; set; }
        public string TotalPaid { get; set; }
        public string LoanStatus { get; set; }
        public int DegreeSort { get; set; }
        public int LevelSort { get; set; }
        public int EmloyeeLoanID { get; set; }
    }
}
