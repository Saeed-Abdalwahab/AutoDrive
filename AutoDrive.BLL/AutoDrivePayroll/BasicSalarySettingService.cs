using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL
{
    public class BasicSalarySettingService
    {
        
        ApplicationDbContext context = new ApplicationDbContext();
        public BasicSalarySettingVM GetByID(int id)
        {
            BasicSalarySetting obj = context.BasicSalarySettings.FirstOrDefault(BSS => BSS.ID == id);
            return new BasicSalarySettingVM()
            {
                ID = obj.ID,
                JobDegreeId = obj.JobDegreeId,
                JobLevelId = obj.JobLevelId,
                Salary = obj.Salary,
                ChangedSalary = obj.ChangedSalary
            };
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                BasicSalarySetting basicSalarySetting = context.BasicSalarySettings.FirstOrDefault(BSS => BSS.ID == id);
                context.BasicSalarySettings.Remove(basicSalarySetting);
                context.SaveChanges();
                result = true;

            }
            catch (Exception ex)
            {

                result=false;
            }
            return result;
        }
        public bool SaveInDataBase(BasicSalarySettingVM model)
        {
           
            bool result = false;
            try
            {
                if (model.ID==0)
                {
                    BasicSalarySetting obj = context.BasicSalarySettings.FirstOrDefault(BSS => BSS.JobDegreeId == model.JobDegreeId && BSS.JobLevelId == model.JobLevelId);
                    if (obj==null)
                    {
                        BasicSalarySetting BasicSalarySettingObj = new BasicSalarySetting();
                        BasicSalarySettingObj.JobDegreeId = model.JobDegreeId;
                        BasicSalarySettingObj.JobLevelId = model.JobLevelId;
                        BasicSalarySettingObj.Salary = model.Salary;
                        BasicSalarySettingObj.ChangedSalary = model.ChangedSalary;
                        BasicSalarySettingObj.ID = model.ID;
                        context.BasicSalarySettings.Add(BasicSalarySettingObj);
                        context.SaveChanges();
                       
                    }
                    else
                    {
                        result = true;
                    }
                   
                }else
                {
                    BasicSalarySetting obj = context.BasicSalarySettings.FirstOrDefault(BSS => BSS.JobDegreeId == model.JobDegreeId && BSS.JobLevelId == model.JobLevelId);
                    context.Dispose();
                    if ( obj == null||obj.ID==model.ID )
                    {
                        context = new ApplicationDbContext();
                        BasicSalarySetting basicSalarySetting = new BasicSalarySetting();
                        basicSalarySetting.ID = model.ID;
                        basicSalarySetting.JobDegreeId = model.JobDegreeId;
                        basicSalarySetting.JobLevelId = model.JobLevelId;
                        basicSalarySetting.Salary = model.Salary;
                        basicSalarySetting.ChangedSalary = model.ChangedSalary;
                        context.Entry(basicSalarySetting).State = EntityState.Modified;
                        context.SaveChanges();
                        
                    }
                    else
                    {
                        result = true;
                    }
                   
                }
                
            }
            catch(Exception ex)
            {
                
            }

            return result;
        }
        public List<BasicSalarySettingVM> GetAll(string Language)
        {
            List<BasicSalarySettingVM> model = new List<BasicSalarySettingVM>();
           
            try {
                if (Language == "ar-EG")
                {
                    model = context.BasicSalarySettings.Select(BSS => new BasicSalarySettingVM
                    {
                        ID = BSS.ID,
                        JobDegreeId = BSS.JobDegreeId,
                        JobLevelId = BSS.JobLevelId,
                        Salary = BSS.Salary,
                        ChangedSalary = BSS.ChangedSalary,
                        JobDegreeName = BSS.JobDegree.Name,
                        JobLeveLName = BSS.JobLevel.Name
                    }).ToList();
                }
                else
                {
                    model = context.BasicSalarySettings.Select(BSS => new BasicSalarySettingVM
                    {
                        ID = BSS.ID,
                        JobDegreeId = BSS.JobDegreeId,
                        JobLevelId = BSS.JobLevelId,
                        Salary = BSS.Salary,
                        ChangedSalary = BSS.ChangedSalary,
                        JobDegreeName = BSS.JobDegree.ENName,
                        JobLeveLName = BSS.JobLevel.EnName
                    }).ToList();
                }
               
            }
            catch(Exception ex)
            {
                model = null;
            }
            return model;
        }
        public List<AddIncreasingDeductionToJob> GetAddIncreasingDeductionToJobs()
        {
            return context.AddIncreasingDeductionToJobs.ToList();
        }
        public void SaveAddingIncreasesDeductionToJob(int BasicSalaryId,int IncresaesDeductionSettingId)
        {
            AddIncreasingDeductionToJob obj = context.AddIncreasingDeductionToJobs.FirstOrDefault(AIDJ => AIDJ.BasicSalarySettingId == BasicSalaryId && AIDJ.IncreasingDeductionSettingId == IncresaesDeductionSettingId);
            if (obj==null)
            {
                AddIncreasingDeductionToJob addIncreasingDeductionToJob = new AddIncreasingDeductionToJob();
                addIncreasingDeductionToJob.BasicSalarySettingId = BasicSalaryId;
                addIncreasingDeductionToJob.IncreasingDeductionSettingId = IncresaesDeductionSettingId;
                context.AddIncreasingDeductionToJobs.Add(addIncreasingDeductionToJob);
                context.SaveChanges();
            }
           
        }
        public void DeleteAddingIncreasesDeductionToJob(int BasicSalaryId, int IncresaesDeductionSettingId)
        {
            AddIncreasingDeductionToJob addIncreasingDeductionToJob= context.AddIncreasingDeductionToJobs.FirstOrDefault(AIDJ => AIDJ.BasicSalarySettingId == BasicSalaryId && AIDJ.IncreasingDeductionSettingId == IncresaesDeductionSettingId);
            if (addIncreasingDeductionToJob!=null)
            {
                context.AddIncreasingDeductionToJobs.Remove(addIncreasingDeductionToJob);
                context.SaveChanges();
            }
        }
    }
}
