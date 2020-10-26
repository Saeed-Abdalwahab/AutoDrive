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

namespace AutoDrive.BLL.AutoDriveMain
{
    public class TraineeBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Get All TraineeEvaluation
        public TraineeVM Getall(Guid? traineeGuid)
        {
            TraineeVM Model = new TraineeVM();
            try
            {
              

                Model = 
                   ( from  t in db.Trainees
                     where t.TraineeGuid==traineeGuid
                     
                     select new
                     {
                         ID = t.ID,
                         ArName = t.ArName,
                         EnName = t.EnName,
                         TraineeGuid = t.TraineeGuid,
                         NationalityId = t.NationalityId,
                         ReligionId = t.ReligionId,
                         DateOfBirth = t.DateOfBirth,
                         NationalId = t.NationalId,
                         NationalTypeId = t.NationalTypeId,
                         Residence = t.Residence,
                         Code = t.Code,

                         Gender = (Gender)t.Gender.Value,
                         GenderId = (Gender)t.Gender,
                         SpeechLanguageId = t.SpeechLanguageId,
                         Photopath = t.Photopath

                     }
                     ).AsEnumerable().Select(x => new TraineeVM
                     {

                         ID = x.ID,
                         ArName = x.ArName,
                         EnName = x.EnName,
                         TraineeGuid = x.TraineeGuid,
                         NationalityId = x.NationalityId,
                         ReligionId = x.ReligionId,
                         DateOfBirth = String.Format("{0:MM/dd/yyyy}", x.DateOfBirth),
                         NationalId = x.NationalId,
                         NationalTypeId = x.NationalTypeId,
                         Residence = x.Residence,
                         Code = x.Code,

                         Gender = x.Gender,
                         GenderId = x.GenderId,
                         SpeechLanguageId = x.SpeechLanguageId,
                         Photopath = x.Photopath
                       
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
        public TraineeVM GetByID(int id)
        {
            TraineeVM Model = new TraineeVM();
            try
            {
                //var tt = (from t in db.Trainees

                //          where t.ID == id
                //          select new
                //          {
                //              t.DateOfBirth,
                //              t.FileOpenDate
                //          }).FirstOrDefault();
                //var y = String.Format("{0:mm/dd/yyyy}", tt.DateOfBirth);
                //var fy = String.Format("{0:mm/dd/yyyy}", tt.FileOpenDate);


                Model = (from t in db.Trainees.Where(o => o.ID == id)
                         select new
                         {
                             ID = t.ID,
                             ArName = t.ArName,
                             EnName = t.EnName,
                             TraineeGuid = t.TraineeGuid,
                             NationalityId = t.NationalityId,
                             ReligionId = t.ReligionId,
                             DateOfBirth = t.DateOfBirth,
                             NationalId = t.NationalId,
                             NationalTypeId = t.NationalTypeId,
                             Residence = t.Residence,
                             Code = t.Code,

                             Gender = (Gender)t.Gender.Value,
                             GenderId = (Gender)t.Gender,
                             SpeechLanguageId = t.SpeechLanguageId,
                             Photopath = t.Photopath

                         }
                     ).AsEnumerable().Select(x => new TraineeVM
                     {

                         ID = x.ID,
                         ArName = x.ArName,
                         EnName = x.EnName,
                         TraineeGuid = x.TraineeGuid,
                         NationalityId = x.NationalityId,
                         ReligionId = x.ReligionId,
                         DateOfBirth = String.Format("{0:MM/dd/yyyy}", x.DateOfBirth),
                         NationalId = x.NationalId,
                         NationalTypeId = x.NationalTypeId,
                         Residence = x.Residence,
                         Code = x.Code,

                         Gender = x.Gender,
                         GenderId = x.GenderId,
                         SpeechLanguageId = x.SpeechLanguageId,
                         Photopath = x.Photopath

                     }).FirstOrDefault();
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }
        #endregion

        #region GetallNatoinality
        public List<NationalityVM> GetallNatoinalities()
        {

            var Model = new List<NationalityVM>();
            try
            {
                Model = db.Nationalities.Select(x => new NationalityVM()
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
        #region GetallReligions
        public List<ReligionVM> GetallReligions()
        {
            var Model = new List<ReligionVM>();
            try
            {
                Model = db.Religions.Select(x => new ReligionVM()
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

        #region GetNationalTypes
        public List<NationalTypeVM> GetallNationalTypes()
        {
            var Model = new List<NationalTypeVM>();
            try
            {
                Model = db.NationalTypes.Select(x => new NationalTypeVM()
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
        #region GetAllSpeechLanguages
        public List<SpeechLanguageVM> GetallSpeechLanguages()
        {
            var Model = new List<SpeechLanguageVM>();
            try
            {
                Model = db.SpeechLanguages.Select(x => new SpeechLanguageVM()
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

        #region SaveinDataBase
        public TraineeVM SaveinDataBase(TraineeVM traineeVM_Obj)
        {
            //var result = false;


            try
            {
                if (traineeVM_Obj.ID > 0)
                {
                    var found = db.Trainees.Any(x => x.Code == traineeVM_Obj.Code && x.ID != traineeVM_Obj.ID);

                    if (found == false)
                    {
                        Trainee trainee_Obj = db.Trainees.FirstOrDefault(x => x.ID == traineeVM_Obj.ID);

                        trainee_Obj.ID = traineeVM_Obj.ID;
                        trainee_Obj.ArName = traineeVM_Obj.ArName;
                        trainee_Obj.EnName = traineeVM_Obj.EnName;
                        trainee_Obj.TraineeGuid = Guid.NewGuid();
                        trainee_Obj.NationalityId = traineeVM_Obj.NationalityId;
                        trainee_Obj.ReligionId = traineeVM_Obj.ReligionId;

                        var d = DateTime.ParseExact(traineeVM_Obj.DateOfBirth, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        trainee_Obj.DateOfBirth = d;

                        trainee_Obj.NationalId = traineeVM_Obj.NationalId;
                        trainee_Obj.NationalTypeId = traineeVM_Obj.NationalTypeId;
                        trainee_Obj.Residence = traineeVM_Obj.Residence;
                        trainee_Obj.Code = traineeVM_Obj.Code;
                        
                        
                        trainee_Obj.Gender = (int)traineeVM_Obj.Gender;
                        trainee_Obj.SpeechLanguageId = traineeVM_Obj.SpeechLanguageId;

                        if (traineeVM_Obj.Photopath != null)
                        {
                            trainee_Obj.Photopath = traineeVM_Obj.Photopath;
                        }
                        else if (traineeVM_Obj.Photopath == null && trainee_Obj.Photopath != null)
                        {
                            traineeVM_Obj.Photopath = trainee_Obj.Photopath;
                        }



                        db.Entry(trainee_Obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        traineeVM_Obj.TraineeGuid = trainee_Obj.TraineeGuid;

                        traineeVM_Obj.Ar_msg = "تم التعديل";
                        traineeVM_Obj.En_msg = "Edit Successfully";

                        return traineeVM_Obj;
                    }
                    else {
                        traineeVM_Obj.Ar_msg = "كود المتدرب مسجل من قبل";
                        traineeVM_Obj.En_msg = "Trainee Code Aready Used";

                        return traineeVM_Obj;
                    }
                }
                else
                {
                    var found = db.Trainees.Any(x => x.Code == traineeVM_Obj.Code );

                    if (found == false)
                    {
                        Trainee trainee_Obj = new Trainee();
                        trainee_Obj.ArName = traineeVM_Obj.ArName;
                        trainee_Obj.EnName = traineeVM_Obj.EnName;
                        trainee_Obj.TraineeGuid = Guid.NewGuid();
                        trainee_Obj.NationalityId = traineeVM_Obj.NationalityId;
                        trainee_Obj.ReligionId = traineeVM_Obj.ReligionId;

                        var d = DateTime.ParseExact(traineeVM_Obj.DateOfBirth, "MM/dd/yyyy", CultureInfo.InvariantCulture);                      
                        trainee_Obj.DateOfBirth = d;  //DateTime.Parse( traineeVM_Obj.DateOfBirth);

                        trainee_Obj.NationalId = traineeVM_Obj.NationalId;
                        trainee_Obj.NationalTypeId = traineeVM_Obj.NationalTypeId;
                        trainee_Obj.Residence = traineeVM_Obj.Residence;
                        trainee_Obj.Code = traineeVM_Obj.Code;
                        
                   
                        trainee_Obj.Gender = (int)traineeVM_Obj.Gender;
                        trainee_Obj.SpeechLanguageId = traineeVM_Obj.SpeechLanguageId;
                        if (traineeVM_Obj.Photopath != null)
                        {
                            trainee_Obj.Photopath = traineeVM_Obj.Photopath;
                        }
                        //else if (traineeVM_Obj.Photopath == null && trainee_Obj.Photopath!=null)
                        //{
                        //    traineeVM_Obj.Photopath = trainee_Obj.Photopath;
                        //}




                        db.Trainees.Add(trainee_Obj);
                        db.SaveChanges();
                        traineeVM_Obj.ID = trainee_Obj.ID;
                        traineeVM_Obj.TraineeGuid = trainee_Obj.TraineeGuid;
                        traineeVM_Obj.Ar_msg = "تم الحفظ بنجاح";
                        traineeVM_Obj.En_msg = "Created Successfully";

                        return traineeVM_Obj;

                    }
                    else {
                        traineeVM_Obj.Ar_msg = "كود المتدرب مسجل من قبل";
                        traineeVM_Obj.En_msg = "Trainee Code Aready Used";

                        return traineeVM_Obj;
                    }
                   
                }
            }
            catch (Exception ex)
            {

                throw ex;
                
            }
           


        }
        #endregion



        #region Delete
        public bool Delete(Guid? traineeGuid)
        {
            try
            {
                Trainee trainee_Obj = db.Trainees.Where(a=>a.TraineeGuid== traineeGuid).FirstOrDefault();
                db.Trainees.Remove(trainee_Obj);
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
