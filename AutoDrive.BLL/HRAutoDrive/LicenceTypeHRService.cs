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
    public class LicenceTypeHRService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<LicenceTypeHR> repository;

        public LicenceTypeHRService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<LicenceTypeHR>(unitOfWork);
        }

        public List<LicenceTypeHRVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<LicenceTypeHRVM>());
        }
        public LicenceTypeHRVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new LicenceTypeHRVM());
        }

        public string Save(LicenceTypeHRVM LicenceTypeHRVM)
        {

            repository.Add(Mapper.Map(LicenceTypeHRVM, new LicenceTypeHR()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(LicenceTypeHRVM LicenceTypeHRVM)
        {

            repository.Update(Mapper.Map(LicenceTypeHRVM, new LicenceTypeHR()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var LicenceTypeHR = repository.Get(ID);
            repository.Remove(LicenceTypeHR);
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
