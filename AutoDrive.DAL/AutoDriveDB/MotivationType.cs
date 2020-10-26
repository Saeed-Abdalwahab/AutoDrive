namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MotivationType")]
    public partial class MotivationType
    {
        public MotivationType()
        {
            MotivationEmployees = new HashSet<MotivationEmployee>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string EnName { get; set; }

        public virtual ICollection<MotivationEmployee> MotivationEmployees { get; set; }
    }
}
