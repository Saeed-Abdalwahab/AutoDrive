using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
  public  class employeeStatusKindservice
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        public IEnumerable<EmployeeStatusKind> employeeStatusKinds(int employeestatusid)
        {
            Repository<EmployeeStatusKind> repository = new Repository<EmployeeStatusKind>(unitOfWork);
            return repository.Find(x => x.EmployeeStatusId == employeestatusid);
        }
    }
}
