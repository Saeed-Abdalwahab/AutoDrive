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
    public class LookUp_DriveLevelBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All DriveLevel

        public List<DriveLevelVM> Getall()
        {


            var Model = new List<DriveLevelVM>();
            try
            {
                Model = db.DriveLevels.Select(x => new DriveLevelVM()
                {
                    ID = x.ID,
                    Name = x.Name,
                    EnName = x.EnName,


                }).ToList();


            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        #endregion

        #region Get DriveLevel By ID
        public DriveLevelVM Get(int ID)
        {
            DriveLevelVM Model = new DriveLevelVM();
            try
            {

                Model = (from x in db.DriveLevels.Where(o => o.ID == ID)
                         select new DriveLevelVM()
                         {

                             ID = x.ID,
                             Name = x.Name,
                             EnName = x.EnName,


                         }).FirstOrDefault();
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }
        #endregion



        #region SaveinDataBase

        public string Save(DriveLevelVM DriveLevelVM_Obj)
        {
            var Enname = db.DriveLevels.FirstOrDefault(x => x.EnName == DriveLevelVM_Obj.EnName);
            var name = db.SpeechLanguages.FirstOrDefault(x => x.Name == DriveLevelVM_Obj.Name);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            DriveLevel DriveLevel_Obj = new DriveLevel();
            DriveLevel_Obj.Name = DriveLevelVM_Obj.Name;
            DriveLevel_Obj.EnName = DriveLevelVM_Obj.EnName;

            db.DriveLevels.Add(DriveLevel_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        public string Edit(DriveLevelVM DriveLevelVM_Obj)
        {
            var Enname = db.DriveLevels.FirstOrDefault(x => x.EnName == DriveLevelVM_Obj.EnName && x.ID != DriveLevelVM_Obj.ID);
            var name = db.DriveLevels.FirstOrDefault(x => x.Name == DriveLevelVM_Obj.Name && x.ID != DriveLevelVM_Obj.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            DriveLevel DriveLevel_Obj = db.DriveLevels.FirstOrDefault(x => x.ID == DriveLevelVM_Obj.ID);

            DriveLevel_Obj.ID = DriveLevelVM_Obj.ID;
            DriveLevel_Obj.Name = DriveLevelVM_Obj.Name;
            DriveLevel_Obj.EnName = DriveLevelVM_Obj.EnName;

            db.Entry(DriveLevel_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            DriveLevel DriveLevel_Obj = db.DriveLevels.Find(ID);
            db.DriveLevels.Remove(DriveLevel_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }

        #endregion
    }
}
