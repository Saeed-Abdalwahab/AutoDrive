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
   public class BloodGroupService
    {

        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<BloodGroup> repository;

        public BloodGroupService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<BloodGroup>(unitOfWork);
        }

        public List<BloodGroupVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<BloodGroupVM>());
        }
        public BloodGroupVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new BloodGroupVM());
        }

        public string Save(BloodGroupVM BloodGroupVM)
        {

            repository.Add(Mapper.Map(BloodGroupVM, new BloodGroup()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(BloodGroupVM BloodGroupVM)
        {

            repository.Update(Mapper.Map(BloodGroupVM, new BloodGroup()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var BloodGroup = repository.Get(ID);
            repository.Remove(BloodGroup);
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
