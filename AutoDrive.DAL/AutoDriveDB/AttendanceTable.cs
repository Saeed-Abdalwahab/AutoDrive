namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttendanceTable")]
    public partial class AttendanceTable
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("TraineeEvaluation")]
        public int TraineeEvaluationId { get; set; }

        public DateTime CourseDateTime { get; set; }

        public int PracticalOrVisual { get; set; }

        public virtual TraineeEvaluation TraineeEvaluation { get; set; }
    }
}
