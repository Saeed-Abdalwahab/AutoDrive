using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM
{
    public class BasicSalarySettingVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedDegree")]
        [Display(Name = "JobDegree", ResourceType = typeof(AutoDriveResources.Resources))]
        public int JobDegreeId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedLevel")]
        [Display(Name = "JobLevel", ResourceType = typeof(AutoDriveResources.Resources))]
        public int JobLevelId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedSalary")]
        [Display(Name = "Salary", ResourceType = typeof(AutoDriveResources.Resources))]
        public double Salary { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "NotExistedChangedSalary")]
        [Display(Name = "ChangedSalary", ResourceType = typeof(AutoDriveResources.Resources))]
        public double ChangedSalary { get; set; }
        public string JobDegreeName { get; set; }
        public String JobLeveLName { get; set; }
    }
}
