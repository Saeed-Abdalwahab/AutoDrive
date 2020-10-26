using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class WorkingHoursSettingDetialsHRController : Controller
    {
        WorkingHoursSettingHRDetialsService workingHoursSettingHRDetialsService = new WorkingHoursSettingHRDetialsService();
        // GET: HR/WorkingHoursSettingDetialsHR
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                ViewBag.WorkingHours = new SelectList(context.WorkingHoursSettingHRs.ToList(), "ID", "EnName");
            }
            else
            {
                ViewBag.WorkingHours = new SelectList(context.WorkingHoursSettingHRs.ToList(), "ID", "ArName");
            }
           
            return View();
        }
        public JsonResult Save(WorkingHoursSettingDetialsHrVM model)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = workingHoursSettingHRDetialsService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetWorkingHoursSettingHRs()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = workingHoursSettingHRDetialsService.GetALL(Language) }, JsonRequestBehavior.AllowGet);
        }
        public void delete(int id)
        {
            workingHoursSettingHRDetialsService.delete(id);
        }
        public JsonResult getWorkingHoursSettingDetialsHRByID(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = workingHoursSettingHRDetialsService.GetByID(id, Language) }, JsonRequestBehavior.AllowGet);
        }
    }
}