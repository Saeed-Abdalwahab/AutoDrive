using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{

    [Table("EmployeeLoan")]
    public partial class EmployeeLoan
    {
      


        public int ID { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }

        public double LoanValue { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LoanDate { get; set; }

        public int FromMonth { get; set; }

        public int FromYear { get; set; }

        public int ToMonth { get; set; }

        public int ToYear { get; set; }

        public double MonthlyValue { get; set; }

        public int LoanStatus { get; set; }

        public bool UnderPaymentOrPaid { get; set; }

        public virtual Employee Employee { get; set; }


    }
}
