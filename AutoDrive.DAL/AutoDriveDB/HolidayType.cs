using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
        [Table("HolidayType")]
    public partial class HolidayType
    { 
        public HolidayType()
        {
            EmployeeVacationAccounts = new HashSet<EmployeeVacationAccount>();
            EmployeeVacations = new HashSet<EmployeeVacation>();
            VacationDeductions = new HashSet<VacationDeduction>();
        }
        public int ID { get; set; }
        [Required]
        [StringLength(250)]
        public string EnName { get; set; }
        [StringLength(250)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<EmployeeVacationAccount> EmployeeVacationAccounts { get; set; }
        public virtual ICollection<EmployeeVacation> EmployeeVacations { get; set; }
        public virtual ICollection<VacationDeduction> VacationDeductions { get; set; }

    }
}
