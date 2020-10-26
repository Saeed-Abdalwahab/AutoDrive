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
   public class EmployeeJobDataService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private Repository<EmployeeJobData> repository;
        public EmployeeJobDataService()
        {
            repository = new Repository<EmployeeJobData>(unitOfWork);
        }
        public EmployeeJobDataVM GetEmployeeJobDataByID(int id)
        {
            return Mapper.Map(repository.Get(id), new EmployeeJobDataVM());
        }
        public IEnumerable<EmployeeJobDataVM> EmployeeJobDataVMs(int employeeid)
        {

            return Mapper.Map(repository.Find(x => x.EmployeeId == employeeid).OrderByDescending(x=>x.StartDate), new List<EmployeeJobDataVM>());
        }
        public bool SaveInDB(EmployeeJobDataVM vM)
        {
            try
            {
                if (vM.ID == 0)
                {
                    var test = repository.FristOrDefault(x => x.EmployeeId==vM.EmployeeId&&x.JobNameId == vM.JobNameId && x.JobLevelId == vM.JobLevelId && x.CarrerFieldId == vM.CarrerFieldId && x.JobFunctionId == vM.JobFunctionId && x.JobDegreeId == vM.JobDegreeId);
                    if (test != null)
                        return false;
                   
                        repository.Add(Mapper.Map(vM, new EmployeeJobData()));
                    unitOfWork.Save();
                    return true;
                }
                else
                {
                    var test = repository.FristOrDefault(x => x.ID!=vM.ID&&x.EmployeeId==vM.EmployeeId&& x.JobNameId == vM.JobNameId && x.JobLevelId == vM.JobLevelId && x.CarrerFieldId == vM.CarrerFieldId && x.JobFunctionId == vM.JobFunctionId && x.JobDegreeId == vM.JobDegreeId);
                    if (test != null)
                        return false;
                    repository.Update(Mapper.Map(vM, new EmployeeJobData()));
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
