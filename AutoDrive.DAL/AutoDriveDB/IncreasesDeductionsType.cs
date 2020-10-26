namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IncreasesDeductionsType")]
    public partial class IncreasesDeductionsType
    {
        public IncreasesDeductionsType()
        {
            IncreasesDeductionsSettings = new HashSet<IncreasesDeductionsSetting>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string EnName { get; set; }
        public int IncreasesOrDeductions { get; set; }

        public virtual ICollection<IncreasesDeductionsSetting> IncreasesDeductionsSettings { get; set; }
    }
}
