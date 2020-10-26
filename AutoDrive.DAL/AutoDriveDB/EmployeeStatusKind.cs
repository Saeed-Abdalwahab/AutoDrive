using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeStatusKind")]
    public partial class EmployeeStatusKind
    {
        public EmployeeStatusKind()
        {
            EmployeeContractDurations = new HashSet<EmployeeContractDuration>();
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
        [ForeignKey("EmployeeStatu")]
        public int EmployeeStatusId { get; set; }

        public virtual ICollection<EmployeeContractDuration> EmployeeContractDurations { get; set; }

        public virtual EmployeeStatu EmployeeStatu { get; set; }

        public virtual ICollection<EmployeeStatusTransaction> EmployeeStatusTransactions { get; set; }
    }
}
