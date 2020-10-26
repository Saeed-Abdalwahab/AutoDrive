using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.AutoDriveDB.HR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AutoDrive.DAL.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Model1", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<AddIncreasingDeductionToJob> AddIncreasingDeductionToJobs { get; set; }
        public virtual DbSet<EmployeeLoan> EmployeeLoans { get; set; }
        public virtual DbSet<ArchiveSettingDrive> ArchiveSettingDrives { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<AttendanceTable> AttendanceTables { get; set; }
        public virtual DbSet<BasicSalarySetting> BasicSalarySettings { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<VacationDeduction> VacationDeductions { get; set; }
        public virtual DbSet<CompanySalaryExchangeDate> CompanySalaryExchangeDates { get; set; }
        public virtual DbSet<CarrerField> CarrerFields { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CourseReservation> CourseReservations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DriveLevel> DriveLevels { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ArchiveSettingHR> ArchiveSettingHRs { get; set; }
        public virtual DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public virtual DbSet<EmployeeVacationAccount> EmployeeVacationAccounts { get; set; }
        public virtual DbSet<EmployeeExperience> EmployeeExperiences { get; set; }
        public virtual DbSet<EmployeeJobData> EmployeeJobDatas { get; set; }
        public virtual DbSet<ExamResult> ExamResults { get; set; }
        public virtual DbSet<ExamType> ExamTypes { get; set; }
        public virtual DbSet<IncreasesDeductionsSetting> IncreasesDeductionsSettings { get; set; }
        public virtual DbSet<EmployeeHoursSetting> EmployeeHoursSettings { get; set; }
        public virtual DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
        public virtual DbSet<IncreasesDeductionsType> IncreasesDeductionsTypes { get; set; }
        public virtual DbSet<WorkingHoursSettingDetialsHR> WorkingHoursSettingDetialsHRs { get; set; }
        public virtual DbSet<WorkingHoursSettingHR> WorkingHoursSettingHRs { get; set; }
        public virtual DbSet<AttendanceFile> AttendanceFiles { get; set; }
        public virtual DbSet<JobDegree> JobDegrees { get; set; }
        public virtual DbSet<JobFunction> JobFunctions { get; set; }
        public virtual DbSet<JobLevel> JobLevels { get; set; }
        public virtual DbSet<JobName> JobNames { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<LicenceCategory> LicenceCategories { get; set; }
        public virtual DbSet<LicenceType> LicenceTypes { get; set; }
        public virtual DbSet<MaritalStatu> MaritalStatus { get; set; }
        public virtual DbSet<MotivationEmployee> MotivationEmployees { get; set; }
        public virtual DbSet<MotivationType> MotivationTypes { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<NationalType> NationalTypes { get; set; }
        public virtual DbSet<HolidayType> HolidayTypes { get; set; }
        public virtual DbSet<ReceiptVoucher> ReceiptVouchers { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<SendToTraffic> SendToTraffics { get; set; }
        public virtual DbSet<SpeechLanguage> SpeechLanguages { get; set; }
        public virtual DbSet<StudyPeriodSetting> StudyPeriodSettings { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }

        public virtual DbSet<TraineeArchive> TraineeArchives { get; set; }
        public virtual DbSet<TraineeAttendance> TraineeAttendances { get; set; }
        public virtual DbSet<TraineeEvaluation> TraineeEvaluations { get; set; }
        public virtual DbSet<WorkTimesSetting> WorkTimesSettings { get; set; }
        public virtual DbSet<EmployeeContractDuration> EmployeeContractDurations { get; set; }
        public virtual DbSet<EmployeeLicenceData> EmployeeLicenceDatas { get; set; }
        public virtual DbSet<EmployeeQualification> EmployeeQualifications { get; set; }
        public virtual DbSet<EmployeeStatu> EmployeeStatus { get; set; }
        public virtual DbSet<EmployeeVacation> EmployeeVacations { get; set; }
        public virtual DbSet<EmployeeStatusKind> EmployeeStatusKinds { get; set; }
        public virtual DbSet<EmployeeStatusTransaction> EmployeeStatusTransactions { get; set; }
        public virtual DbSet<CertificationGrade> CertificationGrades { get; set; }
        public virtual DbSet<CertificationSpecDepart> CertificationSpecDeparts { get; set; }
        public virtual DbSet<CertificationSpecific> CertificationSpecifics { get; set; }
        public virtual DbSet<CertificationType> CertificationTypes { get; set; }
        public virtual DbSet<LicenceKindHR> LicenceKindHRs { get; set; }
        public virtual DbSet<LicenceTypeHR> LicenceTypeHRs { get; set; }
        public virtual DbSet<EmployeeMoney> EmployeeMoneys { get; set; }
        public virtual DbSet<EmplpoyeeMoneyType> EmplpoyeeMoneyTypes { get; set; }
        public virtual DbSet<EmployeeMoneyDetail> EmployeeMoneyDetails { get; set; }
        public virtual DbSet<EmployeeMoneyTypeDetail> EmployeeMoneyTypeDetails { get; set; }
        public virtual DbSet<EmployeeViolation> EmployeeViolations { get; set; }
        public virtual DbSet<ViolationType> ViolationTypes { get; set; }
        public virtual DbSet<TraineeAttendingFollowup> TraineeAttendingFollowups { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestResult> TestResult { get; set; }
        public virtual DbSet<TraineeTestResult> TraineeTestResult { get; set; }




        #region HR
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Honesty> Honestys { get; set; }
        public virtual DbSet<EmployeeArchive> EmployeeArchives { get; set; }
        public virtual DbSet<EmployeeFamily> EmployeeFamilys { get; set; }

        #endregion

    }
}