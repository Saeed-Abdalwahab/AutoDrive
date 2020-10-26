using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("WorkingHoursSettingDetialsHR")]
    public partial class WorkingHoursSettingDetialsHR
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("WorkingHoursSettingHR")]
        public int WorkingHoursSettingHRId { get; set; }
        public virtual WorkingHoursSettingHR WorkingHoursSettingHR { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        [Required]
        public string DayName { get; set; }
        [Required]
        public string DisplayArDayName { get; set; }
        [Required]
        public string DisplayEnDayName { get; set; }
    }
}
