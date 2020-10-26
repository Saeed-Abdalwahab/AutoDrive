namespace AutoDrive.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudyPeriodSetting")]
    public partial class StudyPeriodSetting
    {
        public int ID { get; set; }

        public int DriveLevelId { get; set; }

        public int VisualStudyCount { get; set; }

        public double VisualStudyCost { get; set; }

        public double VisualStudyTotal { get; set; }

        public int PracticalCount { get; set; }

        public double PracticalCost { get; set; }

        public double PracticalTotal { get; set; }

        public double LevelTotal { get; set; }

        public int LevelStatus { get; set; }
    }
}
