using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
using AutoDrive.VM.AutoDriveMainViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.AutoDriveMain
{
    public class TraineeEvaluationBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All TraineeEvaluation
        public TraineeEvaluationVM Getall(TraineeEvaluationVM TraineeEvaluationVM_Obj)
        {
            TraineeEvaluationVM Model = new TraineeEvaluationVM();
            try
            {
                Model = 
                    (from tE in db.TraineeEvaluations
                     where tE.ID == TraineeEvaluationVM_Obj.ID
                     select new {
                         ID = tE.ID,
                         TraineeId = tE.TraineeId,
                         TraineePhotopath = tE.Trainee.Photopath,
                         DateOfBirth = tE.Trainee.DateOfBirth,
                         //TraineeAge = (DateTime.Now - x.Trainee.DateOfBirth).ToString(),
                         Code = tE.Trainee.Code,
                         LicenceTypeId = tE.LicenceTypeId,
                         LicenceCategoryId = tE.LicenceCategoryId,
                         StudyPeriodSettingId = tE.StudyPeriodSettingId,
                         EvaluationEmployeeId = tE.EvaluationEmployeeId,
                         EvaluationDate = tE.EvaluationDate
                     }).AsEnumerable()
                    .Select(x => new TraineeEvaluationVM()
                {
                        ID = x.ID,
                        TraineeId = x.TraineeId,
                        TraineePhotopath = x.TraineePhotopath,
                        DateOfBirth = String.Format("{0:MM/dd/yyyy}", x.DateOfBirth),
                       
                        //TraineeAge = (DateTime.Now - x.Trainee.DateOfBirth).ToString(),
                        Code = x.Code,
                        LicenceTypeId = x.LicenceTypeId,
                        LicenceCategoryId = x.LicenceCategoryId,
                        StudyPeriodSettingId = x.StudyPeriodSettingId,
                        EvaluationEmployeeId = x.EvaluationEmployeeId,
                        EvaluationDate = String.Format("{0:MM/dd/yyyy}", x.EvaluationDate)
                       
                    }).FirstOrDefault();


                string path = Model.TraineePhotopath;

                bool fileExist = File.Exists(path);
                if (!fileExist)
                {
                    Model.TraineePhotopath="t.png";
                }
                


            }
            catch
            {
                Model = null;
            }
            return Model;
        }
        #endregion

        #region Get TraineeEvaluation By ID
        public TraineeEvaluationVM GetByID(int id)
        {
            TraineeEvaluationVM Model = new TraineeEvaluationVM();

            try
               
            {
                Model =
                     (from tE in db.TraineeEvaluations
                      where tE.ID == id
                      select new
                      {
                          ID = tE.ID,
                          TraineeId = tE.TraineeId,
                          TraineePhotopath = tE.Trainee.Photopath,
                          DateOfBirth = tE.Trainee.DateOfBirth,
                         //TraineeAge = (DateTime.Now - x.Trainee.DateOfBirth).ToString(),
                         Code = tE.Trainee.Code,
                          LicenceTypeId = tE.LicenceTypeId,
                          LicenceCategoryId = tE.LicenceCategoryId,
                          StudyPeriodSettingId = tE.StudyPeriodSettingId,
                          EvaluationEmployeeId = tE.EvaluationEmployeeId,
                          EvaluationDate = tE.EvaluationDate
                      }).AsEnumerable()
                     .Select(x => new TraineeEvaluationVM()
                     {
                         ID = x.ID,
                         TraineeId = x.TraineeId,
                         TraineePhotopath = x.TraineePhotopath,
                         DateOfBirth = String.Format("{0:MM/dd/yyyy}", x.DateOfBirth),

                        //TraineeAge = (DateTime.Now - x.Trainee.DateOfBirth).ToString(),
                        Code = x.Code,
                         LicenceTypeId = x.LicenceTypeId,
                         LicenceCategoryId = x.LicenceCategoryId,
                         StudyPeriodSettingId = x.StudyPeriodSettingId,
                         EvaluationEmployeeId = x.EvaluationEmployeeId,
                         EvaluationDate = String.Format("{0:MM/dd/yyyy}", x.EvaluationDate)

                     }).FirstOrDefault();

                string path = Model.TraineePhotopath;

                bool fileExist = File.Exists(path);
                if (!fileExist)
                {
                    Model.TraineePhotopath = "t.png";
                }

            }
            catch (Exception ex)
            {
                var a = ex;
                Model = null;
            }
            return Model;
        }
        #endregion
        #region GetTraineeCode_Age_Photo
        public TraineeEvaluationVM GetTraineeCode_Age_PhotoBLL(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;
            
            var data = db.Trainees.Where(a => a.ID == id).Select(i => new {Code ="dd", i.DateOfBirth, i.Photopath }).FirstOrDefault();
            TraineeEvaluationVM tE = new TraineeEvaluationVM();
            tE.Code = data.Code;
            tE.DateOfBirth = data.DateOfBirth.ToString();
            tE.TraineePhotopath = data.Photopath;

            return tE;
            

           
        }
        #endregion

        #region GetLicenceGategoryByLicenceType
        public object GetLicenceGategoryByLicenceTypeBLL(int licenceTypeId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var data = db.LicenceCategories.Where(a => a.LicenceTypeId == licenceTypeId).Select(i => new { i.ID, i.Name }).ToList();
            return data;
        }
        #endregion

        #region GetallTrainees
        public List<TraineeVM> GetallTrainees()
        {

            var Model = new List<TraineeVM>();
            try
            {
                Model = db.Trainees.Select(x => new TraineeVM()
                {
                    ID = x.ID,
                    ArName = x.ArName+"  "+x.Code,
                    EnName = x.EnName + "  " + x.Code

                }).ToList();


            }
            catch
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
        #region GetallStudyPeriodSettings
        public List<StudyPeriodSettingVM> GetallStudyPeriodSettings()
        {
            var Model = new List<StudyPeriodSettingVM>();
            try
            {
                Model = (from s in db.StudyPeriodSettings
                         join d in db.DriveLevels
                         on s.DriveLevelId equals d.ID
                         where s.LevelStatus == 1
                         select (new StudyPeriodSettingVM()
                         {
                             ID = s.ID,
                             Name = d.Name + " " + s.LevelTotal,
                             EnName = d.EnName + " " + s.LevelTotal
                         })).ToList();

            }
            catch
            {
                Model = null;
            }
            return Model;
        }
        #endregion

        #region GetallStudyPeriodSettings
        public List<EmployeeVM> GetallEvaluationEmployees()
        {
            var Model = new List<EmployeeVM>();
            try
            {
                Model = (from E in db.Employees
                         join EJ in db.EmployeeJobDatas
                         on E.ID equals EJ.EmployeeId
                         join JF in db.JobFunctions
                         on EJ.JobFunctionId equals JF.ID
                         join JN in db.JobNames
                         on EJ.JobNameId equals JN.ID
                         where JF.ID==5 &&JN.ID==33
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
        #endregion


        #region SaveinDataBase
        public  bool SaveinDataBase(TraineeEvaluationVM traineeEvaluationVM_Obj)
        {
            var result = false;

            try
            {
                
               
                var found = db.TraineeEvaluations.Any(a => a.TraineeId == traineeEvaluationVM_Obj.TraineeId && a.EvaluationEmployeeId == traineeEvaluationVM_Obj.EvaluationEmployeeId);
                if (found == false)
                {
                    TraineeEvaluation traineeEvaluation_Obj = new TraineeEvaluation();
                    traineeEvaluation_Obj.TraineeId = traineeEvaluationVM_Obj.TraineeId;
                    //traineeEvaluation_Obj.Trainee.Photopath = traineeEvaluationVM_Obj.TraineePhotopath;
                    traineeEvaluation_Obj.LicenceTypeId = traineeEvaluationVM_Obj.LicenceTypeId;
                    traineeEvaluation_Obj.LicenceCategoryId = traineeEvaluationVM_Obj.LicenceCategoryId;
                    traineeEvaluation_Obj.StudyPeriodSettingId = traineeEvaluationVM_Obj.StudyPeriodSettingId;
                    traineeEvaluation_Obj.EvaluationEmployeeId = traineeEvaluationVM_Obj.EvaluationEmployeeId;

                    var evaluationDate = DateTime.ParseExact(traineeEvaluationVM_Obj.EvaluationDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    traineeEvaluation_Obj.EvaluationDate = evaluationDate;

                    db.TraineeEvaluations.Add(traineeEvaluation_Obj);
                    db.SaveChanges();
                    // traineeEvaluationVM_Obj.ID = traineeEvaluation_Obj.ID;
                    traineeEvaluationVM_Obj.Ar_msg = "تم الحفظ بنجاح";
                    traineeEvaluationVM_Obj.En_msg = "Created Successfully";
                    result = true;

                }
                else {
                    traineeEvaluationVM_Obj.Ar_msg = "هذا التقييم موجود من قبل";
                    traineeEvaluationVM_Obj.En_msg = "Trainee Evaluation Aready Used";
                    result = false;
                }
            
              //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
        #region Delete
        public bool Delete(int id)
        {

            try
            {
                TraineeEvaluation traineeEvaluation_Obj = db.TraineeEvaluations.Find(id);
                db.TraineeEvaluations.Remove(traineeEvaluation_Obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        //---------------------------------------------------
        public List<TraineeEvaluationVM> GetallTraineeEvaluationList()
        {
            List<TraineeEvaluationVM> Model = new List<TraineeEvaluationVM>();
            try
            {

                Model = (from tE in db.TraineeEvaluations

                         select new
                         {
                             ID = tE.ID,

                             TraineeId = tE.TraineeId,
                             TraineeName = tE.Trainee.ArName,
                             TraineeEnName = tE.Trainee.EnName,

                             TraineePhotopath = tE.Trainee.Photopath,
                             DateOfBirth = tE.Trainee.DateOfBirth.ToString(),
                             Code = tE.Trainee.Code,

                             LicenceTypeId = tE.LicenceTypeId,
                             LicenceTypeName = tE.LicenceType.Name,
                             LicenceTypeEnName = tE.LicenceType.EnName,

                             LicenceCategoryId = tE.LicenceCategoryId,
                             LicenceCategoryName = tE.LicenceCategory.Name,
                             LicenceCategoryEnName = tE.LicenceCategory.EnName,

                             StudyPeriodSettingId = tE.StudyPeriodSettingId,
                             StudyPeriodSettingName = tE.StudyPeriodSetting.DriveLevel.Name + " " + tE.StudyPeriodSetting.LevelTotal,
                             StudyPeriodSettingEnName = tE.StudyPeriodSetting.DriveLevel.EnName + " " + tE.StudyPeriodSetting.LevelTotal,

                             EvaluationEmployeeId = tE.EvaluationEmployeeId,
                             EvaluationEmployeeName = tE.Employee.Name,
                             EvaluationEmployeeEnName = tE.Employee.EnName,

                             EvaluationDate = tE.EvaluationDate.ToString(),
                             

                         }).AsEnumerable()
                                  .Select(x => new TraineeEvaluationVM()
                                  {
                                      ID = x.ID,

                                      TraineeId =x.TraineeId,
                                      TraineeName = x.TraineeName,
                                      TraineeEnName = x.TraineeEnName,

                                      TraineePhotopath = x.TraineePhotopath,
                                      DateOfBirth = String.Format("{0:MM/dd/yyyy}", x.DateOfBirth),
     
                                      Code = x.Code,

                                      LicenceTypeId = x.LicenceTypeId,
                                      LicenceTypeName = x.LicenceTypeName,
                                      LicenceTypeEnName = x.LicenceTypeEnName,

                                      LicenceCategoryId = x.LicenceCategoryId,
                                      LicenceCategoryName =x.LicenceCategoryName,
                                      LicenceCategoryEnName = x.LicenceCategoryEnName,

                                      StudyPeriodSettingId = x.StudyPeriodSettingId,
                                      StudyPeriodSettingName = x.StudyPeriodSettingName,
                                      StudyPeriodSettingEnName = x.StudyPeriodSettingEnName,

                                      EvaluationEmployeeId = x.EvaluationEmployeeId,
                                      EvaluationEmployeeName = x.EvaluationEmployeeName,
                                      EvaluationEmployeeEnName = x.EvaluationEmployeeEnName,

                                      EvaluationDate = String.Format("{0:MM/dd/yyyy}", x.EvaluationDate)
                                     

                                  }).ToList();
                             //    db.TraineeEvaluations.Select(x => new TraineeEvaluationVM()


                             //{
                             //    ID = x.ID,

                             //    TraineeId = x.TraineeId,
                             //    TraineeName = x.Trainee.ArName,
                             //    TraineeEnName= x.Trainee.EnName,

                             //    TraineePhotopath = x.Trainee.Photopath,
                             //    DateOfBirth = x.Trainee.DateOfBirth.ToString(),
                             //    Code = x.Trainee.Code,

                             //    LicenceTypeId = x.LicenceTypeId,
                             //    LicenceTypeName = x.LicenceType.Name,
                             //    LicenceTypeEnName = x.LicenceType.EnName,

                             //    LicenceCategoryId = x.LicenceCategoryId,
                             //    LicenceCategoryName = x.LicenceCategory.Name,
                             //    LicenceCategoryEnName = x.LicenceCategory.EnName,

                             //    StudyPeriodSettingId = x.StudyPeriodSettingId,
                             //    StudyPeriodSettingName = x.StudyPeriodSetting.DriveLevel.Name+" "+x.StudyPeriodSetting.LevelTotal,
                             //    StudyPeriodSettingEnName = x.StudyPeriodSetting.DriveLevel.EnName + " " + x.StudyPeriodSetting.LevelTotal,

                             //    EvaluationEmployeeId = x.EvaluationEmployeeId,
                             //    EvaluationEmployeeName=x.Employee.Name,
                             //    EvaluationEmployeeEnName=x.Employee.EnName,

                             //    EvaluationDate = x.EvaluationDate.ToString()


                             //}).ToList();





                    


            }
            catch
            {
                Model = null;
            }
            return Model;
        }



        public bool SaveinDataBaseDataTable(TraineeEvaluationVM traineeEvaluationVM_Obj)
        {
            var result = false;

            try
            {
                if (traineeEvaluationVM_Obj.ID > 0)
                {
                    TraineeEvaluation traineeEvaluation_Obj = db.TraineeEvaluations.FirstOrDefault(x => x.ID == traineeEvaluationVM_Obj.ID);

                    traineeEvaluation_Obj.ID = traineeEvaluationVM_Obj.ID;
                    traineeEvaluation_Obj.TraineeId = traineeEvaluationVM_Obj.TraineeId;

                    traineeEvaluation_Obj.LicenceTypeId = traineeEvaluationVM_Obj.LicenceTypeId;
                    traineeEvaluation_Obj.LicenceCategoryId = traineeEvaluationVM_Obj.LicenceCategoryId;
                    traineeEvaluation_Obj.StudyPeriodSettingId = traineeEvaluationVM_Obj.StudyPeriodSettingId;
                    traineeEvaluation_Obj.EvaluationEmployeeId = traineeEvaluationVM_Obj.EvaluationEmployeeId;


                    var d = DateTime.ParseExact(traineeEvaluationVM_Obj.EvaluationDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    traineeEvaluation_Obj.EvaluationDate = d;


                    db.Entry(traineeEvaluation_Obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    TraineeEvaluation traineeEvaluation_Obj = new TraineeEvaluation();
                    traineeEvaluation_Obj.TraineeId = traineeEvaluationVM_Obj.TraineeId;
                    //traineeEvaluation_Obj.Trainee.Photopath = traineeEvaluationVM_Obj.TraineePhotopath;
                    traineeEvaluation_Obj.LicenceTypeId = traineeEvaluationVM_Obj.LicenceTypeId;
                    traineeEvaluation_Obj.LicenceCategoryId = traineeEvaluationVM_Obj.LicenceCategoryId;
                    traineeEvaluation_Obj.StudyPeriodSettingId = traineeEvaluationVM_Obj.StudyPeriodSettingId;
                    traineeEvaluation_Obj.EvaluationEmployeeId = traineeEvaluationVM_Obj.EvaluationEmployeeId;

                    var evaluationDate = DateTime.ParseExact(traineeEvaluationVM_Obj.EvaluationDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    traineeEvaluation_Obj.EvaluationDate = evaluationDate;

                    db.TraineeEvaluations.Add(traineeEvaluation_Obj);
                    db.SaveChanges();
                    traineeEvaluationVM_Obj.ID = traineeEvaluation_Obj.ID;

                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool DeleteDataBaseDataTable(TraineeEvaluationVM traineeEvaluationVM_Obj)
        {
            try
            {
                TraineeEvaluation traineeEvaluation_Obj = db.TraineeEvaluations.Find(traineeEvaluationVM_Obj.ID);
                db.TraineeEvaluations.Remove(traineeEvaluation_Obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
