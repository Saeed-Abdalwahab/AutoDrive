using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
    public class EmployeeHoursSettingService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool checkWorkingHours(int workingHoursId)
        {
            WorkingHoursSettingHR workingHoursSettingHR = context.WorkingHoursSettingHRs.FirstOrDefault(WHS => WHS.ID == workingHoursId);
            if (workingHoursSettingHR!=null)
            {
                if (workingHoursSettingHR.ToDate!=null&&workingHoursSettingHR.FromDate!=null)
                {
                    return false;
                }
                return true;
            }
            return true;
        }
        public void CreateEmployeeWorkingHours(int WorkingHoursSettingHRId, DateTime? FromDate, DateTime? ToDate, int EmployeeId)
        {
            try
            {
                EmployeeHoursSetting employeeHoursSetting = new EmployeeHoursSetting();
                employeeHoursSetting.EmployeeId = EmployeeId;
                employeeHoursSetting.WorkingHoursSettingHRId = WorkingHoursSettingHRId;
                employeeHoursSetting.FromDate = FromDate;
                employeeHoursSetting.ToDate = ToDate;
                context.EmployeeHoursSettings.Add(employeeHoursSetting);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string SaveInDataBase(EmployeeHoursSettingVM model)
        {
            string res = "";
            try
            {
                EmployeeHoursSetting obj = context.EmployeeHoursSettings.FirstOrDefault(EHS => EHS.EmployeeId == model.EmployeeId && EHS.WorkingHoursSettingHRId == model.WorkingHoursSettingHRId && (((EHS.FromDate == null && model.FromDate == null) || ((EHS.FromDate == null || EHS.FromDate <= model.FromDate) && (EHS.ToDate == null || EHS.ToDate >= model.FromDate)))
                                                                                       && ((EHS.ToDate == null && model.ToDate == null) || ((EHS.FromDate == null || EHS.FromDate <= model.ToDate) && (EHS.ToDate == null || EHS.ToDate >= model.ToDate)))));
                
                if (obj == null || obj.ID == model.ID)
                {
                    if (model.ID == 0)
                    {
                        EmployeeHoursSetting employeeHoursSetting = new EmployeeHoursSetting();
                        employeeHoursSetting.EmployeeId = model.EmployeeId.Value;
                        employeeHoursSetting.WorkingHoursSettingHRId = model.WorkingHoursSettingHRId;
                        employeeHoursSetting.FromDate = model.FromDate;
                        employeeHoursSetting.ToDate = model.ToDate;
                        context.EmployeeHoursSettings.Add(employeeHoursSetting);
                        context.SaveChanges();
                    }
                    else
                    {
                        EmployeeHoursSetting employeeHoursSetting = context.EmployeeHoursSettings.FirstOrDefault(EHS => EHS.ID == model.ID);
                        if (employeeHoursSetting.WorkingHoursSettingHRId != model.WorkingHoursSettingHRId)
                        {
                            DateTime? Date;
                            bool OldWorkingHours = checkWorkingHours(employeeHoursSetting.WorkingHoursSettingHRId);
                            bool NewWorkingHours = checkWorkingHours(model.WorkingHoursSettingHRId);
                            if (OldWorkingHours == false && NewWorkingHours == true)
                            {
                                //Convert from Working Hours that has special period to Working Hours that has no special period
                                Date = null;
                            }
                            else if (OldWorkingHours == true && NewWorkingHours == false)
                            {
                                //Convert from Working Hours that has no special period to Working Hours that has special period
                                WorkingHoursSettingHR workingHoursSettingHR = context.WorkingHoursSettingHRs.FirstOrDefault(WHS => WHS.ID == model.WorkingHoursSettingHRId);
                                Date = workingHoursSettingHR.FromDate;
                            }
                            else
                            {
                                //rest cases
                                Date = model.FromDate;
                            }
                            employeeHoursSetting.ToDate = Date;
                            context.Entry(employeeHoursSetting).State = EntityState.Modified;
                            context.SaveChanges();
                            EmployeeHoursSetting obj2 = new EmployeeHoursSetting();
                            obj2.EmployeeId = model.EmployeeId.Value;
                            obj2.WorkingHoursSettingHRId = model.WorkingHoursSettingHRId;
                            obj2.FromDate = model.FromDate;
                            obj2.ToDate = model.ToDate;
                            context.EmployeeHoursSettings.Add(obj2);
                            context.SaveChanges();
                        }
                        else
                        {
                            employeeHoursSetting.EmployeeId = model.EmployeeId.Value;
                            employeeHoursSetting.WorkingHoursSettingHRId = model.WorkingHoursSettingHRId;
                            employeeHoursSetting.FromDate = model.FromDate;
                            employeeHoursSetting.ToDate = model.ToDate;
                            context.Entry(employeeHoursSetting).State = EntityState.Modified;
                            context.SaveChanges();
                        }

                    }

                    res = "true";
                    return res;
                }
                else
                {
                    res = "Employee Working Hours is existed in the same period";
                    return res;
                }
            }
            catch (Exception ex)
            {
                res = "false";
            }
            return res;
        }
        public List<EmployeeHoursSettingVM> GetALL(string Language,int ID)
        {
            try
            {
                List<EmployeeHoursSettingVM> model = new List<EmployeeHoursSettingVM>();
                List<EmployeeHoursSetting> employeeHoursSettingsLst = context.EmployeeHoursSettings.Where(EHS=>EHS.EmployeeId==ID).ToList();
                if (employeeHoursSettingsLst!=null)
                {
                    if (Language=="en-US")
                    {
                        model = employeeHoursSettingsLst.Select(EHS => new EmployeeHoursSettingVM()
                        {
                            ID = EHS.ID,
                            EmpName = EHS.employee.EnName,
                            WorkingHoursName = EHS.WorkingHoursSettingHR.EnName,
                            from = EHS.FromDate == null ? "" : EHS.FromDate.Value.ToString("dd/MM/yyyyy"),
                            to = EHS.ToDate == null ? "" : EHS.ToDate.Value.ToString("dd/MM/yyyyy")
                        }).ToList();
                    }
                    else
                    {
                        model = employeeHoursSettingsLst.Select(EHS => new EmployeeHoursSettingVM()
                        {
                            ID = EHS.ID,
                            EmpName = EHS.employee.Name,
                            WorkingHoursName = EHS.WorkingHoursSettingHR.ArName,
                            from = EHS.FromDate == null ? "" : EHS.FromDate.Value.ToString("dd/MM/yyyyy"),
                            to = EHS.ToDate == null ? "" : EHS.ToDate.Value.ToString("dd/MM/yyyyy")
                        }).ToList();
                    }
                }
                return model;
            }
            catch (Exception ex) 
            {

                throw;
            }
        }
        public string delete(int id)
        {
            EmployeeHoursSetting employeeHoursSetting = context.EmployeeHoursSettings.FirstOrDefault(EHS => EHS.ID == id);
            context.EmployeeHoursSettings.Remove(employeeHoursSetting);
            context.SaveChanges();
            return Messages.DeleteSucc; 
        }
        public EmployeeHoursSettingVM GetByID(int id,string Language)
        {
            try
            {
                EmployeeHoursSetting model = context.EmployeeHoursSettings.FirstOrDefault(EHS => EHS.ID == id);
                EmployeeHoursSettingVM employeeHoursSetting = new EmployeeHoursSettingVM();
                if (Language=="en-US")
                {
                    employeeHoursSetting= new EmployeeHoursSettingVM()
                    {
                        ID = model.ID,
                        EmployeeId = model.EmployeeId,
                        EmpName = model.employee.EnName,
                        WorkingHoursSettingHRId = model.WorkingHoursSettingHRId,
                        from = model.FromDate == null ? "" : model.FromDate.Value.ToString("dd/MM/yyyy"),
                        to = model.ToDate == null ? "" : model.ToDate.Value.ToString("dd/MM/yyyy"),
                        ToDate=model.ToDate,
                        FromDate=model.FromDate
                    };
                }
                else
                {
                    employeeHoursSetting= new EmployeeHoursSettingVM()
                    {
                        ID = model.ID,
                        EmployeeId = model.EmployeeId,
                        EmpName = model.employee.Name,
                        WorkingHoursSettingHRId = model.WorkingHoursSettingHRId,
                        from = model.FromDate == null ? "" : model.FromDate.Value.ToString("dd/MM/yyyy"),
                        to = model.ToDate == null ? "" : model.ToDate.Value.ToString("dd/MM/yyyy"),
                        ToDate = model.ToDate,
                        FromDate = model.FromDate
                    };
                }
                return employeeHoursSetting;
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
