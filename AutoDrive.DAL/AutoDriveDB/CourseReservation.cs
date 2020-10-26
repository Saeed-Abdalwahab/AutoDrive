namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseReservation")]
    public partial class CourseReservation
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("TraineeEvaluation")]
        public int TraineeEvaluationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime CourseStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CourseEndDate { get; set; }

        public double CourseCost { get; set; }

        public virtual TraineeEvaluation TraineeEvaluation { get; set; }
    }
}
