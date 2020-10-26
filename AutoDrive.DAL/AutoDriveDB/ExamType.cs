namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamType")]
    public partial class ExamType
    {
        public ExamType()
        {
            ExamResults = new HashSet<ExamResult>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<ExamResult> ExamResults { get; set; }
    }
}
