using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;

using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.Core.HR;
using AutoDrive.VM.AutoDriveHR;
using AutoMapper;
using System.Linq;
using AutoDriveResources;

namespace AutoDrive.BLL.HRAutoDrive
{
    public class EmployeeDepartmentService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private IDepartmentemployeeRepository repository;
        public EmployeeDepartmentService()
        {
            repository = new DepartmentEmployeeRepository(unitOfWork);
        }
        public EmployeeDepartmentVM GetEmployeeDepartmentByID(int id)
        {
            return Mapper.Map(repository.Get(id), new EmployeeDepartmentVM());
        }
        public IEnumerable<EmployeeDepartmentVM> employeeDepartmentVMs(int employeeid)
        {
       
            return Mapper.Map(repository.Find(x => x.EmployeeId == employeeid).ToList(), new List<EmployeeDepartmentVM>()).OrderByDescending(x=>x.StartDate);
        }
        public bool SaveInDB(EmployeeDepartmentVM vM)
        {
            try { 
            if (vM.ID == 0)
            {
                    repository.Add(Mapper.Map(vM, new EmployeeDepartment()));
                    unitOfWork.Save();
                    return true;
            }
            else
            {
                    repository.Update(Mapper.Map(vM, new EmployeeDepartment()));
                    unitOfWork.Save();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public string delete(int ID)
        {
            
            repository.Remove(repository.Get(ID));
            unitOfWork.Save();
            return Messages.DeleteSucc;
        }
    }
}
