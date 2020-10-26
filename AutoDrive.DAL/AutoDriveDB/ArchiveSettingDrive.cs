namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArchiveSettingDrive")]
    public partial class ArchiveSettingDrive
    {
        public ArchiveSettingDrive()
        {
            ArchiveSettingDrive1 = new HashSet<ArchiveSettingDrive>();
            TraineeArchives = new HashSet<TraineeArchive>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }


        [Required]
        [StringLength(200)]
        public string EnName { get; set; }


        [ForeignKey("ArchiveSettingDrive2")]
        public int? ParentId { get; set; }

        public virtual ICollection<ArchiveSettingDrive> ArchiveSettingDrive1 { get; set; }

        public virtual ArchiveSettingDrive ArchiveSettingDrive2 { get; set; }

        public virtual ICollection<TraineeArchive> TraineeArchives { get; set; }
    }
}
