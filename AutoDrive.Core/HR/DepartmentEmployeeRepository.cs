
using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;

namespace AutoDrive.Core.HR
{
  public  class DepartmentEmployeeRepository: Repository<EmployeeDepartment>,IDepartmentemployeeRepository
    {
        public DepartmentEmployeeRepository(IUnitOfWork<ApplicationDbContext> unitOfWork)
: base(unitOfWork)
        {
        }
        public DepartmentEmployeeRepository(ApplicationDbContext context)
        : base(context)
        {
        }
    }
}
