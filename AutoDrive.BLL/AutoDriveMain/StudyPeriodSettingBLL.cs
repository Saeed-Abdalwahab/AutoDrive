using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveMainViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace AutoDrive.BLL.AutoDriveMain
{

    public class StudyPeriodSettingBLL 
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All Expenses
        public List<StudyPeriodSettingVM> Getall()
        {

            var Model = new List<StudyPeriodSettingVM>();
            try
            {

                
                Model =
                   // db.StudyPeriodSettings.Select(x => new StudyPeriodSettingVM()

                    (from sp in db.StudyPeriodSettings
                     select new
                     {
                         ID = sp.ID,
                         DriveLevelId = sp.DriveLevelId,
                         DriveLevelName = sp.DriveLevel.Name,
                         DriveLevelEnName = sp.DriveLevel.EnName,
                         VisualStudyCount = sp.VisualStudyCount,
                         VisualStudyCost = sp.VisualStudyCost,
                         VisualStudyTotal = sp.VisualStudyTotal,
                         PracticalCount = sp.PracticalCount,
                         PracticalCost = sp.PracticalCost,
                         PracticalTotal = sp.PracticalTotal,
                         LevelTotal = sp.LevelTotal,
                         LevelStatus = (Status)sp.LevelStatus,
                         LevelStatusName = sp.LevelStatus==1?"مفعل":"غير مفعل",
                         LevelStatusEnName = sp.LevelStatus==1?"Active":"NotActive",

                     }).AsEnumerable().Select(x => new StudyPeriodSettingVM
                     {
                    ID = x.ID,
                    DriveLevelId = x.DriveLevelId,
                    DriveLevelName = x.DriveLevelName,
                    DriveLevelEnName = x.DriveLevelEnName,
                    VisualStudyCount = x.VisualStudyCount,
                    VisualStudyCost = x.VisualStudyCost,
                    VisualStudyTotal = x.VisualStudyTotal,
                    PracticalCount = x.PracticalCount,
                    PracticalCost = x.PracticalCost,
                    PracticalTotal = x.PracticalTotal,
                    LevelTotal = x.LevelTotal,
                   
                         LevelStatus = x.LevelStatus,
                         LevelStatusName = x.LevelStatusName,
                         LevelStatusEnName = x.LevelStatusEnName,
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
        public List<DriveLevelVM> GetallDriveLevel()
        {

            var Model = new List<DriveLevelVM>();
            try
            {
                Model = db.DriveLevels.Select(x => new DriveLevelVM()
                {
                    ID = x.ID,
                    Name = x.Name,
                    EnName=x.EnName

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
        public List<Status> GetallStatus()
        {

            var Model = new List<Status>();
            try
            {
                 Model = new List<Status>();
            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        #endregion
        #region Get StudyPeriodSetting By ID
        public StudyPeriodSettingVM GetByID(int id)
        {
            StudyPeriodSettingVM Model = new StudyPeriodSettingVM();
            try
            {

                Model = (from x in db.StudyPeriodSettings.Where(o => o.ID == id)
                         select new StudyPeriodSettingVM()
                         {

                             ID = x.ID,
                             DriveLevelId = x.DriveLevelId,
                             VisualStudyCount = x.VisualStudyCount,
                             VisualStudyCost = x.VisualStudyCost,
                             VisualStudyTotal = x.VisualStudyTotal,
                             PracticalCount = x.PracticalCount,
                             PracticalCost = x.PracticalCost,
                             PracticalTotal = x.PracticalTotal,
                             LevelTotal = x.LevelTotal,
                             LevelStatus = (Status)x.LevelStatus

                         }).FirstOrDefault();
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }
        #endregion



        #region SaveinDataBase
        public bool SaveinDataBase(StudyPeriodSettingVM StudyPeriodSettingVM_Obj)
        {
            var result = false;


            try
            {
                if (StudyPeriodSettingVM_Obj.ID > 0)
                {
                    StudyPeriodSetting StudyPeriodSetting_Obj = db.StudyPeriodSettings.FirstOrDefault(x => x.ID == StudyPeriodSettingVM_Obj.ID);

                    StudyPeriodSetting_Obj.DriveLevelId = StudyPeriodSettingVM_Obj.DriveLevelId;
                    StudyPeriodSetting_Obj.VisualStudyCount = StudyPeriodSettingVM_Obj.VisualStudyCount;
                    StudyPeriodSetting_Obj.VisualStudyCost = StudyPeriodSettingVM_Obj.VisualStudyCost;
                    StudyPeriodSetting_Obj.VisualStudyTotal = StudyPeriodSettingVM_Obj.VisualStudyTotal;
                    StudyPeriodSetting_Obj.PracticalCount = StudyPeriodSettingVM_Obj.PracticalCount;
                    StudyPeriodSetting_Obj.PracticalCost = StudyPeriodSettingVM_Obj.PracticalCost;
                    StudyPeriodSetting_Obj.PracticalTotal = StudyPeriodSettingVM_Obj.PracticalTotal;
                    StudyPeriodSetting_Obj.LevelTotal = StudyPeriodSettingVM_Obj.LevelTotal;
                    StudyPeriodSetting_Obj.LevelStatus = (int)StudyPeriodSettingVM_Obj.LevelStatus;

                    db.Entry(StudyPeriodSetting_Obj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    StudyPeriodSetting StudyPeriodSetting_Obj = new StudyPeriodSetting();
                    StudyPeriodSetting_Obj.DriveLevelId = StudyPeriodSettingVM_Obj.DriveLevelId;
                    StudyPeriodSetting_Obj.VisualStudyCount = StudyPeriodSettingVM_Obj.VisualStudyCount;
                    StudyPeriodSetting_Obj.VisualStudyCost = StudyPeriodSettingVM_Obj.VisualStudyCost;
                    StudyPeriodSetting_Obj.VisualStudyTotal = StudyPeriodSettingVM_Obj.VisualStudyTotal;
                    StudyPeriodSetting_Obj.PracticalCount = StudyPeriodSettingVM_Obj.PracticalCount;
                    StudyPeriodSetting_Obj.PracticalCost = StudyPeriodSettingVM_Obj.PracticalCost;
                    StudyPeriodSetting_Obj.PracticalTotal = StudyPeriodSettingVM_Obj.PracticalTotal;
                    StudyPeriodSetting_Obj.LevelTotal = StudyPeriodSettingVM_Obj.LevelTotal;
                    StudyPeriodSetting_Obj.LevelStatus = (int)StudyPeriodSettingVM_Obj.LevelStatus;


                    db.StudyPeriodSettings.Add(StudyPeriodSetting_Obj);
                    db.SaveChanges();
                    result = true;
                }
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
                StudyPeriodSetting StudyPeriodSetting_Obj = db.StudyPeriodSettings.Find(id);
                db.StudyPeriodSettings.Remove(StudyPeriodSetting_Obj);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
