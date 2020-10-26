using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
   public class WorkingHoursSettingHRDetialsService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public string GetARDayName(string Day)
        {
            string DayName = "";
            switch (Day)
            {
                case "Sat":
                    DayName = "السبت";
                    break;
                case "Sun":
                    DayName = "الاحد";
                    break;
                case "Mon":
                    DayName = "الاتنين";
                    break;
                case "Tue":
                    DayName = "الثلاثاء";
                    break;
                case "Wed":
                    DayName = "الاربعاء";
                    break;
                case "Thu":
                    DayName = "الخميس";
                    break;
                case "Fri":
                    DayName = "الجمعة";
                    break;
            }
            return DayName;
        }
        public string GetENDayName(string Day)
        {
            string DayName = "";
            switch (Day)
            {
                case "Sat":
                    DayName = "Saturday";
                    break;
                case "Sun":
                    DayName = "Sunday";
                    break;
                case "Mon":
                    DayName = "Monday";
                    break;
                case "Tue":
                    DayName = "Tuesday";
                    break;
                case "Wed":
                    DayName = "Wednesday";
                    break;
                case "Thu":
                    DayName = "Thursday";
                    break;
                case "Fri":
                    DayName = "Friday";
                    break;
            }
            return DayName;
        }
        public string SaveInDataBase(WorkingHoursSettingDetialsHrVM model)
        {
            try
            {
                DateTime from = Convert.ToDateTime(model.FromTime);
                DateTime To = Convert.ToDateTime(model.ToTime);
                TimeSpan fromTime, totime;
                if (model.FromTime.Contains("PM"))
                {
                   fromTime = new TimeSpan(from.Hour, from.Minute, from.Second);
                }
                else
                {
                   fromTime = new TimeSpan(from.Hour, from.Minute, from.Second);
                }
                if (model.ToTime.Contains("PM"))
                {
                    totime = new TimeSpan(To.Hour, To.Minute, To.Second);
                }
                else
                {
                    totime = new TimeSpan(To.Hour, To.Minute, To.Second);
                }
                string Day = model.DayName.ToString();
                WorkingHoursSettingDetialsHR obj = context.WorkingHoursSettingDetialsHRs.FirstOrDefault(WHSD => WHSD.WorkingHoursSettingHRId == model.WorkingHoursSettingHRId&&WHSD.DayName==Day);
                if (obj==null||obj.ID==model.ID)
                {
                    if (model.ID == 0)
                    {
                        WorkingHoursSettingDetialsHR workingHoursSettingDetialsHR = new WorkingHoursSettingDetialsHR();
                        workingHoursSettingDetialsHR.DayName = Day;
                        workingHoursSettingDetialsHR.FromTime = fromTime;
                        workingHoursSettingDetialsHR.ToTime = totime;
                        workingHoursSettingDetialsHR.WorkingHoursSettingHRId = model.WorkingHoursSettingHRId;
                        workingHoursSettingDetialsHR.DisplayArDayName = GetARDayName(Day);
                        workingHoursSettingDetialsHR.DisplayEnDayName = GetENDayName(Day);
                        context.WorkingHoursSettingDetialsHRs.Add(workingHoursSettingDetialsHR); 
                        context.SaveChanges();
                    }
                    else
                    {
                        WorkingHoursSettingDetialsHR workingHoursSettingDetialsHR = context.WorkingHoursSettingDetialsHRs.FirstOrDefault(WHSD => WHSD.ID == model.ID);
                        workingHoursSettingDetialsHR.DayName = Day;
                        workingHoursSettingDetialsHR.FromTime = fromTime;
                        workingHoursSettingDetialsHR.ToTime = totime;
                        workingHoursSettingDetialsHR.WorkingHoursSettingHRId = model.WorkingHoursSettingHRId;
                        workingHoursSettingDetialsHR.DisplayArDayName = GetARDayName(Day);
                        workingHoursSettingDetialsHR.DisplayEnDayName = GetENDayName(Day);
                        context.Entry(workingHoursSettingDetialsHR).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    string res = "true";
                    return res;
                }
                else
                {
                    string res = "Working Hours Detials Existed";
                    return res;
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public List<WorkingHoursSettingDetialsHrVM> GetALL(string Language)
        {
            try
            {
                List<WorkingHoursSettingDetialsHrVM> model = new List<WorkingHoursSettingDetialsHrVM>();
                List<WorkingHoursSettingDetialsHR> workingHoursSettingDetialsHRsLst = context.WorkingHoursSettingDetialsHRs.ToList();
                if (workingHoursSettingDetialsHRsLst.Count>0)
                {
                    foreach (var workingHoursSettingDetialsHR in workingHoursSettingDetialsHRsLst)
                    {
                        string from,to;
                        from = GetTime(workingHoursSettingDetialsHR.FromTime, "ar-EG");
                        to = GetTime(workingHoursSettingDetialsHR.ToTime, "ar-EG");
                        if (Language=="en-US")
                        {
                            model.Add(new WorkingHoursSettingDetialsHrVM()
                            {
                                ID = workingHoursSettingDetialsHR.ID,
                                Day = ((Days)Enum.Parse(typeof(Days), workingHoursSettingDetialsHR.DayName)).GetDisplayName(),
                                FromTime = from,
                                ToTime = to,
                                WorkingHoursName = workingHoursSettingDetialsHR.WorkingHoursSettingHR.EnName
                            });
                        }
                        else
                        {
                            model.Add(new WorkingHoursSettingDetialsHrVM()
                            {
                                ID = workingHoursSettingDetialsHR.ID,
                                Day = ((Days)Enum.Parse(typeof(Days), workingHoursSettingDetialsHR.DayName)).GetDisplayName(),
                                FromTime = from,
                                ToTime = to,
                                WorkingHoursName = workingHoursSettingDetialsHR.WorkingHoursSettingHR.ArName
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
        public void delete(int id)
        {
            try
            {
                WorkingHoursSettingDetialsHR workingHoursSettingDetialsHR = context.WorkingHoursSettingDetialsHRs.FirstOrDefault(WHSD => WHSD.ID == id);
                context.WorkingHoursSettingDetialsHRs.Remove(workingHoursSettingDetialsHR);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public string GetTime(TimeSpan Time,string Language)
        {
            string Minutes = Time.Minutes < 10 ? Time.Minutes.ToString() + "0" : Time.Minutes.ToString();
            string TimeString;
            if (Time.Hours >= 12)
            {
                if (Time.Hours == 12)
                {
                    TimeString = Time.Hours.ToString() + ":" + Minutes;
                }
                else
                {
                    int hours = Time.Hours - 12;
                    TimeString = hours.ToString() + ":" + Minutes;
                }
                if (Language== "ar-EG")
                {
                    TimeString = TimeString + " PM";
                }
                else
                {
                    TimeString = "PM " + TimeString;
                }
            }
            else
            {
                if (Time.Hours == 0)
                {
                    TimeString = (Time.Hours + 12).ToString() + ":" + Minutes;
                }
                else
                    TimeString = Time.Hours.ToString() + ":" +Minutes;
                if (Language== "ar-EG")
                {
                    TimeString = TimeString + " AM";
                }
                else
                {
                    TimeString = "AM " + TimeString;
                }
            }
            return TimeString;
        }
        public WorkingHoursSettingDetialsHrVM GetByID(int id,string Language)
        {
            try
            {
                WorkingHoursSettingDetialsHR workingHoursSettingDetialsHR = context.WorkingHoursSettingDetialsHRs.FirstOrDefault(WHSD => WHSD.ID == id);
                string from, to;
                from = GetTime(workingHoursSettingDetialsHR.FromTime, Language);
                to = GetTime(workingHoursSettingDetialsHR.ToTime, Language);
                WorkingHoursSettingDetialsHrVM model = new WorkingHoursSettingDetialsHrVM();
                model.ID = workingHoursSettingDetialsHR.ID;
                model.WorkingHoursSettingHRId = workingHoursSettingDetialsHR.WorkingHoursSettingHRId;
                model.DayName = (Days)Enum.Parse(typeof(Days), workingHoursSettingDetialsHR.DayName);
                model.FromTime = from;
                model.ToTime = to;
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
} 
