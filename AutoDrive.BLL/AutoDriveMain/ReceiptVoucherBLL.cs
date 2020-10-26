using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
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
    public class ReceiptVoucherBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        #region GetallTrainee
        public List<TraineeVM> GetallTrainee()
        {
            var Model = new List<TraineeVM>();
            try
            {
                Model =
                (from t in db.Trainees
                 join tE in db.TraineeEvaluations
                 on t.ID equals tE.TraineeId
                 join c in db.CourseReservations
                 on tE.ID equals c.TraineeEvaluationId
                 select new TraineeVM()
                 {

                     ID = t.ID,
                     ArName = t.Code + "_" + t.ArName,
                     EnName = t.Code + "_" + t.EnName
                 }
                 ).ToList();

          
            }
            catch
            {
                Model = null;
            }
            return Model;
        }

        public List<EmployeeVM> GetallSafeEmployees()
        {
            var Model = new List<EmployeeVM>();
            try
            {
                Model = (from E in db.Employees
                         join EJ in db.EmployeeJobDatas
                         on E.ID equals EJ.EmployeeId
                         join JN in db.JobNames
                         on EJ.JobNameId equals JN.ID
                         where JN.ID == 36
                         //where JF.Name == "تقييم" && JN.Name == "مهندس"
                         select (new EmployeeVM()
                         {
                             ID = E.ID,
                             Name = E.Name,
                             EnName = E.EnName
                         })).ToList();

            }
            catch
            {
                Model = null;
            }
            return Model;
        }





        public List<CourseReservationVM> GetallCourseReservation()
        {
            var Model = new List<CourseReservationVM>();
            try
            {
                Model = db.CourseReservations.Select(x => new CourseReservationVM()
                {
                    ID = x.ID,
                    CourseCost = x.ID


                }).ToList();
            }
            catch
            {
                Model = null;
            }
            return Model;
        }
        #endregion
        #region Get ReceiptVoucher By ID
        //public ReceiptVoucherVM GetByID(int id)
        //{
        //    ReceiptVoucherVM Model = new ReceiptVoucherVM();
        //    try
        //    {
        //        Model = (from t in db.Trainees
        //                 join tE in db.TraineeEvaluations
        //                 on t.ID equals tE.TraineeId
        //                 join s in db.StudyPeriodSettings
        //                 on tE.StudyPeriodSettingId equals s.ID
        //                 join c in db.CourseReservations
        //                 on tE.ID equals c.TraineeEvaluationId
        //                 join r in db.ReceiptVouchers
        //                 on c.ID equals r.CourseReservationId
        //                 where t.ID == r.TraineeId
        //                 && r.ID==id
        //                 select new ReceiptVoucherVM()
        //                 {
        //                     ID = c.ID,
        //                     TraineeId = t.ID,
        //                     ArNameLevelDateCodeId = t.ArName + "_" + s.DriveLevel.Name + "_" + s.LevelTotal,
        //                     EnNameLevelDateCodeId = t.EnName + "_" + s.DriveLevel.EnName + "_" + s.LevelTotal,

        //                     TraineeEvaluationId = tE.ID,
        //                     ArName = t.ArName,
        //                     EnName = t.EnName,
        //                     Code = t.Code,
        //                     FileNo = t.FileNo,



        //                     StudyPeriodSettingName = s.DriveLevel.Name,
        //                     StudyPeriodSettingEnName = s.DriveLevel.EnName,
        //                     VisualStudyCount = s.VisualStudyCount,
        //                     PracticalCount = s.PracticalCount,
        //                     LevelTotal = s.LevelTotal,

        //                     PaidValue = r.PaidValue,
        //                     PaidDate = r.PaidDate.ToString(),
        //                     ReceiptVoucherCode = r.ReceiptVoucherCode,
        //                     ReceiptVoucherDate = r.ReceiptVoucherDate.ToString(),

        //                     CourseReservationId = r.CourseReservationId,

        //                     //هام EmployeeSafeId=

        //                 }).FirstOrDefault();



        //    }
        //    catch
        //    {
        //        Model = null;
        //    }

        //    return Model;
        //}
        #endregion

        #region CreateReceiptVoucher
        public ReceiptVoucherVM CreateReceiptVoucherBLL(ReceiptVoucherVM receiptVoucherVM_Obj)
        {
            receiptVoucherVM_Obj.ReceiptVoucher_Msg = "";
            try
            {

                

                var objFound = db.ReceiptVouchers.Any(x => x.TraineeId == receiptVoucherVM_Obj.TraineeId && x.ReceiptVoucherCode == receiptVoucherVM_Obj.ReceiptVoucherCode  && x.CourseReservationId == receiptVoucherVM_Obj.CourseReservationId);

                if (objFound == false)
                {
                    ReceiptVoucher receiptVoucher_Obj = new ReceiptVoucher();

                    receiptVoucher_Obj.PaidValue = receiptVoucherVM_Obj.PaidValue;
                    var _PaidDate = DateTime.ParseExact(receiptVoucherVM_Obj.PaidDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    receiptVoucher_Obj.PaidDate = _PaidDate;

                    receiptVoucher_Obj.ReceiptVoucherCode = receiptVoucherVM_Obj.ReceiptVoucherCode;
                    var _ReceiptVoucherDate = DateTime.ParseExact(receiptVoucherVM_Obj.ReceiptVoucherDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    receiptVoucher_Obj.ReceiptVoucherDate = _ReceiptVoucherDate;

                    receiptVoucher_Obj.TraineeId = receiptVoucherVM_Obj.TraineeId;
                    receiptVoucher_Obj.CourseReservationId = receiptVoucherVM_Obj.CourseReservationId;
                    receiptVoucher_Obj.EmployeeSafeId = receiptVoucherVM_Obj.EmployeeSafeId;

                    db.ReceiptVouchers.Add(receiptVoucher_Obj);
                    db.SaveChanges();

                    receiptVoucherVM_Obj.ReceiptVoucher_Msg = "تم اضافه قسط الدوره للمتدرب";
                    return receiptVoucherVM_Obj;
                }
                else
                {
                    receiptVoucherVM_Obj.ReceiptVoucher_Msg = "تم تسجيل هذا القسط للمتدرب قبل ذلك";
                    return receiptVoucherVM_Obj;
                }
                //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetReceiptVoucherByCodeBLL
        public ReceiptVoucherVM GetReceiptVoucherByCodeBLL(int ID)
        {
            db.Configuration.ProxyCreationEnabled = false;

            ReceiptVoucherVM Model = new ReceiptVoucherVM();
           
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         //join r in db.ReceiptVouchers
                         //on c.ID equals r.CourseReservationId
                         where t.ID == ID
                         //&& r.ID == ID
                         select new ReceiptVoucherVM()
                         {
                            // ID = r.ID,
                             TraineeId = t.ID,
                             ArNameLevelDateCodeId = t.ArName + "_" + s.DriveLevel.Name + "_" + s.LevelTotal,
                             EnNameLevelDateCodeId = t.EnName + "_" + s.DriveLevel.EnName + "_" + s.LevelTotal,

                             TraineeEvaluationId = tE.ID,
                             ArName = t.ArName,
                             EnName = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,

                             StudyPeriodSettingName = s.DriveLevel.Name,
                             StudyPeriodSettingEnName = s.DriveLevel.EnName,
                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,
                             LevelTotal = s.LevelTotal,

                             //PaidValue = r.PaidValue,
                             //PaidDate = r.PaidDate.ToString(),
                             //ReceiptVoucherCode = r.ReceiptVoucherCode,
                             //ReceiptVoucherDate = r.ReceiptVoucherDate.ToString(),

                             CourseReservationId = c.ID,

                             //هام EmployeeSafeId=

                         }).FirstOrDefault();
            
            return Model;
        }
        #endregion


        #region DataTable


        public List<ReceiptVoucherVM> Getall()
        {
            List<ReceiptVoucherVM> Model = new List<ReceiptVoucherVM>();
            try
            {
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         join r in db.ReceiptVouchers
                         on c.ID equals r.CourseReservationId
                         where t.ID == r.TraineeId

                         select new 
                         {
                             ID = r.ID,
                             TraineeId = t.ID,
                             ArNameLevelDateCodeId = t.ArName + "_" + s.DriveLevel.Name + "_" + s.LevelTotal,
                             EnNameLevelDateCodeId = t.EnName + "_" + s.DriveLevel.EnName + "_" + s.LevelTotal,

                             TraineeEvaluationId = tE.ID,
                             ArName = t.ArName,
                             EnName = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,



                             StudyPeriodSettingName = s.DriveLevel.Name,
                             StudyPeriodSettingEnName = s.DriveLevel.EnName,
                             VisualStudyCount = s.VisualStudyCount,
                             PracticalCount = s.PracticalCount,
                             LevelTotal = s.LevelTotal,

                             PaidValue = r.PaidValue,
                             PaidDate = r.PaidDate,
                             ReceiptVoucherCode = r.ReceiptVoucherCode,
                             ReceiptVoucherDate = r.ReceiptVoucherDate,

                             CourseReservationId = r.CourseReservationId,

                             EmployeeSafeId=r.EmployeeSafeId

                         }).AsEnumerable().Select(x=>new ReceiptVoucherVM() {
                             ID = x.ID,
                             TraineeId = x.TraineeId,
                             ArNameLevelDateCodeId =x.ArNameLevelDateCodeId,
                             EnNameLevelDateCodeId = x.EnNameLevelDateCodeId,

                             TraineeEvaluationId = x.TraineeEvaluationId,
                             ArName = x.ArName,
                             EnName = x.EnName,
                             Code = x.Code,
                             FileNo = x.FileNo,



                             StudyPeriodSettingName = x.StudyPeriodSettingName,
                             StudyPeriodSettingEnName = x.StudyPeriodSettingEnName,
                             VisualStudyCount = x.VisualStudyCount,
                             PracticalCount =x.PracticalCount,
                             LevelTotal = x.LevelTotal,

                             PaidValue =x.PaidValue,
                             PaidDate = String.Format("{0:MM/dd/yyyy}", x.PaidDate),
                        
                             ReceiptVoucherCode = x.ReceiptVoucherCode,
                             ReceiptVoucherDate = String.Format("{0:MM/dd/yyyy}", x.ReceiptVoucherDate),
                            

                             CourseReservationId =x.CourseReservationId,
                             EmployeeSafeId=x.EmployeeSafeId
                         }) .ToList();



            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        public ReceiptVoucherVM Get(int ID)
        {
            ReceiptVoucherVM Model = new ReceiptVoucherVM();
            try
            {
                Model = (from  r in db.ReceiptVouchers
                         join c in db.CourseReservations
                         on  r.CourseReservationId equals c.ID
                         join t in db.Trainees
                         on r.TraineeId equals t.ID
                         where r.ID==ID
                        

                         select new 
                         {
                          

                             ID = r.ID,
                             PaidValue = r.PaidValue,
                             PaidDate = r.PaidDate,
                             ReceiptVoucherCode = r.ReceiptVoucherCode,
                             ReceiptVoucherDate = r.ReceiptVoucherDate,
                             TraineeId=t.ID,
                             ArName=t.ArName,
                             EnName=t.EnName,
                             Code = t.Code,

                             CourseReservationId = r.CourseReservationId,

                             EmployeeSafeId=r.EmployeeSafeId

                         }).AsEnumerable().Select(x => new ReceiptVoucherVM() {

                             ID = x.ID,
                             PaidValue = x.PaidValue,
                             PaidDate = String.Format("{0:MM/dd/yyyy}", x.PaidDate),
                              
                             ReceiptVoucherCode = x.ReceiptVoucherCode,
                             ReceiptVoucherDate = String.Format("{0:MM/dd/yyyy}", x.ReceiptVoucherDate),
                             TraineeId = x.TraineeId,
                             ArName = x.ArName,
                             EnName = x.EnName,
                             Code = x.Code,
                             CourseReservationId = x.CourseReservationId,
                             EmployeeSafeId=x.EmployeeSafeId

                         }).FirstOrDefault();




            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        public string Edit(ReceiptVoucherVM ReceiptVoucherVM_Obj)
        {
            var objFound = db.ReceiptVouchers.FirstOrDefault(x => x.ID != ReceiptVoucherVM_Obj.ID && x.TraineeId == ReceiptVoucherVM_Obj.TraineeId && x.ReceiptVoucherCode == ReceiptVoucherVM_Obj.ReceiptVoucherCode && x.CourseReservationId == ReceiptVoucherVM_Obj.CourseReservationId&&x.EmployeeSafeId==ReceiptVoucherVM_Obj.EmployeeSafeId);

            //var Enname = db.JobNames.FirstOrDefault(x => x.EnName == jobNameVM_Obj.EnName && x.ID != jobNameVM_Obj.ID);
            //var name = db.JobNames.FirstOrDefault(x => x.Name == jobNameVM_Obj.Name && x.ID != jobNameVM_Obj.ID);
            if (objFound != null )
                return Messages.NameAlreadyExist;
            ReceiptVoucher ReceiptVoucher_Obj = db.ReceiptVouchers.FirstOrDefault(x => x.ID == ReceiptVoucherVM_Obj.ID);



            ReceiptVoucher_Obj.PaidValue = ReceiptVoucherVM_Obj.PaidValue;
            var _PaidDate = DateTime.ParseExact(ReceiptVoucherVM_Obj.PaidDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            ReceiptVoucher_Obj.PaidDate = _PaidDate;

            ReceiptVoucher_Obj.ReceiptVoucherCode = ReceiptVoucherVM_Obj.ReceiptVoucherCode;
            var _ReceiptVoucherDate = DateTime.ParseExact(ReceiptVoucherVM_Obj.ReceiptVoucherDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            ReceiptVoucher_Obj.ReceiptVoucherDate = _ReceiptVoucherDate;

            ReceiptVoucher_Obj.TraineeId = ReceiptVoucherVM_Obj.TraineeId;
            ReceiptVoucher_Obj.CourseReservationId = ReceiptVoucherVM_Obj.CourseReservationId;
            ReceiptVoucher_Obj.EmployeeSafeId = ReceiptVoucherVM_Obj.EmployeeSafeId;

            db.Entry(ReceiptVoucher_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

        public string delete(int ID)
        {
            ReceiptVoucher receiptVoucher_Obj = db.ReceiptVouchers.Find(ID);
            db.ReceiptVouchers.Remove(receiptVoucher_Obj);
            db.SaveChanges();
            
            return Messages.DeleteSucc;
        }
      

        #endregion
        //-----------------------------------------------------------------------------------
      

      


        #region SaveinDataBase

        public string Save(AutoDrive.VM.AutoDriveMainViewModels.JobNameVM jobNameVM_Obj)
        {
            var Enname = db.JobNames.FirstOrDefault(x => x.EnName == jobNameVM_Obj.EnName);
            var name = db.JobNames.FirstOrDefault(x => x.Name == jobNameVM_Obj.Name);
            if (Enname != null || name != null)
                return Messages.NameAlreadyExist;
            JobName jobName_Obj = new JobName();
            jobName_Obj.Name = jobNameVM_Obj.Name;
            jobName_Obj.EnName = jobNameVM_Obj.EnName;

            db.JobNames.Add(jobName_Obj);
            db.SaveChanges();
            //db.JobNames.Commit();
            return "";
        }
        #endregion
        

        
       

       
    }
}
