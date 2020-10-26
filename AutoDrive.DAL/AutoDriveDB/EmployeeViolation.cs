using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AutoDrive.DAL.AutoDriveDB
{

    [Table("EmployeeViolation")]
    public partial class EmployeeViolation
    {
        public int ID { get; set; }
        [ForeignKey("Employee")]

        public int EmployeeId { get; set; }
        [ForeignKey("ViolationType")]
        public int ViolationTypeId { get; set; }

        public int ViolationStatus { get; set; }

        public int? FromMonth { get; set; }

        public int? FromYear { get; set; }

        [Column(TypeName = "date")]
        public DateTime ViolationDate { get; set; }

        public double? ViolationValue { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ViolationType ViolationType { get; set; }
    }
}
