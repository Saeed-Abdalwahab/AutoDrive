namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobLevel")]
    public partial class JobLevel
    {
        public JobLevel()
        {
            BasicSalarySettings = new HashSet<BasicSalarySetting>();
            EmployeeJobDatas = new HashSet<EmployeeJobData>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string EnName { get; set; }
        public int LevelSort { get; set; }

        public virtual ICollection<BasicSalarySetting> BasicSalarySettings { get; set; }

        public virtual ICollection<EmployeeJobData> EmployeeJobDatas { get; set; }
    }
}
