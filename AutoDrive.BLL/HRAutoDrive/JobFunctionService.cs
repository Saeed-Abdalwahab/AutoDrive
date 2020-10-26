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
   public class JobFunctionService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<JobFunction> repository;

        public JobFunctionService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<JobFunction>(unitOfWork);
        }
        public List<JobFunctionVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<JobFunctionVM>());
        }
        public JobFunctionVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new JobFunctionVM());
        }
        public List<JobFunctionVM> GetJobFunctionsByJobNameId(int JobNameId, string language)
        {
            var List = language == "en" ? repository.Find(x => x.JobNameId == JobNameId).Select(x => new JobFunctionVM { ID = x.ID, Name = x.EnName }) :
                repository.Find(x => x.JobNameId == JobNameId).Select(x => new JobFunctionVM { ID = x.ID, Name = x.Name });
            return Mapper.Map(List, new List<JobFunctionVM>());
        }
        public int JobNameIdByJobFunctionID(int JobFunctionID)
        {
            return repository.Get(JobFunctionID).JobNameId;
        }
        public string Save(JobFunctionVM JobFunctionVM)
        {
            repository.Add(Mapper.Map(JobFunctionVM, new JobFunction()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(JobFunctionVM JobFunctionVM)
        {
            var Enname = repository.SingleOrDefault(x => x.EnName == JobFunctionVM.EnName && x.ID != JobFunctionVM.ID);
            var name = repository.SingleOrDefault(x => x.Name == JobFunctionVM.Name && x.ID != JobFunctionVM.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            repository.Update(Mapper.Map(JobFunctionVM, new JobFunction()));
            unitOfWork.Save();

            return "";
        }
        public string delete(int ID)
        {
            var JobFunction = repository.Get(ID);
            repository.Remove(JobFunction);
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
        public IEnumerable<JobName> JobNames()
        {
            Repository<JobName> rep = new Repository<JobName>(unitOfWork);

            return rep.GetAll();
        }
    }
}
