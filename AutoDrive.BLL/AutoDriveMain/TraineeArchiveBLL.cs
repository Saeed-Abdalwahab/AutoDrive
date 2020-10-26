using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.AutoDriveMain
{
    public class TraineeArchiveBLL
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<TraineeArchiveVM> Getall22(int? id)
        {


            var Model = new List<TraineeArchiveVM>();
            try
            {
                Model = (from x in db.TraineeArchives

                         where x.ArchiveSettingDriveId== id
                         select new
                         {

                             ID = x.ID,
                             TraineeId = x.TraineeId,
                             ArchiveSettingDriveId = x.ArchiveSettingDriveId,
                             Note = x.Note,
                             Number = x.Number,
                             Date = x.Date,
                             ImageName = x.ImageName


                         }).AsEnumerable()
                         .Select(a => new TraineeArchiveVM()
                         {
                             ID = a.ID,
                             TraineeId = a.TraineeId,
                             ArchiveSettingDriveId = a.ArchiveSettingDriveId,
                             Note = a.Note,
                             Number = a.Number,
                             Date = String.Format("{0:dd/mm/yyyy}", a.Date),
                             ImageName = a.ImageName

                         }).ToList();


            }
            catch
            {
                Model = null;
            }

            return Model;
            //var List = id != 0 ? db.TraineeArchives.Where(x => x.ArchiveSettingDriveId == id).ToList() : db.TraineeArchives.Where(x => x.ArchiveSettingDrive.ParentId == null).ToList();

            ////TraineeArchiveVM List2 = id != 0 ? 
            //var c = (from TA in db.TraineeArchives
            //         join ASD in db.ArchiveSettingDrives
            //         on TA.ArchiveSettingDriveId equals ASD.ID
            //         where ASD.ParentId == id
            //         select new TraineeArchiveVM()
            //         {
            //             ID = TA.ID,
            //             ArchiveSettingDriveId=ASD.ID,
            //             Name = ASD.Name,
            //             EnName = ASD.EnName,
            //             ParentId = ASD.ParentId,
            //             ParentName = ASD.Name,
            //             ParentEnName = ASD.EnName

            //         }).ToList();
            //var c2 = (from TA in db.TraineeArchives
            //         join ASD in db.ArchiveSettingDrives
            //         on TA.ArchiveSettingDriveId equals ASD.ID
            //         where ASD.ParentId == null
            //         select new TraineeArchiveVM()
            //         {
            //             ID = TA.ID,
            //             ArchiveSettingDriveId = ASD.ID,
            //             Name = ASD.Name,
            //             EnName = ASD.EnName,
            //             ParentId = ASD.ParentId,
            //             ParentName = ASD.Name,
            //             ParentEnName = ASD.EnName

            //         }).ToList();
            ////db.TraineeArchives.Where(x => x.ArchiveSettingDrive.ParentId == id).ToList() : db.TraineeArchives.Where(x => x.ArchiveSettingDrive.ParentId == null).ToList();







            //var ListVM = List.Select(x => new TraineeArchiveVM()
            //{
            //    ID = x.ID,

            //    Name = x.ArchiveSettingDrive.Name,
            //    EnName = x.ArchiveSettingDrive.EnName,
            //    ParentId = x.ArchiveSettingDrive.ParentId,
            //    ParentName = x.ArchiveSettingDrive.Name,
            //    ParentEnName = x.ArchiveSettingDrive.EnName

            //}).ToList();

            //return ListVM;
        }
        public List<ArchiveSettingDrive> GetallTree()
        {
            var List = db.ArchiveSettingDrives.ToList();
            return List;
        }
        public TraineeArchiveVM Get(int ID)
        {
            TraineeArchiveVM Model = new TraineeArchiveVM();
            try
            {

                Model = (from x in db.TraineeArchives.Where(o => o.ArchiveSettingDrive.ID == ID)
                         select new TraineeArchiveVM()
                         {

                             ID = x.ID,
                             Name = x.ArchiveSettingDrive.Name,
                             EnName = x.ArchiveSettingDrive.EnName,
                             ParentId = x.ArchiveSettingDrive.ParentId,
                             ParentName = x.ArchiveSettingDrive.Name,
                             ParentEnName = x.ArchiveSettingDrive.EnName


                         }).FirstOrDefault();
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;

        }

        //public string Save(ArchiveSettingDriveVM ArchiveSettingDriveVM_Obj)
        //{

        //    var name = db.ArchiveSettingDrives.FirstOrDefault(x => x.Name == ArchiveSettingDriveVM_Obj.Name);
        //    if (name != null)
        //        return Messages.NameAlreadyExist;
        //    ArchiveSettingDrive ArchiveSettingDrive_Obj = new ArchiveSettingDrive();
        //    ArchiveSettingDrive_Obj.Name = ArchiveSettingDriveVM_Obj.Name;
        //    ArchiveSettingDrive_Obj.EnName = ArchiveSettingDriveVM_Obj.EnName;
        //    ArchiveSettingDrive_Obj.ParentId = ArchiveSettingDriveVM_Obj.ParentId;


        //    db.ArchiveSettingDrives.Add(ArchiveSettingDrive_Obj);
        //    db.SaveChanges();
        //    return "";
        //}
        //public string Edit(ArchiveSettingDriveVM ArchiveSettingDriveVM_Obj)
        //{
        //    var name = db.JobNames.FirstOrDefault(x => x.Name == ArchiveSettingDriveVM_Obj.Name && x.ID != ArchiveSettingDriveVM_Obj.ID);
        //    if (name != null)
        //        return Messages.NameAlreadyExist;
        //    ArchiveSettingDrive ArchiveSettingDrive_Obj = db.ArchiveSettingDrives.FirstOrDefault(x => x.ID == ArchiveSettingDriveVM_Obj.ID);

        //    ArchiveSettingDrive_Obj.ID = ArchiveSettingDriveVM_Obj.ID;
        //    ArchiveSettingDrive_Obj.Name = ArchiveSettingDriveVM_Obj.Name;
        //    ArchiveSettingDrive_Obj.EnName = ArchiveSettingDriveVM_Obj.EnName;
        //    ArchiveSettingDrive_Obj.ParentId = ArchiveSettingDriveVM_Obj.ParentId;


        //    db.Entry(ArchiveSettingDrive_Obj).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //    return "";
        //}
        //public string delete(int ID)
        //{
        //    ArchiveSettingDrive ArchiveSettingDrive_Obj = db.ArchiveSettingDrives.Find(ID);
        //    db.ArchiveSettingDrives.Remove(ArchiveSettingDrive_Obj);
        //    db.SaveChanges();
        //    return Messages.DeleteSucc;
        //}
        //public bool NameCheck(string Name, int ID)
        //{
        //    if (ID == 0)
        //    {
        //        return db.ArchiveSettingDrives.FirstOrDefault(x => x.Name == Name) == null ? true : false;
        //    }
        //    return db.ArchiveSettingDrives.FirstOrDefault(x => x.Name == Name && x.ID != ID) == null ? true : false;
        //}
        //public bool ENNameCheck(string EnName, int ID)
        //{
        //    if (ID == 0)
        //    {
        //        return db.ArchiveSettingDrives.FirstOrDefault(x => x.EnName == EnName) == null ? true : false;
        //    }
        //    return db.ArchiveSettingDrives.FirstOrDefault(x => x.EnName == EnName && x.ID != ID) == null ? true : false;

        //}

        public IEnumerable<TraineeArchiveVM> ArchiveSettingHRs(string Any, string language)
        {

            if (language.ToLower() == "en-us")
            {
                return db.TraineeArchives.Where(x => x.ArchiveSettingDrive.EnName.Contains(Any)).ToList().Select(x => new TraineeArchiveVM()
                {
                    ID = x.ID,
                    Name = x.ArchiveSettingDrive.ParentId != null ? x.ArchiveSettingDrive.EnName + "-" + x.ArchiveSettingDrive.ArchiveSettingDrive2.EnName : x.ArchiveSettingDrive.EnName
                });
            }
            else
            {
                return db.TraineeArchives.Where(x => x.ArchiveSettingDrive.Name.Contains(Any)).ToList().Select(x => new TraineeArchiveVM()
                {
                    ID = x.ID,
                    Name = x.ArchiveSettingDrive.ParentId != null ? x.ArchiveSettingDrive.Name + "-" + x.ArchiveSettingDrive.ArchiveSettingDrive2.Name : x.ArchiveSettingDrive.Name
                });
            }

        }






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

        public TraineeArchiveVM GetbyId(Guid TraineeGuid)
        {
            TraineeArchiveVM Model = new TraineeArchiveVM();
            try
            {
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         

                         where t.TraineeGuid == TraineeGuid && t.FileNo != null

                         select new
                         {
                             //ID = c.ID,
                             TraineeId = t.ID,
                             TraineeGuid = t.TraineeGuid,
                            

                             Ar_Name = t.ArName,
                             En_Name = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,

                         

                         }).AsEnumerable()
                           .Select(x => new TraineeArchiveVM
                           {
                                //ID = x.ID,
                                TraineeId = x.TraineeId,
                               TraineeGuid = x.TraineeGuid,
                                Ar_Name = x.Ar_Name,
                               En_Name = x.En_Name,
                               Code = x.Code,
                               FileNo = x.FileNo,


                           }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Model = null;
            }

            return Model;
        }
        public TraineeArchiveVM GetbyId2(int ASDId)
        {
            TraineeArchiveVM Model = new TraineeArchiveVM();
            try
            {
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         join s in db.StudyPeriodSettings
                         on tE.StudyPeriodSettingId equals s.ID
                         join c in db.CourseReservations
                         on tE.ID equals c.TraineeEvaluationId
                         join tA in db.TraineeArchives
                         on t.ID equals tA.TraineeId
                         join Asd in db.ArchiveSettingDrives
                         on tA.ArchiveSettingDriveId equals Asd.ID

                         where Asd.ID == ASDId && t.FileNo != null

                         select new
                         {
                             //ID = c.ID,
                             TraineeId = t.ID,
                             TraineeGuid = t.TraineeGuid,


                             Ar_Name = t.ArName,
                             En_Name = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,



                         }).AsEnumerable()
                           .Select(x => new TraineeArchiveVM
                           {
                               //ID = x.ID,
                               TraineeId = x.TraineeId,
                               TraineeGuid = x.TraineeGuid,
                               Ar_Name = x.Ar_Name,
                               En_Name = x.En_Name,
                               Code = x.Code,
                               FileNo = x.FileNo,


                           }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                Model = null;
            }

            return Model;
        }

        public TraineeArchiveVM GetTraineeByCodeBLL(int NCode)
        {

            db.Configuration.ProxyCreationEnabled = false;

            var Model = (from t in db.Trainees
                        
                         where t.ID == NCode
                         select new TraineeArchiveVM()
                         {
                             //ID = c.ID,
                             TraineeId = t.ID,
                             CodeId = t.ID,                            
                             Ar_Name = t.ArName,
                             En_Name = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,
                             TraineeGuid = t.TraineeGuid
                             

                         }).FirstOrDefault();

            return Model;
        }










        public TraineeArchiveVM SaveinDataBase(TraineeArchiveVM TraineeArchiveVM_Obj)
        {
            //var result = false;


            try
            {
                if (TraineeArchiveVM_Obj.ID > 0)
                {
                    var found = db.TraineeArchives.Any(x => x.Number == TraineeArchiveVM_Obj.Number && x.ID != TraineeArchiveVM_Obj.ID);

                    if (found == false || TraineeArchiveVM_Obj.Number == null)
                    {
                        TraineeArchive TraineeArchive_Obj = db.TraineeArchives.FirstOrDefault(x => x.ID == TraineeArchiveVM_Obj.ID);

                        //TraineeArchive_Obj.ID = TraineeArchiveVM_Obj.ID;
                        TraineeArchive_Obj.ArchiveSettingDriveId = TraineeArchiveVM_Obj.ArchiveSettingDriveId;
                        TraineeArchive_Obj.TraineeId = TraineeArchiveVM_Obj.TraineeId;
                        TraineeArchive_Obj.Note = TraineeArchiveVM_Obj.Note;
                        TraineeArchive_Obj.Number = TraineeArchiveVM_Obj.Number;
                        //TraineeArchive_Obj.Date = TraineeArchiveVM_Obj.Date;
                        // HH:mm
                        if (TraineeArchiveVM_Obj.Date!=null)
                            { 
                        var d = DateTime.ParseExact(TraineeArchiveVM_Obj.Date, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                        TraineeArchive_Obj.Date = d;
                        }


                        if (TraineeArchiveVM_Obj.ImageName != null)
                        {
                            TraineeArchive_Obj.ImageName =TraineeArchiveVM_Obj.ImageName;
                        }
                        else if (TraineeArchiveVM_Obj.ImageName == null && TraineeArchive_Obj.ImageName != null)
                        {
                            TraineeArchiveVM_Obj.ImageName = TraineeArchive_Obj.ImageName;
                        }



                        db.Entry(TraineeArchive_Obj).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        //TraineeArchiveVM_Obj.TraineeGuid = TraineeArchive_Obj.TraineeGuid;

                        TraineeArchiveVM_Obj.Ar_msg = "تم التعديل";
                        TraineeArchiveVM_Obj.En_msg = "Edit Successfully";

                        return TraineeArchiveVM_Obj;
                    }
                    else
                    {
                        TraineeArchiveVM_Obj.Ar_msg = "ارشفه المستند مسجل من قبل";
                        TraineeArchiveVM_Obj.En_msg = "Trainee Archive Aready Used";

                        return TraineeArchiveVM_Obj;
                    }
                }
                else
                {
                    var found = db.TraineeArchives.Any(x => x.Number == TraineeArchiveVM_Obj.Number);

                    if (found == false || TraineeArchiveVM_Obj.Number==null)
                    {
                        TraineeArchive TraineeArchive_Obj = new TraineeArchive();
                        TraineeArchive_Obj.ArchiveSettingDriveId = TraineeArchiveVM_Obj.ArchiveSettingDriveId;
                        TraineeArchive_Obj.TraineeId = TraineeArchiveVM_Obj.TraineeId;
                        TraineeArchive_Obj.Note = TraineeArchiveVM_Obj.Note;
                        TraineeArchive_Obj.Number = TraineeArchiveVM_Obj.Number;
                        //TraineeArchive_Obj.Date = TraineeArchiveVM_Obj.Date;

                        if (TraineeArchiveVM_Obj.Date != null)
                        {
                            var d = DateTime.ParseExact(TraineeArchiveVM_Obj.Date, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                            TraineeArchive_Obj.Date = d;

                        }

                        if (TraineeArchiveVM_Obj.ImageName != null)
                        {
                            TraineeArchive_Obj.ImageName = TraineeArchiveVM_Obj.ImageName;
                        }
                       


                        db.TraineeArchives.Add(TraineeArchive_Obj);
                        db.SaveChanges();
                        TraineeArchiveVM_Obj.ID = TraineeArchive_Obj.ID;
                        //TraineeArchiveVM_Obj.TraineeGuid = TraineeArchive_Obj.TraineeGuid;
                        TraineeArchiveVM_Obj.Ar_msg = "تم الحفظ بنجاح";
                        TraineeArchiveVM_Obj.En_msg = "Created Successfully";

                        return TraineeArchiveVM_Obj;

                    }
                    else
                    {
                        TraineeArchiveVM_Obj.Ar_msg = "ارشفه المستند مسجل من قبل";
                        TraineeArchiveVM_Obj.En_msg = "Trainee Archive Aready Used";

                        return TraineeArchiveVM_Obj;
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;

            }



        }




        public List<TraineeArchiveVM> Getall()
        {


            var Model = new List<TraineeArchiveVM>();
            try
            {
                Model = (from x in db.TraineeArchives


                         select new
                         {

                             ID = x.ID,
                             TraineeId = x.TraineeId,
                             ArchiveSettingDriveId = x.ArchiveSettingDriveId,
                             Note = x.Note,
                             Number = x.Number,
                             Date = x.Date,
                             ImageName = x.ImageName
                            


                         }).AsEnumerable()
                         .Select(a => new TraineeArchiveVM()
                         {
                             ID = a.ID,
                             TraineeId = a.TraineeId,
                             ArchiveSettingDriveId = a.ArchiveSettingDriveId,
                             Note = a.Note,
                             Number = a.Number,
                             Date = String.Format("{0:dd/mm/yyyy}", a.Date),
                             ImageName = a.ImageName
                         }).ToList();


            }
            catch
            {
                Model = null;
            }

            return Model;
        }
        public TraineeArchiveVM Get2(int ID)
        {
            TraineeArchiveVM Model = new TraineeArchiveVM();
            try
            {

                Model = (from x in db.TraineeArchives.Where(o => o.ID == ID)


                         select new
                         {

                             ID = x.ID,
                             TraineeId = x.TraineeId,
                             ArchiveSettingDriveId = x.ArchiveSettingDriveId,
                             Note = x.Note,
                             Number = x.Number,
                             Date = x.Date,
                             ImageName = x.ImageName
                             //ImgVirtualPath = ConfigurationManager.AppSettings["TraineeLocalPath"]+ x.ImageName


                         }).AsEnumerable()
                         .Select(a => new TraineeArchiveVM()
                         {
                             ID = a.ID,
                             TraineeId = a.TraineeId,
                             ArchiveSettingDriveId = a.ArchiveSettingDriveId,
                             Note = a.Note,
                             Number = a.Number,
                             Date = String.Format("{0:dd/mm/yyyy}", a.Date),
                             ImageName = a.ImageName
                            // ImgVirtualPath = a.ImgVirtualPath 
                         }).FirstOrDefault();
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }
        public string Edit(TraineeArchiveVM TraineeArchiveVM_Obj)
        {
            var FoundNumber = db.TraineeArchives.FirstOrDefault(x => x.Number == TraineeArchiveVM_Obj.Number && x.ID != TraineeArchiveVM_Obj.ID);
            if (FoundNumber != null && TraineeArchiveVM_Obj.Number != null)
                return Messages.NameAlreadyExist;


            if (FoundNumber == null || TraineeArchiveVM_Obj.Number == null)
            {
                TraineeArchive TraineeArchive_Obj = db.TraineeArchives.FirstOrDefault(x => x.ID == TraineeArchiveVM_Obj.ID);

                //TraineeArchive_Obj.ID = TraineeArchiveVM_Obj.ID;
                TraineeArchive_Obj.ArchiveSettingDriveId = TraineeArchiveVM_Obj.ArchiveSettingDriveId;
                TraineeArchive_Obj.TraineeId = TraineeArchiveVM_Obj.TraineeId;
                TraineeArchive_Obj.Note = TraineeArchiveVM_Obj.Note;
                TraineeArchive_Obj.Number = TraineeArchiveVM_Obj.Number;
                //TraineeArchive_Obj.Date = TraineeArchiveVM_Obj.Date;

                if(TraineeArchiveVM_Obj.Date!=null)
                { 
                var d = DateTime.ParseExact(TraineeArchiveVM_Obj.Date, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                TraineeArchive_Obj.Date = d;
                }


                if (TraineeArchiveVM_Obj.ImageName != null)
                {
                    TraineeArchive_Obj.ImageName = TraineeArchiveVM_Obj.ID + TraineeArchiveVM_Obj.ImageName;
                }
                else if (TraineeArchiveVM_Obj.ImageName == null && TraineeArchive_Obj.ImageName != null)
                {
                    TraineeArchiveVM_Obj.ImageName = TraineeArchive_Obj.ImageName;
                }



                db.Entry(TraineeArchive_Obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //TraineeArchiveVM_Obj.TraineeGuid = TraineeArchive_Obj.TraineeGuid;

                TraineeArchiveVM_Obj.Ar_msg = "تم التعديل";
                TraineeArchiveVM_Obj.En_msg = "Edit Successfully";

              
            }
            return "";

        }
        public string EditFile(TraineeArchiveVM TraineeArchiveVM_Obj)
        {
            var FoundNumber = db.TraineeArchives.FirstOrDefault(x => x.Number == TraineeArchiveVM_Obj.Number && x.ID != TraineeArchiveVM_Obj.ID);
            if (FoundNumber != null)
                return Messages.NameAlreadyExist;
            TraineeArchive TraineeArchive_Obj = db.TraineeArchives.FirstOrDefault(x => x.ID == TraineeArchiveVM_Obj.ID);



          


            if (TraineeArchiveVM_Obj.ImageName != null)
            {
                if ((System.IO.File.Exists(ConfigurationManager.AppSettings["TraineeLocalPath"] + TraineeArchive_Obj.ImageName)))
                {
                    System.IO.File.Delete(ConfigurationManager.AppSettings["TraineeLocalPath"] + TraineeArchive_Obj.ImageName);
                }
                TraineeArchive_Obj.ImageName = TraineeArchiveVM_Obj.ImageName;
            }
            else if (TraineeArchiveVM_Obj.ImageName == null && TraineeArchive_Obj.ImageName != null)
            {
                TraineeArchiveVM_Obj.ImageName = TraineeArchive_Obj.ImageName;
            }



            db.Entry(TraineeArchive_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //TraineeArchiveVM_Obj.TraineeGuid = TraineeArchive_Obj.TraineeGuid;

            TraineeArchiveVM_Obj.Ar_msg = "تم التعديل";
            TraineeArchiveVM_Obj.En_msg = "Edit Successfully";

            return "";
            //}
            //else
            //{
            //    TraineeArchiveVM_Obj.Ar_msg = "ارشفه المستند مسجل من قبل";
            //    TraineeArchiveVM_Obj.En_msg = "Trainee Archive Aready Used";

            //    return TraineeArchiveVM_Obj;
            //}



        }
        public string delete(int ID)
        {
            TraineeArchive TraineeArchive_Obj = db.TraineeArchives.Find(ID);
            if ((System.IO.File.Exists(ConfigurationManager.AppSettings["TraineeLocalPath"] + TraineeArchive_Obj.ImageName)))
            {
                System.IO.File.Delete(ConfigurationManager.AppSettings["TraineeLocalPath"] + TraineeArchive_Obj.ImageName);
            }
            db.TraineeArchives.Remove(TraineeArchive_Obj);
            db.SaveChanges();
            return Messages.DeleteSucc;
        }
    }
}
