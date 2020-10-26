namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeJobData")]
    public partial class EmployeeJobData
    {
        [Key]
        public int ID { get; set; }

        public int EmployeeId { get; set; }

        public int JobDegreeId { get; set; }

        public int CarrerFieldId { get; set; }
        public int JobLevelId { get; set; }
        public int JobNameId { get; set; }
        public int? JobFunctionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public virtual CarrerField CarrerField { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual JobDegree JobDegree { get; set; }
        public virtual JobFunction JobFunction { get; set; }

        public virtual JobLevel JobLevel { get; set; }

        public virtual JobName JobName { get; set; }
    }
}
