using AutoDrive.BLL.AutoDrivePayroll;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDrivePayroll;
using System.Web.Script.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class VacationDeductionsController : Controller
    {
        VacationDeductionsService vacationDeductionsService = new VacationDeductionsService();
        public List<MonthVM> GetMonthsList()
        {
            List<MonthVM> MonthsLst = new List<MonthVM>();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                MonthsLst.Add(new MonthVM() { key = 1, Value = "January" });
                MonthsLst.Add(new MonthVM { key = 2, Value = "Febeuary" });
                MonthsLst.Add(new MonthVM() { key = 3, Value = "March" });
                MonthsLst.Add(new MonthVM() { key = 4, Value = "April" });
                MonthsLst.Add(new MonthVM() { key = 5, Value = "May" });
                MonthsLst.Add(new MonthVM() { key = 6, Value = "June" });
                MonthsLst.Add(new MonthVM() { key = 7, Value = "July" });
                MonthsLst.Add(new MonthVM() { key = 8, Value = "August" });
                MonthsLst.Add(new MonthVM() { key = 9, Value = "September" });
                MonthsLst.Add(new MonthVM() { key = 10, Value = "October" });
                MonthsLst.Add(new MonthVM() { key = 11, Value = "November" });
                MonthsLst.Add(new MonthVM() { key = 12, Value = "December" });
            }
            else
            {
                MonthsLst.Add(new MonthVM() { key = 1, Value = "يناير" });
                MonthsLst.Add(new MonthVM { key = 2, Value = "فبراير" });
                MonthsLst.Add(new MonthVM() { key = 3, Value = "مارس" });
                MonthsLst.Add(new MonthVM() { key = 4, Value = "ابريل" });
                MonthsLst.Add(new MonthVM() { key = 5, Value = "مايو" });
                MonthsLst.Add(new MonthVM() { key = 6, Value = "يونيو" });
                MonthsLst.Add(new MonthVM() { key = 7, Value = "يوليو" });
                MonthsLst.Add(new MonthVM() { key = 8, Value = "اغسطس" });
                MonthsLst.Add(new MonthVM() { key = 9, Value = "ستمبر" });
                MonthsLst.Add(new MonthVM() { key = 10, Value = "اكتوبر" });
                MonthsLst.Add(new MonthVM() { key = 11, Value = "نوفمبر" });
                MonthsLst.Add(new MonthVM() { key = 12, Value = "ديسمبر" });
            }
            return MonthsLst;
        }
        public List<YearVM> GetYearsList()
        {
            int year = DateTime.Now.Year;
            List<YearVM> YearsLST = new List<YearVM>();
            for (int i = year - 2; i <= year + 2; i++)
            {
                YearsLST.Add(new YearVM() { key = i, Value = i });
            }
            return YearsLST;
        }
        // GET: Payroll/VacationDeductions
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            int year = DateTime.Now.Year;
            Dictionary<int, int> YearsLST = new Dictionary<int, int>();
            for (int i = year - 2; i <= year + 2; i++)
            {
                YearsLST.Add(i, i);
            }
            ViewBag.YearsLst = new SelectList(YearsLST, "Key", "Value", DateTime.Now.Year);
            Dictionary<int, string> HolidayType = new Dictionary<int, string>();
            Dictionary<int, string> DisplayedVacation = new Dictionary<int, string>();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                foreach (var item in context.HolidayTypes.ToList())
                {
                    HolidayType.Add(item.ID, item.EnName);
                }
                HolidayType.Add(-1, "All");
                ViewBag.HolidayTypes = new SelectList(HolidayType, "Key", "Value");
                DisplayedVacation.Add(1, "Not Discounted");
                DisplayedVacation.Add(2, "Discounted");
                ViewBag.DisplayedVacation = new SelectList(DisplayedVacation, "Key", "Value");

            }
            else
            {
                foreach (var item in context.HolidayTypes.ToList())
                {
                    HolidayType.Add(item.ID, item.Name);
                }
                HolidayType.Add(-1, "الكل");
                ViewBag.HolidayTypes = new SelectList(HolidayType, "Key", "Value");
                DisplayedVacation.Add(1, "غير المخصومة");
                DisplayedVacation.Add(2, "المخصومة");
                ViewBag.DisplayedVacation = new SelectList(DisplayedVacation, "Key", "Value");
            }
            List<MonthVM> MonthsLst = GetMonthsList();
            ViewBag.Months= MonthsLst;
            ViewBag.Years = GetYearsList();
            return View();
        }
        public JsonResult Search(VacationDeductionFiltersVM model)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { msg = vacationDeductionsService.Search(model, Language) }, JsonRequestBehavior.AllowGet);

        }
        public void save(int EmployeeId,int HolidayTypeId,int FirstMonth,int FirstYear,int DaysNumber,string DaysDate,double Value,int Month,int Year)
        {
            vacationDeductionsService.save(EmployeeId, HolidayTypeId, FirstMonth, FirstYear, DaysNumber, DaysDate, Value, Month, Year);
        }
        public void delete(int id)
        {
            vacationDeductionsService.delete(id);
        }
        public JsonResult edit(int Id,double Value,int Month,int Year)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = vacationDeductionsService.edit(Id,Value,Month,Year, Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCheckSalaryStatus(int EmployeeId,int Month,int Year)
        {
            return Json(new { data = vacationDeductionsService.GetCheckSalaryStatus(EmployeeId, Month, Year) }, JsonRequestBehavior.AllowGet);
        }
    }
}