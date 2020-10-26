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
    public class LicenceTypeBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();
    
        #region Get All LicenceType

        public List<LicenceTypeVM> Getall()
        {


            var Model = new List<LicenceTypeVM>();
            try
            {
                Model = db.LicenceTypes.Select(x => new LicenceTypeVM()
                {
                    ID = x.ID,
                    Name = x.Name,
                    EnName = x.EnName,
                    MinimalAge = x.MinimalAge

                }).ToList();


            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        #endregion

        #region Get LicenceType By ID
        public LicenceTypeVM Get(int ID)
        {
            LicenceTypeVM Model = new LicenceTypeVM();
            try
            {

                Model = (from x in db.LicenceTypes.Where(o => o.ID == ID)
                         select new LicenceTypeVM()
                         {

                             ID = x.ID,
                             Name = x.Name,
                             EnName = x.EnName,
                             MinimalAge = x.MinimalAge

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

        public string Save(LicenceTypeVM LicenceTypeVM_Obj)
        {
            var Enname = db.LicenceTypes.FirstOrDefault(x => x.EnName == LicenceTypeVM_Obj.EnName);
            var name = db.LicenceTypes.FirstOrDefault(x => x.Name == LicenceTypeVM_Obj.Name);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            LicenceType LicenceType_Obj = new LicenceType();
            LicenceType_Obj.Name = LicenceTypeVM_Obj.Name;
            LicenceType_Obj.EnName = LicenceTypeVM_Obj.EnName;
            LicenceType_Obj.MinimalAge = LicenceTypeVM_Obj.MinimalAge;


            db.LicenceTypes.Add(LicenceType_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        public string Edit(LicenceTypeVM LicenceTypeVM_Obj)
        {
            var Enname = db.LicenceTypes.FirstOrDefault(x => x.EnName == LicenceTypeVM_Obj.EnName && x.ID != LicenceTypeVM_Obj.ID);
            var name = db.LicenceTypes.FirstOrDefault(x => x.Name == LicenceTypeVM_Obj.Name && x.ID != LicenceTypeVM_Obj.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            LicenceType LicenceType_Obj = db.LicenceTypes.FirstOrDefault(x => x.ID == LicenceTypeVM_Obj.ID);

            LicenceType_Obj.ID = LicenceTypeVM_Obj.ID;
            LicenceType_Obj.Name = LicenceTypeVM_Obj.Name;
            LicenceType_Obj.EnName = LicenceTypeVM_Obj.EnName;
            LicenceType_Obj.MinimalAge = LicenceTypeVM_Obj.MinimalAge;

            db.Entry(LicenceType_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            LicenceType LicenceType_Obj = db.LicenceTypes.Find(ID);
            db.LicenceTypes.Remove(LicenceType_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }

        #endregion
    }
}
