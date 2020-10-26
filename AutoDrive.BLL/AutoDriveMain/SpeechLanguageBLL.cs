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
    public class SpeechLanguageBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All SpeechLanguage

        public List<SpeechLanguageVM> Getall()
        {


            var Model = new List<SpeechLanguageVM>();
            try
            {
                Model = db.SpeechLanguages.Select(x => new SpeechLanguageVM()
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

        #region Get SpeechLanguage By ID
        public SpeechLanguageVM Get(int ID)
        {
            SpeechLanguageVM Model = new SpeechLanguageVM();
            try
            {

                Model = (from x in db.SpeechLanguages.Where(o => o.ID == ID)
                         select new SpeechLanguageVM()
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

        public string Save(SpeechLanguageVM SpeechLanguageVM_Obj)
        {
            var Enname = db.SpeechLanguages.FirstOrDefault(x => x.EnName == SpeechLanguageVM_Obj.EnName);
            var name = db.SpeechLanguages.FirstOrDefault(x => x.Name == SpeechLanguageVM_Obj.Name);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            SpeechLanguage SpeechLanguage_Obj = new SpeechLanguage();
            SpeechLanguage_Obj.Name = SpeechLanguageVM_Obj.Name;
            SpeechLanguage_Obj.EnName = SpeechLanguageVM_Obj.EnName;

            db.SpeechLanguages.Add(SpeechLanguage_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        public string Edit(SpeechLanguageVM SpeechLanguageVM_Obj)
        {
            var Enname = db.SpeechLanguages.FirstOrDefault(x => x.EnName == SpeechLanguageVM_Obj.EnName && x.ID != SpeechLanguageVM_Obj.ID);
            var name = db.Religions.FirstOrDefault(x => x.Name == SpeechLanguageVM_Obj.Name && x.ID != SpeechLanguageVM_Obj.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            SpeechLanguage SpeechLanguage_Obj = db.SpeechLanguages.FirstOrDefault(x => x.ID == SpeechLanguageVM_Obj.ID);

            SpeechLanguage_Obj.ID = SpeechLanguageVM_Obj.ID;
            SpeechLanguage_Obj.Name = SpeechLanguageVM_Obj.Name;
            SpeechLanguage_Obj.EnName = SpeechLanguageVM_Obj.EnName;

            db.Entry(SpeechLanguage_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            SpeechLanguage SpeechLanguage_Obj = db.SpeechLanguages.Find(ID);
            db.SpeechLanguages.Remove(SpeechLanguage_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }

        #endregion
    }
}
