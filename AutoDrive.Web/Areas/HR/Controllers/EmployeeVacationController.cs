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
    public class EmployeeVacationController : Controller
    {
        EmployeeVacationService employeeVacationService = new EmployeeVacationService();
        // GET: HR/EmployeeVacation
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                ViewBag.HolidayTypes = new SelectList(context.HolidayTypes.ToList(), "ID", "EnName");
            }
            else
                ViewBag.HolidayTypes = new SelectList(context.HolidayTypes.ToList(), "ID", "Name");
            ViewBag.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ViewBag.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
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

            return Json(employeeVacationService.SearchEmployee(Text, Language).ToList().Select(x => new { Name = x.Name, ID = x.ID }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(EmployeeVacationVM employeeVacation)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = employeeVacationService.SaveInDataBase(employeeVacation) }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetVacations(string DateFrom,string DateTo)
        {
            DateTime From = Convert.ToDateTime(DateFrom);
            DateTime To = Convert.ToDateTime(DateTo);
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeVacationService.GetAll(Language,From,To) }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult deleteEmployeeVacation(int id)
        {
            return Json(new { data = employeeVacationService.deleteEmployeeVacation(id) }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getEmployeeVacationByID(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeVacationService.getEmployeeVacationByID(Language,id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CalculateDays(DateTime FromDate,DateTime ToDate)
        {
           Double days= (ToDate-FromDate).TotalDays+1;
            return Json(new { data =days }, JsonRequestBehavior.AllowGet);

        }
    }
}