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
    public class TraineeFileBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();




        #region Get All TraineeEvaluation
        public TraineeFileVM GetTraineeFile(Guid? traineeGuid)
        {
            TraineeFileVM Model = new TraineeFileVM();
            try
            {
              





                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
   on t.ID equals tE.TraineeId
                         where t.TraineeGuid == traineeGuid
                         select new
                         {
                             ID = t.ID,
                             ArName = t.ArName,
                             EnName = t.EnName,
                             TraineeGuid = t.TraineeGuid,
                             Code = t.Code,
                             FileNo = t.FileNo,
                             FileOpenDate = t.FileOpenDate,
                             LicenceTypeName = tE.LicenceType.Name,
                             LicenceTypeEnName = tE.LicenceType.EnName,
                             LicenceCategoryName = tE.LicenceCategory.Name,
                             LicenceCategoryEnName = tE.LicenceCategory.EnName
                         }).AsEnumerable()
                          .Select(x => new TraineeFileVM()
                          {
                              ID = x.ID,
                              ArName = x.ArName,
                              EnName = x.EnName,
                              TraineeGuid = x.TraineeGuid,
                              Code = x.Code,
                              FileNo = x.FileNo,
                              FileOpenDate = String.Format("{0:MM/dd/yyyy}", x.FileOpenDate),
                             
                              LicenceTypeName = x.LicenceTypeName,
                              LicenceTypeEnName = x.LicenceTypeEnName,
                              LicenceCategoryName =x.LicenceCategoryName,
                              LicenceCategoryEnName =x.LicenceCategoryEnName
                          }).FirstOrDefault();
                           


                
            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        #endregion

        #region Get Trainee By ID
        public TraineeFileVM GetByID(int id)
        {
            TraineeFileVM Model = new TraineeFileVM();
            try
            {
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         where t.ID == id
                         select new
                         {
                             ID = t.ID,
                             ArName = t.ArName,
                             EnName = t.EnName,
                             TraineeGuid = t.TraineeGuid,
                             Code = t.Code,
                             FileNo = t.FileNo,
                             FileOpenDate = t.FileOpenDate,
                             LicenceTypeName = tE.LicenceType.Name,
                             LicenceTypeEnName = tE.LicenceType.EnName,
                             LicenceCategoryName = tE.LicenceCategory.Name,
                             LicenceCategoryEnName = tE.LicenceCategory.EnName
                         }).AsEnumerable()
                         .Select(x => new TraineeFileVM()
                         {
                             ID = x.ID,
                             ArName = x.ArName,
                             EnName = x.EnName,
                             TraineeGuid = x.TraineeGuid,
                             Code = x.Code,
                             FileNo = x.FileNo,
                             FileOpenDate = String.Format("{0:MM/dd/yyyy}", x.FileOpenDate),

                             LicenceTypeName = x.LicenceTypeName,
                             LicenceTypeEnName = x.LicenceTypeEnName,
                             LicenceCategoryName = x.LicenceCategoryName,
                             LicenceCategoryEnName = x.LicenceCategoryEnName
                         }).FirstOrDefault();

            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }
        #endregion

        #region GetallLicenceTypes
        public List<LicenceTypeVM> GetallLicenceTypes()
        {
            var Model = new List<LicenceTypeVM>();
            try
            {
                Model = db.LicenceTypes.Select(x => new LicenceTypeVM()
                {
                    ID = x.ID,
                    Name = x.Name,
                    EnName = x.EnName
                }).ToList();
            }
            catch
            {
                Model = null;
            }
            return Model;
        }
        #endregion
        #region GetallLicenceCategories
        public List<LicenceCategoryVM> GetallLicenceCategories()
        {
            var Model = new List<LicenceCategoryVM>();
            try
            {
                Model = db.LicenceCategories.Select(x => new LicenceCategoryVM()
                {
                    ID = x.ID,
                    Name = x.Name,
                    EnName = x.EnName
                }).ToList();
            }
            catch
            {
                Model = null;
            }
            return Model;
        }
        #endregion

        #region
        
        public List<TraineeFileVM> GetallCodeTrainee()
        {
            var Model = new List<TraineeFileVM>();
            try
            {
                Model=(from t in db.Trainees
                    join tE in db.TraineeEvaluations
                    on t.ID equals tE.TraineeId
                    select  new TraineeFileVM()
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
        public TraineeFileVM SaveinDataBase(TraineeFileVM traineeFileVM_Obj)
        {
            //var result = false;
            try
            {
                var objFound = db.Trainees.Any(x => x.ID != traineeFileVM_Obj.ID && x.FileNo == traineeFileVM_Obj.FileNo);
                if(objFound==false)
                { 
                Trainee trainee_Obj = db.Trainees.FirstOrDefault(x => x.ID == traineeFileVM_Obj.ID);

                trainee_Obj.FileNo = traineeFileVM_Obj.FileNo;

                var d = DateTime.ParseExact(traineeFileVM_Obj.FileOpenDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                trainee_Obj.FileOpenDate = d;

                db.Entry(trainee_Obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    traineeFileVM_Obj.objAlreadyFound = "تم حفظ رقم وتاريخ الملف لهذا المتدربٍ";
                    return traineeFileVM_Obj;
                }

                traineeFileVM_Obj.objAlreadyFound = "هذا الملف موجود للمتدرب هذا";
                return traineeFileVM_Obj;
            }
            catch (Exception ex)
            {
                traineeFileVM_Obj = null;

                return traineeFileVM_Obj;
            }
        }
        #endregion



        #region Delete
        public TraineeFileVM DeleteTraineeFileBLL(TraineeFileVM traineeFileVM_Obj)
        {
            try
            {
                Trainee trainee_Obj = db.Trainees.FirstOrDefault(x => x.ID == traineeFileVM_Obj.ID);

                trainee_Obj.FileNo = null;
                trainee_Obj.FileOpenDate =null;

                db.Entry(trainee_Obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                traineeFileVM_Obj = null;
                traineeFileVM_Obj.objAlreadyFound = "تم حذف رقم وتارخ الملف";
                //traineeFileVM_Obj.objDeleted = "تم حذف رقم وتارخ الملف";
                return traineeFileVM_Obj;


            }
            catch (Exception ex)
            {
                //traineeFileVM_Obj.objAlreadyFound = "لا يمكن حذف الملف لارتباطه ببيانات اخرى";
                return null;
            }
        }
        #endregion


        public TraineeFileVM GetTraineeFileByCodeBLL(int NCode=0) {

            db.Configuration.ProxyCreationEnabled = false;

         var TraineeFileobj = (from t in db.Trainees
                               join tE in db.TraineeEvaluations
                               on t.ID equals tE.TraineeId
                               where t.ID == NCode
                               select new
                     {
                                   ID = t.ID,
                                   ArName = t.ArName,
                                   EnName = t.EnName,
                                   TraineeGuid = t.TraineeGuid,

                                   Code = t.Code,
                                   FileNo = t.FileNo,
                                   // FileOpenDate = t.FileOpenDate.ToString(),
                                   FileOpenDate = t.FileOpenDate,

                                   LicenceTypeName = tE.LicenceType.Name,
                                   LicenceTypeEnName = tE.LicenceType.EnName,
                                   LicenceCategoryName = tE.LicenceCategory.Name,
                                   LicenceCategoryEnName = tE.LicenceCategory.EnName
                               }).AsEnumerable()
                        .Select(x => new TraineeFileVM()
                        {
                            ID = x.ID,
                            ArName =x.ArName,
                            EnName = x.EnName,
                            TraineeGuid = x.TraineeGuid,

                            Code =x.Code,
                            FileNo = x.FileNo,
                            // FileOpenDate = t.FileOpenDate.ToString(),
                            FileOpenDate = String.Format("{0:MM/dd/yyyy}", x.FileOpenDate),

                            LicenceTypeName = x.LicenceTypeName,
                            LicenceTypeEnName = x.LicenceTypeEnName,
                            LicenceCategoryName = x.LicenceCategoryName,
                            LicenceCategoryEnName = x.LicenceCategoryEnName,
                        }).FirstOrDefault();

            return TraineeFileobj;
        }
    }
}
