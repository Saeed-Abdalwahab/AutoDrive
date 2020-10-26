namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            Areas = new HashSet<Area>();
            Employees = new HashSet<Employee>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string EnName { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}