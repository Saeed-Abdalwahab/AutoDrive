using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDrivePayroll
{
    public class EmployeeLoanVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "Employee", ResourceType = typeof(AutoDriveResources.Resources))]
        public int EmployeeID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "LoanValue", ResourceType = typeof(AutoDriveResources.Resources))]
        public double LoanValue { get; set; }
        [Display(Name = "LoanDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public DateTime? LoanDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "PaymentDateFrom", ResourceType = typeof(AutoDriveResources.Resources))]
        public Months FromMonth { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int FromYear { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "PaymentDateTo", ResourceType = typeof(AutoDriveResources.Resources))]
        public Months ToMonth { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        public int ToYear { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        [Display(Name = "MonthlyValue", ResourceType = typeof(AutoDriveResources.Resources))]
        public double MonthlyValue { get; set; }
        [Display(Name = "LoanStatus", ResourceType = typeof(AutoDriveResources.Resources))]
        public LoanStatus LoanStatus { get; set; }
        [Display(Name = "UnderPaymentOrPaid", ResourceType = typeof(AutoDriveResources.Resources))]
        public bool UnderPaymentOrPaid { get; set; }
        public string EmpName { get; set; }
        public String Status { get; set; }
        public string FromMonthName { get; set; }
        public string ToMonthName { get; set; }
        public string ToYearName { get; set; }
        public string fromYearName { get; set; }
        public string UnderPayORPaidName { get; set; }
        public bool check { get; set; }
    }
}
