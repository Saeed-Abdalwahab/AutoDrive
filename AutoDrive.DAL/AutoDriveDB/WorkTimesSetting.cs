namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkTimesSetting")]
    public partial class WorkTimesSetting
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string DisplayDayName { get; set; }      
        [Required]
        public string DisplayDayNameEn { get; set; }
        [Required]

        public string DayName { get; set; }
        [Required]

        public TimeSpan FromHour { get; set; }
        [Required]

        public TimeSpan ToHour { get; set; }

        public double? NOWorkHours { get; set; }
    }
}
