using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using System.Data.Entity.Spatial;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("AddIncreasingDeductionToJob")]
    public class AddIncreasingDeductionToJob
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("IncreasesDeductionsSetting")]
        public int IncreasingDeductionSettingId { get; set; }
        [ForeignKey("BasicSalarySetting")]

        public int BasicSalarySettingId { get; set; }
        public IncreasesDeductionsSetting IncreasesDeductionsSetting { get; set; }

        public BasicSalarySetting BasicSalarySetting { get; set; }
    }
}
