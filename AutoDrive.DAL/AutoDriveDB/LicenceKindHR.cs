using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{

    [Table("LicenceKindHR")]
    public partial class LicenceKindHR
    {
        public LicenceKindHR()
        {
            EmployeeLicenceDatas = new HashSet<EmployeeLicenceData>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [ForeignKey("LicenceTypeHR")]

        public int LicenceTypeHRId { get; set; }

        [Required]
        [StringLength(250)]
        public string EnName { get; set; }

        public virtual ICollection<EmployeeLicenceData> EmployeeLicenceDatas { get; set; }

        public virtual LicenceTypeHR LicenceTypeHR { get; set; }
    }
}
