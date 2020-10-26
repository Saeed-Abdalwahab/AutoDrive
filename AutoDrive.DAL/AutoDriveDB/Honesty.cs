using AutoDrive.DAL.AutoDriveDB.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("Honesty")]
    public class Honesty
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public DateTime? HonestyDate { get; set; }
        public DateTime? honestyEndDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Item Item { get; set; }
    }
}
