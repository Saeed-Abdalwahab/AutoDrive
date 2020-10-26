namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SpeechLanguage")]
    public partial class SpeechLanguage
    {
        public SpeechLanguage()
        {
            Trainees = new HashSet<Trainee>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EnName { get; set; }

        public virtual ICollection<Trainee> Trainees { get; set; }
    }
}
