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
    public class LicenceCategoryBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public List<LicenceTypeVM> GetallLicenceType()
        {

            var Model = new List<LicenceTypeVM>();
            try
            {
                Model = db.LicenceTypes.Select(x => new LicenceTypeVM()
                {
                    ID = x.ID,
                    Name = x.Name,
                    EnName=x.EnName

                }).ToList();


            }
            catch
            {
                Model = null;
            }

            return Model;
        }

        public List<LicenceCategoryVM> Getall()
        {


            var Model = new List<LicenceCategoryVM>();
            try
            {
                Model = db.LicenceCategories.Select(x => new LicenceCategoryVM()
                {
                    ID = x.ID,
                    Name = x.Name,
                    EnName = x.EnName,
                    LicenceTypeId = x.LicenceTypeId,
                    LicenceTypeName = x.LicenceType.Name,
                    LicenceTypeEnName=x.LicenceType.EnName
                }).ToList();


            }
            catch
            {
                Model = null;
            }

            return Model;
        }
       
        public LicenceCategoryVM Get(int ID)
        {
            LicenceCategoryVM Model = new LicenceCategoryVM();
            try
            {

                Model = (from x in db.LicenceCategories.Where(o => o.ID == ID)
                         select new LicenceCategoryVM()
                         {

                             ID = x.ID,
                             Name = x.Name,
                             EnName = x.EnName,
                             LicenceTypeId = x.LicenceTypeId,
                             LicenceTypeName = x.LicenceType.Name,
                             LicenceTypeEnName = x.LicenceType.EnName

                         }).FirstOrDefault();
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }
       



      

        public string Save(LicenceCategoryVM LicenceCategoryVM_Obj)
        {
            var Enname = db.LicenceCategories.FirstOrDefault(x => x.EnName == LicenceCategoryVM_Obj.EnName&& x.LicenceTypeId== LicenceCategoryVM_Obj.LicenceTypeId);
            var name = db.LicenceCategories.FirstOrDefault(x => x.Name == LicenceCategoryVM_Obj.Name && x.LicenceTypeId == LicenceCategoryVM_Obj.LicenceTypeId);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            LicenceCategory licenceCategory_Obj = new LicenceCategory();
            licenceCategory_Obj.Name = LicenceCategoryVM_Obj.Name;
            licenceCategory_Obj.EnName = LicenceCategoryVM_Obj.EnName;
            licenceCategory_Obj.LicenceTypeId = LicenceCategoryVM_Obj.LicenceTypeId;

            db.LicenceCategories.Add(licenceCategory_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
       
        public string Edit(LicenceCategoryVM LicenceCategoryVM_Obj)
        {
            var Enname = db.LicenceCategories.FirstOrDefault(x => x.EnName == LicenceCategoryVM_Obj.EnName && x.ID != LicenceCategoryVM_Obj.ID && x.LicenceTypeId != LicenceCategoryVM_Obj.LicenceTypeId);
            var name = db.LicenceCategories.FirstOrDefault(x => x.Name == LicenceCategoryVM_Obj.Name && x.ID != LicenceCategoryVM_Obj.ID && x.LicenceTypeId != LicenceCategoryVM_Obj.LicenceTypeId);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            LicenceCategory LicenceCategory_Obj = db.LicenceCategories.FirstOrDefault(x => x.ID == LicenceCategoryVM_Obj.ID);

            LicenceCategory_Obj.ID = LicenceCategoryVM_Obj.ID;
            LicenceCategory_Obj.Name = LicenceCategoryVM_Obj.Name;
            LicenceCategory_Obj.EnName = LicenceCategoryVM_Obj.EnName;
            LicenceCategory_Obj.LicenceTypeId = LicenceCategoryVM_Obj.LicenceTypeId;


            db.Entry(LicenceCategory_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        public string delete(int ID)
        {
            LicenceCategory licenceCategory_Obj = db.LicenceCategories.Find(ID);
            db.LicenceCategories.Remove(licenceCategory_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }

      
     
    



     
    }
}
