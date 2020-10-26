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
    public class TraineeTestResultBLL
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
                         where t.FileNo != null

                         select new TraineeVM()
                         {

                             ID = t.ID,
                             TraineeGuid = t.TraineeGuid,
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

        public List<TestVM> GetAllTest()
        {
            List<TestVM> Model = new List<TestVM>();
            try
            {
                Model = (from t in db.Test

                         select new TestVM()
                         {

                             ID = t.ID,
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

        public List<TestResultVM> GetAllTestResult()
        {
            List<TestResultVM> Model = new List<TestResultVM>();
            try
            {
                Model = (from t in db.TestResult

                         select new TestResultVM()
                         {
                             ID = t.ID,
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

        public TraineeTestResultVM GetbyGuid(Guid TraineeGuid)
        {
            TraineeTestResultVM Model = new TraineeTestResultVM();
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
                             TraineeGuid = t.TraineeGuid,
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

                            

                         }).AsEnumerable()
                           .Select(x => new TraineeTestResultVM
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
                               VisualStudyCount = x.VisualStudyCount,
                               PracticalCount = x.PracticalCount,
                                // practicalOrVisual = x.practicalOrVisual,


                               
                           }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Model = null;
            }

            return Model;
        }



        //-----------------------------------------------------------------
        public List<TraineeTestResultVM> Getall(int _TraineeId)
        {
            List<TraineeTestResultVM> Model = new List<TraineeTestResultVM>();
            try
            {

                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId

                         join TTR in db.TraineeTestResult
                         on tE.ID equals TTR.TraineeEvaluationId

                         where t.FileNo != null && tE.TraineeId == _TraineeId

                         select new
                         {
                             ID = TTR.ID,
                             TraineeId = TTR.TraineeId,
                             TraineeEvaluationId = TTR.TraineeEvaluationId,
                             TestDate = TTR.TestDate,
                             TestId = TTR.TestId,
                             TestResultId = TTR.TestResultId,

                             ArName = t.ArName,
                             EnName = t.EnName,

                             ArTestId=TTR.Test.ArName,
                             EnTestId = TTR.Test.EnName,

                             ArTestResultId = TTR.TestResult.ArName,
                             EnTestResultId = TTR.TestResult.EnName,

                             Day=TTR.TestDate
                         }).AsEnumerable()
                          .Select(x => new TraineeTestResultVM
                          {
                              ID = x.ID,
                              TraineeId = x.TraineeId,
                              TraineeEvaluationId = x.TraineeEvaluationId,
                              TestDate = String.Format("{0:dd/mm/yyyy}", x.TestDate),
                              TestId = x.TestId,
                              TestResultId = x.TestResultId,

                              ArName = x.ArName,
                              EnName = x.EnName,

                              ArTestId =x.ArTestId,
                              EnTestId = x.EnTestId,

                              ArTestResultId = x.ArTestResultId,
                              EnTestResultId =x.EnTestResultId,

                              ArDay = x.Day.ToString("dddd", new System.Globalization.CultureInfo("ar-AE")),
                              EnDay = x.Day.ToString("dddd", new System.Globalization.CultureInfo("En-Us"))
                          }).ToList();
                return Model;
            }
            catch (Exception ex)
            {
                return Model = null;
            }
        }



        public TraineeTestResultVM Get(int ID)
        {
            TraineeTestResultVM Model = new TraineeTestResultVM();
            try
            {


                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId

                         join TTR in db.TraineeTestResult
                         on tE.ID equals TTR.TraineeEvaluationId

                         where t.FileNo != null && TTR.ID == ID

                         select new
                         {
                             ID = TTR.ID,
                             TraineeId = TTR.TraineeId,
                             TraineeEvaluationId = TTR.TraineeEvaluationId,
                             TestDate = TTR.TestDate,
                             TestId = TTR.TestId,
                             TestResultId = TTR.TestResultId,

                             ArName = t.ArName,
                             EnName = t.EnName,



                         }).AsEnumerable()
                          .Select(x => new TraineeTestResultVM
                          {
                              ID = x.ID,
                              TraineeId = x.TraineeId,
                              TraineeEvaluationId = x.TraineeEvaluationId,
                              TestDate = String.Format("{0:dd/mm/yyyy}", x.TestDate),
                              TestId = x.TestId,
                              TestResultId = x.TestResultId,

                              ArName = x.ArName,
                              EnName = x.EnName,

                          }).FirstOrDefault();


            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }





        public string Create(TraineeTestResultVM TraineeTestResultVM_Obj)
        {
            var _TestDate = DateTime.ParseExact(TraineeTestResultVM_Obj.TestDate.ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);


            var objFound = db.TraineeTestResult.Any(x => x.TestId == TraineeTestResultVM_Obj.TestId );

            if (objFound != false)
                return Messages.TestAlreadyExist;

            TraineeTestResult TraineeTestResult_Obj = new TraineeTestResult();

            TraineeTestResult_Obj.TraineeId = TraineeTestResultVM_Obj.TraineeId;
            TraineeTestResult_Obj.TraineeEvaluationId = TraineeTestResultVM_Obj.TraineeEvaluationId;
            TraineeTestResult_Obj.TestDate = _TestDate;
            TraineeTestResult_Obj.TestId = TraineeTestResultVM_Obj.TestId;
            TraineeTestResult_Obj.TestResultId = TraineeTestResultVM_Obj.TestResultId;

            db.TraineeTestResult.Add(TraineeTestResult_Obj);
            db.SaveChanges();
            return "";
        }

        public string Edit(TraineeTestResultVM TraineeTestResultVM_Obj)
        {
            var _TestDate = DateTime.ParseExact(TraineeTestResultVM_Obj.TestDate.ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);


            var objFound = db.TraineeTestResult.Any(x => x.TestId == TraineeTestResultVM_Obj.TestId && x.ID != TraineeTestResultVM_Obj.ID);

            if (objFound != false)
                return Messages.TestAlreadyExist;




            TraineeTestResult TraineeTestResult_Obj = db.TraineeTestResult.FirstOrDefault(x => x.ID == TraineeTestResultVM_Obj.ID);
            TraineeTestResult_Obj.ID = TraineeTestResultVM_Obj.ID;

            TraineeTestResult_Obj.TraineeId = TraineeTestResultVM_Obj.TraineeId;

            TraineeTestResult_Obj.TraineeEvaluationId = TraineeTestResultVM_Obj.TraineeEvaluationId;
            TraineeTestResult_Obj.TestDate = _TestDate;

            TraineeTestResult_Obj.TestId = TraineeTestResultVM_Obj.TestId;
            TraineeTestResult_Obj.TestResultId = TraineeTestResultVM_Obj.TestResultId;

            db.Entry(TraineeTestResult_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }


        public string delete(int ID)
        {
            TraineeTestResult TraineeTestResult_Obj = db.TraineeTestResult.Find(ID);
            db.TraineeTestResult.Remove(TraineeTestResult_Obj);
            db.SaveChanges();
            return Messages.DeleteSucc;
        }


    }
}
