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
    public class TraineeAttendingFollowupBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public List<TraineeVM> GetAllTrainee()
        {
            List<TraineeVM> Model = new List<TraineeVM>();
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

                         select new TraineeVM()
                         {

                             ID = t.ID,
                             TraineeGuid=t.TraineeGuid,
                             ArName = t.ArName,
                             EnName = t.EnName,


                         }).Distinct().ToList();


            }
            catch
            {
                Model = null;
            }

            return Model;
        }


        public List<TraineeAttendanceVM> GetAllTraineeAttendances()
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

                             ArTraineeAttendance =  tA.AttendanceDate,
                             EnTraineeAttendance =  tA.AttendanceDate,
                             ArPracticalOrVisual = tA.PracticalOrVisual,
                             EnPracticalOrVisual = tA.PracticalOrVisual,

                         }).AsEnumerable()
                           .Select(x => new TraineeAttendanceVM
                           {
                               ID = x.ID,
                               ArTraineeAttendance = String.Format("{0:MM/dd/yyyy}",x.ArTraineeAttendance) + " - "+(x.ArPracticalOrVisual == 1 ? "عملى" : "نظرى"),
                               EnTraineeAttendance = String.Format("{0:MM/dd/yyyy}", x.ArTraineeAttendance) + " - "+(x.EnPracticalOrVisual == 1 ? "Practical" : "Visual"),

                           }).ToList();
            }

            catch
            {
                Model = null;
            }

            return Model;
        }



        public TraineeAttendingFollowupVM GetbyId(Guid TraineeGuid)
        {
            TraineeAttendingFollowupVM Model = new TraineeAttendingFollowupVM();
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

                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,

                             //practicalOrVisual =tA.PracticalOrVisual,

                             CourseStartDate = c.CourseStartDate,
                             CourseEndDate = c.CourseEndDate,

                         }).AsEnumerable()
                           .Select(x => new TraineeAttendingFollowupVM
                           {
                                //ID = x.ID,
                                TraineeId = x.TraineeId,
                               TraineeGuid=x.TraineeGuid,
                               TraineeEvaluationId = x.TraineeEvaluationId,
                                //AttendanceDate = x.AttendanceDate,

                                ArName = x.ArName,
                               EnName = x.EnName,
                               Code = x.Code,
                               FileNo = x.FileNo,

                               StudyPeriodSettingName = x.StudyPeriodSettingName,
                               StudyPeriodSettingEnName = x.StudyPeriodSettingEnName,
                               VisualStudyCount = x.VisualStudyCount,
                               PracticalCount = x.PracticalCount,
                                // practicalOrVisual = x.practicalOrVisual,


                                CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                               CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),
                           }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Model = null;
            }

            return Model;
        }


        //----------------------------------------------------------------------
        public List<TraineeAttendingFollowupVM> Getall(int _TraineeId)
        {


            var Model = new List<TraineeAttendingFollowupVM>();
            try
            {
                Model =
                (from t in db.Trainees
                 join tE in db.TraineeEvaluations
                 on t.ID equals tE.TraineeId
                 join s in db.StudyPeriodSettings
                 on tE.StudyPeriodSettingId equals s.ID
                 join c in db.CourseReservations
                 on tE.ID equals c.TraineeEvaluationId
                 join tA in db.TraineeAttendances
                 on tE.ID equals tA.TraineeEvaluationId
                 join tAF in db.TraineeAttendingFollowups
                 on tA.ID equals tAF.TraineeAttendanceId

                 where t.FileNo != null && tA.TraineeId== _TraineeId

                  select new 
                 {

                     ID = tAF.ID,

                     TraineeAttendanceId = tAF.TraineeAttendanceId,
                     Day_ofWeek= tA.AttendanceDate,

                     AttendanceDatetime = tAF.AttendanceDatetime,
                     LeaveDateTime = tAF.LeaveDateTime,
                     ArTraineeAttendance = tA.AttendanceDate,
                     EnTraineeAttendance = tA.AttendanceDate,
                     ArPracticalOrVisual = tA.PracticalOrVisual,
                     EnPracticalOrVisual = tA.PracticalOrVisual,


                 }).AsEnumerable()
                           .Select(x => new TraineeAttendingFollowupVM
                           {
                               ID = x.ID,
                               TraineeAttendanceId=x.TraineeAttendanceId,
                               Day_ofWeek=x.Day_ofWeek.DayOfWeek.ToString(),


                               ArTraineeAttendance = x.ArTraineeAttendance.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - " + (x.ArPracticalOrVisual == 1 ? "عملى" : "نظرى"),
                               EnTraineeAttendance = x.EnTraineeAttendance.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - " + (x.EnPracticalOrVisual == 1 ? "Practical" : "Visual"),

                               AttendanceDatetime = x.AttendanceDatetime.ToString("dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                               LeaveDateTime = x.LeaveDateTime.ToString("dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)

                           }).ToList();

                //DateTime currentTime = DateTime.Now;
                //DateTime x30MinsLater = currentTime.AddMinutes(30);
                //Console.WriteLine(string.Format("{0} {1}", currentTime, x30MinsLater));
            }
            catch
            {
                Model = null;
            }

            return Model;
        }


     
        public TraineeAttendingFollowupVM Get(int ID)
        {
            TraineeAttendingFollowupVM Model = new TraineeAttendingFollowupVM();
            try
            {
                Model = 
                (from t in db.Trainees
                 join tE in db.TraineeEvaluations
                 on t.ID equals tE.TraineeId
                 join s in db.StudyPeriodSettings
                 on tE.StudyPeriodSettingId equals s.ID
                 join c in db.CourseReservations
                 on tE.ID equals c.TraineeEvaluationId
                 join tA in db.TraineeAttendances
                 on tE.ID equals tA.TraineeEvaluationId
                 join tAF in db.TraineeAttendingFollowups
                 on tA.ID equals tAF.TraineeAttendanceId

                 where t.FileNo != null && tAF.ID==ID

                 select new
                 {

                     ID = tAF.ID,

                     TraineeAttendanceId = tAF.TraineeAttendanceId,
                     Day_ofWeek = tA.AttendanceDate,

                     AttendanceDatetime = tAF.AttendanceDatetime,
                     LeaveDateTime = tAF.LeaveDateTime,
                     ArTraineeAttendance = tA.AttendanceDate,
                     EnTraineeAttendance = tA.AttendanceDate,
                     ArPracticalOrVisual = tA.PracticalOrVisual,
                     EnPracticalOrVisual = tA.PracticalOrVisual,


                 }).AsEnumerable()
                           .Select(x => new TraineeAttendingFollowupVM
                           {
                               ID = x.ID,
                               TraineeAttendanceId = x.TraineeAttendanceId,
                               Day_ofWeek = x.Day_ofWeek.DayOfWeek.ToString(),


                               ArTraineeAttendance = x.ArTraineeAttendance.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - " + (x.ArPracticalOrVisual == 1 ? "عملى" : "نظرى"),
                               EnTraineeAttendance = x.EnTraineeAttendance.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - " + (x.EnPracticalOrVisual == 1 ? "Practical" : "Visual"),

                               AttendanceDatetime = x.AttendanceDatetime.ToString("dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                               LeaveDateTime = x.LeaveDateTime.ToString("dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture)

                           }).FirstOrDefault();

           
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }






        public string Save(TraineeAttendingFollowupVM TraineeAttendingFollowupVM_Obj)
        {
            var AttID = db.TraineeAttendingFollowups.FirstOrDefault(x => x.TraineeAttendanceId == TraineeAttendingFollowupVM_Obj.TraineeAttendanceId );
            //var name = db.TraineeAttendingFollowups.FirstOrDefault(x => x.Name == TraineeAttendingFollowupVM_Obj.ArTraineeAttendance);
            if (AttID != null)
                return Messages.NameAlreadyExist;
            TraineeAttendingFollowup TraineeAttendingFollowup_Obj = new TraineeAttendingFollowup();
            TraineeAttendingFollowup_Obj.TraineeAttendanceId = TraineeAttendingFollowupVM_Obj.TraineeAttendanceId;


            var _AttendanceDatetime = DateTime.ParseExact(TraineeAttendingFollowupVM_Obj.AttendanceDatetime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
            TraineeAttendingFollowup_Obj.AttendanceDatetime = _AttendanceDatetime;

            var _LeaveDateTime = DateTime.ParseExact(TraineeAttendingFollowupVM_Obj.LeaveDateTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
            TraineeAttendingFollowup_Obj.LeaveDateTime = _LeaveDateTime;

            db.TraineeAttendingFollowups.Add(TraineeAttendingFollowup_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }

        public string Edit(TraineeAttendingFollowupVM TraineeAttendingFollowupVM_Obj)
        {
            var AttID = db.TraineeAttendingFollowups.FirstOrDefault(x => x.TraineeAttendanceId == TraineeAttendingFollowupVM_Obj.TraineeAttendanceId && x.ID!= TraineeAttendingFollowupVM_Obj.ID);
            //var name = db.TraineeAttendingFollowups.FirstOrDefault(x => x.Name == TraineeAttendingFollowupVM_Obj.ArTraineeAttendance);
            if (AttID != null)
                return Messages.NameAlreadyExist;
            TraineeAttendingFollowup TraineeAttendingFollowup_Obj = db.TraineeAttendingFollowups.Where(a=>a.ID== TraineeAttendingFollowupVM_Obj.ID).FirstOrDefault();
            TraineeAttendingFollowup_Obj.TraineeAttendanceId = TraineeAttendingFollowupVM_Obj.TraineeAttendanceId;


            var _AttendanceDatetime = DateTime.ParseExact(TraineeAttendingFollowupVM_Obj.AttendanceDatetime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
            TraineeAttendingFollowup_Obj.AttendanceDatetime = _AttendanceDatetime;

            var _LeaveDateTime = DateTime.ParseExact(TraineeAttendingFollowupVM_Obj.LeaveDateTime, "yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
            TraineeAttendingFollowup_Obj.LeaveDateTime = _LeaveDateTime;

            db.Entry(TraineeAttendingFollowup_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }

        
        public string delete(int ID)
        {
            TraineeAttendingFollowup TraineeAttendingFollowup_Obj = db.TraineeAttendingFollowups.Find(ID);
            db.TraineeAttendingFollowups.Remove(TraineeAttendingFollowup_Obj);
            db.SaveChanges();
            return Messages.DeleteSucc;
        }




}
}
