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
    public class SendToTrafficController : Controller
    {
        private SendToTrafficBLL SendToTraffic_Obj = new SendToTrafficBLL();


        // GET: AutoDriveMain/SendToTraffic
        [HttpGet]
        public ActionResult Index(Guid? TraineeGuid)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeGuid = new SelectList(SendToTraffic_Obj.GetAllTrainee(), "TraineeGuid", "EnName");
               ViewBag.SenderEmployeeId = new SelectList(SendToTraffic_Obj.GetAllEmployee(), "ID", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.TraineeGuid = new SelectList(SendToTraffic_Obj.GetAllTrainee(), "TraineeGuid", "ArName");
               ViewBag.SenderEmployeeId = new SelectList(SendToTraffic_Obj.GetAllEmployee(), "ID", "Name");

            }
            if (TraineeGuid == null)
            {
                SendToTrafficVM obj = new SendToTrafficVM();
                return View(obj);
            }
            else
            {
                SendToTrafficVM obj = SendToTraffic_Obj.GetbyGuid(TraineeGuid);
                return View(obj);
            }

           
        }


        [HttpGet]
        public ActionResult GetDetails(Guid TraineeGuid)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeGuid = new SelectList(SendToTraffic_Obj.GetAllTrainee(), "TraineeGuid", "EnName");
                ViewBag.SenderEmployeeId = new SelectList(SendToTraffic_Obj.GetAllEmployee(), "ID", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.TraineeGuid = new SelectList(SendToTraffic_Obj.GetAllTrainee(), "TraineeGuid", "ArName");
                ViewBag.SenderEmployeeId = new SelectList(SendToTraffic_Obj.GetAllEmployee(), "ID", "Name");

            }
            SendToTrafficVM obj = SendToTraffic_Obj.GetbyGuid(TraineeGuid);
            return View(obj);
            //return View();
        }


        public JsonResult GetSendToTrafficbyGuid(Guid TraineeGuid)
        {

            SendToTrafficVM obj = SendToTraffic_Obj.GetbyGuid(TraineeGuid);

            return Json(new { obj = obj }, JsonRequestBehavior.AllowGet);

        }


        //---------------------------------------------------------------------

        [HttpGet]
        public JsonResult Getall(int _TraineeId)
        {
            var jobName_List = SendToTraffic_Obj.Getall(_TraineeId);
            return Json(new { data = jobName_List }, JsonRequestBehavior.AllowGet);
        }

        
        [HttpGet]
        public JsonResult GetSendToTraffic_byGuid(Guid TraineeGuid)
        {
            var model = SendToTraffic_Obj.GetbyGuid(TraineeGuid);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetbyID(int ID)
        {
            var data = SendToTraffic_Obj.Get(ID);

            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.SenderEmployeeId = new SelectList(SendToTraffic_Obj.GetAllEmployee(), "ID", "EnName", data.SenderEmployeeId);

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.SenderEmployeeId = new SelectList(SendToTraffic_Obj.GetAllEmployee(), "ID", "Name", data.SenderEmployeeId);

            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(SendToTrafficVM SendToTrafficVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = SendToTraffic_Obj.Create(SendToTrafficVM_Obj);
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
        public JsonResult Edit(SendToTrafficVM SendToTrafficVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = SendToTraffic_Obj.Edit(SendToTrafficVM_Obj);
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
                return Json(new { status = true, Msg = SendToTraffic_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}