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
    public class SendToTrafficBLL
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


        public List<EmployeeVM> GetAllEmployee()
        {
            List<EmployeeVM> Model = new List<EmployeeVM>();
            try
            {
                Model = (from e in db.Employees                         
                         select new EmployeeVM()
                         {
                             ID = e.ID,
                             Name = e.ID +"_" +e.Name,
                             EnName = e.ID + "_"+e.EnName,                             
                         }).ToList();
            }
            catch
            {
                Model = null;
            }
            return Model;
        }


        public SendToTrafficVM GetbyGuid(Guid? TraineeGuid)
        {
            SendToTrafficVM Model = new SendToTrafficVM();
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
                             TraineeEvaluationId = tE.ID,
                             

                             ArName = t.ArName,
                             EnName = t.EnName,
                             Code = t.Code,
                             FileNo = t.FileNo,

                            

                         }).AsEnumerable()
                           .Select(x => new SendToTrafficVM
                           {
                                //ID = x.ID,
                               TraineeId = x.TraineeId,
                               TraineeGuid = x.TraineeGuid,
                               TraineeEvaluationId = x.TraineeEvaluationId,

                                ArName = x.ArName,
                               EnName = x.EnName,
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




        //-----------------------------------------------------------------
        public List<SendToTrafficVM> Getall(int _TraineeId)
        {
            List<SendToTrafficVM> Model = new List<SendToTrafficVM>();
            try
            {

                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId

                         join ST in db.SendToTraffics
                         on tE.ID equals ST.TraineeEvaluationId
                        
                         where t.FileNo != null /*&& tE.TraineeId == _TraineeId*/

                         select new
                         {
                             ID = ST.ID,
                             TraineeId = ST.TraineeId,
                             TraineeEvaluationId = ST.TraineeEvaluationId,
                             SendDate = ST.SendDate,
                             SenderEmployeeId = ST.SenderEmployeeId,

                             ArName = t.ArName,
                             EnName = t.EnName,

                             ArEmpName = ST.SenderEmployeeId + "_"+ST.Employee.Name,
                             EnEmpName = ST.SenderEmployeeId +"_"+ ST.Employee.EnName,

                         }).AsEnumerable()
                          .Select(x => new SendToTrafficVM
                          {
                              ID = x.ID,
                              TraineeId = x.TraineeId,
                              TraineeEvaluationId = x.TraineeEvaluationId,
                              SendDate = String.Format("{0:dd/mm/yyyy}", x.SendDate),
                              SenderEmployeeId = x.SenderEmployeeId,
                              ArName = x.ArName,
                              EnName = x.EnName,
                              ArEmpName = x.ArEmpName,
                              EnEmpName = x.EnEmpName,
                          }).ToList();
                return Model;
            }
            catch (Exception ex)
            {
                return Model = null;
            }
        }



        public SendToTrafficVM Get(int ID)
        {
            SendToTrafficVM Model = new SendToTrafficVM();
            try
            {
                Model = (from t in db.Trainees
                         join tE in db.TraineeEvaluations
                         on t.ID equals tE.TraineeId
                         
                         join ST in db.SendToTraffics
                         on tE.ID equals ST.TraineeEvaluationId

                         where t.FileNo != null && ST.ID == ID


                         select new
                         {
                             ID = ST.ID,
                             TraineeId = ST.TraineeId,
                             TraineeEvaluationId = ST.TraineeEvaluationId,
                             SendDate = ST.SendDate,
                             SenderEmployeeId=ST.SenderEmployeeId,
                             ArEmpName = ST.Employee.Name,
                             EnEmpName = ST.Employee.EnName,

                         }).AsEnumerable()
                          .Select(x => new SendToTrafficVM
                          {
                              ID = x.ID,
                              TraineeId = x.TraineeId,
                              TraineeEvaluationId = x.TraineeEvaluationId,
                              SendDate = String.Format("{0:dd/mm/yyyy}", x.SendDate),
                              SenderEmployeeId=x.SenderEmployeeId,
                              ArEmpName = x.ArEmpName,
                              EnEmpName = x.EnEmpName,
                          }).FirstOrDefault();
            }
            catch (Exception)
            {

                Model = null;
            }
            return Model;
        }




      




        public string Create(SendToTrafficVM SendToTrafficVM_Obj)
        {
            var _SendDate = DateTime.ParseExact(SendToTrafficVM_Obj.SendDate.ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);


            var objFound = db.SendToTraffics.Any(x => x.SendDate == _SendDate && x.SenderEmployeeId== SendToTrafficVM_Obj.SenderEmployeeId);

            if (objFound != false)
                return Messages.NameAlreadyExist;



            SendToTraffic SendToTraffic_Obj = new SendToTraffic();

            SendToTraffic_Obj.TraineeId = SendToTrafficVM_Obj.TraineeId;

            SendToTraffic_Obj.TraineeEvaluationId = SendToTrafficVM_Obj.TraineeEvaluationId;
            SendToTraffic_Obj.SendDate = _SendDate;

            SendToTraffic_Obj.SenderEmployeeId = SendToTrafficVM_Obj.SenderEmployeeId;


            db.SendToTraffics.Add(SendToTraffic_Obj);
            db.SaveChanges();
            return "";
        }

        public string Edit(SendToTrafficVM SendToTrafficVM_Obj)
        {
            var _SendDate = DateTime.ParseExact(SendToTrafficVM_Obj.SendDate.ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);


            var objFound = db.SendToTraffics.Any(x => x.SendDate == _SendDate && x.SenderEmployeeId == SendToTrafficVM_Obj.SenderEmployeeId && x.ID!= SendToTrafficVM_Obj.ID);

            if (objFound != false)
                return Messages.NameAlreadyExist;




            SendToTraffic SendToTraffic_Obj = db.SendToTraffics.FirstOrDefault(x => x.ID == SendToTrafficVM_Obj.ID);
            SendToTraffic_Obj.ID = SendToTrafficVM_Obj.ID;

            SendToTraffic_Obj.TraineeId = SendToTrafficVM_Obj.TraineeId;

            SendToTraffic_Obj.TraineeEvaluationId = SendToTrafficVM_Obj.TraineeEvaluationId;
            SendToTraffic_Obj.SendDate = _SendDate;

            SendToTraffic_Obj.SenderEmployeeId = SendToTrafficVM_Obj.SenderEmployeeId;

            db.Entry(SendToTraffic_Obj).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return "";
        }

      
        public string delete(int ID)
        {
            SendToTraffic SendToTraffic_Obj = db.SendToTraffics.Find(ID);
            db.SendToTraffics.Remove(SendToTraffic_Obj);
            db.SaveChanges();
            return Messages.DeleteSucc;
        }



    }
}
