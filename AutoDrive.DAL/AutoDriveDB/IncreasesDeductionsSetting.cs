namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IncreasesDeductionsSetting")]
    public partial class IncreasesDeductionsSetting
    {
        public IncreasesDeductionsSetting()
        {
            AddIncreasingDeductionToJobs = new HashSet<AddIncreasingDeductionToJob>();
            EmployeeMoneyDetails = new HashSet<EmployeeMoneyDetail>();

        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string EnName { get; set; }

        public double Value { get; set; }

        public int RatioOrValue { get; set; }

        public int? BasicSalaryOrAll { get; set; }

        public int IncreasesDeductionsTypeId { get; set; }
        public virtual ICollection<AddIncreasingDeductionToJob> AddIncreasingDeductionToJobs { get; set; }
        public virtual ICollection<EmployeeMoneyDetail> EmployeeMoneyDetails { get; set; }
        public virtual IncreasesDeductionsType IncreasesDeductionsType { get; set; }
    }
}
