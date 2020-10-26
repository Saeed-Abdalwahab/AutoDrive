
using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;


namespace AutoDrive.Core.HR
{
   public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork<ApplicationDbContext> unitOfWork)
        : base(unitOfWork)
        {
        }
        public EmployeeRepository(ApplicationDbContext context)
        : base(context)
        {
        }      
    }
}
