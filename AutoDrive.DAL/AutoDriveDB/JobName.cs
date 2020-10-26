namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobName")]
    public partial class JobName
    {
        public JobName()
        {
            EmployeeJobDatas = new HashSet<EmployeeJobData>();
            JobFunctions = new HashSet<JobFunction>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EnName { get; set; }


        public virtual ICollection<EmployeeJobData> EmployeeJobDatas { get; set; }

        public virtual ICollection<JobFunction> JobFunctions { get; set; }
    }
}
