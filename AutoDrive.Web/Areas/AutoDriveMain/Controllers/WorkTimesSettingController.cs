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
    public class WorkTimesSettingController : Controller
    {
        // GET: AutoDriveMain/WorkTimesSetting
        private WorkTimesSettingBLL WorkTimesSettingBLL_Obj = new WorkTimesSettingBLL();

        // GET: WorkTimesSetting
        public ActionResult Index()
        {
            WorkTimesSettingVM WorkTimesSettingVM_Obj= WorkTimesSettingBLL_Obj.weekAll();

            return View(WorkTimesSettingVM_Obj);
        }
        [HttpPost]
        public ActionResult CreateWorkTime(WorkTimesSettingVM WorkTimesSettingVM_Obj)
        {
            var obj= WorkTimesSettingBLL_Obj.CreateWeek(WorkTimesSettingVM_Obj);
            Session["robj"] = obj;
            //if (objNotFound == true)
            //{
            //    ViewBag.TraineeFound = "تم الاضافه بنجاح";
            //    //TraineeEvaluationVM obj
            //    return RedirectToAction("Index"/*, obj*/);
            //}
            //else
            //{
            //    ViewBag.TraineeFound = "هذا الموظف له تقييم سابق";
            //    return RedirectToAction("Index", traineeEvaluation_VMObj);
            //}
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult Getall()
        {

            var WorkTimesSetting_List = WorkTimesSettingBLL_Obj.Getall();
            return Json(new { data = WorkTimesSetting_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(WorkTimesSettingBLL_Obj.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(WorkTimesSettingVM WorkTimesSettingVM_Obj)
        {
            
                    var Mssg = WorkTimesSettingBLL_Obj.Edit(WorkTimesSettingVM_Obj);
                    //var Mssg = WorkTimesSettingVM_Obj.ID == 0 ? WorkTimesSettingBLL_Obj.CreateWeek(WorkTimesSettingVM_Obj) : WorkTimesSettingBLL_Obj.Edit(WorkTimesSettingVM_Obj);

                    return Mssg == "" ? Json(new { status = true }, JsonRequestBehavior.AllowGet) : Json(new { status = false, Msg = Mssg }, JsonRequestBehavior.AllowGet);

                
           
        }


        [HttpPost]
        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = WorkTimesSettingBLL_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}