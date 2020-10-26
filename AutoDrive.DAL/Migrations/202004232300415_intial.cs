namespace AutoDrive.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddIncreasingDeductionToJob",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IncreasingDeductionSettingId = c.Int(nullable: false),
                        BasicSalarySettingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BasicSalarySetting", t => t.BasicSalarySettingId, cascadeDelete: false)
                .ForeignKey("dbo.IncreasesDeductionsSetting", t => t.IncreasingDeductionSettingId, cascadeDelete: false)
                .Index(t => t.IncreasingDeductionSettingId)
                .Index(t => t.BasicSalarySettingId);
            
            CreateTable(
                "dbo.BasicSalarySetting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobDegreeId = c.Int(nullable: false),
                        JobLevelId = c.Int(nullable: false),
                        Salary = c.Double(nullable: false),
                        ChangedSalary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobDegree", t => t.JobDegreeId, cascadeDelete: false)
                .ForeignKey("dbo.JobLevel", t => t.JobLevelId, cascadeDelete: false)
                .Index(t => t.JobDegreeId)
                .Index(t => t.JobLevelId);
            
            CreateTable(
                "dbo.JobDegree",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        ENName = c.String(nullable: false, maxLength: 250),
                        DegreeSort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeJobData",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        JobDegreeId = c.Int(nullable: false),
                        CarrerFieldId = c.Int(nullable: false),
                        JobLevelId = c.Int(nullable: false),
                        JobNameId = c.Int(nullable: false),
                        JobFunctionId = c.Int(),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CarrerField", t => t.CarrerFieldId, cascadeDelete: false)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.JobDegree", t => t.JobDegreeId, cascadeDelete: false)
                .ForeignKey("dbo.JobFunction", t => t.JobFunctionId)
                .ForeignKey("dbo.JobName", t => t.JobNameId, cascadeDelete: false)
                .ForeignKey("dbo.JobLevel", t => t.JobLevelId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.JobDegreeId)
                .Index(t => t.CarrerFieldId)
                .Index(t => t.JobLevelId)
                .Index(t => t.JobNameId)
                .Index(t => t.JobFunctionId);
            
            CreateTable(
                "dbo.CarrerField",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                        NationalityId = c.Int(),
                        ReligionId = c.Int(),
                        DateOfBirth = c.DateTime(storeType: "date"),
                        Gender = c.Int(),
                        BloodGroupId = c.Int(),
                        BirthPlaceCountryId = c.Int(),
                        BirthPlaceAreaId = c.Int(),
                        MaritalStatusId = c.Int(),
                        Residence = c.String(),
                        Code = c.Long(),
                        MobileNumber = c.String(maxLength: 50),
                        ProfileImge = c.String(nullable: false),
                        HousePhone = c.String(maxLength: 50),
                        EMail = c.String(maxLength: 250),
                        EmployeeGuid = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Area", t => t.BirthPlaceAreaId)
                .ForeignKey("dbo.BloodGroup", t => t.BloodGroupId)
                .ForeignKey("dbo.Country", t => t.BirthPlaceCountryId)
                .ForeignKey("dbo.MaritalStatus", t => t.MaritalStatusId)
                .ForeignKey("dbo.Nationality", t => t.NationalityId)
                .ForeignKey("dbo.Religion", t => t.ReligionId)
                .Index(t => t.NationalityId)
                .Index(t => t.ReligionId)
                .Index(t => t.BloodGroupId)
                .Index(t => t.BirthPlaceCountryId)
                .Index(t => t.BirthPlaceAreaId)
                .Index(t => t.MaritalStatusId);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: false)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BloodGroup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeContractDuration",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                        Duration = c.Double(),
                        EmployeeId = c.Int(nullable: false),
                        EmployeeStatusId = c.Int(nullable: false),
                        EmployeeStatusKindId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.EmployeeStatus", t => t.EmployeeStatusId, cascadeDelete: false)
                .ForeignKey("dbo.EmployeeStatusKind", t => t.EmployeeStatusKindId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.EmployeeStatusId)
                .Index(t => t.EmployeeStatusKindId);
            
            CreateTable(
                "dbo.EmployeeStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        EnName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeStatusKind",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        EnName = c.String(nullable: false, maxLength: 250),
                        EmployeeStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmployeeStatus", t => t.EmployeeStatusId, cascadeDelete: false)
                .Index(t => t.EmployeeStatusId);
            
            CreateTable(
                "dbo.EmployeeStatusTransaction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                        EmployeeStatusId = c.Int(nullable: false),
                        EmployeeStatusKindId = c.Int(nullable: false),
                        EmployeeContractDurationId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.EmployeeContractDuration", t => t.EmployeeContractDurationId)
                .ForeignKey("dbo.EmployeeStatus", t => t.EmployeeStatusId, cascadeDelete: false)
                .ForeignKey("dbo.EmployeeStatusKind", t => t.EmployeeStatusKindId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.EmployeeStatusId)
                .Index(t => t.EmployeeStatusKindId)
                .Index(t => t.EmployeeContractDurationId);
            
            CreateTable(
                "dbo.EmployeeDepartment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EnName = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Department", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.EmployeeExperience",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FromMonth = c.Int(nullable: false),
                        FromYear = c.Int(nullable: false),
                        ToMonth = c.Int(nullable: false),
                        ToYear = c.Int(nullable: false),
                        CompanyName = c.String(nullable: false),
                        JobSpecification = c.String(),
                        CompanyAddress = c.String(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.EmployeeLicenceData",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LicenceTypeHRId = c.Int(nullable: false),
                        LicenceKindHRId = c.Int(),
                        LicenseNumber = c.String(maxLength: 250),
                        ReleaseDate = c.DateTime(storeType: "date"),
                        EmployeeId = c.Int(nullable: false),
                        EndDate = c.DateTime(storeType: "date"),
                        SourceAreaId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Area", t => t.SourceAreaId)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.LicenceKindHR", t => t.LicenceKindHRId)
                .ForeignKey("dbo.LicenceTypeHR", t => t.LicenceTypeHRId, cascadeDelete: false)
                .Index(t => t.LicenceTypeHRId)
                .Index(t => t.LicenceKindHRId)
                .Index(t => t.EmployeeId)
                .Index(t => t.SourceAreaId);
            
            CreateTable(
                "dbo.LicenceKindHR",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        LicenceTypeHRId = c.Int(nullable: false),
                        EnName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LicenceTypeHR", t => t.LicenceTypeHRId, cascadeDelete: false)
                .Index(t => t.LicenceTypeHRId);
            
            CreateTable(
                "dbo.LicenceTypeHR",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        EnName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeLoan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        LoanValue = c.Double(nullable: false),
                        LoanDate = c.DateTime(storeType: "date"),
                        FromMonth = c.Int(nullable: false),
                        FromYear = c.Int(nullable: false),
                        ToMonth = c.Int(nullable: false),
                        ToYear = c.Int(nullable: false),
                        MonthlyValue = c.Double(nullable: false),
                        LoanStatus = c.Int(nullable: false),
                        UnderPaymentOrPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeID, cascadeDelete: false)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.EmployeeVacationAccounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        HolidayTypeId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        DaysNumber = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.HolidayType", t => t.HolidayTypeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.HolidayTypeId);
            
            CreateTable(
                "dbo.HolidayType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnName = c.String(nullable: false, maxLength: 250),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeVacation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        VacationDedutionId = c.Int(),
                        EmployeeMoneyDetailsId = c.Int(),
                        HolidayTypeId = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(nullable: false, storeType: "date"),
                        NODays = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.EmployeeMoneyDetails", t => t.EmployeeMoneyDetailsId)
                .ForeignKey("dbo.HolidayType", t => t.HolidayTypeId, cascadeDelete: false)
                .ForeignKey("dbo.VacationDeductions", t => t.VacationDedutionId)
                .Index(t => t.EmployeeId)
                .Index(t => t.VacationDedutionId)
                .Index(t => t.EmployeeMoneyDetailsId)
                .Index(t => t.HolidayTypeId);
            
            CreateTable(
                "dbo.EmployeeMoneyDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeMoneyId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        EmployeeMoneyTypeDetailsId = c.Int(),
                        IncreasesDeductionsSettingId = c.Int(),
                        Value = c.Double(nullable: false),
                        FromTableId = c.Int(),
                        FromTableName = c.String(maxLength: 500),
                        OperationType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.EmployeeMoney", t => t.EmployeeMoneyId, cascadeDelete: false)
                .ForeignKey("dbo.EmployeeMoneyTypeDetails", t => t.EmployeeMoneyTypeDetailsId)
                .ForeignKey("dbo.IncreasesDeductionsSetting", t => t.IncreasesDeductionsSettingId)
                .Index(t => t.EmployeeMoneyId)
                .Index(t => t.EmployeeId)
                .Index(t => t.EmployeeMoneyTypeDetailsId)
                .Index(t => t.IncreasesDeductionsSettingId);
            
            CreateTable(
                "dbo.EmployeeMoney",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        EmployeeMoneyTypeId = c.Int(nullable: false),
                        TransDate = c.DateTime(nullable: false, storeType: "date"),
                        TotalMoney = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        InMonth = c.Int(),
                        InYear = c.Int(),
                        ReceiptDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.EmplpoyeeMoneyType", t => t.EmployeeMoneyTypeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.EmployeeMoneyTypeId);
            
            CreateTable(
                "dbo.EmplpoyeeMoneyType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        EnName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeMoneyTypeDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        EnName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IncreasesDeductionsSetting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        EnName = c.String(nullable: false, maxLength: 250),
                        Value = c.Double(nullable: false),
                        RatioOrValue = c.Int(nullable: false),
                        BasicSalaryOrAll = c.Int(),
                        IncreasesDeductionsTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.IncreasesDeductionsType", t => t.IncreasesDeductionsTypeId, cascadeDelete: false)
                .Index(t => t.IncreasesDeductionsTypeId);
            
            CreateTable(
                "dbo.IncreasesDeductionsType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(),
                        IncreasesOrDeductions = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VacationDeductions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        DaysDate = c.String(nullable: false),
                        DaysNumber = c.Int(nullable: false),
                        HolidayTypeId = c.Int(nullable: false),
                        DeductionValue = c.Double(nullable: false),
                        DeductionFromMonth = c.Int(nullable: false),
                        DeductionFromYear = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.HolidayType", t => t.HolidayTypeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.HolidayTypeId);
            
            CreateTable(
                "dbo.MaritalStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MotivationEmployee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MotivationDate = c.DateTime(nullable: false, storeType: "date"),
                        MotivationTypeId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        MotivationStatus = c.Int(nullable: false),
                        WithSalaryOrForm = c.Int(nullable: false),
                        InMonth = c.Int(nullable: false),
                        InYear = c.Int(nullable: false),
                        Value = c.Double(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.MotivationType", t => t.MotivationTypeId, cascadeDelete: false)
                .Index(t => t.MotivationTypeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.MotivationType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        EnName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Nationality",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Trainee",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArName = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                        NationalityId = c.Int(nullable: false),
                        ReligionId = c.Int(),
                        DateOfBirth = c.DateTime(storeType: "date"),
                        NationalId = c.Long(),
                        NationalTypeId = c.Int(),
                        Residence = c.String(),
                        Code = c.String(maxLength: 50),
                        FileNo = c.String(maxLength: 50),
                        FileOpenDate = c.DateTime(storeType: "date"),
                        Gender = c.Int(),
                        SpeechLanguageId = c.Int(),
                        Photopath = c.String(),
                        TraineeGuid = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nationality", t => t.NationalityId, cascadeDelete: false)
                .ForeignKey("dbo.NationalType", t => t.NationalTypeId)
                .ForeignKey("dbo.Religion", t => t.ReligionId)
                .ForeignKey("dbo.SpeechLanguage", t => t.SpeechLanguageId)
                .Index(t => t.NationalityId)
                .Index(t => t.ReligionId)
                .Index(t => t.NationalTypeId)
                .Index(t => t.SpeechLanguageId);
            
            CreateTable(
                "dbo.ExamResult",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamDate = c.DateTime(nullable: false, storeType: "date"),
                        ExamTypeId = c.Int(nullable: false),
                        ExamResult = c.Int(nullable: false),
                        TraineeId = c.Int(nullable: false),
                        TraineeEvaluationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExamType", t => t.ExamTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Trainee", t => t.TraineeId, cascadeDelete: false)
                .ForeignKey("dbo.TraineeEvaluation", t => t.TraineeEvaluationId, cascadeDelete: false)
                .Index(t => t.ExamTypeId)
                .Index(t => t.TraineeId)
                .Index(t => t.TraineeEvaluationId);
            
            CreateTable(
                "dbo.ExamType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TraineeEvaluation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeId = c.Int(),
                        LicenceTypeId = c.Int(),
                        LicenceCategoryId = c.Int(),
                        StudyPeriodSettingId = c.Int(),
                        EvaluationEmployeeId = c.Int(),
                        EvaluationDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EvaluationEmployeeId)
                .ForeignKey("dbo.LicenceType", t => t.LicenceTypeId)
                .ForeignKey("dbo.LicenceCategory", t => t.LicenceCategoryId)
                .ForeignKey("dbo.StudyPeriodSetting", t => t.StudyPeriodSettingId)
                .ForeignKey("dbo.Trainee", t => t.TraineeId)
                .Index(t => t.TraineeId)
                .Index(t => t.LicenceTypeId)
                .Index(t => t.LicenceCategoryId)
                .Index(t => t.StudyPeriodSettingId)
                .Index(t => t.EvaluationEmployeeId);
            
            CreateTable(
                "dbo.AttendanceTable",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeEvaluationId = c.Int(nullable: false),
                        CourseDateTime = c.DateTime(nullable: false),
                        PracticalOrVisual = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TraineeEvaluation", t => t.TraineeEvaluationId, cascadeDelete: false)
                .Index(t => t.TraineeEvaluationId);
            
            CreateTable(
                "dbo.CourseReservation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeEvaluationId = c.Int(nullable: false),
                        CourseStartDate = c.DateTime(nullable: false, storeType: "date"),
                        CourseEndDate = c.DateTime(storeType: "date"),
                        CourseCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TraineeEvaluation", t => t.TraineeEvaluationId, cascadeDelete: false)
                .Index(t => t.TraineeEvaluationId);
            
            CreateTable(
                "dbo.LicenceCategory",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                        LicenceTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LicenceType", t => t.LicenceTypeId, cascadeDelete: false)
                .Index(t => t.LicenceTypeId);
            
            CreateTable(
                "dbo.LicenceType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                        MinimalAge = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SendToTraffic",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeId = c.Int(nullable: false),
                        TraineeEvaluationId = c.Int(nullable: false),
                        SendDate = c.DateTime(storeType: "date"),
                        SenderEmployeeId = c.Int(),
                        Employee_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.Employee_ID)
                .ForeignKey("dbo.Trainee", t => t.TraineeId, cascadeDelete: false)
                .ForeignKey("dbo.TraineeEvaluation", t => t.TraineeEvaluationId, cascadeDelete: false)
                .Index(t => t.TraineeId)
                .Index(t => t.TraineeEvaluationId)
                .Index(t => t.Employee_ID);
            
            CreateTable(
                "dbo.StudyPeriodSetting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DriveLevelId = c.Int(nullable: false),
                        VisualStudyCount = c.Int(nullable: false),
                        VisualStudyCost = c.Double(nullable: false),
                        VisualStudyTotal = c.Double(nullable: false),
                        PracticalCount = c.Int(nullable: false),
                        PracticalCost = c.Double(nullable: false),
                        PracticalTotal = c.Double(nullable: false),
                        LevelTotal = c.Double(nullable: false),
                        LevelStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DriveLevel", t => t.DriveLevelId, cascadeDelete: false)
                .Index(t => t.DriveLevelId);
            
            CreateTable(
                "dbo.DriveLevel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TraineeAttendance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeId = c.Int(nullable: false),
                        AttendanceDate = c.DateTime(nullable: false, storeType: "date"),
                        PracticalOrVisual = c.Int(nullable: false),
                        TraineeEvaluationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Trainee", t => t.TraineeId, cascadeDelete: false)
                .ForeignKey("dbo.TraineeEvaluation", t => t.TraineeEvaluationId, cascadeDelete: false)
                .Index(t => t.TraineeId)
                .Index(t => t.TraineeEvaluationId);
            
            CreateTable(
                "dbo.NationalType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReceiptVoucher",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PaidValue = c.Double(nullable: false),
                        PaidDate = c.DateTime(nullable: false),
                        ReceiptVoucherCode = c.Int(nullable: false),
                        ReceiptVoucherDate = c.DateTime(nullable: false),
                        TraineeId = c.Int(nullable: false),
                        CourseReservationId = c.Int(nullable: false),
                        EmployeeSafeId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CourseReservation", t => t.CourseReservationId, cascadeDelete: false)
                .ForeignKey("dbo.Employee", t => t.EmployeeSafeId)
                .ForeignKey("dbo.Trainee", t => t.TraineeId, cascadeDelete: false)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseReservationId)
                .Index(t => t.EmployeeSafeId);
            
            CreateTable(
                "dbo.Religion",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        EnName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SpeechLanguage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TraineeArchive",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArchiveSettingDriveId = c.Int(nullable: false),
                        TraineeId = c.Int(nullable: false),
                        Note = c.String(),
                        Number = c.String(),
                        Date = c.DateTime(),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArchiveSettingDrive", t => t.ArchiveSettingDriveId, cascadeDelete: false)
                .ForeignKey("dbo.Trainee", t => t.TraineeId, cascadeDelete: false)
                .Index(t => t.ArchiveSettingDriveId)
                .Index(t => t.TraineeId);
            
            CreateTable(
                "dbo.ArchiveSettingDrive",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        EnName = c.String(nullable: false, maxLength: 200),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArchiveSettingDrive", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.JobFunction",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                        JobNameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobName", t => t.JobNameId, cascadeDelete: false)
                .Index(t => t.JobNameId);
            
            CreateTable(
                "dbo.JobName",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JobLevel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EnName = c.String(nullable: false),
                        LevelSort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ArchiveSettingHRs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EnName = c.String(),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArchiveSettingHRs", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.CertificationGrade",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        NameEng = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeQualification",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CetificationTypeId = c.Int(nullable: false),
                        CertificationSpecificId = c.Int(nullable: false),
                        CertificationSpecDepartId = c.Int(),
                        CertificationGradeId = c.Int(),
                        QualificationDiscribtion = c.String(),
                        GraduationYear = c.Int(),
                        GraduationMonth = c.Int(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CertificationGrade", t => t.CertificationGradeId)
                .ForeignKey("dbo.CertificationType", t => t.CetificationTypeId, cascadeDelete: false)
                .ForeignKey("dbo.CertificationSpecDepart", t => t.CertificationSpecDepartId)
                .ForeignKey("dbo.CertificationSpecific", t => t.CertificationSpecificId, cascadeDelete: false)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.CetificationTypeId)
                .Index(t => t.CertificationSpecificId)
                .Index(t => t.CertificationSpecDepartId)
                .Index(t => t.CertificationGradeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.CertificationSpecDepart",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CertificationSpecificID = c.Int(),
                        EnName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CertificationSpecific", t => t.CertificationSpecificID)
                .Index(t => t.CertificationSpecificID);
            
            CreateTable(
                "dbo.CertificationSpecific",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        CertificationTypeID = c.Int(nullable: false),
                        EnName = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CertificationType", t => t.CertificationTypeID, cascadeDelete: false)
                .Index(t => t.CertificationTypeID);
            
            CreateTable(
                "dbo.CertificationType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        EnName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanySalaryExchangeDate",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeViolation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ViolationTypeId = c.Int(nullable: false),
                        ViolationStatus = c.Int(nullable: false),
                        FromMonth = c.Int(),
                        FromYear = c.Int(),
                        ViolationDate = c.DateTime(nullable: false, storeType: "date"),
                        ViolationValue = c.Double(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.ViolationType", t => t.ViolationTypeId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.ViolationTypeId);
            
            CreateTable(
                "dbo.ViolationType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        EnName = c.String(nullable: false, maxLength: 250),
                        DaysNumber = c.Double(),
                        FromBaseSalaryOrOverall = c.Int(),
                        FromMoneyOrMoral = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Honesties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        HonestyDate = c.DateTime(),
                        honestyEndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("dbo.Item", t => t.ItemId, cascadeDelete: false)
                .Index(t => t.EmployeeId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EnName = c.String(),
                        ItemType = c.String(),
                        SerialNumber = c.String(),
                        ItemCode = c.String(),
                        ItemPathImage = c.String(),
                        ItemParentId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Item", t => t.ItemParentId)
                .Index(t => t.ItemParentId);
            
            CreateTable(
                "dbo.JobTitle",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TraineeAttendingFollowup",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TraineeAttendanceId = c.Int(nullable: false),
                        AttendanceDatetime = c.DateTime(nullable: false),
                        LeaveDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TraineeAttendance", t => t.TraineeAttendanceId, cascadeDelete: false)
                .Index(t => t.TraineeAttendanceId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WorkTimesSetting",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayDayName = c.String(nullable: false),
                        DisplayDayNameEn = c.String(nullable: false),
                        DayName = c.String(nullable: false),
                        FromHour = c.Time(nullable: false, precision: 7),
                        ToHour = c.Time(nullable: false, precision: 7),
                        NOWorkHours = c.Double(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TraineeAttendingFollowup", "TraineeAttendanceId", "dbo.TraineeAttendance");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Honesties", "ItemId", "dbo.Item");
            DropForeignKey("dbo.Item", "ItemParentId", "dbo.Item");
            DropForeignKey("dbo.Honesties", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeViolation", "ViolationTypeId", "dbo.ViolationType");
            DropForeignKey("dbo.EmployeeViolation", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeQualification", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeQualification", "CertificationSpecificId", "dbo.CertificationSpecific");
            DropForeignKey("dbo.EmployeeQualification", "CertificationSpecDepartId", "dbo.CertificationSpecDepart");
            DropForeignKey("dbo.CertificationSpecDepart", "CertificationSpecificID", "dbo.CertificationSpecific");
            DropForeignKey("dbo.CertificationSpecific", "CertificationTypeID", "dbo.CertificationType");
            DropForeignKey("dbo.EmployeeQualification", "CetificationTypeId", "dbo.CertificationType");
            DropForeignKey("dbo.EmployeeQualification", "CertificationGradeId", "dbo.CertificationGrade");
            DropForeignKey("dbo.ArchiveSettingHRs", "ParentID", "dbo.ArchiveSettingHRs");
            DropForeignKey("dbo.AddIncreasingDeductionToJob", "IncreasingDeductionSettingId", "dbo.IncreasesDeductionsSetting");
            DropForeignKey("dbo.AddIncreasingDeductionToJob", "BasicSalarySettingId", "dbo.BasicSalarySetting");
            DropForeignKey("dbo.BasicSalarySetting", "JobLevelId", "dbo.JobLevel");
            DropForeignKey("dbo.BasicSalarySetting", "JobDegreeId", "dbo.JobDegree");
            DropForeignKey("dbo.EmployeeJobData", "JobLevelId", "dbo.JobLevel");
            DropForeignKey("dbo.JobFunction", "JobNameId", "dbo.JobName");
            DropForeignKey("dbo.EmployeeJobData", "JobNameId", "dbo.JobName");
            DropForeignKey("dbo.EmployeeJobData", "JobFunctionId", "dbo.JobFunction");
            DropForeignKey("dbo.EmployeeJobData", "JobDegreeId", "dbo.JobDegree");
            DropForeignKey("dbo.TraineeArchive", "TraineeId", "dbo.Trainee");
            DropForeignKey("dbo.TraineeArchive", "ArchiveSettingDriveId", "dbo.ArchiveSettingDrive");
            DropForeignKey("dbo.ArchiveSettingDrive", "ParentId", "dbo.ArchiveSettingDrive");
            DropForeignKey("dbo.Trainee", "SpeechLanguageId", "dbo.SpeechLanguage");
            DropForeignKey("dbo.Trainee", "ReligionId", "dbo.Religion");
            DropForeignKey("dbo.Employee", "ReligionId", "dbo.Religion");
            DropForeignKey("dbo.ReceiptVoucher", "TraineeId", "dbo.Trainee");
            DropForeignKey("dbo.ReceiptVoucher", "EmployeeSafeId", "dbo.Employee");
            DropForeignKey("dbo.ReceiptVoucher", "CourseReservationId", "dbo.CourseReservation");
            DropForeignKey("dbo.Trainee", "NationalTypeId", "dbo.NationalType");
            DropForeignKey("dbo.Trainee", "NationalityId", "dbo.Nationality");
            DropForeignKey("dbo.TraineeAttendance", "TraineeEvaluationId", "dbo.TraineeEvaluation");
            DropForeignKey("dbo.TraineeAttendance", "TraineeId", "dbo.Trainee");
            DropForeignKey("dbo.TraineeEvaluation", "TraineeId", "dbo.Trainee");
            DropForeignKey("dbo.TraineeEvaluation", "StudyPeriodSettingId", "dbo.StudyPeriodSetting");
            DropForeignKey("dbo.StudyPeriodSetting", "DriveLevelId", "dbo.DriveLevel");
            DropForeignKey("dbo.SendToTraffic", "TraineeEvaluationId", "dbo.TraineeEvaluation");
            DropForeignKey("dbo.SendToTraffic", "TraineeId", "dbo.Trainee");
            DropForeignKey("dbo.SendToTraffic", "Employee_ID", "dbo.Employee");
            DropForeignKey("dbo.TraineeEvaluation", "LicenceCategoryId", "dbo.LicenceCategory");
            DropForeignKey("dbo.TraineeEvaluation", "LicenceTypeId", "dbo.LicenceType");
            DropForeignKey("dbo.LicenceCategory", "LicenceTypeId", "dbo.LicenceType");
            DropForeignKey("dbo.ExamResult", "TraineeEvaluationId", "dbo.TraineeEvaluation");
            DropForeignKey("dbo.TraineeEvaluation", "EvaluationEmployeeId", "dbo.Employee");
            DropForeignKey("dbo.CourseReservation", "TraineeEvaluationId", "dbo.TraineeEvaluation");
            DropForeignKey("dbo.AttendanceTable", "TraineeEvaluationId", "dbo.TraineeEvaluation");
            DropForeignKey("dbo.ExamResult", "TraineeId", "dbo.Trainee");
            DropForeignKey("dbo.ExamResult", "ExamTypeId", "dbo.ExamType");
            DropForeignKey("dbo.Employee", "NationalityId", "dbo.Nationality");
            DropForeignKey("dbo.MotivationEmployee", "MotivationTypeId", "dbo.MotivationType");
            DropForeignKey("dbo.MotivationEmployee", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "MaritalStatusId", "dbo.MaritalStatus");
            DropForeignKey("dbo.EmployeeVacationAccounts", "HolidayTypeId", "dbo.HolidayType");
            DropForeignKey("dbo.EmployeeVacation", "VacationDedutionId", "dbo.VacationDeductions");
            DropForeignKey("dbo.VacationDeductions", "HolidayTypeId", "dbo.HolidayType");
            DropForeignKey("dbo.VacationDeductions", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeVacation", "HolidayTypeId", "dbo.HolidayType");
            DropForeignKey("dbo.EmployeeVacation", "EmployeeMoneyDetailsId", "dbo.EmployeeMoneyDetails");
            DropForeignKey("dbo.EmployeeMoneyDetails", "IncreasesDeductionsSettingId", "dbo.IncreasesDeductionsSetting");
            DropForeignKey("dbo.IncreasesDeductionsSetting", "IncreasesDeductionsTypeId", "dbo.IncreasesDeductionsType");
            DropForeignKey("dbo.EmployeeMoneyDetails", "EmployeeMoneyTypeDetailsId", "dbo.EmployeeMoneyTypeDetails");
            DropForeignKey("dbo.EmployeeMoney", "EmployeeMoneyTypeId", "dbo.EmplpoyeeMoneyType");
            DropForeignKey("dbo.EmployeeMoneyDetails", "EmployeeMoneyId", "dbo.EmployeeMoney");
            DropForeignKey("dbo.EmployeeMoney", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeMoneyDetails", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeVacation", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeVacationAccounts", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeLoan", "EmployeeID", "dbo.Employee");
            DropForeignKey("dbo.EmployeeLicenceData", "LicenceTypeHRId", "dbo.LicenceTypeHR");
            DropForeignKey("dbo.EmployeeLicenceData", "LicenceKindHRId", "dbo.LicenceKindHR");
            DropForeignKey("dbo.LicenceKindHR", "LicenceTypeHRId", "dbo.LicenceTypeHR");
            DropForeignKey("dbo.EmployeeLicenceData", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeLicenceData", "SourceAreaId", "dbo.Area");
            DropForeignKey("dbo.EmployeeJobData", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeExperience", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeDepartment", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeDepartment", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Department", "ParentId", "dbo.Department");
            DropForeignKey("dbo.EmployeeContractDuration", "EmployeeStatusKindId", "dbo.EmployeeStatusKind");
            DropForeignKey("dbo.EmployeeContractDuration", "EmployeeStatusId", "dbo.EmployeeStatus");
            DropForeignKey("dbo.EmployeeStatusTransaction", "EmployeeStatusKindId", "dbo.EmployeeStatusKind");
            DropForeignKey("dbo.EmployeeStatusTransaction", "EmployeeStatusId", "dbo.EmployeeStatus");
            DropForeignKey("dbo.EmployeeStatusTransaction", "EmployeeContractDurationId", "dbo.EmployeeContractDuration");
            DropForeignKey("dbo.EmployeeStatusTransaction", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.EmployeeStatusKind", "EmployeeStatusId", "dbo.EmployeeStatus");
            DropForeignKey("dbo.EmployeeContractDuration", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Employee", "BirthPlaceCountryId", "dbo.Country");
            DropForeignKey("dbo.Employee", "BloodGroupId", "dbo.BloodGroup");
            DropForeignKey("dbo.Employee", "BirthPlaceAreaId", "dbo.Area");
            DropForeignKey("dbo.Area", "CountryId", "dbo.Country");
            DropForeignKey("dbo.EmployeeJobData", "CarrerFieldId", "dbo.CarrerField");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TraineeAttendingFollowup", new[] { "TraineeAttendanceId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Item", new[] { "ItemParentId" });
            DropIndex("dbo.Honesties", new[] { "ItemId" });
            DropIndex("dbo.Honesties", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeViolation", new[] { "ViolationTypeId" });
            DropIndex("dbo.EmployeeViolation", new[] { "EmployeeId" });
            DropIndex("dbo.CertificationSpecific", new[] { "CertificationTypeID" });
            DropIndex("dbo.CertificationSpecDepart", new[] { "CertificationSpecificID" });
            DropIndex("dbo.EmployeeQualification", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeQualification", new[] { "CertificationGradeId" });
            DropIndex("dbo.EmployeeQualification", new[] { "CertificationSpecDepartId" });
            DropIndex("dbo.EmployeeQualification", new[] { "CertificationSpecificId" });
            DropIndex("dbo.EmployeeQualification", new[] { "CetificationTypeId" });
            DropIndex("dbo.ArchiveSettingHRs", new[] { "ParentID" });
            DropIndex("dbo.JobFunction", new[] { "JobNameId" });
            DropIndex("dbo.ArchiveSettingDrive", new[] { "ParentId" });
            DropIndex("dbo.TraineeArchive", new[] { "TraineeId" });
            DropIndex("dbo.TraineeArchive", new[] { "ArchiveSettingDriveId" });
            DropIndex("dbo.ReceiptVoucher", new[] { "EmployeeSafeId" });
            DropIndex("dbo.ReceiptVoucher", new[] { "CourseReservationId" });
            DropIndex("dbo.ReceiptVoucher", new[] { "TraineeId" });
            DropIndex("dbo.TraineeAttendance", new[] { "TraineeEvaluationId" });
            DropIndex("dbo.TraineeAttendance", new[] { "TraineeId" });
            DropIndex("dbo.StudyPeriodSetting", new[] { "DriveLevelId" });
            DropIndex("dbo.SendToTraffic", new[] { "Employee_ID" });
            DropIndex("dbo.SendToTraffic", new[] { "TraineeEvaluationId" });
            DropIndex("dbo.SendToTraffic", new[] { "TraineeId" });
            DropIndex("dbo.LicenceCategory", new[] { "LicenceTypeId" });
            DropIndex("dbo.CourseReservation", new[] { "TraineeEvaluationId" });
            DropIndex("dbo.AttendanceTable", new[] { "TraineeEvaluationId" });
            DropIndex("dbo.TraineeEvaluation", new[] { "EvaluationEmployeeId" });
            DropIndex("dbo.TraineeEvaluation", new[] { "StudyPeriodSettingId" });
            DropIndex("dbo.TraineeEvaluation", new[] { "LicenceCategoryId" });
            DropIndex("dbo.TraineeEvaluation", new[] { "LicenceTypeId" });
            DropIndex("dbo.TraineeEvaluation", new[] { "TraineeId" });
            DropIndex("dbo.ExamResult", new[] { "TraineeEvaluationId" });
            DropIndex("dbo.ExamResult", new[] { "TraineeId" });
            DropIndex("dbo.ExamResult", new[] { "ExamTypeId" });
            DropIndex("dbo.Trainee", new[] { "SpeechLanguageId" });
            DropIndex("dbo.Trainee", new[] { "NationalTypeId" });
            DropIndex("dbo.Trainee", new[] { "ReligionId" });
            DropIndex("dbo.Trainee", new[] { "NationalityId" });
            DropIndex("dbo.MotivationEmployee", new[] { "EmployeeId" });
            DropIndex("dbo.MotivationEmployee", new[] { "MotivationTypeId" });
            DropIndex("dbo.VacationDeductions", new[] { "HolidayTypeId" });
            DropIndex("dbo.VacationDeductions", new[] { "EmployeeId" });
            DropIndex("dbo.IncreasesDeductionsSetting", new[] { "IncreasesDeductionsTypeId" });
            DropIndex("dbo.EmployeeMoney", new[] { "EmployeeMoneyTypeId" });
            DropIndex("dbo.EmployeeMoney", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeMoneyDetails", new[] { "IncreasesDeductionsSettingId" });
            DropIndex("dbo.EmployeeMoneyDetails", new[] { "EmployeeMoneyTypeDetailsId" });
            DropIndex("dbo.EmployeeMoneyDetails", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeMoneyDetails", new[] { "EmployeeMoneyId" });
            DropIndex("dbo.EmployeeVacation", new[] { "HolidayTypeId" });
            DropIndex("dbo.EmployeeVacation", new[] { "EmployeeMoneyDetailsId" });
            DropIndex("dbo.EmployeeVacation", new[] { "VacationDedutionId" });
            DropIndex("dbo.EmployeeVacation", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeVacationAccounts", new[] { "HolidayTypeId" });
            DropIndex("dbo.EmployeeVacationAccounts", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeLoan", new[] { "EmployeeID" });
            DropIndex("dbo.LicenceKindHR", new[] { "LicenceTypeHRId" });
            DropIndex("dbo.EmployeeLicenceData", new[] { "SourceAreaId" });
            DropIndex("dbo.EmployeeLicenceData", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeLicenceData", new[] { "LicenceKindHRId" });
            DropIndex("dbo.EmployeeLicenceData", new[] { "LicenceTypeHRId" });
            DropIndex("dbo.EmployeeExperience", new[] { "EmployeeId" });
            DropIndex("dbo.Department", new[] { "ParentId" });
            DropIndex("dbo.EmployeeDepartment", new[] { "DepartmentId" });
            DropIndex("dbo.EmployeeDepartment", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeStatusTransaction", new[] { "EmployeeContractDurationId" });
            DropIndex("dbo.EmployeeStatusTransaction", new[] { "EmployeeStatusKindId" });
            DropIndex("dbo.EmployeeStatusTransaction", new[] { "EmployeeStatusId" });
            DropIndex("dbo.EmployeeStatusTransaction", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeStatusKind", new[] { "EmployeeStatusId" });
            DropIndex("dbo.EmployeeContractDuration", new[] { "EmployeeStatusKindId" });
            DropIndex("dbo.EmployeeContractDuration", new[] { "EmployeeStatusId" });
            DropIndex("dbo.EmployeeContractDuration", new[] { "EmployeeId" });
            DropIndex("dbo.Area", new[] { "CountryId" });
            DropIndex("dbo.Employee", new[] { "MaritalStatusId" });
            DropIndex("dbo.Employee", new[] { "BirthPlaceAreaId" });
            DropIndex("dbo.Employee", new[] { "BirthPlaceCountryId" });
            DropIndex("dbo.Employee", new[] { "BloodGroupId" });
            DropIndex("dbo.Employee", new[] { "ReligionId" });
            DropIndex("dbo.Employee", new[] { "NationalityId" });
            DropIndex("dbo.EmployeeJobData", new[] { "JobFunctionId" });
            DropIndex("dbo.EmployeeJobData", new[] { "JobNameId" });
            DropIndex("dbo.EmployeeJobData", new[] { "JobLevelId" });
            DropIndex("dbo.EmployeeJobData", new[] { "CarrerFieldId" });
            DropIndex("dbo.EmployeeJobData", new[] { "JobDegreeId" });
            DropIndex("dbo.EmployeeJobData", new[] { "EmployeeId" });
            DropIndex("dbo.BasicSalarySetting", new[] { "JobLevelId" });
            DropIndex("dbo.BasicSalarySetting", new[] { "JobDegreeId" });
            DropIndex("dbo.AddIncreasingDeductionToJob", new[] { "BasicSalarySettingId" });
            DropIndex("dbo.AddIncreasingDeductionToJob", new[] { "IncreasingDeductionSettingId" });
            DropTable("dbo.WorkTimesSetting");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TraineeAttendingFollowup");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.JobTitle");
            DropTable("dbo.Item");
            DropTable("dbo.Honesties");
            DropTable("dbo.ViolationType");
            DropTable("dbo.EmployeeViolation");
            DropTable("dbo.CompanySalaryExchangeDate");
            DropTable("dbo.CertificationType");
            DropTable("dbo.CertificationSpecific");
            DropTable("dbo.CertificationSpecDepart");
            DropTable("dbo.EmployeeQualification");
            DropTable("dbo.CertificationGrade");
            DropTable("dbo.ArchiveSettingHRs");
            DropTable("dbo.JobLevel");
            DropTable("dbo.JobName");
            DropTable("dbo.JobFunction");
            DropTable("dbo.ArchiveSettingDrive");
            DropTable("dbo.TraineeArchive");
            DropTable("dbo.SpeechLanguage");
            DropTable("dbo.Religion");
            DropTable("dbo.ReceiptVoucher");
            DropTable("dbo.NationalType");
            DropTable("dbo.TraineeAttendance");
            DropTable("dbo.DriveLevel");
            DropTable("dbo.StudyPeriodSetting");
            DropTable("dbo.SendToTraffic");
            DropTable("dbo.LicenceType");
            DropTable("dbo.LicenceCategory");
            DropTable("dbo.CourseReservation");
            DropTable("dbo.AttendanceTable");
            DropTable("dbo.TraineeEvaluation");
            DropTable("dbo.ExamType");
            DropTable("dbo.ExamResult");
            DropTable("dbo.Trainee");
            DropTable("dbo.Nationality");
            DropTable("dbo.MotivationType");
            DropTable("dbo.MotivationEmployee");
            DropTable("dbo.MaritalStatus");
            DropTable("dbo.VacationDeductions");
            DropTable("dbo.IncreasesDeductionsType");
            DropTable("dbo.IncreasesDeductionsSetting");
            DropTable("dbo.EmployeeMoneyTypeDetails");
            DropTable("dbo.EmplpoyeeMoneyType");
            DropTable("dbo.EmployeeMoney");
            DropTable("dbo.EmployeeMoneyDetails");
            DropTable("dbo.EmployeeVacation");
            DropTable("dbo.HolidayType");
            DropTable("dbo.EmployeeVacationAccounts");
            DropTable("dbo.EmployeeLoan");
            DropTable("dbo.LicenceTypeHR");
            DropTable("dbo.LicenceKindHR");
            DropTable("dbo.EmployeeLicenceData");
            DropTable("dbo.EmployeeExperience");
            DropTable("dbo.Department");
            DropTable("dbo.EmployeeDepartment");
            DropTable("dbo.EmployeeStatusTransaction");
            DropTable("dbo.EmployeeStatusKind");
            DropTable("dbo.EmployeeStatus");
            DropTable("dbo.EmployeeContractDuration");
            DropTable("dbo.BloodGroup");
            DropTable("dbo.Country");
            DropTable("dbo.Area");
            DropTable("dbo.Employee");
            DropTable("dbo.CarrerField");
            DropTable("dbo.EmployeeJobData");
            DropTable("dbo.JobDegree");
            DropTable("dbo.BasicSalarySetting");
            DropTable("dbo.AddIncreasingDeductionToJob");
        }
    }
}
