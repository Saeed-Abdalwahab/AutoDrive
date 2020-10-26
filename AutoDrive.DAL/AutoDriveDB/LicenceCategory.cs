namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LicenceCategory")]
    public partial class LicenceCategory
    {
        public LicenceCategory()
        {
            TraineeEvaluations = new HashSet<TraineeEvaluation>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EnName { get; set; }

        public int LicenceTypeId { get; set; }

        public virtual LicenceType LicenceType { get; set; }

        public virtual ICollection<TraineeEvaluation> TraineeEvaluations { get; set; }
    }
}
