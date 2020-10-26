using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoDrive.VM.AutoDriveHR
{
  public  class EmployeeDepartMentQulificationsVM
    {
        public EmployeeVM employeeVM { get; set; }
        public EmployeeDepartmentVM EmployeeDepartmentVM { get; set; }
        public EmployeeLicenceDataVM employeeLicenceDataVM { get; set; }

        public EmployeeQualificationVM employeeQualificationVM { get; set; }
        public EmployeeContractDurationVM employeeContractDurationVM { get; set; }
        public EmployeeExperienceVM employeeExperienceVM { get; set; }
        public EmployeeJobDataVM EmployeeJobDataVM { get; set; }
        

    }
}
