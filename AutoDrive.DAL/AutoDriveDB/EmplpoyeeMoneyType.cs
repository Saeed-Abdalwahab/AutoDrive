using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    [Table("EmplpoyeeMoneyType")]
    public partial class EmplpoyeeMoneyType
    {
        public EmplpoyeeMoneyType()
        {
            EmployeeMoneys = new HashSet<EmployeeMoney>();
        }
        [Key]
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string EnName { get; set; }

        public virtual ICollection<EmployeeMoney> EmployeeMoneys { get; set; }
    }
}
