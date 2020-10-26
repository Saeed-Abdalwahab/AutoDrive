using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{

    [Table("EmployeeLicenceData")]
    public partial class EmployeeLicenceData
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("LicenceTypeHR")]

        public int LicenceTypeHRId { get; set; }
        [ForeignKey("LicenceKindHR")]

        public int? LicenceKindHRId { get; set; }

        [StringLength(250)]
        public string LicenseNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [ForeignKey("Area")]

        public int? SourceAreaId { get; set; }

        public virtual Area Area { get; set; }

        public virtual LicenceKindHR LicenceKindHR { get; set; }

        public virtual LicenceTypeHR LicenceTypeHR { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
