using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class WorkingHoursSettingHRController : Controller
    {
        WorkingHoursSettingHRService workingHoursSettingHRService = new WorkingHoursSettingHRService();
        // GET: HR/WorkingHoursSettingHR
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Save(WorkingHoursSettingHRVM model)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = workingHoursSettingHRService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }
        }
       public JsonResult GetWorkingHoursSettingHRs()
        {
            return Json(new { data = workingHoursSettingHRService.GetALL() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getWorkingHoursSettingHRByID(int id)
        {
            return Json(new { data = workingHoursSettingHRService.GetByID(id) }, JsonRequestBehavior.AllowGet);
        }
        public void delete(int id)
        {
            workingHoursSettingHRService.delete(id);
        }
        public JsonResult GetEmployeeHoursSForEmployee()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = workingHoursSettingHRService.GetEmployeeHoursSForEmployee(Language) }, JsonRequestBehavior.AllowGet);
        }
    }
}