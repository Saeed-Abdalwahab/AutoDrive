using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmployeeQualification")]
    public partial class EmployeeQualification
    {
        [Key]
        public int ID { get; set; }

        public int CetificationTypeId { get; set; }
        [ForeignKey("CertificationSpecific")]

        public int CertificationSpecificId { get; set; }
        [ForeignKey("CertificationSpecDepart")]

        public int? CertificationSpecDepartId { get; set; }
        [ForeignKey("CertificationGrade")]

        public int? CertificationGradeId { get; set; }

        public string QualificationDiscribtion { get; set; }

        public int? GraduationYear { get; set; }

        public int? GraduationMonth { get; set; }
        [ForeignKey("Employee")]

        public int EmployeeId { get; set; }

        public virtual CertificationGrade CertificationGrade { get; set; }

        public virtual CertificationSpecDepart CertificationSpecDepart { get; set; }

        public virtual CertificationSpecific CertificationSpecific { get; set; }
        [ForeignKey("CetificationTypeId")]
        public virtual CertificationType CertificationType { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
