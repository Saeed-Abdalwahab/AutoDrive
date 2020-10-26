using AutoDrive.BLL.AutoDrivePayroll;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class SalariesController : Controller
    {
        SalariesService salariesService = new SalariesService();
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Payroll/Salaries
        public ActionResult Index()
        {
            int year = DateTime.Now.Year;
            Dictionary<int, int> YearsLST = new Dictionary<int, int>();
            for (int i = year - 2; i <= year + 2; i++)
            {
                YearsLST.Add(i, i);
            }
            ViewBag.YearsLst = new SelectList(YearsLST, "Key", "Value", DateTime.Now.Year);
            return View();
        }
        public JsonResult CalculateSalaries(SalariesVM model)
        {
           
            if (!(ModelState.IsValid) || (model.Month == 0) )
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = salariesService.CalculateSalaries(model) }, JsonRequestBehavior.AllowGet);

            }


        }
        public ActionResult IndexSalaryApproval()
        {
            int year = DateTime.Now.Year;
            Dictionary<int, int> YearsLST = new Dictionary<int, int>();
            for (int i = year - 2; i <= year + 2; i++)
            {
                YearsLST.Add(i, i);
            }
            ViewBag.YearsLst = new SelectList(YearsLST, "Key", "Value", DateTime.Now.Year);
            return View();
        }
        public JsonResult ViewSalariesTB(int[] DeptIDs, int Month, int Year, int SalaryStatus)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = salariesService.ViewSalaries(DeptIDs, Month, Year, SalaryStatus,Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EmployeeSalaryApproval(int id)
        {
            return Json(new { data = salariesService.EmployeeSalaryApproval(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EmployeesSalariesApproval(int[] EmployeeMoneyIDs)
        {
           return Json(new { data = salariesService.EmployeesSalariesApproval(EmployeeMoneyIDs) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowSalaryVocabulary(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            ViewBag.Language = Language;
            IEnumerable<EmployeeMoneyDetail> EmployeeMoneyDetials = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == id).OrderByDescending(EMD=>EMD.EmployeeMoneyTypeDetailsId).ToList();
            return PartialView(EmployeeMoneyDetials);
        }
        public ActionResult EmployeeSalaryIndex()
        {
            int year = DateTime.Now.Year;
            Dictionary<int, int> YearsLST = new Dictionary<int, int>();
            for (int i = year - 2; i <= year + 2; i++)
            {
                YearsLST.Add(i, i);
            }
            ViewBag.YearsLst = new SelectList(YearsLST, "Key", "Value", DateTime.Now.Year);
            return View();
        }
        public ActionResult Search(InquiryEmployeeSalaryVM model)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            SearchedEmployeeSalaryVM searchedEmployeeSalary= salariesService.Search(model, Language);
            return PartialView(searchedEmployeeSalary);
        }
    }
}