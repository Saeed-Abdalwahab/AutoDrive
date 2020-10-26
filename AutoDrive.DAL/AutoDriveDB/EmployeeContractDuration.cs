using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeContractDuration")]
    public partial class EmployeeContractDuration
    {
        public EmployeeContractDuration()
        {
            EmployeeStatusTransactions = new HashSet<EmployeeStatusTransaction>();
        }
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public double? Duration { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeStatu")]

        public int EmployeeStatusId { get; set; }
        [ForeignKey("EmployeeStatusKind")]

        public int EmployeeStatusKindId { get; set; }

        public virtual EmployeeStatu EmployeeStatu { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual EmployeeStatusKind EmployeeStatusKind { get; set; }

        public virtual ICollection<EmployeeStatusTransaction> EmployeeStatusTransactions { get; set; }
    }
}
