using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AutoDrive.DAL.AutoDriveDB
{
    

    [Table("CompanySalaryExchangeDate")]
    public partial class CompanySalaryExchangeDate
    {
        public int ID { get; set; }
        [Required]
        public int DayNumber { get; set; }
    }
}
