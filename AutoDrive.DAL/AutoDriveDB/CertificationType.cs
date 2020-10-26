using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{

    [Table("CertificationType")]
    public partial class CertificationType
    {
        public CertificationType()
        {
            CertificationSpecifics = new HashSet<CertificationSpecific>();
            EmployeeQualifications = new HashSet<EmployeeQualification>();
        }
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string EnName { get; set; }

        public virtual ICollection<CertificationSpecific> CertificationSpecifics { get; set; }

        public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; }
    }
}
