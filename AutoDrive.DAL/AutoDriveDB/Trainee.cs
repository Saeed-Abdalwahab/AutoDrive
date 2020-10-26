namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trainee")]
    public partial class Trainee
    {
        public Trainee()
        {
            ExamResults = new HashSet<ExamResult>();
            ReceiptVouchers = new HashSet<ReceiptVoucher>();
            SendToTraffics = new HashSet<SendToTraffic>();
            TraineeArchives = new HashSet<TraineeArchive>();
            TraineeAttendances = new HashSet<TraineeAttendance>();
            TraineeEvaluations = new HashSet<TraineeEvaluation>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string ArName { get; set; }

        [Required]
        public string EnName { get; set; }

        public int NationalityId { get; set; }

        public int? ReligionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public long? NationalId { get; set; }

        public int? NationalTypeId { get; set; }

        public string Residence { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string FileNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FileOpenDate { get; set; }

        public int? Gender { get; set; }

        public int? SpeechLanguageId { get; set; }

        public string Photopath { get; set; }
        public Guid? TraineeGuid { get; set; }
        public virtual ICollection<ExamResult> ExamResults { get; set; }

        public virtual Nationality Nationality { get; set; }

        public virtual NationalType NationalType { get; set; }

        public virtual ICollection<ReceiptVoucher> ReceiptVouchers { get; set; }

        public virtual Religion Religion { get; set; }

        public virtual ICollection<SendToTraffic> SendToTraffics { get; set; }

        public virtual SpeechLanguage SpeechLanguage { get; set; }

        public virtual ICollection<TraineeArchive> TraineeArchives { get; set; }

        public virtual ICollection<TraineeAttendance> TraineeAttendances { get; set; }

        public virtual ICollection<TraineeEvaluation> TraineeEvaluations { get; set; }
    }
}
