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
namespace AutoDrive.BLL.HRAutoDrive
{
   public class ReligionService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Religion> repository;
        public ReligionService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Religion>(unitOfWork);
        }

        public List<ReligionVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<ReligionVM>());
        }
        public ReligionVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new ReligionVM());
        }

        public string Save(ReligionVM ReligionVM)
        {

            repository.Add(Mapper.Map(ReligionVM, new Religion()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(ReligionVM ReligionVM)
        {

            repository.Update(Mapper.Map(ReligionVM, new Religion()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var Religion = repository.Get(ID);
            repository.Remove(Religion);
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
