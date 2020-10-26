using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeVacation")]
    public partial class EmployeeVacation
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        public virtual Employee employee { get; set; }
        [ForeignKey("VacationDeduction")]
        public int? VacationDedutionId { get; set; }
        public VacationDeduction VacationDeduction { get; set; }

        [ForeignKey("EmployeeMoneyDetail")]
        public int? EmployeeMoneyDetailsId { get; set; }
        public EmployeeMoneyDetail EmployeeMoneyDetail { get; set; }

        [ForeignKey("holidayType")]
        public int HolidayTypeId { get; set; }
        public virtual HolidayType holidayType { get; set; }
        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime ToDate { get; set; }
        public int NODays { get; set; }
        public string Notes { get; set; }


    }
}
