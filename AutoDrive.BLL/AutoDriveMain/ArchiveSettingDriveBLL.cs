using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.AutoDriveMain
{
    public class ArchiveSettingDriveBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<ArchiveSettingDriveVM> Getall(int id)
        {

            var List = id != 0 ? db.ArchiveSettingDrives.Where(x => x.ParentId == id).ToList() : db.ArchiveSettingDrives.Where(x => x.ParentId == null).ToList();
            var ListVM = List.Select(x => new ArchiveSettingDriveVM()
            {
                ID = x.ID,
                Name = x.Name,
                EnName=x.EnName,
                ParentId = x.ParentId,
                ParentName = x.Name,
                ParentEnName=x.EnName

            }).ToList();

            return ListVM;
        }
        public List<ArchiveSettingDrive> GetallTree()
        {
            var List = db.ArchiveSettingDrives.ToList();
            return List;
        }
        public ArchiveSettingDriveVM Get(int ID)
        {
            ArchiveSettingDriveVM Model = new ArchiveSettingDriveVM();
            try
            {

                Model = (from x in db.ArchiveSettingDrives.Where(o => o.ID == ID)
                         select new ArchiveSettingDriveVM()
                         {

                             ID = x.ID,
                             Name = x.Name,
                             EnName = x.EnName,
                             ParentId = x.ParentId,
                             ParentName = x.Name,
                             ParentEnName=x.EnName


                         }).FirstOrDefault();
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
            
        }

        public string Save(ArchiveSettingDriveVM ArchiveSettingDriveVM_Obj)
        {

            var name = db.ArchiveSettingDrives.FirstOrDefault(x => x.Name == ArchiveSettingDriveVM_Obj.Name);
            if ( name != null)
                return Messages.NameAlreadyExist;
            ArchiveSettingDrive ArchiveSettingDrive_Obj = new ArchiveSettingDrive();
            ArchiveSettingDrive_Obj.Name = ArchiveSettingDriveVM_Obj.Name;
            ArchiveSettingDrive_Obj.EnName = ArchiveSettingDriveVM_Obj.EnName;
            ArchiveSettingDrive_Obj.ParentId = ArchiveSettingDriveVM_Obj.ParentId;


            db.ArchiveSettingDrives.Add(ArchiveSettingDrive_Obj);
            db.SaveChanges();
            return "";
        }
        public string Edit(ArchiveSettingDriveVM ArchiveSettingDriveVM_Obj)
        {
            var name = db.JobNames.FirstOrDefault(x => x.Name == ArchiveSettingDriveVM_Obj.Name && x.ID != ArchiveSettingDriveVM_Obj.ID);
            if ( name != null)
                return Messages.NameAlreadyExist;
            ArchiveSettingDrive ArchiveSettingDrive_Obj = db.ArchiveSettingDrives.FirstOrDefault(x => x.ID == ArchiveSettingDriveVM_Obj.ID);

            ArchiveSettingDrive_Obj.ID = ArchiveSettingDriveVM_Obj.ID;
            ArchiveSettingDrive_Obj.Name = ArchiveSettingDriveVM_Obj.Name;
            ArchiveSettingDrive_Obj.EnName = ArchiveSettingDriveVM_Obj.EnName;
            ArchiveSettingDrive_Obj.ParentId = ArchiveSettingDriveVM_Obj.ParentId;
            

            db.Entry(ArchiveSettingDrive_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }
        public string delete(int ID)
        {
            ArchiveSettingDrive ArchiveSettingDrive_Obj = db.ArchiveSettingDrives.Find(ID);
            db.ArchiveSettingDrives.Remove(ArchiveSettingDrive_Obj);
            db.SaveChanges();
            return Messages.DeleteSucc;
        }
        public bool NameCheck(string Name, int ID)
        {
            if (ID == 0)
            { 
                return db.ArchiveSettingDrives.FirstOrDefault(x => x.Name == Name) == null ? true : false;
            }
            return db.ArchiveSettingDrives.FirstOrDefault(x => x.Name == Name && x.ID != ID) == null ? true : false;
        }
        public bool ENNameCheck(string EnName, int ID)
        {
            if (ID == 0)
            { 
                return db.ArchiveSettingDrives.FirstOrDefault(x => x.EnName == EnName) == null ? true : false;
            }
            return db.ArchiveSettingDrives.FirstOrDefault(x => x.EnName == EnName && x.ID != ID) == null ? true : false;

        }

        public IEnumerable<ArchiveSettingDriveVM> ArchiveSettingHRs(string Any, string language)
        {

            if (language.ToLower() == "en-us")
            {
                return db.ArchiveSettingDrives.Where(x => x.EnName.Contains(Any)).ToList().Select(x => new ArchiveSettingDriveVM()
                {
                    ID = x.ID,
                    Name = x.ParentId != null ? x.EnName + "-" + x.ArchiveSettingDrive2.EnName : x.EnName
                });
            }
            else
            {
                return db.ArchiveSettingDrives.Where(x => x.Name.Contains(Any)).ToList().Select(x => new ArchiveSettingDriveVM()
                {
                    ID = x.ID,
                    Name = x.ParentId != null ? x.Name + "-" + x.ArchiveSettingDrive2.Name : x.Name
                });
            }

        }
    }
}
