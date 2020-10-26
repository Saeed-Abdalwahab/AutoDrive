namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeExperience")]
    public partial class EmployeeExperience
    {
        [Key]
        public int ID { get; set; }

        public int FromMonth { get; set; }

        public int FromYear { get; set; }

        public int ToMonth { get; set; }

        public int ToYear { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string JobSpecification { get; set; }

        public string CompanyAddress { get; set; }
        [ForeignKey("Employee")]

        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
