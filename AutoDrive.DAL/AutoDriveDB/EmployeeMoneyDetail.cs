using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    public partial class EmployeeMoneyDetail
        
    {
        public EmployeeMoneyDetail()
        {
            employeeVacations = new HashSet<EmployeeVacation>();

        }
        [Key]
        public int ID { get; set; }

        public int EmployeeMoneyId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeMoneyTypeDetail")]
        public int? EmployeeMoneyTypeDetailsId { get; set; }
        [ForeignKey("IncreasesDeductionsSetting")]
        public int? IncreasesDeductionsSettingId { get; set; }

        public double Value { get; set; }

        public int? FromTableId { get; set; }

        [StringLength(500)]
        public string FromTableName { get; set; }

        [Required]
        [StringLength(50)]
        public string OperationType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EmployeeMoney EmployeeMoney { get; set; }
        public virtual IncreasesDeductionsSetting IncreasesDeductionsSetting { get; set; }

        public virtual EmployeeMoneyTypeDetail EmployeeMoneyTypeDetail { get; set; }
        public ICollection<EmployeeVacation> employeeVacations { get; set; }
    }
}
