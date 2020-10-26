using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("WorkingHoursSettingHR")]
    public partial class WorkingHoursSettingHR
    {
        public WorkingHoursSettingHR()
        {
            WorkingHoursSettingDetialsHRs = new HashSet<WorkingHoursSettingDetialsHR>();
            EmployeeHoursSettings = new HashSet<EmployeeHoursSetting>();
        }
        [Key]
        public int ID { get; set; }
        [Required]
        public string ArName { get; set; }
        [Required]
        public string EnName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }
        public virtual ICollection<WorkingHoursSettingDetialsHR> WorkingHoursSettingDetialsHRs { get; set; }
        public virtual ICollection<EmployeeHoursSetting> EmployeeHoursSettings { get; set; }
    }
}
