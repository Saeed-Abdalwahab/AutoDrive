namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TraineeEvaluation")]
    public partial class TraineeEvaluation
    {
        public TraineeEvaluation()
        {
            AttendanceTables = new HashSet<AttendanceTable>();
            CourseReservations = new HashSet<CourseReservation>();
            ExamResults = new HashSet<ExamResult>();
            SendToTraffics = new HashSet<SendToTraffic>();
            TraineeAttendances = new HashSet<TraineeAttendance>();
        }
        [Key]
        public int ID { get; set; }

        public int? TraineeId { get; set; }

        public int? LicenceTypeId { get; set; }

        public int? LicenceCategoryId { get; set; }

        public int? StudyPeriodSettingId { get; set; }

        [ForeignKey("Employee")]
        public int? EvaluationEmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EvaluationDate { get; set; }

        public virtual ICollection<AttendanceTable> AttendanceTables { get; set; }

        public virtual ICollection<CourseReservation> CourseReservations { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<ExamResult> ExamResults { get; set; }

        public virtual LicenceCategory LicenceCategory { get; set; }

        public virtual LicenceType LicenceType { get; set; }

        public virtual ICollection<SendToTraffic> SendToTraffics { get; set; }

        public virtual StudyPeriodSetting StudyPeriodSetting { get; set; }

        public virtual Trainee Trainee { get; set; }

        public virtual ICollection<TraineeAttendance> TraineeAttendances { get; set; }
    }
}
