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
    public class JobDegreeService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool SaveInDataBase(JobDegreeVM model)
        {
            bool result = false;
           
            try
            {
                if (model.ID==0)
                {
                    JobDegree obj = context.JobDegrees.FirstOrDefault(JD => JD.Name == model.Name || JD.ENName == model.Name||JD.DegreeSort==model.DegreeSort);
                    if (obj==null)
                    {
                        JobDegree jobDegreeObj = new JobDegree();
                        jobDegreeObj.ENName = model.ENName;
                        jobDegreeObj.Name = model.Name;
                        jobDegreeObj.DegreeSort = model.DegreeSort;
                        context.JobDegrees.Add(jobDegreeObj);
                        context.SaveChanges();
                        result = true;
                    }
                    else
                        result = false;
                        
                }
                else
                {
                    JobDegree obj = context.JobDegrees.FirstOrDefault(JD => JD.Name == model.Name || JD.ENName == model.Name||JD.DegreeSort==model.DegreeSort);
                    context.Dispose();
                    if (obj == null || obj.ID == model.ID)
                    {
                        context = new ApplicationDbContext();
                        JobDegree jobDegree = new JobDegree();
                        jobDegree.ID = model.ID;
                        jobDegree.Name = model.Name;
                        jobDegree.ENName = model.ENName;
                        jobDegree.DegreeSort = model.DegreeSort;
                        context.Entry(jobDegree).State = EntityState.Modified;
                        context.SaveChanges();
                        result = true;
                    }
                    else
                        result = false;
                    
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public bool CheckName(string Name,int id)
        {
            bool res = false;
            if (id==0)
            {
              
                JobDegree jobDegree = context.JobDegrees.FirstOrDefault(JD => JD.Name == Name);
                if (jobDegree == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                JobDegree jobDegree = context.JobDegrees.FirstOrDefault(JD => JD.Name == Name);
                if (jobDegree == null || jobDegree.ID == id)
                {
                    res = true;
                }
                else
                    res = false;
            }
           
            return res;

        }
        public bool CheckENName(string Name,int Id)
        {
            bool res = false;
            if (Id == 0)
            {

                JobDegree jobDegree = context.JobDegrees.FirstOrDefault(JD => JD.ENName == Name);
                if (jobDegree == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                JobDegree jobDegree = context.JobDegrees.FirstOrDefault(JD => JD.ENName == Name);
                if (jobDegree == null || jobDegree.ID == Id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public bool CheckDegreeSort(int DegreeSort, int Id)
        {
            bool res = false;
            if (Id == 0)
            {

                JobDegree jobDegree = context.JobDegrees.FirstOrDefault(JD => JD.DegreeSort==DegreeSort);
                if (jobDegree == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                JobDegree jobDegree = context.JobDegrees.FirstOrDefault(JD => JD.DegreeSort == DegreeSort);
                if (jobDegree == null || jobDegree.ID == Id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public List<JobDegreeVM> GetAll()
        {
            List<JobDegreeVM> model = new List<JobDegreeVM>();
            try
            {
                model = context.JobDegrees.Select(JD => new JobDegreeVM()
                {
                    ID = JD.ID,
                    Name = JD.Name,
                    ENName = JD.ENName,
                    DegreeSort=JD.DegreeSort
                }).ToList();

            }catch(Exception ex)
            {
                model = null;
                throw ex;
            }
            return model;
        }
        public void Delete(int id)
        {
            try
            {
                JobDegree jobDegree = context.JobDegrees.FirstOrDefault(JD => JD.ID == id);
                context.JobDegrees.Remove(jobDegree);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public JobDegreeVM GetByID(int id)
        {
            JobDegree jobDegree = context.JobDegrees.FirstOrDefault(JD => JD.ID == id);
            return new JobDegreeVM()
            {
                ID = jobDegree.ID,
                Name = jobDegree.Name,
                ENName = jobDegree.ENName,
                DegreeSort=jobDegree.DegreeSort
            };
        }
    }
}
