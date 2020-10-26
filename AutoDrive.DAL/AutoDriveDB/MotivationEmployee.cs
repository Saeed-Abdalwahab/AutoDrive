namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MotivationEmployee")]
    public partial class MotivationEmployee
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime MotivationDate { get; set; }

        public int MotivationTypeId { get; set; }

        public int EmployeeId { get; set; }
        public int MotivationStatus { get; set; }
        public int WithSalaryOrForm { get; set; }

        public int InMonth { get; set; }

        public int InYear { get; set; }
        public double Value { get; set; }

        public string Note { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual MotivationType MotivationType { get; set; }
    }
}
