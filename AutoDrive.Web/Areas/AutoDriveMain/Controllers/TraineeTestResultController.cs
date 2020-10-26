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
    public class TraineeTestResultController : Controller
    {

        private TraineeTestResultBLL TraineeTestResult_Obj = new TraineeTestResultBLL();

        // GET: AutoDriveMain/TraineeTestResult
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeGuid = new SelectList(TraineeTestResult_Obj.GetAllTrainee(), "TraineeGuid", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";
                ViewBag.TraineeGuid = new SelectList(TraineeTestResult_Obj.GetAllTrainee(), "TraineeGuid", "ArName");

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
                ViewBag.TestId = new SelectList(TraineeTestResult_Obj.GetAllTest(), "ID", "EnName");
                ViewBag.TestResultId = new SelectList(TraineeTestResult_Obj.GetAllTestResult(), "ID", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.TestId = new SelectList(TraineeTestResult_Obj.GetAllTest(), "ID", "ArName");
                ViewBag.TestResultId = new SelectList(TraineeTestResult_Obj.GetAllTestResult(), "ID", "ArName");

            }
            TraineeTestResultVM Obj = TraineeTestResult_Obj.GetbyGuid(TraineeGuid);


            return View(Obj);
        }



        //---------------------------------------------------------------------

        [HttpGet]
        public JsonResult Getall(int _TraineeId)
        {
            var jobName_List = TraineeTestResult_Obj.Getall(_TraineeId);
            return Json(new { data = jobName_List }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetTraineeTestResult_byGuid(Guid TraineeGuid)
        {
            var model = TraineeTestResult_Obj.GetbyGuid(TraineeGuid);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetbyID(int ID)
        {
            var data = TraineeTestResult_Obj.Get(ID);

            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TestId = new SelectList(TraineeTestResult_Obj.GetAllTest(), "ID", "EnName", data.TestId);
                ViewBag.TestResultId = new SelectList(TraineeTestResult_Obj.GetAllTestResult(), "ID", "EnName", data.TestResultId);

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.TestId = new SelectList(TraineeTestResult_Obj.GetAllTest(), "ID", "ArName", data.TestId);
                ViewBag.TestResultId = new SelectList(TraineeTestResult_Obj.GetAllTestResult(), "ID", "ArName", data.TestResultId);

            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(TraineeTestResultVM TraineeTestResultVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = TraineeTestResult_Obj.Create(TraineeTestResultVM_Obj);
                    return Mssg == "" ? Json(new { status = true }, JsonRequestBehavior.AllowGet) : Json(new { status = false, Msg = Mssg }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { status = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new { status = false, Msg = Messages.NotValidMsg }, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public JsonResult Edit(TraineeTestResultVM TraineeTestResultVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = TraineeTestResult_Obj.Edit(TraineeTestResultVM_Obj);
                    return Mssg == "" ? Json(new { status = true }, JsonRequestBehavior.AllowGet) : Json(new { status = false, Msg = Mssg }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { status = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new { status = false, Msg = Messages.NotValidMsg }, JsonRequestBehavior.AllowGet);

            }
        }



        [HttpPost]
        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = TraineeTestResult_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}