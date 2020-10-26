using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    public class VacationDeduction
    {
        public VacationDeduction()
        {
            employeeVacations = new HashSet<EmployeeVacation>();
        }
        public int ID { get; set; }
        [ForeignKey("Employee")]

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        [Required]
        public string DaysDate { get; set; }
        public int DaysNumber { get; set; }
        [ForeignKey("holidayType")]
        public int HolidayTypeId { get; set; }
        public virtual HolidayType holidayType { get; set; }

        public double DeductionValue { get; set; }
        public int DeductionFromMonth { get; set; }
        public int DeductionFromYear { get; set; }
        public virtual ICollection<EmployeeVacation> employeeVacations { get; set; }






    }
}
