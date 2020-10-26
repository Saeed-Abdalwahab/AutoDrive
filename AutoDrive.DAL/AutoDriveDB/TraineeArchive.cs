namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TraineeArchive")]
    public partial class TraineeArchive
    {
        [Key]
        public int ID { get; set; }

        public int ArchiveSettingDriveId { get; set; }

        public int TraineeId { get; set; }

        public string Note { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public string ImageName { get; set; }

        public virtual ArchiveSettingDrive ArchiveSettingDrive { get; set; }

        public virtual Trainee Trainee { get; set; }
    }
}
