namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReceiptVoucher")]
    public partial class ReceiptVoucher
    {
        [Key]
        public int ID { get; set; }

        public double PaidValue { get; set; }

        public DateTime PaidDate { get; set; }

        public int ReceiptVoucherCode { get; set; }

        public DateTime ReceiptVoucherDate { get; set; }


        public int TraineeId { get; set; }

        public int CourseReservationId { get; set; }
        public int? EmployeeSafeId { get; set; }
        [ForeignKey("EmployeeSafeId")]

        public virtual Employee Employee { get; set; }

        public virtual Trainee Trainee { get; set; }
        [ForeignKey("CourseReservationId")]
        public virtual CourseReservation CourseReservation { get; set; }
    }
}
