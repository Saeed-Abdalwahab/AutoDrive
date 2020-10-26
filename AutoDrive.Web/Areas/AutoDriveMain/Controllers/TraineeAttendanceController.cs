using AutoDrive.BLL.AutoDriveMain;

using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class TraineeAttendanceController : Controller
    {

        private TraineeAttendanceBLL TraineeAttendance_Obj = new TraineeAttendanceBLL();

        // GET: AutoDriveMain/TraineeAttendance
        [HttpGet]
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeGuid = new SelectList(TraineeAttendance_Obj.GetAllTrainee(), "TraineeGuid", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";
                ViewBag.TraineeGuid = new SelectList(TraineeAttendance_Obj.GetAllTrainee(), "TraineeGuid", "ArName");

            }
            return View();
        }


        [HttpGet]
        public ActionResult GetDetails(Guid TraineeGuid)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";

            }
            TraineeAttendanceVM Obj = TraineeAttendance_Obj.GetbyId(TraineeGuid);


            return View(Obj);
        }




        //---------------------------------------------------------------------

        [HttpGet]
        public JsonResult Getall(int _TraineeId)
        {
            var jobName_List = TraineeAttendance_Obj.Getall(_TraineeId);
            return Json(new { data = jobName_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {
            return Json(TraineeAttendance_Obj.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(TraineeAttendanceVM TraineeAttendanceVM_Obj)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    var Mssg = TraineeAttendance_Obj.Edit(TraineeAttendanceVM_Obj);
                    //var Mssg =  TraineeAttendance_Obj.Edit(TraineeAttendanceVM_Obj);

                    return Mssg == "" ? Json(new { status = true }, JsonRequestBehavior.AllowGet) : Json(new { status = false, Msg = Mssg }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { status = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            //}
            //else
            //{
            //    return Json(new { status = false, Msg = Messages.NotValidMsg }, JsonRequestBehavior.AllowGet);
            //}
        }
        [HttpPost]
        public JsonResult Save2()
        {
            
            try
            {
                var Mssg =TraineeAttendance_Obj.Save();
                

                return Mssg == "" ? Json(new { status = true }, JsonRequestBehavior.AllowGet) : Json(new { status = false, Msg = Mssg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
          
        }

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = TraineeAttendance_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }


        }

    }
}