namespace AutoDrive.DAL.AutoDriveDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public Employee()
        {
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
            EmployeeExperiences = new HashSet<EmployeeExperience>();
            EmployeeJobDatas = new HashSet<EmployeeJobData>();
            MotivationEmployees = new HashSet<MotivationEmployee>();
            ReceiptVouchers = new HashSet<ReceiptVoucher>();
            SendToTraffics = new HashSet<SendToTraffic>();
            TraineeEvaluations = new HashSet<TraineeEvaluation>();
            EmployeeContractDurations = new HashSet<EmployeeContractDuration>();
            EmployeeLicenceDatas = new HashSet<EmployeeLicenceData>();
            EmployeeLoans = new HashSet<EmployeeLoan>();
            EmployeeVacationAccounts = new HashSet<EmployeeVacationAccount>();
            EmployeeVacations = new HashSet<EmployeeVacation>();
            EmployeeHoursSettings = new HashSet<EmployeeHoursSetting>();
            EmployeeAttendances = new HashSet<EmployeeAttendance>();
        }
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EnName { get; set; }

        public int? NationalityId { get; set; }

        public int? ReligionId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public int? Gender { get; set; }
        
        public int? BloodGroupId { get; set; }
        [ForeignKey("Country")]
        public int? BirthPlaceCountryId { get; set; }
        [ForeignKey("Area")]

        public int? BirthPlaceAreaId { get; set; }
        [ForeignKey("MaritalStatu")]

        public int? MaritalStatusId { get; set; }

        public string Residence { get; set; }

        public long? Code { get; set; }

        [StringLength(50)]
        public string MobileNumber { get; set; }
        [Required]
        public string ProfileImge { get; set; }


        [StringLength(50)]
        public string HousePhone { get; set; }
        [StringLength(250)]
        public string EMail { get; set; }
        public Guid? EmployeeGuid { get; set; }
        public virtual Area Area { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public virtual Country Country { get; set; }
        public virtual MaritalStatu MaritalStatu { get; set; }

        public virtual Nationality Nationality { get; set; }

        public virtual Religion Religion { get; set; }

        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
        public virtual ICollection<EmployeeVacation> EmployeeVacations { get; set; }


        public virtual ICollection<EmployeeExperience> EmployeeExperiences { get; set; }

        public virtual ICollection<EmployeeJobData> EmployeeJobDatas { get; set; }

        public virtual ICollection<MotivationEmployee> MotivationEmployees { get; set; }

        public virtual ICollection<ReceiptVoucher> ReceiptVouchers { get; set; }

        public virtual ICollection<SendToTraffic> SendToTraffics { get; set; }

        public virtual ICollection<TraineeEvaluation> TraineeEvaluations { get; set; }
        public virtual ICollection<EmployeeContractDuration> EmployeeContractDurations { get; set; }
        public virtual ICollection<EmployeeLicenceData> EmployeeLicenceDatas { get; set; }
        public virtual ICollection<EmployeeLoan> EmployeeLoans { get; set; }
        public virtual ICollection<EmployeeVacationAccount> EmployeeVacationAccounts { get; set; }
        public virtual ICollection<EmployeeHoursSetting> EmployeeHoursSettings { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
    }
}
