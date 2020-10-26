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
    public class NationalTypeBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All NationalType
        public List<NationalTypeVM> Getall()
        {


            var Model = new List<NationalTypeVM>();
            try
            {
                Model = db.NationalTypes.Select(x => new NationalTypeVM()
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

        #region Get NationalType By ID
        public NationalTypeVM Get(int ID)
        {
            NationalTypeVM Model = new NationalTypeVM();
            try
            {

                Model = (from x in db.NationalTypes.Where(o => o.ID == ID)
                         select new NationalTypeVM()
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

        public string Save(NationalTypeVM nationalTypeVM_Obj)
        {
            var Enname = db.NationalTypes.FirstOrDefault(x => x.EnName == nationalTypeVM_Obj.EnName);
            var name = db.NationalTypes.FirstOrDefault(x => x.Name == nationalTypeVM_Obj.Name);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            NationalType nationalType_Obj = new NationalType();
            nationalType_Obj.Name = nationalTypeVM_Obj.Name;
            nationalType_Obj.EnName = nationalTypeVM_Obj.EnName;

            db.NationalTypes.Add(nationalType_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        public string Edit(NationalTypeVM nationalTypeVM_Obj)
        {
            var Enname = db.NationalTypes.FirstOrDefault(x => x.EnName == nationalTypeVM_Obj.EnName && x.ID != nationalTypeVM_Obj.ID);
            var name = db.NationalTypes.FirstOrDefault(x => x.Name == nationalTypeVM_Obj.Name && x.ID != nationalTypeVM_Obj.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            NationalType nationalType_Obj = db.NationalTypes.FirstOrDefault(x => x.ID == nationalTypeVM_Obj.ID);

            nationalType_Obj.ID = nationalTypeVM_Obj.ID;
            nationalType_Obj.Name = nationalTypeVM_Obj.Name;
            nationalType_Obj.EnName = nationalTypeVM_Obj.EnName;

            db.Entry(nationalType_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            NationalType nationalType_Obj = db.NationalTypes.Find(ID);
            db.NationalTypes.Remove(nationalType_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }

        #endregion



    }
}
