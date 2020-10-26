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
    public class TestBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All Test
        public List<TestVM> Getall()
        {


            var Model = new List<TestVM>();
            try
            {
                Model = db.Test.Select(x => new TestVM()
                {
                    ID = x.ID,
                    ArName = x.ArName,
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

        #region Get Test By ID
        public TestVM Get(int ID)
        {
            TestVM Model = new TestVM();
            try
            {

                Model = (from x in db.Test.Where(o => o.ID == ID)
                         select new TestVM()
                         {

                             ID = x.ID,
                             ArName = x.ArName,
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

        public string Save(TestVM TestVM_Obj)
        {
            var Enname = db.Test.FirstOrDefault(x => x.EnName == TestVM_Obj.EnName);
            var Arname = db.Test.FirstOrDefault(x => x.ArName == TestVM_Obj.ArName);
            if (Enname != null || Arname != null)
                return Messages.NameAlreadyExist;
            Test Test_Obj = new Test();
            Test_Obj.ArName = TestVM_Obj.ArName;
            Test_Obj.EnName = TestVM_Obj.EnName;

            db.Test.Add(Test_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        public string Edit(TestVM TestVM_Obj)
        {
            var Enname = db.Test.FirstOrDefault(x => x.EnName == TestVM_Obj.EnName && x.ID != TestVM_Obj.ID);
            var Arname = db.Test.FirstOrDefault(x => x.ArName == TestVM_Obj.ArName && x.ID != TestVM_Obj.ID);
            if (Enname != null || Arname != null)
                return Messages.NameAlreadyExist;
            Test Test_Obj = db.Test.FirstOrDefault(x => x.ID == TestVM_Obj.ID);

            Test_Obj.ID = TestVM_Obj.ID;
            Test_Obj.ArName = TestVM_Obj.ArName;
            Test_Obj.EnName = TestVM_Obj.EnName;

            db.Entry(Test_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            Test Test_Obj = db.Test.Find(ID);
            db.Test.Remove(Test_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }

        #endregion




    }
}
