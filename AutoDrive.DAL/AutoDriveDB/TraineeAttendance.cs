namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TraineeAttendance")]
    public partial class TraineeAttendance
    {
        [Key]   
        public int ID { get; set; }  
          
        public int TraineeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime AttendanceDate { get; set; }

        public int PracticalOrVisual { get; set; }

        public int TraineeEvaluationId { get; set; }

        public virtual Trainee Trainee { get; set; }

        public virtual TraineeEvaluation TraineeEvaluation { get; set; }
    }
}
