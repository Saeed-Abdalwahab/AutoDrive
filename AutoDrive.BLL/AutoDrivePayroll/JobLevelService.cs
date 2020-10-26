using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.AutoDrivePayroll
{
   public class JobLevelService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool SaveInDataBase(JobLevelVM model)
        {
            bool result = false;
            try
            {
                if (model.ID==0)
                {
                    JobLevel obj = context.JobLevels.FirstOrDefault(JL => JL.Name == model.Name||JL.EnName==model.Name||JL.LevelSort==model.LevelSort);
                    if (obj == null)
                    {
                        JobLevel jobLevel = new JobLevel();
                        jobLevel.Name = model.Name;
                        jobLevel.EnName = model.EnName;
                        jobLevel.LevelSort = model.LevelSort;
                        context.JobLevels.Add(jobLevel);
                        context.SaveChanges();
                        result = true;
                    }
                    else
                        result = false; ;
                    
                }
                else
                {
                    JobLevel obj = context.JobLevels.FirstOrDefault(JL => JL.Name == model.Name||JL.EnName==model.EnName||JL.LevelSort==model.LevelSort);
                    context.Dispose();
                    if (obj == null||obj.ID == model.ID)
                    {
                        context = new ApplicationDbContext();
                        JobLevel jobLevel = new JobLevel();
                        jobLevel.ID = model.ID;
                        jobLevel.Name = model.Name;
                        jobLevel.EnName = model.EnName;
                        jobLevel.LevelSort = model.LevelSort;
                        context.Entry(jobLevel).State = EntityState.Modified;
                        context.SaveChanges();
                        result = true;
                    }
                    else
                        result = false;
                   
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        public bool CheckName(string Name, int id)
        {
            bool res = false;
            if (id == 0)
            {

                JobLevel jobLevel = context.JobLevels.FirstOrDefault(JL => JL.Name == Name);
                if (jobLevel == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                JobLevel jobLevel = context.JobLevels.FirstOrDefault(JL => JL.Name == Name);
                if (jobLevel == null || jobLevel.ID == id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public bool CheckLevelSort(int LevelSort, int id)
        {
            bool res = false;
            if (id == 0)
            {

                JobLevel jobLevel = context.JobLevels.FirstOrDefault(JL => JL.LevelSort==LevelSort);
                if (jobLevel == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                JobLevel jobLevel = context.JobLevels.FirstOrDefault(JL => JL.LevelSort==LevelSort);

                if (jobLevel == null || jobLevel.ID == id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public bool CheckENName(string Name, int Id)
        {
            bool res = false;
            if (Id == 0)
            {

                JobLevel jobLevel = context.JobLevels.FirstOrDefault(JL => JL.EnName == Name);
                if (jobLevel == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                JobLevel jobLevel = context.JobLevels.FirstOrDefault(JL => JL.EnName == Name);
                if (jobLevel == null || jobLevel.ID == Id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public List<JobLevelVM> GetALL()
        {
            List<JobLevelVM> model = new List<JobLevelVM>();
            try
            {
                model = context.JobLevels.Select(JL => new JobLevelVM()
                {
                    ID = JL.ID,
                    Name = JL.Name,
                    EnName = JL.EnName,
                    LevelSort=JL.LevelSort
                }).ToList();
            }
            catch (Exception ex)
            {
                model = null;
                throw ex;
            }
            return model;
        }
        public JobLevelVM GetById(int id)
        {
            JobLevel jobLevel = context.JobLevels.FirstOrDefault(JL => JL.ID == id);
            return new JobLevelVM()
            {
                ID = jobLevel.ID,
                Name = jobLevel.Name,
                EnName = jobLevel.EnName,
                LevelSort=jobLevel.LevelSort
            };
        }
        public void Delete(int id)
        {
            
            try
            {
                JobLevel jobLevel = context.JobLevels.FirstOrDefault(JL => JL.ID == id);
                context.JobLevels.Remove(jobLevel);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
