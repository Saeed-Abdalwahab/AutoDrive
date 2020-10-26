namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BasicSalarySetting")]
    public partial class BasicSalarySetting
    {
        public BasicSalarySetting()
        {
            AddIncreasingDeductionToJobs = new HashSet<AddIncreasingDeductionToJob>();

        }
        [Key]
        public int ID { get; set; }
        [ForeignKey("JobDegree")]

        public int JobDegreeId { get; set; }
        [ForeignKey("JobLevel")]
        public int JobLevelId { get; set; }

        public double Salary { get; set; }

        public double ChangedSalary { get; set; }

        public virtual JobDegree JobDegree { get; set; }

        public virtual JobLevel JobLevel { get; set; }
        public virtual ICollection<AddIncreasingDeductionToJob> AddIncreasingDeductionToJobs { get; set; }

    }
}
