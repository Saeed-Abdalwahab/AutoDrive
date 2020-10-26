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
   public class MaritalStatuService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<MaritalStatu> repository;

        public MaritalStatuService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<MaritalStatu>(unitOfWork);
        }

        public List<MaritalStatuVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<MaritalStatuVM>());
        }
        public MaritalStatuVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new MaritalStatuVM());
        }

        public string Save(MaritalStatuVM MaritalStatuVM)
        {

            repository.Add(Mapper.Map(MaritalStatuVM, new MaritalStatu()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(MaritalStatuVM MaritalStatuVM)
        {

            repository.Update(Mapper.Map(MaritalStatuVM, new MaritalStatu()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var MaritalStatu = repository.Get(ID);
            repository.Remove(MaritalStatu);
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
