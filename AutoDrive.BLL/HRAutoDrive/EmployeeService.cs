using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Text;
using System.Threading.Tasks;
using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.Core.HR;
using System.Linq;
using System.Web;

namespace AutoDrive.BLL.HRAutoDrive
{
   public class EmployeeService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private IEmployeeRepository employeeRepository;
        ApplicationDbContext context = new ApplicationDbContext();
        public EmployeeService()
        {
            employeeRepository = new EmployeeRepository(unitOfWork);
        }
        public EmployeeVM GetByID(string id)
        {
            return Mapper.Map(employeeRepository.FristOrDefault(x=>x.EmployeeGuid.ToString()==id),new EmployeeVM());
        }
        public EmployeeVM GetByid(Guid id)
        {

            var model = (from E in context.Employees.Where(o=>o.EmployeeGuid== id)
                         join ED in context.EmployeeDepartments on E.ID equals ED.EmployeeId
                         join D in context.Departments on ED.DepartmentId equals D.ID
                         select new EmployeeVM()
                         {
                             ID = E.ID,
                             EnName = E.EnName,
                             Name = E.Name,
                             Code = E.Code,
                             EmployeeDepartment=D.Name
                         }).FirstOrDefault();

            return model;

        }
        public IEnumerable<Employee> employees()
        {
            return employeeRepository.GetAll();
        }
        public IEnumerable<Employee> Employees(string Any, string language)
        {

            if (language == "en-US")
            {
                return employeeRepository.Find(x => x.EnName.Contains(Any)).ToList().Select(x => new Employee()
                {
                    ID = x.ID,
                    Name =  x.EnName,
                    EmployeeGuid=x.EmployeeGuid
                });
            }
            else
            {
               
                return employeeRepository.Find(x => x.Name.Contains(Any)).ToList().Select(x => new Employee()
                {
                    ID = x.ID,
                    Name = x.Name,
                    EmployeeGuid=x.EmployeeGuid

                });
            }

        }


        public bool EditEmployee(EmployeeVM employeeVM)
        {
            try {

          
            employeeRepository.Update(Mapper.Map(employeeVM,new Employee()));
            unitOfWork.Save();
         
                
                return true;
            }
            catch
            {
                unitOfWork.Rollback();
                return false;
            }
        }
        public bool SaveAllEmployeeInfo(EmployeeDepartMentQulificationsVM employeeDepartMentQulificationsVM,out Employee employeereturn)
        {
            try
            {
                
                unitOfWork.CreateTransaction();
                //Employee employee = GetEmployee(employeeDepartMentQulificationsVM.employeeVM);
                Employee employee = Mapper.Map(employeeDepartMentQulificationsVM.employeeVM,new Employee());

                
                employee.ProfileImge = employeeDepartMentQulificationsVM.employeeVM.ProfileImge;
                employee.EmployeeGuid = Guid.NewGuid();
                employeeRepository.Add(employee);
                unitOfWork.Save();
                employeereturn = employee;
               

                employeeDepartMentQulificationsVM.EmployeeDepartmentVM.EmployeeId = employee.ID;
                EmployeeDepartment employeeDepartment = new EmployeeDepartment();
                Mapper.Map(employeeDepartMentQulificationsVM.EmployeeDepartmentVM, employeeDepartment);
                Repository<EmployeeDepartment> repositoryDepartment = new Repository<EmployeeDepartment>(unitOfWork);
                employeeDepartment.EmployeeId = employee.ID;
                repositoryDepartment.Add(employeeDepartment);
                unitOfWork.Save();


                Repository<EmployeeQualification> repositoryQulifications = new Repository<EmployeeQualification>(unitOfWork);
                EmployeeQualification employeeQualification = new EmployeeQualification();
                Mapper.Map(employeeDepartMentQulificationsVM.employeeQualificationVM, employeeQualification);
                employeeQualification.EmployeeId = employee.ID;
                repositoryQulifications.Add(employeeQualification);
                unitOfWork.Save();


                Repository<EmployeeContractDuration> repositoryEmployeeContractDuration = new Repository<EmployeeContractDuration>(unitOfWork);
                EmployeeContractDuration employeeContractDuration = new EmployeeContractDuration();
                Mapper.Map(employeeDepartMentQulificationsVM.employeeContractDurationVM, employeeContractDuration);
                employeeContractDuration.EmployeeId = employee.ID;
                employeeContractDuration.EmployeeStatusId = (int)EmployeeStatus.InService;
                repositoryEmployeeContractDuration.Add(employeeContractDuration);
                unitOfWork.Save();

                Repository<EmployeeStatusTransaction> repositoryEmployeeStatusTransaction = new Repository<EmployeeStatusTransaction>(unitOfWork);
                EmployeeStatusTransaction employeeStatusTransaction = new EmployeeStatusTransaction();
                employeeStatusTransaction.EmployeeId = employee.ID;
                employeeStatusTransaction.StartDate = employeeContractDuration.FromDate;
                employeeStatusTransaction.EndDate = employeeContractDuration.EndDate;
                employeeStatusTransaction.EmployeeStatusKindId = employeeContractDuration.EmployeeStatusKindId;
                employeeStatusTransaction.EmployeeContractDurationId = employeeContractDuration.ID;
           
                employeeStatusTransaction.EmployeeStatusId = employeeContractDuration.EmployeeStatusId;

                repositoryEmployeeStatusTransaction.Add(employeeStatusTransaction);
                unitOfWork.Save();

                if (employeeDepartMentQulificationsVM.employeeExperienceVM != null) { 
                Repository<EmployeeExperience> repositoryEmployeeExperience = new Repository<EmployeeExperience>(unitOfWork);
                EmployeeExperience EmployeeExperience = new EmployeeExperience();
                Mapper.Map(employeeDepartMentQulificationsVM.employeeExperienceVM, EmployeeExperience);
                EmployeeExperience.EmployeeId = employee.ID;
                repositoryEmployeeExperience.Add(EmployeeExperience);
                unitOfWork.Save();
                }

                Repository<EmployeeLicenceData> repositoryEmployeeLicenceData = new Repository<EmployeeLicenceData>(unitOfWork);
                EmployeeLicenceData employeeLicenceData = new EmployeeLicenceData();
                Mapper.Map(employeeDepartMentQulificationsVM.employeeLicenceDataVM, employeeLicenceData);
                employeeLicenceData.EmployeeId = employee.ID;
                repositoryEmployeeLicenceData.Add(employeeLicenceData);
                unitOfWork.Save();


                 Repository<EmployeeJobData> repositoryEmployeeJobData = new Repository<EmployeeJobData>(unitOfWork);
                EmployeeJobData employeeJobData = new EmployeeJobData();
                employeeDepartMentQulificationsVM.EmployeeJobDataVM.EmployeeId = employee.ID;
                Mapper.Map(employeeDepartMentQulificationsVM.EmployeeJobDataVM, employeeJobData);
                repositoryEmployeeJobData.Add(employeeJobData);
                unitOfWork.Save();




                unitOfWork.Commit();



                    return true;
            }
            catch 
            {
                unitOfWork.Rollback();
                employeereturn = null;
                return false;
            }

        }


