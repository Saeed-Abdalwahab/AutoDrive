using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    public partial class EmployeeStatu
    {
        public EmployeeStatu()
        {
            EmployeeContractDurations = new HashSet<EmployeeContractDuration>();
            EmployeeStatusKinds = new HashSet<EmployeeStatusKind>();
            EmployeeStatusTransactions = new HashSet<EmployeeStatusTransaction>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string EnName { get; set; }

        public virtual ICollection<EmployeeContractDuration> EmployeeContractDurations { get; set; }

        public virtual ICollection<EmployeeStatusKind> EmployeeStatusKinds { get; set; }

        public virtual ICollection<EmployeeStatusTransaction> EmployeeStatusTransactions { get; set; }
    }

}
