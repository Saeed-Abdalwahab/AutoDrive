using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("TraineeAttendingFollowup")]
    public partial class TraineeAttendingFollowup
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("TraineeAttendance")]
        public int TraineeAttendanceId { get; set; }

        public DateTime AttendanceDatetime { get; set; }

        public DateTime LeaveDateTime { get; set; }

        public virtual TraineeAttendance TraineeAttendance { get; set; }
        
    }
}
