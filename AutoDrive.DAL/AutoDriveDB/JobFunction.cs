namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JobFunction")]
    public partial class JobFunction
    {
        public JobFunction()
        {
            EmployeeJobDatas = new HashSet<EmployeeJobData>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

         [Required]
        public string EnName { get; set; }



        public int JobNameId { get; set; }

        public virtual ICollection<EmployeeJobData> EmployeeJobDatas { get; set; }

        public virtual JobName JobName { get; set; }
    }
}
