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
  public  class CertificationTypeService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<CertificationType> repository;

        public CertificationTypeService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<CertificationType>(unitOfWork);
        }

        public List<CertificationTypeVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<CertificationTypeVM>());
        }
        public CertificationTypeVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new CertificationTypeVM());
        }

        public string Save(CertificationTypeVM CertificationTypeVM)
        {

            repository.Add(Mapper.Map(CertificationTypeVM, new CertificationType()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(CertificationTypeVM CertificationTypeVM)
        {

            repository.Update(Mapper.Map(CertificationTypeVM, new CertificationType()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var CertificationType = repository.Get(ID);
            repository.Remove(CertificationType);
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
