using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("CertificationSpecDepart")]
    public partial class CertificationSpecDepart
    {
        public CertificationSpecDepart()
        {
            EmployeeQualifications = new HashSet<EmployeeQualification>();
        }
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [ForeignKey("CertificationSpecific")]

        public int? CertificationSpecificID { get; set; }

        [StringLength(50)]
        public string EnName { get; set; }

        public virtual CertificationSpecific CertificationSpecific { get; set; }

        public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; }
    }
}
