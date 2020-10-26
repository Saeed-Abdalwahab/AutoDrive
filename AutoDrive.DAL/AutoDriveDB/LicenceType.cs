namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LicenceType")]
    public partial class LicenceType
    {
        public LicenceType()
        {
            LicenceCategories = new HashSet<LicenceCategory>();
            TraineeEvaluations = new HashSet<TraineeEvaluation>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        [Required]
        [StringLength(50)]
        public string EnName { get; set; }

        public int? MinimalAge { get; set; }

        public virtual ICollection<LicenceCategory> LicenceCategories { get; set; }

        public virtual ICollection<TraineeEvaluation> TraineeEvaluations { get; set; }
    }
}
