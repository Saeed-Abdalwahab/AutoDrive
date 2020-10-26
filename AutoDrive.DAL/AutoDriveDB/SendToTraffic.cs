namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SendToTraffic")]
    public partial class SendToTraffic
    {
        [Key]
        public int ID { get; set; }

        public int TraineeId { get; set; }

        public int TraineeEvaluationId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SendDate { get; set; }

        public int? SenderEmployeeId { get; set; }


        [ForeignKey("SenderEmployeeId")]
        public virtual Employee Employee { get; set; }

        public virtual Trainee Trainee { get; set; }

        public virtual TraineeEvaluation TraineeEvaluation { get; set; }
    }
}
