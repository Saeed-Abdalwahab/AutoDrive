using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeStatusTransaction")]
    public partial class EmployeeStatusTransaction
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Employee")]

        public int EmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        [ForeignKey("EmployeeStatu")]
        public int EmployeeStatusId { get; set; }

        [ForeignKey("EmployeeStatusKind")]
        public int EmployeeStatusKindId { get; set; }
        [ForeignKey("EmployeeContractDuration")]
        public int? EmployeeContractDurationId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EmployeeContractDuration EmployeeContractDuration { get; set; }

        public virtual EmployeeStatu EmployeeStatu { get; set; }

        public virtual EmployeeStatusKind EmployeeStatusKind { get; set; }
    }
}
