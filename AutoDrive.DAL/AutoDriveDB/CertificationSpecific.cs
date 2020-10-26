using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("CertificationSpecific")]
    public partial class CertificationSpecific
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CertificationSpecific()
        {
            CertificationSpecDeparts = new HashSet<CertificationSpecDepart>();
            EmployeeQualifications = new HashSet<EmployeeQualification>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Required]
        [ForeignKey("CertificationType")]
        public int CertificationTypeID { get; set; }

        [Required]
        [StringLength(250)]
        public string EnName { get; set; }

        
        public virtual ICollection<CertificationSpecDepart> CertificationSpecDeparts { get; set; }

        public virtual CertificationType CertificationType { get; set; }

        
        public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; }
    }
}
