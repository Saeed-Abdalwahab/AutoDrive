using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class EmployeeHoursSettingController : Controller
    {
        EmployeeHoursSettingService employeeHoursSettingService = new EmployeeHoursSettingService();
        // GET: HR/EmployeeHoursSetting
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
        public JsonResult checkWorkingHours(int WorkingHoursId)
        {
            return Json(new { data = employeeHoursSettingService.checkWorkingHours(WorkingHoursId) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(EmployeeHoursSettingVM model)
        {
            var result = employeeHoursSettingService.SaveInDataBase(model);
            if (result == "true")
            {
                return Json(new { status = true, msg = Messages.SaveSucc }, JsonRequestBehavior.AllowGet);
            }
            else if (result== "Employee Working Hours is existed in the same period")
            {
                return Json(new { status = result, msg = Messages.ExistedEmployeeWorkingHours }, JsonRequestBehavior.AllowGet);
            }
            else
            return  Json(new { status = false, msg = Messages.SaveErr }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetEmployeeHoursSettings(int ID)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeHoursSettingService.GetALL(Language,ID) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, msg = employeeHoursSettingService.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult GetForm(int ID = 0, int? EmployeeID = null)
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
            if (ID > 0)
            {
              return PartialView(getEmployeeHoursSettingByID(ID));
            }
            EmployeeHoursSettingVM vM = new EmployeeHoursSettingVM();
            vM.EmployeeId = EmployeeID;
            return PartialView(vM);
        }
        public EmployeeHoursSettingVM getEmployeeHoursSettingByID(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return  employeeHoursSettingService.GetByID(id, Language);
        }
        public void Create(int WorkingHoursSettingHRId,DateTime? FromDate,DateTime? ToDate,int EmpId)
        {
            employeeHoursSettingService.CreateEmployeeWorkingHours(WorkingHoursSettingHRId, FromDate,ToDate,EmpId);
        }
    }
}