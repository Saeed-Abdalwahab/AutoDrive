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
  public  class MotivationTypeService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<MotivationType> repository;

        public MotivationTypeService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<MotivationType>(unitOfWork);
        }

        public List<MotivationTypeVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<MotivationTypeVM>());
        }
        public MotivationTypeVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new MotivationTypeVM());
        }

        public string Save(MotivationTypeVM MotivationTypeVM)
        {

            repository.Add(Mapper.Map(MotivationTypeVM, new MotivationType()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(MotivationTypeVM MotivationTypeVM)
        {

            repository.Update(Mapper.Map(MotivationTypeVM, new MotivationType()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var MotivationType = repository.Get(ID);
            repository.Remove(MotivationType);
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
