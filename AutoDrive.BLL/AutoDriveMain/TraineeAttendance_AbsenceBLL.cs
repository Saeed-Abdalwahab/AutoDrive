using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveMainViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.AutoDriveMain
{
    public class TraineeAttendance_AbsenceBLL
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
                             TraineeGuid= t.TraineeGuid,
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

        public List<TraineeAttendingFollowupVM> Getall_byID(int traineeId)
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
                 //join tAF in db.TraineeAttendingFollowups
                 //on tA.ID equals tAF.TraineeAttendanceId

                 where t.FileNo != null && t.ID == traineeId
                // &&  db.TraineeAttendingFollowups.Any(f => f.TraineeAttendanceId == tA.ID)
               
                 select new
                 {

                     ID = tA.ID,
                     TraineeId=t.ID,
                     Day_ofWeek = tA.AttendanceDate,

                  
                     ArTraineeAttendance = tA.AttendanceDate,
                     EnTraineeAttendance = tA.AttendanceDate,
                     ArPracticalOrVisual = tA.PracticalOrVisual,
                     EnPracticalOrVisual = tA.PracticalOrVisual,

                     EnAttendanceOrAbsence = db.TraineeAttendingFollowups.Any(f => f.TraineeAttendanceId == tA.ID)==true?"Attendance":"Absence",
                     ArAttendanceOrAbsence = db.TraineeAttendingFollowups.Any(f => f.TraineeAttendanceId == tA.ID) == true ? "حضور" : "غياب",

                 }).AsEnumerable()
                           .Select(x => new TraineeAttendingFollowupVM
                           {
                               ID = x.ID,
                               TraineeId=x.TraineeId,
                               Day_ofWeek = x.Day_ofWeek.DayOfWeek.ToString(),


                               ArTraineeAttendance = x.ArTraineeAttendance.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - " + (x.ArPracticalOrVisual == 1 ? "عملى" : "نظرى"),
                               EnTraineeAttendance = x.EnTraineeAttendance.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " - " + (x.EnPracticalOrVisual == 1 ? "Practical" : "Visual"),

                               EnAttendanceOrAbsence = x.EnAttendanceOrAbsence,
                               ArAttendanceOrAbsence= x.ArAttendanceOrAbsence,
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
    }
}
