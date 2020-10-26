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
   public class CountryService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Country> repository;
        
        public CountryService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Country>(unitOfWork);
        }

        public List<CountryVM> Getall()
        {
           var List= repository.GetAll().ToList();
            return Mapper.Map(List,new List<CountryVM>());
        }
        public CountryVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new CountryVM());
        }
        
        public string Save(CountryVM countryVM)
        {
            var Enname = repository.FristOrDefault(x => x.EnName == countryVM.EnName);
            var name = repository.FristOrDefault(x => x.Name == countryVM.Name);
            if(Enname != null || name != null)
            return Messages.NameAlreadyExist;
            unitOfWork.CreateTransaction();
            repository.Add(Mapper.Map(countryVM, new Country()));
            unitOfWork.Save();
            unitOfWork.Commit();
            return "";
        }
        public string Edit(CountryVM countryVM)
        {
            var Enname = repository.FristOrDefault(x => x.EnName == countryVM.EnName&&x.ID!=countryVM.ID);
            var name = repository.FristOrDefault(x => x.Name == countryVM.Name&&x.ID != countryVM.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            unitOfWork.CreateTransaction();
            repository.Update(Mapper.Map(countryVM, new Country()));
            unitOfWork.Save();
            unitOfWork.Commit();
            return "";
        }
        public string delete (int ID)
        {
            var Country = repository.Get(ID);
            repository.Remove(Country);
            unitOfWork.Save();
            return Messages.DeleteSucc;
        }
    }
}
