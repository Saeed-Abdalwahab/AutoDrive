using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.Core.HR;
using System.Linq;
using AutoDriveResources;
using AutoMapper;

namespace AutoDrive.BLL.HRAutoDrive
{
 public   class CertificationSpecificService
    {

        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private IRepository<CertificationSpecific> repository;
        public CertificationSpecificService()
        {
            repository = new Repository<CertificationSpecific>(unitOfWork);
        }


        public List<CertificationSpecificVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<CertificationSpecificVM>());
        }
        public CertificationSpecificVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new CertificationSpecificVM());
        }

        public string Save(CertificationSpecificVM CertificationSpecificVM)
        {

            repository.Add(Mapper.Map(CertificationSpecificVM, new CertificationSpecific()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(CertificationSpecificVM CertificationSpecificVM)
        {
            repository.Update(Mapper.Map(CertificationSpecificVM, new CertificationSpecific()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var CertificationSpecific = repository.Get(ID);
            repository.Remove(CertificationSpecific);
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
        public IEnumerable<CertificationSpecific> certificationSpecifics(int CertificationTypeID)
        {
            return repository.Find(x=>x.CertificationTypeID== CertificationTypeID);
        }
        public IEnumerable<CertificationType> certificationTypes()
        {
            Repository<CertificationType> rep = new Repository<CertificationType>(unitOfWork);
            return rep.GetAll();
        }

    }
}
