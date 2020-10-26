using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeHoursSetting")]
    public partial class EmployeeHoursSetting
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        public virtual Employee employee { get; set; }
        [ForeignKey("WorkingHoursSettingHR")]
        public int WorkingHoursSettingHRId { get; set; }
        public virtual WorkingHoursSettingHR WorkingHoursSettingHR { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }
    }
}
