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
 public   class CarrerFieldService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<CarrerField> repository;

        public CarrerFieldService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<CarrerField>(unitOfWork);
        }

        public List<CarrerFieldVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<CarrerFieldVM>());
        }
        public CarrerFieldVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new CarrerFieldVM());
        }

        public string Save(CarrerFieldVM CarrerFieldVM)
        {

            repository.Add(Mapper.Map(CarrerFieldVM, new CarrerField()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(CarrerFieldVM CarrerFieldVM)
        {

            repository.Update(Mapper.Map(CarrerFieldVM, new CarrerField()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var CarrerField = repository.Get(ID);
            repository.Remove(CarrerField);
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
