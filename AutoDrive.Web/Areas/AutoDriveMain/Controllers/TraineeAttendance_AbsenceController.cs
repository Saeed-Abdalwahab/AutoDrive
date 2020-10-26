using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.VM.AutoDriveMainViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{

    public class TraineeAttendance_AbsenceController : Controller
    {
        private TraineeAttendance_AbsenceBLL TraineeAttendance_Absence_Obj = new TraineeAttendance_AbsenceBLL();

        // GET: AutoDriveMain/TraineeAttendance_Absence
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeGuid = new SelectList(TraineeAttendance_Absence_Obj.GetAllTrainee(), "TraineeGuid", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";
                ViewBag.TraineeGuid = new SelectList(TraineeAttendance_Absence_Obj.GetAllTrainee(), "TraineeGuid", "ArName");

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
            TraineeAttendingFollowupVM Obj = TraineeAttendance_Absence_Obj.GetbyId(TraineeGuid);

            return View(Obj);
        }
        [HttpGet]
        public JsonResult Getall(int TraineeId)
        {

            var TraineeAttending_Absence_List = TraineeAttendance_Absence_Obj.Getall_byID(TraineeId);
            return Json(new { data = TraineeAttending_Absence_List }, JsonRequestBehavior.AllowGet);
        }
    }
}