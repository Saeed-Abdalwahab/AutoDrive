using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
    public class WorkingHoursSettingHRService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool compareIDs(List<WorkingHoursSettingHR> workingHoursSettingHRsLst,int id)
        {
            foreach (var item in workingHoursSettingHRsLst)
            {
                if (item.ID==id)
                {
                    return true;
                }
            }
            return false;
        }
        public string SaveInDataBase(WorkingHoursSettingHRVM model)
        {
            string result = "";
            WorkingHoursSettingHR workingHoursSettingHR= context.WorkingHoursSettingHRs.FirstOrDefault(WHS => WHS.ArName == model.ArName && WHS.EnName == model.EnName && model.ToDate==WHS.ToDate&&model.FromDate==WHS.FromDate);
            List<WorkingHoursSettingHR> workingHoursSettingHRInSamePriod = context.WorkingHoursSettingHRs.Where(WHS => (WHS.ArName == model.ArName && WHS.EnName == model.EnName) && (((WHS.FromDate==null||WHS.FromDate<= model.FromDate) && (WHS.ToDate==null||WHS.ToDate>= model.FromDate)) || ((WHS.FromDate==null|| WHS.FromDate<= model.ToDate) && (WHS.ToDate==null||WHS.ToDate>= model.ToDate)))).ToList();
            if (workingHoursSettingHR == null ||workingHoursSettingHR.ID==model.ID)
            {
                if (workingHoursSettingHRInSamePriod.Count == 0|| (compareIDs(workingHoursSettingHRInSamePriod, model.ID)&&workingHoursSettingHRInSamePriod.Count==1))
                {
                    if (model.ID == 0)
                    {
                        WorkingHoursSettingHR obj = new WorkingHoursSettingHR();
                        obj.ArName = model.ArName;
                        obj.EnName = model.EnName;
                        obj.FromDate = model.FromDate;
                        obj.ToDate = model.ToDate;
                        context.WorkingHoursSettingHRs.Add(obj);
                        context.SaveChanges();
                    }
                    else
                    {
                        WorkingHoursSettingHR obj = context.WorkingHoursSettingHRs.FirstOrDefault(WHS => WHS.ID == model.ID);
                        obj.ArName = model.ArName;
                        obj.EnName = model.EnName;
                        obj.FromDate = model.FromDate;
                        obj.ToDate = model.ToDate;
                        context.Entry(obj).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    result = "true";
                    return result;
                }
                else
                {
                    result = "Working Hours Setting is existed in the same period";
                    return result;
                }
                
            }
            else
            {
                result = "Working Hours Setting is existed";
                return result;
            }
        }
        public List<WorkingHoursSettingHRVM> GetALL()
        {
            try
            {
                List<WorkingHoursSettingHRVM> model = new List<WorkingHoursSettingHRVM>();
                model = context.WorkingHoursSettingHRs.ToList().Select(WHS => new WorkingHoursSettingHRVM()
                {
                    ID = WHS.ID,
                    ArName = WHS.ArName,
                    EnName = WHS.EnName,
                    From = (WHS.FromDate == null) ? "" : WHS.FromDate.Value.ToString("dd/MM/yyyy"),
                    To = (WHS.ToDate == null) ? "" : WHS.ToDate.Value.ToString("dd/MM/yyyy")
                }).ToList();
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public WorkingHoursSettingHRVM GetByID(int id)
        {
            try
            {
                WorkingHoursSettingHR model = context.WorkingHoursSettingHRs.FirstOrDefault(WHS => WHS.ID == id);
                return new WorkingHoursSettingHRVM()
                {
                    ID=model.ID,
                    ArName = model.ArName,
                    EnName = model.EnName,
                    From = (model.FromDate == null) ? "" : model.FromDate.Value.ToString("dd/MM/yyyy"),
                    To = (model.ToDate == null) ? "" : model.ToDate.Value.ToString("dd/MM/yyyy")
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void delete(int id)
        {
            try
            {
                WorkingHoursSettingHR workingHoursSettingHR = context.WorkingHoursSettingHRs.FirstOrDefault(WHS => WHS.ID == id);
                context.WorkingHoursSettingHRs.Remove(workingHoursSettingHR);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<WorkingHoursSettingHRVM>GetEmployeeHoursSForEmployee( string Language)
        {
            try
            {
                List<WorkingHoursSettingHR> workingHoursSettingHRsLst = context.WorkingHoursSettingHRs.ToList();
                List<WorkingHoursSettingHRVM> model = new List<WorkingHoursSettingHRVM>();
                if (workingHoursSettingHRsLst!=null&& workingHoursSettingHRsLst.Count>0)
                {
                    foreach (var WorkingHours in workingHoursSettingHRsLst)
                    {
                        if (Language == "en-US")
                        {
                            model.Add(new WorkingHoursSettingHRVM()
                            {
                                ID = WorkingHours.ID,
                                WorkingHoursName = WorkingHours.EnName,
                                From = (WorkingHours.FromDate == null || WorkingHours.ToDate == null) ? "" : WorkingHours.FromDate.Value.ToString("dd/MM/yyyy"),
                                To = (WorkingHours.FromDate == null || WorkingHours.ToDate == null) ? "" : WorkingHours.ToDate.Value.ToString("dd/MM/yyyy")
                            });
                        }
                        else
                        {
                            model.Add(new WorkingHoursSettingHRVM()
                            {
                                ID = WorkingHours.ID,
                                WorkingHoursName = WorkingHours.ArName,
                                From = (WorkingHours.FromDate == null || WorkingHours.ToDate == null) ? "" : WorkingHours.FromDate.Value.ToString("dd/MM/yyyy"),
                                To = (WorkingHours.FromDate == null || WorkingHours.ToDate == null) ? "" : WorkingHours.ToDate.Value.ToString("dd/MM/yyyy")
                            });
                        }
                    }
                   
                }
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
