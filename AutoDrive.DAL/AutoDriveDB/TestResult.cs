using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    public class TestResult
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string ArName { get; set; }

        [Required]
        public string EnName { get; set; }
    }
}
