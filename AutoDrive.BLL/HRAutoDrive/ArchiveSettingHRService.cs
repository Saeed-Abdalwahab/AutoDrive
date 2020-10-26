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
  public  class ArchiveSettingHRService
    {
          private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private IRepository<ArchiveSettingHR> repository;
        public ArchiveSettingHRService()
        {
            repository = new Repository<ArchiveSettingHR>(unitOfWork);
        }


        public List<ArchiveSettingHRVM> Getall(int id)
        {
            
            var List = id != 0 ? repository.Find(x=>x.ParentID==id).ToList(): repository.Find(x=>x.ParentID==null).ToList();
            return Mapper.Map(List, new List<ArchiveSettingHRVM>());
        }
        public List<ArchiveSettingHR> GetallTree()
        {
            var List = repository.GetAll().ToList();
            return List;
        }
        public ArchiveSettingHRVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new ArchiveSettingHRVM());
        }

        public string Save(ArchiveSettingHRVM ArchiveSettingHRVM)
        {

            repository.Add(Mapper.Map(ArchiveSettingHRVM, new ArchiveSettingHR()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(ArchiveSettingHRVM ArchiveSettingHRVM)
        {
            repository.Update(Mapper.Map(ArchiveSettingHRVM, new ArchiveSettingHR()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var ArchiveSettingHR = repository.Get(ID);
            repository.Remove(ArchiveSettingHR);
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

        public IEnumerable<ArchiveSettingHRVM> ArchiveSettingHRs(string Any,string language)
        {
            
            if(language.ToLower()== "en-us")
            {
                return repository.Find( x=>x.EnName.Contains(Any)).ToList().Select(x => new ArchiveSettingHRVM()
                {
                    ID = x.ID,
                    Name = x.ParentID != null ? x.EnName + "-" + x.Parent.EnName : x.EnName
                });
            }
            else {
                 return repository.Find(x => x.Name.Contains(Any)).ToList().Select(x => new ArchiveSettingHRVM()
                {
                    ID = x.ID,
                    Name = x.ParentID != null ? x.Name + "-" + x.Parent.Name : x.Name
                });
            }
            
        }
    }
}
