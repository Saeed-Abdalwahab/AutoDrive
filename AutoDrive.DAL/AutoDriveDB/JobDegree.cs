namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobDegree")]
    public partial class JobDegree
    {
        public JobDegree()
        {
            BasicSalarySettings = new HashSet<BasicSalarySetting>();
            EmployeeJobDatas = new HashSet<EmployeeJobData>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string ENName { get; set; }
        public int DegreeSort { get; set; }
        public virtual ICollection<BasicSalarySetting> BasicSalarySettings { get; set; }

        public virtual ICollection<EmployeeJobData> EmployeeJobDatas { get; set; }
    }
}
