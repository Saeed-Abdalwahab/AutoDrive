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
    public class EmployeeVacationAccountController : Controller
    {
        EmployeeVacationAccountService employeeVacationAccountService = new EmployeeVacationAccountService();
        // GET: HR/EmployeeVacationAccount
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
            int year = DateTime.Now.Year;
            Dictionary<int, int> YearsLST = new Dictionary<int, int>();
            for (int i = year - 2; i <= year + 2; i++)
            {
                YearsLST.Add(i, i);
            }
            ViewBag.YearsLst = new SelectList(YearsLST, "Key", "Value", DateTime.Now.Year);
            return View();
        }
        public JsonResult Save(EmployeeVacationAccountVM employeeVacationAccount)
        {
            if (!(ModelState.IsValid))
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = employeeVacationAccountService.SaveInDataBase(employeeVacationAccount) }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetEmployeeVacationAccount()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeVacationAccountService.GetAll(Language) }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult getEmployeeVacationAccountByID(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeVacationAccountService.getEmployeeVacationAccountByID(id, Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteEmployeeVacationAccount(int id)
        {
            return Json(new { data = employeeVacationAccountService.deleteEmployeeVacationAccount(id) }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult VacationAccountInquiryIndex()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Dictionary<int, string> HolidayType = new Dictionary<int, string>();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                foreach (var item in context.HolidayTypes.ToList())
                {
                    HolidayType.Add(item.ID, item.EnName);
                }
                HolidayType.Add(-1, "All");
            }
            else
            {
                foreach (var item in context.HolidayTypes.ToList())
                {
                    HolidayType.Add(item.ID, item.Name);
                }
                HolidayType.Add(-1, "الكل");
            }
            ViewBag.HolidayTypes = new SelectList(HolidayType, "Key", "Value");
            return View();
        }
        public ActionResult Search(EmployeeVacationAccountVM model)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
          List<EmployeeVacationAccountInquiryVM> employeeVacationAccountInquiriesLst=employeeVacationAccountService.seacrh(model, Language);
            return PartialView(employeeVacationAccountInquiriesLst);
        }
    }
    }