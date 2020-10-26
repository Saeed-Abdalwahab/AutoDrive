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
  public  class EmployeeContractDurationService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private Repository<EmployeeContractDuration> repository;
        public EmployeeContractDurationService()
        {
            repository = new Repository<EmployeeContractDuration>(unitOfWork);
        }
        public EmployeeContractDurationVM GetEmployeeContractDurationByID(int id)
        {
            return Mapper.Map(repository.Get(id), new EmployeeContractDurationVM());
        }
        public IEnumerable<EmployeeContractDurationVM> EmployeeContractDurationVMs(int employeeid)
        {

            return Mapper.Map(repository.Find(x => x.EmployeeId == employeeid).ToList(), new List<EmployeeContractDurationVM>());
        }
        public bool SaveInDB(EmployeeContractDurationVM vM)
        {
            try
            {
                if (vM.ID == 0)
                {
                    unitOfWork.CreateTransaction();
                    var EmployeeContractDuration = Mapper.Map(vM, new EmployeeContractDuration());
                    repository.Add(EmployeeContractDuration);
                    unitOfWork.Save();

                    Repository<EmployeeStatusTransaction> repositoryEmployeeStatusTransaction = new Repository<EmployeeStatusTransaction>(unitOfWork);
                    EmployeeStatusTransaction employeeStatusTransaction = new EmployeeStatusTransaction();
                    employeeStatusTransaction.EmployeeId = EmployeeContractDuration.EmployeeId;
                    employeeStatusTransaction.StartDate = EmployeeContractDuration.FromDate;
                    employeeStatusTransaction.EndDate = EmployeeContractDuration.EndDate;
                    employeeStatusTransaction.EmployeeStatusKindId = EmployeeContractDuration.EmployeeStatusKindId;
                    employeeStatusTransaction.EmployeeContractDurationId = EmployeeContractDuration.ID;
                    employeeStatusTransaction.EmployeeStatusId = EmployeeContractDuration.EmployeeStatusId;
                    repositoryEmployeeStatusTransaction.Add(employeeStatusTransaction);
                   
                    unitOfWork.Save();

                    unitOfWork.Commit();
                    return true;
                }
                else
                {
                    unitOfWork.CreateTransaction();

                    repository.Update(Mapper.Map(vM, new EmployeeContractDuration()));
                    unitOfWork.Save();


                    Repository<EmployeeStatusTransaction> repositoryEmployeeStatusTransaction = new Repository<EmployeeStatusTransaction>(unitOfWork);
                    EmployeeStatusTransaction employeeStatusTransaction = repositoryEmployeeStatusTransaction.FristOrDefault(x => x.EmployeeContractDurationId == vM.ID);
                    employeeStatusTransaction.EmployeeId = vM.EmployeeId;
                    employeeStatusTransaction.StartDate = vM.FromDate;
                    employeeStatusTransaction.EndDate = vM.EndDate;
                    employeeStatusTransaction.EmployeeStatusKindId = vM.EmployeeStatusKindId;

                    employeeStatusTransaction.EmployeeStatusId = vM.EmployeeStatusId;
                    repositoryEmployeeStatusTransaction.Update(employeeStatusTransaction);

                    unitOfWork.Save();
                    
                    unitOfWork.Commit();

                    return true;
                }

            }
            catch
            {
                unitOfWork.Rollback();

                return false;

            }
        }

        public string delete(int ID)
        {
            try
            {
                unitOfWork.CreateTransaction();
                Repository<EmployeeStatusTransaction> repositoryEmployeeStatusTransaction = new Repository<EmployeeStatusTransaction>(unitOfWork);
                EmployeeStatusTransaction employeeStatusTransaction = repositoryEmployeeStatusTransaction.FristOrDefault(x => x.EmployeeContractDurationId == ID);

                repositoryEmployeeStatusTransaction.Remove(employeeStatusTransaction);
                unitOfWork.Save();


                repository.Remove(repository.Get(ID));

                unitOfWork.Save();

             
                unitOfWork.Commit();
                return Messages.DeleteSucc;
            }
            catch
            {
                unitOfWork.Rollback();
                return Messages.DeleteErr;

            }
        }
    }
}
