using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutoDrive.BLL.HRAutoDrive
{
  public  class JobNameService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<JobName> repository;

        public JobNameService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<JobName>(unitOfWork);
        }

        public List<JobNameVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<JobNameVM>());
        }
        public JobNameVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new JobNameVM());
        }

        public string Save(JobNameVM JobNameVM)
        {

            repository.Add(Mapper.Map(JobNameVM, new JobName()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(JobNameVM JobNameVM)
        {

            repository.Update(Mapper.Map(JobNameVM, new JobName()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var JobName = repository.Get(ID);
            repository.Remove(JobName);
            unitOfWork.Save();
            return Messages.DeleteSucc;
        }
        public bool NameCheck(string Name, int ID)
        {
            if (ID == 0)
                return repository.FristOrDefault(x => x.Name == Name) == null ? true : false;
            return repository.FristOrDefault(x => x.Name == Name && x.ID != ID) == null ? true : false;
        }
        public bool ENNameCheck(string EnName, int ID)
        {
            if (ID == 0)
                return repository.FristOrDefault(x => x.EnName == EnName) == null ? true : false;
            return repository.FristOrDefault(x => x.EnName == EnName && x.ID != ID) == null ? true : false;

        }
    }
}
