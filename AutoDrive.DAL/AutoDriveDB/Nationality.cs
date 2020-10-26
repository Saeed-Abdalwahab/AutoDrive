namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nationality")]
    public partial class Nationality
    {
        public Nationality()
        {
            Employees = new HashSet<Employee>();
            Trainees = new HashSet<Trainee>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EnName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Trainee> Trainees { get; set; }
    }
}
