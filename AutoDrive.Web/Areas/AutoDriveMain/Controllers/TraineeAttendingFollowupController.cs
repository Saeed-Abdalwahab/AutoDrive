using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class TraineeAttendingFollowupController : Controller
    {
        private TraineeAttendingFollowupBLL TraineeAttendingFollowup_Obj = new TraineeAttendingFollowupBLL();

        // GET: AutoDriveMain/TraineeAttendingFollowup
        [HttpGet]
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeGuid = new SelectList(TraineeAttendingFollowup_Obj.GetAllTrainee(), "TraineeGuid", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";
                ViewBag.TraineeGuid = new SelectList(TraineeAttendingFollowup_Obj.GetAllTrainee(), "TraineeGuid", "ArName");

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
                ViewBag.TraineeAttendanceId = new SelectList(TraineeAttendingFollowup_Obj.GetAllTraineeAttendances(), "ID", "EnTraineeAttendance");

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";
                ViewBag.TraineeAttendanceId = new SelectList(TraineeAttendingFollowup_Obj.GetAllTraineeAttendances(), "ID", "ArTraineeAttendance");

            }
            TraineeAttendingFollowupVM Obj = TraineeAttendingFollowup_Obj.GetbyId(TraineeGuid);


            return View(Obj);
        }

        //------------------------------------------------------------------------
        [HttpGet]
        public JsonResult Getall(int _TraineeId)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeAttendanceId = new SelectList(TraineeAttendingFollowup_Obj.GetAllTraineeAttendances(), "ID", "EnTraineeAttendance");

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";
                ViewBag.TraineeAttendanceId = new SelectList(TraineeAttendingFollowup_Obj.GetAllTraineeAttendances(), "ID", "ArTraineeAttendance");

            }
            var TraineeAttendingFollowup_List = TraineeAttendingFollowup_Obj.Getall(_TraineeId);
            return Json(new { data = TraineeAttendingFollowup_List }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetbyID(int ID)
        {
            var data = TraineeAttendingFollowup_Obj.Get(ID);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(TraineeAttendingFollowupVM TraineeAttendingFollowupVM_Obj)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeAttendanceId = new SelectList(TraineeAttendingFollowup_Obj.GetAllTraineeAttendances(), "ID", "EnTraineeAttendance", TraineeAttendingFollowupVM_Obj.TraineeAttendanceId);

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";
                ViewBag.TraineeAttendanceId = new SelectList(TraineeAttendingFollowup_Obj.GetAllTraineeAttendances(), "ID", "ArTraineeAttendance", TraineeAttendingFollowupVM_Obj.TraineeAttendanceId);

            }
            var Mssg = TraineeAttendingFollowupVM_Obj.ID == 0 ? TraineeAttendingFollowup_Obj.Save(TraineeAttendingFollowupVM_Obj) : TraineeAttendingFollowup_Obj.Edit(TraineeAttendingFollowupVM_Obj);


            //var Mssg = WorkTimesSettingVM_Obj.ID == 0 ? WorkTimesSettingBLL_Obj.CreateWeek(WorkTimesSettingVM_Obj) : WorkTimesSettingBLL_Obj.Edit(WorkTimesSettingVM_Obj);

            return Mssg == "" ? Json(new { status = true }, JsonRequestBehavior.AllowGet) : Json(new { status = false, Msg = Mssg }, JsonRequestBehavior.AllowGet);



        }


        [HttpPost]
        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = TraineeAttendingFollowup_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}