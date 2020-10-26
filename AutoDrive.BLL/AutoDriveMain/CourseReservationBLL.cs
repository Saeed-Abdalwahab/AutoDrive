using AutoDrive.DAL.AutoDriveDB;
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
   public class CourseReservationBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

    



        #region Get CourseReservation
        public CourseReservationVM GetCourseReservation(CourseReservationVM CourseReservation_VMObj)
        {
            CourseReservationVM Model = new CourseReservationVM();
            try
            {
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         where t.ID == CourseReservation_VMObj.TraineeId
                         select new
                         {
                             ID = c.ID,
                             TraineeId = t.ID,
                             CodeId = t.ID,
                             TraineeEvaluationId = tE.ID,
                             ArName = t.ArName,
                             EnName = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,

                             LicenceTypeName = tE.LicenceType.Name,
                             LicenceTypeEnName = tE.LicenceType.EnName,
                             LicenceCategoryName = tE.LicenceCategory.Name,
                             LicenceCategoryEnName = tE.LicenceCategory.EnName,

                             StudyPeriodSettingName = s.DriveLevel.Name,
                             StudyPeriodSettingEnName = s.DriveLevel.EnName,

                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,

                             CourseStartDate = c.CourseStartDate,
                             CourseEndDate = c.CourseEndDate,
                             CourseCost = c.CourseCost

                         }).AsEnumerable().Select(x => new CourseReservationVM
                         {
                             ID = x.ID,
                             TraineeId = x.TraineeId,
                             CodeId = x.CodeId,
                             TraineeEvaluationId = x.TraineeEvaluationId,
                             ArName = x.ArName,
                             EnName = x.EnName,
                             Code = x.Code,
                             FileNo =x.FileNo,

                             LicenceTypeName =x.LicenceTypeName,
                             LicenceTypeEnName = x.LicenceTypeEnName,
                             LicenceCategoryName = x.LicenceCategoryName,
                             LicenceCategoryEnName = x.LicenceCategoryEnName,

                             StudyPeriodSettingName = x.StudyPeriodSettingName,
                             StudyPeriodSettingEnName = x.StudyPeriodSettingEnName,

                             VisualStudyCount = x.VisualStudyCount,
                             PracticalCount = x.PracticalCount,

                             CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                             CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),
                            
                         }).FirstOrDefault();



            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        #endregion

        #region Get CourseReservation By ID
        public CourseReservationVM GetByID(int id)
        {
            CourseReservationVM Model = new CourseReservationVM();
            try
            {

              

                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         where c.ID == id
                         select new
                         {
                             ID = c.ID,
                             TraineeId = t.ID,
                             CodeId = t.ID,
                             TraineeEvaluationId = tE.ID,
                             ArName = t.ArName,
                             EnName = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,

                             LicenceTypeName = tE.LicenceType.Name,
                             LicenceTypeEnName = tE.LicenceType.EnName,
                             LicenceCategoryName = tE.LicenceCategory.Name,
                             LicenceCategoryEnName = tE.LicenceCategory.EnName,

                             StudyPeriodSettingName = s.DriveLevel.Name,
                             StudyPeriodSettingEnName = s.DriveLevel.EnName,

                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,

                             CourseStartDate = c.CourseStartDate,
                             CourseEndDate = c.CourseEndDate,
                             CourseCost = c.CourseCost

                         }).AsEnumerable().Select(x => new CourseReservationVM
                         {
                             ID = x.ID,
                             TraineeId = x.TraineeId,
                             CodeId = x.CodeId,
                             TraineeEvaluationId = x.TraineeEvaluationId,
                             ArName = x.ArName,
                             EnName = x.EnName,
                             Code = x.Code,
                             FileNo = x.FileNo,

                             LicenceTypeName = x.LicenceTypeName,
                             LicenceTypeEnName = x.LicenceTypeEnName,
                             LicenceCategoryName = x.LicenceCategoryName,
                             LicenceCategoryEnName = x.LicenceCategoryEnName,

                             StudyPeriodSettingName = x.StudyPeriodSettingName,
                             StudyPeriodSettingEnName = x.StudyPeriodSettingEnName,

                             VisualStudyCount = x.VisualStudyCount,
                             PracticalCount = x.PracticalCount,

                             CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                             CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),
                             CourseCost = x.CourseCost
                         }).FirstOrDefault();

            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        #endregion
        #region GetallTrainee
        public List<TraineeVM> GetallTrainee()
        {
            var Model = new List<TraineeVM>();
            try
            {
                
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                       
                         //on tE.ID equals co.TraineeEvaluationId
                         where t.FileNo!="" && !db.CourseReservations.Any(f=>f.TraineeEvaluationId==tE.ID)
                         select new TraineeVM()
                         {
                             ID = t.ID,
                             Code = t.Code

                         }).ToList();
               
            }
            catch
            {
                Model = null;
            }
            return Model;
        }
        #endregion

        #region SaveinDataBase
        public CourseReservationVM SaveinDataBase(CourseReservationVM courseReservationVM_Obj)
        {
            courseReservationVM_Obj.CourseReservation_Msg = "";
            try
            {

                //if (courseReservationVM_Obj.ID > 0)
                //{
                //    var Start_DateC = DateTime.ParseExact(courseReservationVM_Obj.CourseStartDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);

                //    var End_DateC = DateTime.ParseExact(courseReservationVM_Obj.CourseEndDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);

                //    var objFound = db.CourseReservations.Any(x => x.ID != courseReservationVM_Obj.ID && x.TraineeEvaluation.TraineeId == courseReservationVM_Obj.TraineeId && (x.CourseStartDate <= Start_DateC && x.CourseEndDate >= End_DateC));
                //    if (objFound == false)
                //    {
                //        CourseReservation courseReservation_Obj = db.CourseReservations.FirstOrDefault(x => x.ID == courseReservationVM_Obj.ID);

                //        courseReservation_Obj.TraineeEvaluationId = courseReservationVM_Obj.TraineeEvaluationId;

                //        var Start_Date = DateTime.ParseExact(courseReservationVM_Obj.CourseStartDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);
                //        courseReservation_Obj.CourseStartDate = Start_Date;

                //        var End_Date = DateTime.ParseExact(courseReservationVM_Obj.CourseEndDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);
                //        courseReservation_Obj.CourseEndDate = End_Date;

                //        courseReservation_Obj.CourseCost = courseReservationVM_Obj.CourseCost;


                //        db.Entry(courseReservation_Obj).State = System.Data.Entity.EntityState.Modified;
                //        db.SaveChanges();
                //        courseReservationVM_Obj.CourseReservation_Msg = "تم تعديل حجز دورة دراسيه";
                //        return courseReservationVM_Obj;
                //    }
                //    else
                //    {
                //        courseReservationVM_Obj.CourseReservation_Msg = "توجد دوره دراسيه لهذا المتدرب خلال هذه الفتره";
                //        return courseReservationVM_Obj;

                //    }
                //}
                //else
                //{
                    //var Start_DateC = DateTime.ParseExact(courseReservationVM_Obj.CourseStartDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);

                    //var End_DateC = DateTime.ParseExact(courseReservationVM_Obj.CourseEndDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);

                    var objFound = db.CourseReservations.Any(x => x.TraineeEvaluation.TraineeId == courseReservationVM_Obj.TraineeId && x.ID!=0);

                    if (objFound == false)
                    {
                        CourseReservation courseReservation_Obj = new CourseReservation();

                        courseReservation_Obj.TraineeEvaluationId = courseReservationVM_Obj.TraineeEvaluationId;

                        var Start_Date = DateTime.ParseExact(courseReservationVM_Obj.CourseStartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        courseReservation_Obj.CourseStartDate = Start_Date;

                    if(courseReservationVM_Obj.CourseEndDate!=null)
                    { 
                        var End_Date = DateTime.ParseExact(courseReservationVM_Obj.CourseEndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        courseReservation_Obj.CourseEndDate = End_Date;
                    }
                    courseReservation_Obj.CourseCost = courseReservationVM_Obj.CourseCost;


                        db.CourseReservations.Add(courseReservation_Obj);
                        db.SaveChanges();
                        courseReservationVM_Obj.ID = courseReservation_Obj.ID;
                        courseReservationVM_Obj.CourseReservation_Msg = "تم حجز دورة دراسيه";
                        return courseReservationVM_Obj;
                }
                else
                {
                    courseReservationVM_Obj.CourseReservation_Msg = "توجد دوره دراسيه لهذا المتدرب ";
                    return courseReservationVM_Obj;
                }
          //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        #region Delete
        public CourseReservationVM DeleteCourseReservationBLL(CourseReservationVM courseReservationVM_Obj)
        {
            try
            {
                courseReservationVM_Obj.CourseReservation_Msg = "";
                CourseReservation courseReservation_Obj = db.CourseReservations.FirstOrDefault(x => x.ID == courseReservationVM_Obj.ID);



                db.CourseReservations.Remove(courseReservation_Obj);
                db.SaveChanges();
                courseReservationVM_Obj.CourseReservation_Msg = "تم حذف هذه الدوره الدراسيه";
                return courseReservationVM_Obj;


            }
            catch (Exception ex)
            {
                courseReservationVM_Obj.CourseReservation_Msg = "لا يمكن حذف الملف لارتباطه ببيانات اخرى";
                return courseReservationVM_Obj;
            }
        }
        #endregion


        #region
        public CourseReservationVM GetCourseReservationByCodeBLL(int NCode)
        {

            db.Configuration.ProxyCreationEnabled = false;

             

            var Model = (from t in db.Trainees
                                  join tE in db.TraineeEvaluations
                                  on t.ID equals tE.TraineeId
                                  join s in db.StudyPeriodSettings
                                  on tE.StudyPeriodSettingId equals s.ID
                                  //join c in db.CourseReservations
                                  //on tE.ID equals c.TraineeEvaluationId
                                  where t.ID == NCode
                                  select new CourseReservationVM()
                                  {
                                      //ID = c.ID,
                                      TraineeId = t.ID,
                                      CodeId=t.ID,
                                      TraineeEvaluationId=tE.ID,
                                      ArName = t.ArName,
                                      EnName = t.EnName,
                                      Code = t.Code,
                                      FileNo = t.FileNo,

                                      LicenceTypeName = tE.LicenceType.Name,
                                      LicenceTypeEnName = tE.LicenceType.EnName,
                                      LicenceCategoryName = tE.LicenceCategory.Name,
                                      LicenceCategoryEnName = tE.LicenceCategory.EnName,

                                      StudyPeriodSettingName = s.DriveLevel.Name,
                                      StudyPeriodSettingEnName = s.DriveLevel.EnName,

                                      VisualStudyCount = s.VisualStudyCount,
                                      PracticalCount = s.PracticalCount,

                                      //CourseStartDate = c.CourseStartDate.ToString(),
                                      //CourseEndDate = c.CourseEndDate.ToString(),
                                      //CourseCost = c.CourseCost

                                  }).FirstOrDefault();

            return Model;
        }
        #endregion
        #region DataTable


        public List<CourseReservationVM> GetAllCourseReservationBLL()
        {
            List<CourseReservationVM> Model = new List<CourseReservationVM>();
            try
            {
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId

                         select new CourseReservationVM()
                         {
                             ID = c.ID,
                             TraineeId = t.ID,
                             TraineeEvaluationId = tE.ID,
                             ArName = t.ArName,
                             EnName = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,

                             LicenceTypeName = tE.LicenceType.Name,
                             LicenceTypeEnName = tE.LicenceType.EnName,
                             LicenceCategoryName = tE.LicenceCategory.Name,
                             LicenceCategoryEnName = tE.LicenceCategory.EnName,

                             StudyPeriodSettingName = s.DriveLevel.Name,
                             StudyPeriodSettingEnName = s.DriveLevel.EnName,

                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,

                             CourseStartDate = c.CourseStartDate.ToString(),
                             CourseEndDate = c.CourseEndDate.ToString(),
                             CourseCost = c.CourseCost

                         }).AsEnumerable().Select(x => new CourseReservationVM {

                             ID = x.ID,
                             TraineeId = x.TraineeId,
                             TraineeEvaluationId = x.TraineeEvaluationId,
                             ArName = x.ArName,
                             EnName = x.EnName,
                             Code = x.Code,
                             FileNo = x.FileNo,

                             LicenceTypeName = x.LicenceTypeName,
                             LicenceTypeEnName = x.LicenceTypeEnName,
                             LicenceCategoryName =x.LicenceCategoryName,
                             LicenceCategoryEnName = x.LicenceCategoryEnName,

                             StudyPeriodSettingName = x.StudyPeriodSettingName,
                             StudyPeriodSettingEnName = x.StudyPeriodSettingEnName,

                             VisualStudyCount = x.VisualStudyCount,
                             PracticalCount = x.PracticalCount,

                           
                             CourseStartDate = String.Format("{0:MM/dd/yyyy}", x.CourseStartDate),
                             CourseEndDate = String.Format("{0:MM/dd/yyyy}", x.CourseEndDate),
                             CourseCost =x.CourseCost
                         }).ToList();



                            
            }
            catch
            {
                Model = null;
            }

            return Model;
        }


        public bool SaveinDataBaseDataTable(CourseReservationVM courseReservationVM_Obj)
        {
            var result = false;

            try
            {
                if (courseReservationVM_Obj.ID > 0)
                {
                    var Start_DateC = DateTime.ParseExact(courseReservationVM_Obj.CourseStartDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);

                    var End_DateC = DateTime.ParseExact(courseReservationVM_Obj.CourseEndDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);

                    var objFound = db.CourseReservations.Any(x => x.ID != courseReservationVM_Obj.ID && x.TraineeEvaluation.TraineeId == courseReservationVM_Obj.TraineeId && (x.CourseStartDate <= Start_DateC && x.CourseEndDate >= End_DateC));
                    if (objFound == false)
                    {
                        CourseReservation courseReservation_Obj = db.CourseReservations.FirstOrDefault(x => x.ID == courseReservationVM_Obj.ID);

                        courseReservation_Obj.TraineeEvaluationId = courseReservationVM_Obj.TraineeEvaluationId;

                        var Start_Date = DateTime.ParseExact(courseReservationVM_Obj.CourseStartDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);
                        courseReservation_Obj.CourseStartDate = Start_Date;

                        var End_Date = DateTime.ParseExact(courseReservationVM_Obj.CourseEndDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);
                        courseReservation_Obj.CourseEndDate = End_Date;

                        courseReservation_Obj.CourseCost = courseReservationVM_Obj.CourseCost;


                        db.Entry(courseReservation_Obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        courseReservationVM_Obj.CourseReservation_Msg = "تم تعديل حجز دورة دراسيه";
                        //return courseReservationVM_Obj;
                        return true;
                    }
                    else
                    {
                        courseReservationVM_Obj.CourseReservation_Msg = "توجد دوره دراسيه لهذا المتدرب خلال هذه الفتره";
                        //return courseReservationVM_Obj;
                        return false;

                    }
                }
                else
                {
                    var Start_DateC = DateTime.ParseExact(courseReservationVM_Obj.CourseStartDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);

                    var End_DateC = DateTime.ParseExact(courseReservationVM_Obj.CourseEndDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);

                    var objFound = db.CourseReservations.Any(x => x.TraineeEvaluation.TraineeId == courseReservationVM_Obj.TraineeId && (x.CourseStartDate <= Start_DateC && x.CourseEndDate >= End_DateC));

                    if (objFound == false)
                    {
                        CourseReservation courseReservation_Obj = new CourseReservation();

                        courseReservation_Obj.TraineeEvaluationId = courseReservationVM_Obj.TraineeEvaluationId;

                        //var Start_Date = DateTime.ParseExact(courseReservationVM_Obj.CourseStartDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);
                        courseReservation_Obj.CourseStartDate = Start_DateC;

                        //var End_Date = DateTime.ParseExact(courseReservationVM_Obj.CourseEndDate.ToString(), "M/dd/yyyy", CultureInfo.InvariantCulture);
                        courseReservation_Obj.CourseEndDate = End_DateC;

                        courseReservation_Obj.CourseCost = courseReservationVM_Obj.CourseCost;


                        db.CourseReservations.Add(courseReservation_Obj);
                        db.SaveChanges();
                        courseReservationVM_Obj.ID = courseReservation_Obj.ID;
                        courseReservationVM_Obj.CourseReservation_Msg = "تم حجز دورة دراسيه";
                        // return courseReservationVM_Obj;
                        return true;
                    }
                    else
                    {
                        courseReservationVM_Obj.CourseReservation_Msg = "توجد دوره دراسيه لهذا المتدرب خلال هذه الفتره";
                        //return courseReservationVM_Obj;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public bool DeleteDataBaseDataTable(CourseReservationVM courseReservationVM_Obj)
        {
            try
            {
                CourseReservation courseReservation_Obj = db.CourseReservations.Find(courseReservationVM_Obj.ID);
                db.CourseReservations.Remove(courseReservation_Obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}
