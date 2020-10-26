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
    public class TestResultBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All Test
        public List<TestResultVM> Getall()
        {


            var Model = new List<TestResultVM>();
            try
            {
                Model = db.TestResult.Select(x => new TestResultVM()
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

        #region Get TestResult By ID
        public TestResultVM Get(int ID)
        {
            TestResultVM Model = new TestResultVM();
            try
            {

                Model = (from x in db.TestResult.Where(o => o.ID == ID)
                         select new TestResultVM()
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

        public string Save(TestResultVM TestResultVM_Obj)
        {
            var Enname = db.TestResult.FirstOrDefault(x => x.EnName == TestResultVM_Obj.EnName);
            var Arname = db.TestResult.FirstOrDefault(x => x.ArName == TestResultVM_Obj.ArName);
            if (Enname != null || Arname != null)
                return Messages.NameAlreadyExist;
            TestResult TestResult_Obj = new TestResult();
            TestResult_Obj.ArName = TestResultVM_Obj.ArName;
            TestResult_Obj.EnName = TestResultVM_Obj.EnName;

            db.TestResult.Add(TestResult_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        public string Edit(TestResultVM TestResultVM_Obj)
        {
            var Enname = db.TestResult.FirstOrDefault(x => x.EnName == TestResultVM_Obj.EnName && x.ID != TestResultVM_Obj.ID);
            var Arname = db.TestResult.FirstOrDefault(x => x.ArName == TestResultVM_Obj.ArName && x.ID != TestResultVM_Obj.ID);
            if (Enname != null || Arname != null)
                return Messages.NameAlreadyExist;
            TestResult TestResult_Obj = db.TestResult.FirstOrDefault(x => x.ID == TestResultVM_Obj.ID);

            TestResult_Obj.ID = TestResultVM_Obj.ID;
            TestResult_Obj.ArName = TestResultVM_Obj.ArName;
            TestResult_Obj.EnName = TestResultVM_Obj.EnName;

            db.Entry(TestResult_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            TestResult TestResult_Obj = db.TestResult.Find(ID);
            db.TestResult.Remove(TestResult_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }

        #endregion



    }
}
