using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("CertificationGrade")]
    public partial class CertificationGrade
    {
        public CertificationGrade()
        {
            EmployeeQualifications = new HashSet<EmployeeQualification>();
        }
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NameEng { get; set; }

        public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; }
    }
}
