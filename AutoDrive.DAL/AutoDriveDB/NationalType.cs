namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NationalType")]
    public partial class NationalType
    {
        public NationalType()
        {
            Trainees = new HashSet<Trainee>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string EnName { get; set; }

        public virtual ICollection<Trainee> Trainees { get; set; }
    }
}
