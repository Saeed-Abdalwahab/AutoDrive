using AutoDrive.Static.Enums;
using AutoDriveResources;
using Microsoft.ApplicationInsights.AspNetCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoDrive.VM.Helper;
namespace AutoDrive.VM.AutoDriveHR
{
    public class EmployeeVM
    {
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ArName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]

        public string EnName { get; set; }
        [Display(Name = "Nationality", ResourceType = typeof(AutoDriveResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]

        public int? NationalityId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "ProfileImge", ResourceType = typeof(AutoDriveResources.Resources))]
       
        public string ProfileImge { get; set; }

        [Display(Name = "Religion", ResourceType = typeof(AutoDriveResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
       
        public int? ReligionId { get; set; }

        [Display(Name = "DateOfBirth", ResourceType = typeof(AutoDriveResources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime DateOfBirth { get; set; } 
        [Display(Name = "Gender", ResourceType = typeof(AutoDriveResources.Resources))]

        public Gender? Gender { get; set; }
        [Display(Name = "BloodGroup", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? BloodGroupId { get; set; }
        [Display(Name = "BirthPlaceCountry", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? BirthPlaceCountryId { get; set; }
        [Display(Name = "BirthPlaceArea", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? BirthPlaceAreaId { get; set; }
        [Display(Name = "MaritalStatus", ResourceType = typeof(AutoDriveResources.Resources))]

        public int? MaritalStatusId { get; set; }
        [Display(Name = "Residence", ResourceType = typeof(AutoDriveResources.Resources))]

        public string Residence { get; set; }
        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]

        public long? Code { get; set; }
        [Display(Name = "MobileNumber", ResourceType = typeof(AutoDriveResources.Resources))]

        [StringLength(50)]
        public string MobileNumber { get; set; }
        [Display(Name = "HousePhone", ResourceType = typeof(AutoDriveResources.Resources))]

        [StringLength(50)]
        public string HousePhone { get; set; }
        [Display(Name = "EMail", ResourceType = typeof(AutoDriveResources.Resources))]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailValidation")]
        public string EMail { get; set; }

        public string EmployeeDepartment { get; set; }


    }
}
