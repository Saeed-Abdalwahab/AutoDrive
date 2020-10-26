namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            Departments = new HashSet<Department>();
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
        }
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string EnName { get; set; }

        [ForeignKey("department")]
        public int? ParentId { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
        public virtual Department department { get; set; }

        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}
