using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB
{
  public  class ArchiveSettingHR
    {
        public ArchiveSettingHR()
        {
            archiveSettingHRs = new HashSet<ArchiveSettingHR>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string EnName { get; set; }
        [ForeignKey("Parent")]
        public int? ParentID { get; set; }
        public ArchiveSettingHR Parent { get; set; }
        public ICollection<ArchiveSettingHR> archiveSettingHRs { get; set; }

    }
}
