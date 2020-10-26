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
  public  class AreaService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Area> repository;

        public AreaService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Area>(unitOfWork);
        }

        public List<AreaVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<AreaVM>());
        }
        public AreaVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new AreaVM());
        }
        public List<AreaVM> GetAreasByCountryID(int countryid,string language)
        {
            var List = language == "en" ? repository.Find(x => x.CountryId == countryid).Select(x => new AreaVM { ID = x.ID, Name = x.EnName }) :
                repository.Find(x => x.CountryId == countryid).Select(x => new AreaVM { ID = x.ID, Name = x.Name });
            return Mapper.Map(List, new List<AreaVM>());

        }
        public  int CountryIDByAreaID(int AreaID)
        {
            return repository.Get(AreaID).CountryId;
        }
        public string Save(AreaVM AreaVM)
        {
          
            repository.Add(Mapper.Map(AreaVM, new Area()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(AreaVM AreaVM)
        {
            var Enname = repository.SingleOrDefault(x => x.EnName == AreaVM.EnName && x.ID != AreaVM.ID);
            var name = repository.SingleOrDefault(x => x.Name == AreaVM.Name && x.ID != AreaVM.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            unitOfWork.CreateTransaction();
            repository.Update(Mapper.Map(AreaVM, new Area()));
            unitOfWork.Save();
            unitOfWork.Commit();
            return "";
        }
        public string delete(int ID)
        {
            var Area = repository.Get(ID);
            repository.Remove(Area);
            unitOfWork.Save();
            return Messages.DeleteSucc;
        }

        public bool NameCheck(string Name,int ID)
        {
            if (ID == 0)
                return repository.FristOrDefault(x => x.Name == Name) == null ? true : false;
            return repository.FristOrDefault(x => x.Name == Name && x.ID != ID) == null ? true : false;
        }
        public bool ENNameCheck(string EnName, int ID)
        {
            if(ID==0)
                return repository.FristOrDefault(x => x.EnName == EnName) == null ? true : false;
            return repository.FristOrDefault(x => x.EnName == EnName && x.ID != ID) == null ? true : false;

        }
        public IEnumerable<Country> Counteries()
        {
            Repository<Country> rep = new Repository<Country>(unitOfWork);
          
            return rep.GetAll();
        }
    }
}
