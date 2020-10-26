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
    public class ReligionBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All Religion

        public List<ReligionVM> Getall()
        {


            var Model = new List<ReligionVM>();
            try
            {
                Model = db.Religions.Select(x => new ReligionVM()
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

        #region Get Religion By ID
        public ReligionVM Get(int ID)
        {
            ReligionVM Model = new ReligionVM();
            try
            {

                Model = (from x in db.Religions.Where(o => o.ID == ID)
                         select new ReligionVM()
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

        public string Save(ReligionVM ReligionVM_Obj)
        {
            var Enname = db.Religions.FirstOrDefault(x => x.EnName == ReligionVM_Obj.EnName);
            var name = db.Religions.FirstOrDefault(x => x.Name == ReligionVM_Obj.Name);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            Religion Religion_Obj = new Religion();
            Religion_Obj.Name = ReligionVM_Obj.Name;
            Religion_Obj.EnName = ReligionVM_Obj.EnName;

            db.Religions.Add(Religion_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        public string Edit(ReligionVM ReligionVM_Obj)
        {
            var Enname = db.Religions.FirstOrDefault(x => x.EnName == ReligionVM_Obj.EnName && x.ID != ReligionVM_Obj.ID);
            var name = db.Religions.FirstOrDefault(x => x.Name == ReligionVM_Obj.Name && x.ID != ReligionVM_Obj.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            Religion Religion_Obj = db.Religions.FirstOrDefault(x => x.ID == ReligionVM_Obj.ID);

            Religion_Obj.ID = ReligionVM_Obj.ID;
            Religion_Obj.Name = ReligionVM_Obj.Name;
            Religion_Obj.EnName = ReligionVM_Obj.EnName;

            db.Entry(Religion_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            Religion Religion_Obj = db.Religions.Find(ID);
            db.Religions.Remove(Religion_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }

        #endregion

    }
}