        //Helper methods
        public IEnumerable<Nationality> Nationalities()
        {
            Repository<Nationality> repository = new Repository<Nationality>(unitOfWork);
            return repository.GetAll();

        }
        public IEnumerable<Religion> Religions()
        {
            Repository<Religion> repository = new Repository<Religion>(unitOfWork);
            return repository.GetAll();

        }
        public IEnumerable<BloodGroup> BloodGroups()
        {
            Repository<BloodGroup> repository = new Repository<BloodGroup>(unitOfWork);
            return repository.GetAll();

        }
        public IEnumerable<LicenceTypeHR> licenceTypeHRs()
        {
            Repository<LicenceTypeHR> repository = new Repository<LicenceTypeHR>(unitOfWork);
            return repository.GetAll();

        }
        public IEnumerable<Area> areas()
        {
            Repository<Area> repository = new Repository<Area>(unitOfWork);
            return repository.GetAll();
        } public IEnumerable<Country> countries()
        {
            Repository<Country> repository = new Repository<Country>(unitOfWork);
            return repository.GetAll();
        } public IEnumerable<MaritalStatu> maritalStatus()
        {
            Repository<MaritalStatu> repository = new Repository<MaritalStatu>(unitOfWork);
            return repository.GetAll();
        } public IEnumerable<JobDegree> JobDegrees()
        {
            Repository<JobDegree> repository = new Repository<JobDegree>(unitOfWork);
            return repository.GetAll();
        }
        public IEnumerable<CarrerField> CarrerFields()
        {
            Repository<CarrerField> repository = new Repository<CarrerField>(unitOfWork);
            return repository.GetAll();
        }
        public IEnumerable<JobLevel> JobLevels()
        {
            Repository<JobLevel> repository = new Repository<JobLevel>(unitOfWork);
            return repository.GetAll();
        }
        public IEnumerable<JobName> JobNames()
        {
            Repository<JobName> repository = new Repository<JobName>(unitOfWork);
            return repository.GetAll();
        }  public IEnumerable<JobFunction> JobFunctions()
        {
            Repository<JobFunction> repository = new Repository<JobFunction>(unitOfWork);
            return repository.GetAll();
        }
        public IEnumerable<CertificationType> certificationTypes()
        {
            Repository<CertificationType> repository = new Repository<CertificationType>(unitOfWork);
            return repository.GetAll();
        }
        public IEnumerable<CertificationGrade> certificationGrades()
        {
            Repository<CertificationGrade> repository = new Repository<CertificationGrade>(unitOfWork);
            return repository.GetAll();
        }
        public IEnumerable<EmployeeStatu> employeeStatus()
        {
            Repository<EmployeeStatu> repository = new Repository<EmployeeStatu>(unitOfWork);
            return repository.GetAll();
        }  public IEnumerable<DAL.AutoDriveDB.EmployeeStatusKind> EmployeeStatusKinds()
        {
            Repository<DAL.AutoDriveDB.EmployeeStatusKind> repository = new Repository<DAL.AutoDriveDB.EmployeeStatusKind>(unitOfWork);
            return repository.Find(x=>x.EmployeeStatusId==(int)EmployeeStatus.InService);
        }




    }
}
