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
    public class DepartmentService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private IRepository<Department> repository;
        public DepartmentService()
        {
            repository = new Repository<Department>(unitOfWork);
        }


        public List<DepartmentVM> Getall(int id)
        {
            
            var List = id != 0 ? repository.Find(x=>x.ParentId==id).ToList(): repository.Find(x=>x.ParentId==null).ToList();
            return Mapper.Map(List, new List<DepartmentVM>());
        }
        public List<Department> GetallTree()
        {
            var List = repository.GetAll().ToList();
            return List;
        }
        public DepartmentVM Get(int ID)
        {
            return Mapper.Map(repository.Get(ID), new DepartmentVM());
        }

        public string Save(DepartmentVM DepartmentVM)
        {

            repository.Add(Mapper.Map(DepartmentVM, new Department()));
            unitOfWork.Save();
            return "";
        }
        public string Edit(DepartmentVM DepartmentVM)
        {
            repository.Update(Mapper.Map(DepartmentVM, new Department()));
            unitOfWork.Save();
            return "";
        }
        public string delete(int ID)
        {
            var Department = repository.Get(ID);
            repository.Remove(Department);
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

        public IEnumerable<DepartmentVM> Departments(string Any,string language)
        {
            
            if(language.ToLower()== "en-us")
            {
                return repository.Find( x=>x.EnName.Contains(Any)).ToList().Select(x => new DepartmentVM()
                {
                    ID = x.ID,
                    Name = x.ParentId != null ? x.EnName + "-" + x.department.EnName : x.EnName
                });
            }
            else {
                 return repository.Find(x => x.Name.Contains(Any)).ToList().Select(x => new DepartmentVM()
                {
                    ID = x.ID,
                    Name = x.ParentId != null ? x.Name + "-" + x.department.Name : x.Name
                });
            }
            
        }
    }
}
