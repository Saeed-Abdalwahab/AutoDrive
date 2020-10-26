using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeMoney")]
    public partial class EmployeeMoney
    {
        public EmployeeMoney()
        {
            EmployeeMoneyDetails = new HashSet<EmployeeMoneyDetail>();
        }
        [Key]
        public int ID { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmplpoyeeMoneyType")]
        public int EmployeeMoneyTypeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime TransDate { get; set; }

        public double TotalMoney { get; set; }

        public int Status { get; set; }

        public int? InMonth { get; set; }

        public int? InYear { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceiptDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EmplpoyeeMoneyType EmplpoyeeMoneyType { get; set; }

        public virtual ICollection<EmployeeMoneyDetail> EmployeeMoneyDetails { get; set; }
    }
}
