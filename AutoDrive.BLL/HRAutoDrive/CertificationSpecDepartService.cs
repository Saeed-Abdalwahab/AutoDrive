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
  public  class CertificationSpecDepartService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();

        private IRepository<CertificationSpecDepart> repository;
        public CertificationSpecDepartService()
        {
            repository = new Repository<CertificationSpecDepart>(unitOfWork);
        }


        public List<CertificationSpecDepartVM> Getall()
        {
            var List = repository.GetAll().ToList();
            return Mapper.Map(List, new List<CertificationSpecDepartVM>());
        }
        public CertificationSpecDepartVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new CertificationSpecDepartVM());
        }

        public string Save(CertificationSpecDepartVM CertificationSpecDepartVM)
        {

            repository.Add(Mapper.Map(CertificationSpecDepartVM, new CertificationSpecDepart()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(CertificationSpecDepartVM CertificationSpecDepartVM)
        {
            repository.Update(Mapper.Map(CertificationSpecDepartVM, new CertificationSpecDepart()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var CertificationSpecDepart = repository.Get(ID);
            repository.Remove(CertificationSpecDepart);
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



        public IEnumerable<CertificationSpecDepart> certificationSpecDeparts(int CertificationSpecificID)
        {
            return repository.Find(x => x.CertificationSpecificID == CertificationSpecificID);
        }
        public IEnumerable<CertificationSpecific> CertificationSpecifics()
        {
            Repository<CertificationSpecific> rep = new Repository<CertificationSpecific>(unitOfWork);

            return rep.GetAll();
        }

    }
}
