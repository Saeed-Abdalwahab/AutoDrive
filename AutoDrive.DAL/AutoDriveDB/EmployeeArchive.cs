using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeArchive")]
    public class EmployeeArchive
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("ArchiveSettingHR")]
        public int ArchiveSettingHRsId { get; set; }

        public string Number { get; set; }
        public string Notes { get; set; }
        public DateTime? Date { get; set; }
        public string ImageName { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ArchiveSettingHR ArchiveSettingHR { get; set; }

        //public virtual Item Item { get; set; }
    }
}
