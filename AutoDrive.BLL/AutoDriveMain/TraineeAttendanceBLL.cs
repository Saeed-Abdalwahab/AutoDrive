using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDrive.Static.Enums;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.AutoDriveMain
{
    public class TraineeAttendanceBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public List<TraineeVM> GetAllTrainee()
        {
           List< TraineeVM> Model = new List<TraineeVM>();
            try
            {
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         where t.FileNo != null

                         select new TraineeVM()
                         {

                             ID = t.ID,
                             TraineeGuid=t.TraineeGuid,
                             ArName = t.ArName,
                             EnName = t.EnName,


                         }).ToList();

                    
            }
            catch
            {
                Model = null;
            }

            return Model;
        }

        public TraineeAttendanceVM GetbyId(Guid TraineeGuid)
        {
            TraineeAttendanceVM Model = new TraineeAttendanceVM();
            try
            {
                 Model = (from t in db.Trainees
                              join tE in db.TraineeEvaluations
                              on t.ID equals tE.TraineeId
                              join s in db.StudyPeriodSettings
                              on tE.StudyPeriodSettingId equals s.ID
                              join c in db.CourseReservations
                              on tE.ID equals c.TraineeEvaluationId
                              //join tA in db.TraineeAttendances
                              //on tE.ID equals tA.TraineeEvaluationId

                              where t.TraineeGuid == TraineeGuid && t.FileNo != null

                              select new
                              {
                                  //ID = c.ID,
                                  TraineeId = t.ID,
                                  TraineeGuid=t.TraineeGuid,
                                  TraineeEvaluationId = tE.ID,
                                  //AttendanceDate = tA.AttendanceDate,

                                  ArName = t.ArName,
                                  EnName = t.EnName,
                                  Code = t.Code,
                                  FileNo = t.FileNo,

                                  StudyPeriodSettingName = s.DriveLevel.Name,
                                  StudyPeriodSettingEnName = s.DriveLevel.EnName,

                                  VisualStudyCount=s.VisualStudyCount,
                                  PracticalCount=s.PracticalCount,

                                  //practicalOrVisual =tA.PracticalOrVisual,

                                  CourseStartDate =  c.CourseStartDate,
                                  CourseEndDate =c.CourseEndDate,

                              }).AsEnumerable()
                            .Select(x => new TraineeAttendanceVM
                            {
                                //ID = x.ID,
                                TraineeId = x.TraineeId,
                                TraineeGuid = x.TraineeGuid,
                                TraineeEvaluationId = x.TraineeEvaluationId,
                                //AttendanceDate = x.AttendanceDate,

                                ArName = x.ArName,
                                EnName = x.EnName,
                                Code = x.Code,
                                FileNo = x.FileNo,

                                StudyPeriodSettingName = x.StudyPeriodSettingName,
                                StudyPeriodSettingEnName = x.StudyPeriodSettingEnName,
                                VisualStudyCount=x.VisualStudyCount,
                                PracticalCount=x.PracticalCount,
                               // practicalOrVisual = x.practicalOrVisual,


                                CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                                CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),
                            }).FirstOrDefault();  

            }
            catch(Exception ex)
            {
                Model = null;
            }

            return Model;
        }



        //--------------------------------------------------------------------------
      
        public List<TraineeAttendanceVM> Getall(int _TraineeId)
        {
           List<TraineeAttendanceVM> Model = new List<TraineeAttendanceVM>();
            try
            {

                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         join tA in db.TraineeAttendances
                         on tE.ID equals tA.TraineeEvaluationId

                         where t.FileNo != null && tA.TraineeId== _TraineeId


                         select new
                         {
                             ID = tA.ID,
                             TraineeId = t.ID,
                             TraineeEvaluationId = tE.ID,
                             AttendanceDate = tA.AttendanceDate,
                             //Practicaldays = c.CourseStartDate,
                             //Visualdays=c.CourseStartDate,
                             DaysWork = tA.AttendanceDate,

                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,

                             PracticalOrVisual = tA.PracticalOrVisual,

                             CourseStartDate = c.CourseStartDate,
                             CourseEndDate = c.CourseEndDate,

                         }).AsEnumerable()
                          .Select(x => new TraineeAttendanceVM
                          {
                              ID = x.ID,
                              TraineeId = x.TraineeId,
                              TraineeEvaluationId = x.TraineeEvaluationId,
                              AttendanceDate = String.Format("{0:MM/dd/yyyy}", x.AttendanceDate),
                              //Practicaldays = String.Format("{0:MM/dd/yyyy}", x.AttendanceDate),
                              //Visualdays = String.Format("{0:MM/dd/yyyy}", x.AttendanceDate),
                              // DaysWork="",
                              DaysWork = x.DaysWork.DayOfWeek.ToString(),
                              VisualStudyCount = x.VisualStudyCount,
                              PracticalCount = x.PracticalCount,
                              PracticalOrVisual = x.PracticalOrVisual==1? "عملى" : "نظرى",
                              PracticalOrVisualEn= x.PracticalOrVisual == 1 ? "Practical" : "Visual",
                              CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                              CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),
                          }).ToList();

                
            

                return Model;



            }
            catch (Exception ex)
            {
               return Model = null;
            }



        }
     

       
        public TraineeAttendanceVM Get(int ID)
        {
            TraineeAttendanceVM Model = new TraineeAttendanceVM();
            try
            {

                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         join tA in db.TraineeAttendances
                         on tE.ID equals tA.TraineeEvaluationId
                         
                         where t.FileNo != null && tA.ID==ID 

                         select new
                         {
                             ID = tA.ID,
                             TraineeId = t.ID,
                             TraineeEvaluationId = tE.ID,
                             AttendanceDate = tA.AttendanceDate,
                             ArName=t.ArName,
                             EnName=t.EnName,
                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,

                             //practicalOrVisual =tA.PracticalOrVisual,

                             CourseStartDate = c.CourseStartDate,
                             CourseEndDate = c.CourseEndDate,

                         }).AsEnumerable()
                            .Select(x => new TraineeAttendanceVM
                            {
                                ID = x.ID,
                                TraineeId = x.TraineeId,
                                TraineeEvaluationId = x.TraineeEvaluationId,
                                AttendanceDate = String.Format("{0:MM/dd/yyyy}", x.AttendanceDate),
                               ArName=x.ArName,
                               EnName=x.EnName,

                                VisualStudyCount = x.VisualStudyCount,
                                PracticalCount = x.PracticalCount,
                                // practicalOrVisual = x.practicalOrVisual,

                                CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                                CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),
                            }).FirstOrDefault();



            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }





        public string Save()
        {

            List<TraineeAttendanceVM> Model = new List<TraineeAttendanceVM>();
            try
            {

                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         join tA in db.TraineeAttendances
                         on tE.ID equals tA.TraineeEvaluationId

                         where t.FileNo != null

                         select new
                         {
                             ID = tA.ID,
                             TraineeId = t.ID,
                             TraineeEvaluationId = tE.ID,
                             AttendanceDate = c.CourseStartDate,
                             //Practicaldays = c.CourseStartDate,
                             //Visualdays = c.CourseStartDate,

                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,

                             PracticalOrVisual = tA.PracticalOrVisual,


                             CourseStartDate = c.CourseStartDate,
                             CourseEndDate = c.CourseEndDate,

                         }).AsEnumerable()
                          .Select(x => new TraineeAttendanceVM
                          {
                              ID = x.ID,
                              TraineeId = x.TraineeId,
                              TraineeEvaluationId = x.TraineeEvaluationId,
                              AttendanceDate = String.Format("{0:MM/dd/yyyy}", x.AttendanceDate),
                              //Practicaldays = String.Format("{0:MM/dd/yyyy}", x.AttendanceDate),
                              //Visualdays = String.Format("{0:MM/dd/yyyy}", x.AttendanceDate),
                              // DaysWork="",

                              VisualStudyCount = x.VisualStudyCount,
                              PracticalCount = x.PracticalCount,
                              // practicalOrVisual = x.practicalOrVisual,
                              PracticalOrVisual = x.PracticalOrVisual == 1 ? "عملى" : "نظرى",
                              PracticalOrVisualEn = x.PracticalOrVisual == 1 ? "Practical" : "Visual",

                              CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                              CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),
                          }).ToList();


                var ModelAtt = (from t in db.Trainees
                                join tE in db.TraineeEvaluations
                                on t.ID equals tE.TraineeId
                                join s in db.StudyPeriodSettings
                                on tE.StudyPeriodSettingId equals s.ID
                                join c in db.CourseReservations
                                on tE.ID equals c.TraineeEvaluationId


                                where t.FileNo != null
                                select new
                                {
                                    TraineeId = t.ID,
                                    TraineeEvaluationId = tE.ID,

                                    AttendanceDate = c.CourseStartDate,
                                    //Practicaldays = c.CourseStartDate,
                                    //Visualdays = c.CourseStartDate,

                                    VisualStudyCount = s.VisualStudyCount,
                                    PracticalCount = s.PracticalCount,

                                    //practicalOrVisual =tA.PracticalOrVisual,

                                    CourseStartDate = c.CourseStartDate,
                                    CourseEndDate = c.CourseEndDate,



                                }).AsEnumerable()
                                .Select(x => new TraineeAttendanceVM
                                {
                                    TraineeId = x.TraineeId,
                                    TraineeEvaluationId = x.TraineeEvaluationId,
                                    AttendanceDate = String.Format("{0:MM/dd/yyyy}", ""),
                                    //Practicaldays = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                                    // Visualdays = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                                    // DaysWork="",

                                    VisualStudyCount = x.VisualStudyCount,
                                    PracticalCount = x.PracticalCount,
                                    // practicalOrVisual = x.practicalOrVisual,



                                    CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                                    CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),

                                }).FirstOrDefault();

                //DateTime _AttendanceDate = new DateTime();
                //if (ModelAtt.AttendanceDate!="") {
                // _AttendanceDate = DateTime.ParseExact(ModelAtt.AttendanceDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //}
                //else { 
                var _AttendanceDate = DateTime.ParseExact(ModelAtt.CourseStartDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var _CourseEndDate = DateTime.ParseExact(ModelAtt.CourseEndDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                //}
                var _WorkDay = db.WorkTimesSettings.ToList();


                var PVCount = ModelAtt.PracticalCount + ModelAtt.VisualStudyCount;

                int fAll = db.TraineeAttendances.Where(a => a.TraineeEvaluationId == ModelAtt.TraineeEvaluationId && a.TraineeId == ModelAtt.TraineeId).Count();

                int V = 1; int P = 1;
                //while (_AttendanceDate.Date <= _CourseEndDate.Date)
                //{


                    
                        //var loopDayofWeek2 = db.WorkTimesSettings.ToList();
                        //foreach (var item in loopDayofWeek)
                        //{


                        DateTime startDate = _AttendanceDate.Date;
                        DateTime endDate = _CourseEndDate.Date;
                        for (DateTime _date = startDate; _date <= endDate; _date = _date.AddDays(1))
                        {


                        TraineeAttendance TraineeAttendance_Obj = new TraineeAttendance();

                        var countM = db.TraineeAttendances.Where(a => a.TraineeEvaluationId == ModelAtt.TraineeEvaluationId && a.TraineeId == ModelAtt.TraineeId).ToList();
                        var li = 0;
                        if (countM.Count() <= PVCount)
                        {

                            var loopDayofWeek = db.WorkTimesSettings.ToList();
                            foreach (var item in loopDayofWeek)
                            {



                                var c = _date.DayOfWeek.ToString();

                                if (item.DisplayDayNameEn == c)
                                {

                                    //ModelAtt.AttendanceDate = String.Format("{0:MM/dd/yyyy}", _AttendanceDate);
                                    TraineeAttendance_Obj.AttendanceDate = _date;


                                    TraineeAttendance_Obj.TraineeId = ModelAtt.TraineeId;
                                    TraineeAttendance_Obj.TraineeEvaluationId = ModelAtt.TraineeEvaluationId;

                                    if (countM.Where(a => a.PracticalOrVisual == 2).Count() < ModelAtt.VisualStudyCount)
                                    {
                                        TraineeAttendance_Obj.PracticalOrVisual = 2;
                                        db.TraineeAttendances.Add(TraineeAttendance_Obj);
                                        db.SaveChanges();
                                        //ModelAtt.PracticalOrVisual = PracticalOrVisual.VisualStudy.ToString();
                                        //V++;
                                        //fAll++;
                                        //li++;
                                    }
                                    if (countM.Where(a => a.PracticalOrVisual == 1).Count() < ModelAtt.PracticalCount)
                                    {
                                        TraineeAttendance_Obj.PracticalOrVisual = 1;
                                        db.TraineeAttendances.Add(TraineeAttendance_Obj);
                                        db.SaveChanges();
                                        //ModelAtt.PracticalOrVisual = PracticalOrVisual.PracticalStudy.ToString();
                                        // P++;
                                        //fAll++;
                                        //li++;
                                    }
                                    //fAll++;
                                    //Model.Add(ModelAtt);
                                   // _AttendanceDate = _AttendanceDate.AddDays(1);

                                }
                                else
                                {
                                  //  _AttendanceDate = _AttendanceDate.AddDays(1);
                                }
                            }

                        }


                    }



               // }


                return "";



            }
            catch (Exception ex)
            {
                return "";
            }
           
        }

        public string Edit(TraineeAttendanceVM TraineeAttendanceVM_Obj)
        {
            var _AttendanceDate_T = DateTime.ParseExact(TraineeAttendanceVM_Obj.AttendanceDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
           // TraineeAttendance_Obj.AttendanceDate = _AttendanceDate_T;
            //DateTime.ParseExact(.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
            int P_or_V = 1;
            if (TraineeAttendanceVM_Obj.PracticalOrVisual == "Practical" || TraineeAttendanceVM_Obj.PracticalOrVisual == "عملى")
            {
                P_or_V = 1;
            }
            else {
                P_or_V = 2;
            }


            var found = db.TraineeAttendances.FirstOrDefault(x => x.TraineeEvaluationId == TraineeAttendanceVM_Obj.TraineeEvaluationId && x.TraineeId == TraineeAttendanceVM_Obj.TraineeId&& x.PracticalOrVisual== P_or_V && x.AttendanceDate== _AttendanceDate_T && x.ID!= TraineeAttendanceVM_Obj.ID);

            if (found != null)
                return Messages.NameAlreadyExist;
            TraineeAttendance TraineeAttendance_Obj = db.TraineeAttendances.Find(TraineeAttendanceVM_Obj.ID);


            var _AttendanceDate = DateTime.ParseExact(TraineeAttendanceVM_Obj.AttendanceDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TraineeAttendance_Obj.AttendanceDate = _AttendanceDate;
            //TraineeAttendance_Obj.PracticalOrVisual = 1;

            db.Entry(TraineeAttendance_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        
        public string delete(int ID)
        {
            TraineeAttendance TraineeAttendance_Obj = db.TraineeAttendances.Find(ID);
            db.TraineeAttendances.Remove(TraineeAttendance_Obj);
            db.SaveChanges();
            return Messages.DeleteSucc;
        }

      
    }
}
