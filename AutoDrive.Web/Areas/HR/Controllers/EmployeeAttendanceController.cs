using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class EmployeeAttendanceController : Controller
    {
        EmployeeAttendanceService employeeAttendanceService = new EmployeeAttendanceService();
        // GET: HR/EmployeeAttendance
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SearchEmployee(string Text)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }

            return Json(employeeAttendanceService.SearchEmployee(Text, Language).ToList().Select(x => new { Name = x.EmployeeName, ID = x.EmployeeId, code=x.Code, DeptName=x.DeptartmentName }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(EmployeeAttendanceVM model)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = employeeAttendanceService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetEmployeeAttendances()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeAttendanceService.GetALL(Language) }, JsonRequestBehavior.AllowGet);
        }
        public void delete(int id)
        {
            employeeAttendanceService.delete(id);
        }
        public JsonResult getEmployeeAttendanceByID(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeAttendanceService.GetByID(id, Language) }, JsonRequestBehavior.AllowGet);
        }
    }
}