using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("LicenceTypeHR")]
    public partial class LicenceTypeHR
    {
        public LicenceTypeHR()
        {
            EmployeeLicenceDatas = new HashSet<EmployeeLicenceData>();
            LicenceKindHRs = new HashSet<LicenceKindHR>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string EnName { get; set; }

        public virtual ICollection<EmployeeLicenceData> EmployeeLicenceDatas { get; set; }

        public virtual ICollection<LicenceKindHR> LicenceKindHRs { get; set; }
    }
}
