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
 public   class NationalityService
    {
         private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Nationality> repository;
        
        public NationalityService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Nationality>(unitOfWork);
        }

        public List<NationalityVM> Getall()
        {
           var List= repository.GetAll().ToList();
            return Mapper.Map(List,new List<NationalityVM>());
        }
        public NationalityVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new NationalityVM());
        }
      
        public string Save(NationalityVM NationalityVM)
        {
          
            repository.Add(Mapper.Map(NationalityVM, new Nationality()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(NationalityVM NationalityVM)
        {
            
            repository.Update(Mapper.Map(NationalityVM, new Nationality()));
            unitOfWork.Save();
            return "";
        }
        public string delete (int ID)
        {
            var Nationality = repository.Get(ID);
            repository.Remove(Nationality);
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
