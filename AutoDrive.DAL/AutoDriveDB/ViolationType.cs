using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("ViolationType")]
    public partial class ViolationType
    {
        public ViolationType()
        {
            EmployeeViolations = new HashSet<EmployeeViolation>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string EnName { get; set; }

        public double? DaysNumber { get; set; }

        public int? FromBaseSalaryOrOverall { get; set; }

        public int FromMoneyOrMoral { get; set; }

        public virtual ICollection<EmployeeViolation> EmployeeViolations { get; set; }
    }
}
