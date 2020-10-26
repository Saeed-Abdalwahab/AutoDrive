using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("AttendanceFile")]
    public partial class AttendanceFile
    {
        public AttendanceFile()
        {
            EmployeeAttendances = new HashSet<EmployeeAttendance>();
        }
        [Key]
        public int ID { get; set; }
        [Required]
        public string FileName { get; set; }
        [Column(TypeName = "date")]
        public DateTime UploadDate { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
    }
}
