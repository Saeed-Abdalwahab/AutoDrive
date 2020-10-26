using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.DAL.AutoDriveDB.HR
{

    [Table("Item")]
    public class Item
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string  EnName { get; set; }
        public string ItemType { get; set; }
        public string SerialNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemPathImage { get; set; }

        [ForeignKey("item")]
        public int? ItemParentId { get; set; }

        public virtual Item item { get; set; }
    }
}
