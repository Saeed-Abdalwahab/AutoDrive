using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
 public   class EmployeeVacationAccount
    {
        public int ID { get; set; }
        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        public virtual Employee employee { get; set; }
        [ForeignKey("holidayType")]

        public int HolidayTypeId { get; set; }
        public virtual HolidayType holidayType { get; set; }

        public int Year { get; set; }
        public int DaysNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
