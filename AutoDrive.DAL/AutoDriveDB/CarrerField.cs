namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CarrerField")]
    public partial class CarrerField
    {
        public CarrerField()
        {
            EmployeeJobDatas = new HashSet<EmployeeJobData>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string EnName { get; set; }

        public virtual ICollection<EmployeeJobData> EmployeeJobDatas { get; set; }
    }
}
