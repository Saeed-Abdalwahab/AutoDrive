using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.AutoDriveMain
{
    public class WorkTimesSettingBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public WorkTimesSettingVM weekAll()
        {
            var _week = db.WorkTimesSettings.Where(a => a.DayName == "sat" || a.DayName == "sun"||a.DayName=="mon"||a.DayName=="tue"||a.DayName=="wed"||a.DayName=="thu"||a.DayName=="fri").ToList();
            
            
            WorkTimesSettingVM WorkTimesSettingVM_Obj = new WorkTimesSettingVM();
            foreach (var item in _week)
            {
                if (item.DayName == "sat") {
                    WorkTimesSettingVM_Obj.sat = true;
                }
                if (item.DayName == "sun")
                {
                    WorkTimesSettingVM_Obj.sun = true;
                }
                if (item.DayName == "mon")
                {
                    WorkTimesSettingVM_Obj.mon = true;
                }
                if (item.DayName == "tue") {
                    WorkTimesSettingVM_Obj.tue = true;
                }
                if (item.DayName == "wed")
                {
                    WorkTimesSettingVM_Obj.wed = true;
                }
                if (item.DayName == "thu")
                {
                    WorkTimesSettingVM_Obj.thu = true;
                }
                if (item.DayName == "fri")
                {
                    WorkTimesSettingVM_Obj.fri = true;
                }
            }
            return WorkTimesSettingVM_Obj;


        }


        public string CreateWeek(WorkTimesSettingVM WorkTimesSettingVM_Obj)
        {
       

            WorkTimesSetting WorkTimesSetting_Obj = new WorkTimesSetting();
            var sat = db.WorkTimesSettings.Any(x=>x.DayName=="sat");
            var sun= db.WorkTimesSettings.Any(x => x.DayName == "sun");
            var mon = db.WorkTimesSettings.Any(x => x.DayName == "mon");
            var tue = db.WorkTimesSettings.Any(x => x.DayName == "tue");
            var wed = db.WorkTimesSettings.Any(x => x.DayName == "wed");
            var thu = db.WorkTimesSettings.Any(x => x.DayName == "thu");
            var fri = db.WorkTimesSettings.Any(x => x.DayName == "fri");

            //var Enname = db.WorkTimesSettings.FirstOrDefault(x => x.DisplayDayNameEn == WorkTimesSettingVM_Obj.DisplayDayNameEn && x.ID != WorkTimesSettingVM_Obj.ID);
            //var name = db.WorkTimesSettings.FirstOrDefault(x => x.DisplayDayName == WorkTimesSettingVM_Obj.DisplayDayName && x.ID != WorkTimesSettingVM_Obj.ID);
            //if (Enname != null || name != null)
            //    return Messages.NameAlreadyExist;
            //---------------------------------------------
            WorkTimesSettingVM_Obj.Mess = null;
            if (WorkTimesSettingVM_Obj.sat == true && sat != false)
            {
                WorkTimesSettingVM_Obj.Mess = Messages.sat;
            }
            if (WorkTimesSettingVM_Obj.sun == true && sun != false)
            {
                WorkTimesSettingVM_Obj.Mess = WorkTimesSettingVM_Obj.Mess+" " + Messages.sun;
            }
            if (WorkTimesSettingVM_Obj.mon == true && mon != false)
            {
                WorkTimesSettingVM_Obj.Mess = WorkTimesSettingVM_Obj.Mess + " " + Messages.mon;
            }
            if (WorkTimesSettingVM_Obj.tue == true && tue != false)
            {
                WorkTimesSettingVM_Obj.Mess = WorkTimesSettingVM_Obj.Mess + " " + Messages.tue;
            }
            if (WorkTimesSettingVM_Obj.wed == true && wed != false)
            {
                WorkTimesSettingVM_Obj.Mess = WorkTimesSettingVM_Obj.Mess + " " + Messages.wed;
            }
            if (WorkTimesSettingVM_Obj.thu == true && thu != false)
            {
                WorkTimesSettingVM_Obj.Mess = WorkTimesSettingVM_Obj.Mess + " " + Messages.thu;
            }
            if (WorkTimesSettingVM_Obj.fri == true && fri != false)
            {
                WorkTimesSettingVM_Obj.Mess = WorkTimesSettingVM_Obj.Mess + " " + Messages.fri;
            }
            if (WorkTimesSettingVM_Obj.Mess != null)
            {
              return  WorkTimesSettingVM_Obj.Mess = Messages.NameAlreadyExist + " " + WorkTimesSettingVM_Obj.Mess;
            }




            //--------------------------------------------
            if (WorkTimesSettingVM_Obj.sat == false && WorkTimesSettingVM_Obj.sun == false && WorkTimesSettingVM_Obj.mon == false && WorkTimesSettingVM_Obj.tue == false && WorkTimesSettingVM_Obj.wed == false && WorkTimesSettingVM_Obj.thu == false && WorkTimesSettingVM_Obj.fri == false)
            {
                return Messages.ChooseADay;

            }
            if (WorkTimesSettingVM_Obj.sat == true &&sat==false)
                {
                    WorkTimesSetting_Obj.DisplayDayName = "السبت";
                    WorkTimesSetting_Obj.DisplayDayNameEn = "Saturday";
                    WorkTimesSetting_Obj.DayName = "sat";
                    WorkTimesSetting_Obj.FromHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.FromHour);
                    WorkTimesSetting_Obj.ToHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.ToHour);
                    var x = ((WorkTimesSetting_Obj.ToHour).Subtract((WorkTimesSetting_Obj.FromHour)).Hours);

                    WorkTimesSetting_Obj.NOWorkHours = Convert.ToDouble(x);

                    db.WorkTimesSettings.Add(WorkTimesSetting_Obj);
                    db.SaveChanges();
                }
                if (WorkTimesSettingVM_Obj.sun == true && sun == false)
                {
                    WorkTimesSetting_Obj.DisplayDayName = "الاحد";
                    WorkTimesSetting_Obj.DisplayDayNameEn = "Sunday";
                    WorkTimesSetting_Obj.DayName = "sun";
                    WorkTimesSetting_Obj.FromHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.FromHour);
                    WorkTimesSetting_Obj.ToHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.ToHour);
                    var x = ((WorkTimesSetting_Obj.ToHour).Subtract((WorkTimesSetting_Obj.FromHour)).Hours);

                    WorkTimesSetting_Obj.NOWorkHours = Convert.ToDouble(x);
                    db.WorkTimesSettings.Add(WorkTimesSetting_Obj);
                    db.SaveChanges();
                }
                if (WorkTimesSettingVM_Obj.mon == true && mon == false)
                {
                    WorkTimesSetting_Obj.DisplayDayName = "الاثنين";
                    WorkTimesSetting_Obj.DisplayDayNameEn = "Monday";
                    WorkTimesSetting_Obj.DayName = "mon";
                    WorkTimesSetting_Obj.FromHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.FromHour);
                    WorkTimesSetting_Obj.ToHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.ToHour);
                    var x = ((WorkTimesSetting_Obj.ToHour).Subtract((WorkTimesSetting_Obj.FromHour)).Hours);

                    WorkTimesSetting_Obj.NOWorkHours = Convert.ToDouble(x);
                    db.WorkTimesSettings.Add(WorkTimesSetting_Obj);
                    db.SaveChanges();
                }
                if (WorkTimesSettingVM_Obj.tue == true && tue == false)
                {
                    WorkTimesSetting_Obj.DisplayDayName = "الثلثاء";
                    WorkTimesSetting_Obj.DisplayDayNameEn = "Tuesday";
                    WorkTimesSetting_Obj.DayName = "tue";
                    WorkTimesSetting_Obj.FromHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.FromHour);
                    WorkTimesSetting_Obj.ToHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.ToHour);
                    var x = ((WorkTimesSetting_Obj.ToHour).Subtract((WorkTimesSetting_Obj.FromHour)).Hours);

                    WorkTimesSetting_Obj.NOWorkHours = Convert.ToDouble(x);
                    db.WorkTimesSettings.Add(WorkTimesSetting_Obj);
                    db.SaveChanges();
                }
                if (WorkTimesSettingVM_Obj.wed == true && wed == false)
                {
                    WorkTimesSetting_Obj.DisplayDayName = "الاربعاء";
                    WorkTimesSetting_Obj.DisplayDayNameEn = "Wednesday";
                    WorkTimesSetting_Obj.DayName = "wed";
                    WorkTimesSetting_Obj.FromHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.FromHour);
                    WorkTimesSetting_Obj.ToHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.ToHour);
                    var x = ((WorkTimesSetting_Obj.ToHour).Subtract((WorkTimesSetting_Obj.FromHour)).Hours);

                    WorkTimesSetting_Obj.NOWorkHours = Convert.ToDouble(x);
                    db.WorkTimesSettings.Add(WorkTimesSetting_Obj);
                    db.SaveChanges();
                }
                if (WorkTimesSettingVM_Obj.thu == true && thu == false)
                {
                    WorkTimesSetting_Obj.DisplayDayName = "الخميس";
                    WorkTimesSetting_Obj.DisplayDayNameEn = "Thursday";
                    WorkTimesSetting_Obj.DayName = "thu";
                    WorkTimesSetting_Obj.FromHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.FromHour);
                    WorkTimesSetting_Obj.ToHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.ToHour);
                    var x = ((WorkTimesSetting_Obj.ToHour).Subtract((WorkTimesSetting_Obj.FromHour)).Hours);

                    WorkTimesSetting_Obj.NOWorkHours = Convert.ToDouble(x);
                    db.WorkTimesSettings.Add(WorkTimesSetting_Obj);
                    db.SaveChanges();
                }
                if (WorkTimesSettingVM_Obj.fri == true && fri == false)
                {
                    WorkTimesSetting_Obj.DisplayDayName = "الجمعه";
                    WorkTimesSetting_Obj.DisplayDayNameEn = "Friday";
                    WorkTimesSetting_Obj.DayName = "fri";
                    WorkTimesSetting_Obj.FromHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.FromHour);
                    WorkTimesSetting_Obj.ToHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.ToHour);
                    var x = ((WorkTimesSetting_Obj.ToHour).Subtract((WorkTimesSetting_Obj.FromHour)).Hours);

                    WorkTimesSetting_Obj.NOWorkHours = Convert.ToDouble(x);
                    db.WorkTimesSettings.Add(WorkTimesSetting_Obj);
                    db.SaveChanges();
                
            }
            WorkTimesSettingVM_Obj.Mess = null;
            
            return Messages.SaveSucc;
        }

        public IEnumerable<WorkTimesSettingVM> Getall()
        {


          // var   v=tt.ToString(@"hh\:mm\:ss");
             var   Model = (from m in db.WorkTimesSettings
                            select new  {
                                ID = m.ID,
                                DisplayDayName = m.DisplayDayName,
                                DisplayDayNameEn = m.DisplayDayNameEn,
                                DayName = m.DayName,
                                FromHour = m.FromHour,
                                ToHour = m.ToHour,
                                NOWorkHours = m.NOWorkHours

                            }).AsEnumerable()
    .Select(x => new WorkTimesSettingVM {
            ID = x.ID,
            DisplayDayName = x.DisplayDayName,
            DisplayDayNameEn = x.DisplayDayNameEn,
            DayName = x.DayName,
            FromHour = x.FromHour.ToString("c"),
            ToHour = x.ToHour.ToString("c"),
            NOWorkHours = x.NOWorkHours
         });  
            return Model;
        }
        public WorkTimesSettingVM Get(int ID)
        {
            var Model = (from m in db.WorkTimesSettings
                         where m.ID==ID
                         select new
                         {
                             ID = m.ID,
                             DisplayDayName = m.DisplayDayName,
                             DisplayDayNameEn = m.DisplayDayNameEn,
                             DayName = m.DayName,
                             FromHour = m.FromHour,
                             ToHour = m.ToHour,
                             NOWorkHours = m.NOWorkHours

                         }).AsEnumerable()
   .Select(x => new WorkTimesSettingVM
   {
       ID = x.ID,
       DisplayDayName = x.DisplayDayName,
       DisplayDayNameEn = x.DisplayDayNameEn,
       DayName = x.DayName,
       FromHour = x.FromHour.ToString("c"),
       ToHour = x.ToHour.ToString("c"),
       NOWorkHours = x.NOWorkHours
   }).FirstOrDefault();
            return Model;
        }

        public string Edit(WorkTimesSettingVM WorkTimesSettingVM_Obj)
        {
            var Enname = db.WorkTimesSettings.FirstOrDefault(x => x.DisplayDayNameEn == WorkTimesSettingVM_Obj.DisplayDayNameEn && x.ID != WorkTimesSettingVM_Obj.ID);
            var name = db.WorkTimesSettings.FirstOrDefault(x => x.DisplayDayName == WorkTimesSettingVM_Obj.DisplayDayName && x.ID != WorkTimesSettingVM_Obj.ID);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            WorkTimesSetting WorkTimesSetting_Obj = db.WorkTimesSettings.FirstOrDefault(x => x.ID == WorkTimesSettingVM_Obj.ID);

            WorkTimesSetting_Obj.ID = WorkTimesSettingVM_Obj.ID;
            WorkTimesSetting_Obj.DisplayDayName = WorkTimesSettingVM_Obj.DisplayDayName;
            WorkTimesSetting_Obj.DisplayDayNameEn = WorkTimesSettingVM_Obj.DisplayDayNameEn;
            WorkTimesSetting_Obj.DayName = WorkTimesSettingVM_Obj.DayName;

           // var _FromHour = DateTime.ParseExact(WorkTimesSettingVM_Obj.FromHour, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);

            WorkTimesSetting_Obj.FromHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.FromHour);
            //WorkTimesSetting_Obj.FromHour = _FromHour;

            // var _ToHour = DateTime.ParseExact(WorkTimesSettingVM_Obj.ToHour, "hh:mm:ss tt", System.Globalization.CultureInfo.CurrentCulture);
            // WorkTimesSetting_Obj.ToHour = _ToHour;

            WorkTimesSetting_Obj.ToHour = TimeSpan.Parse(WorkTimesSettingVM_Obj.ToHour);

            WorkTimesSetting_Obj.NOWorkHours = WorkTimesSettingVM_Obj.NOWorkHours;


            db.Entry(WorkTimesSetting_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        public string delete(int ID)
        {
            WorkTimesSetting WorkTimesSetting_Obj = db.WorkTimesSettings.Find(ID);
            db.WorkTimesSettings.Remove(WorkTimesSetting_Obj);
            db.SaveChanges();
            // return true;
            return Messages.DeleteSucc;
        }
    }
}
