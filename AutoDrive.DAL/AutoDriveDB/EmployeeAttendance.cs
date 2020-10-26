using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeAttendance")]
    public class EmployeeAttendance
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("employee")]
        public int EmployeeId { get; set; }
        public virtual Employee employee { get; set; }
        [ForeignKey("AttendanceFile")]
        public int? AttendanceFileId { get; set; }
        public virtual AttendanceFile AttendanceFile { get; set; }
        [Column(TypeName = "date")]
        public DateTime AttendanceDate  { get; set; }
        public TimeSpan AttendanceTime { get; set; }
        [Required]
        public int AttendanceType { get; set; }
    }
}
