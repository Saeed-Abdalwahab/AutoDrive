using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
    public partial class EmployeeMoneyTypeDetail
    {
        public EmployeeMoneyTypeDetail()
        {
            EmployeeMoneyDetails = new HashSet<EmployeeMoneyDetail>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string EnName { get; set; }

        public virtual ICollection<EmployeeMoneyDetail> EmployeeMoneyDetails { get; set; }
    }

}
