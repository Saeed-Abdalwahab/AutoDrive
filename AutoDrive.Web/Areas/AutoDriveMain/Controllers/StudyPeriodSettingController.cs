using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveMainViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class StudyPeriodSettingController : Controller
    {
        private StudyPeriodSettingBLL studyPeriodSettingBLL = new StudyPeriodSettingBLL();
        
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.DriveLevelId = new SelectList(studyPeriodSettingBLL.GetallDriveLevel(), "ID", "EnName");
            }
            else {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.DriveLevelId = new SelectList(studyPeriodSettingBLL.GetallDriveLevel(), "ID", "Name");

            }
            return View();
        }

        [HttpGet]
        public JsonResult GetStudyPeriodSetting()
        {
            var studyPeriodSetting_List = studyPeriodSettingBLL.Getall();
            return Json(new { data = studyPeriodSetting_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetStudyPeriodSetting_byId(int ID)
        {

            return Json(new { data = studyPeriodSettingBLL.GetByID(ID) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save_studyPeriodSetting(StudyPeriodSettingVM studyPeriodSetting_VMObj)
        {
     
                return Json(new { msg = studyPeriodSettingBLL.SaveinDataBase(studyPeriodSetting_VMObj) }, JsonRequestBehavior.AllowGet);
            

        }

      
        [HttpGet]
        public ActionResult GetDelete(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studyPeriodSettingVM = studyPeriodSettingBLL.GetByID((int)ID);
            if (studyPeriodSettingBLL == null)
            {
                return HttpNotFound();
            }
            return PartialView(studyPeriodSettingVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool Delete(int ID)
        {
            
            var r=   studyPeriodSettingBLL.Delete(ID);
            return r;
        }

    }
}