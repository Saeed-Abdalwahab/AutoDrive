using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeFamily")]
    public class EmployeeFamily
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public string ArName { get; set; }

        public string EnName { get; set; }
        public int Gender { get; set; }
        public int Relation { get; set; }
        public string Note { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
