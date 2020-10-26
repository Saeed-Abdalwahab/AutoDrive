namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamResult")]
    public partial class ExamResult
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExamDate { get; set; }
        public int ExamTypeId { get; set; }

        [Column("ExamResult")]
        public int ExamResult1 { get; set; }

        public int TraineeId { get; set; }

        public int TraineeEvaluationId { get; set; }

        public virtual ExamType ExamType { get; set; }

        public virtual Trainee Trainee { get; set; }

        public virtual TraineeEvaluation TraineeEvaluation { get; set; }
    }
}
