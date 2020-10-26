namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DriveLevel")]
    public partial class DriveLevel
    {
        public DriveLevel()
        {
            StudyPeriodSettings = new HashSet<StudyPeriodSetting>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string EnName { get; set; }

        public virtual ICollection<StudyPeriodSetting> StudyPeriodSettings { get; set; }
    }
}
