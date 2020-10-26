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
    public class NationalityBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All Nationality
        public List<NationalityVM> Getall()
        {


            var Model = new List<NationalityVM>();
            try
            {
                Model = db.Nationalities.Select(x => new NationalityVM()
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

        #region Get Nationality By ID
        public NationalityVM Get(int ID)
        {
            NationalityVM Model = new NationalityVM();
            try
            {

                Model = (from x in db.Nationalities.Where(o => o.ID == ID)
                         select new NationalityVM()
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

        public string Save(NationalityVM nationalityVM_Obj)
        {
            var Enname = db.JobNames.FirstOrDefault(x => x.EnName == nationalityVM_Obj.EnName);
            var name = db.JobNames.FirstOrDefault(x => x.Name == nationalityVM_Obj.Name);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            Nationality nationality_Obj = new Nationality();
            nationality_Obj.Name = nationalityVM_Obj.Name;
            nationality_Obj.EnName = nationalityVM_Obj.EnName;

            db.Nationalities.Add(nationality_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        public string Edit(NationalityVM nationalityVM_Obj)
        {
            var Enname = db.JobNames.FirstOrDefault(x => x.EnName == nationalityVM_Obj.EnName && x.ID != nationalityVM_Obj.ID);
            var name = db.JobNames.FirstOrDefault(x => x.Name == nationalityVM_Obj.Name && x.ID != nationalityVM_Obj.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            Nationality nationality_Obj = db.Nationalities.FirstOrDefault(x => x.ID == nationalityVM_Obj.ID);

            nationality_Obj.ID = nationalityVM_Obj.ID;
            nationality_Obj.Name = nationalityVM_Obj.Name;
            nationality_Obj.EnName = nationalityVM_Obj.EnName;

            db.Entry(nationality_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            Nationality nationality_Obj = db.Nationalities.Find(ID);
            db.Nationalities.Remove(nationality_Obj);
            db.SaveChanges();
            return Messages.DeleteSucc;
        }

        #endregion



    }
}
