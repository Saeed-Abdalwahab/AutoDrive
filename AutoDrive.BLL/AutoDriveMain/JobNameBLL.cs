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
    public class JobNameBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All NationalType
        public List<JobNameVM> Getall()
        {


            var Model = new List<JobNameVM>();
            try
            {
                Model = db.JobNames.Select(x => new JobNameVM()
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

        #region Get JobName By ID
        public JobNameVM Get(int ID)
        {
            JobNameVM Model = new JobNameVM();
            try
            {

                Model = (from x in db.JobNames.Where(o => o.ID == ID)
                         select new JobNameVM()
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
       
        public string Save(JobNameVM jobNameVM_Obj)
        {
            var Enname = db.JobNames.FirstOrDefault(x => x.EnName == jobNameVM_Obj.EnName);
            var name = db.JobNames.FirstOrDefault(x => x.Name == jobNameVM_Obj.Name);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            JobName jobName_Obj = new JobName();
            jobName_Obj.Name = jobNameVM_Obj.Name;
            jobName_Obj.EnName = jobNameVM_Obj.EnName;

            db.JobNames.Add(jobName_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";                                                            
        }
        #endregion
        public string Edit(JobNameVM jobNameVM_Obj)
        {
            var Enname = db.JobNames.FirstOrDefault(x => x.EnName == jobNameVM_Obj.EnName && x.ID != jobNameVM_Obj.ID);
            var name = db.JobNames.FirstOrDefault(x => x.Name == jobNameVM_Obj.Name && x.ID != jobNameVM_Obj.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            JobName jobName_Obj = db.JobNames.FirstOrDefault(x => x.ID == jobNameVM_Obj.ID);

            jobName_Obj.ID = jobNameVM_Obj.ID;
            jobName_Obj.Name = jobNameVM_Obj.Name;
            jobName_Obj.EnName = jobNameVM_Obj.EnName;

            db.Entry(jobName_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        #region Delete
        public string delete(int ID)
        {
            JobName jobName_Obj = db.JobNames.Find(ID);
            db.JobNames.Remove(jobName_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }
     
        #endregion
       

        
       
        
    }
}
