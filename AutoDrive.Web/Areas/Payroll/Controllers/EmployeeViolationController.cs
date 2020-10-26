using AutoDrive.BLL.AutoDrivePayroll;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class EmployeeViolationController : Controller
    {
        EmployeeViolationService employeeViolationService = new EmployeeViolationService();
        // GET: Payroll/EmployeeViolation
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                ViewBag.ViolationTypes = new SelectList(context.ViolationTypes.ToList(), "ID", "EnName");
            }
            else
                ViewBag.ViolationTypes = new SelectList(context.ViolationTypes.ToList(), "ID", "Name");
            int year = DateTime.Now.Year;
            Dictionary<int, int> YearsLST = new Dictionary<int, int>();
            for (int i = year - 2; i <= year + 2; i++)
            {
                YearsLST.Add(i, i);
            }
            ViewBag.YearsLst = new SelectList(YearsLST, "Key", "Value", DateTime.Now.Year);
            return View();
        }
        public JsonResult GetMonayORMoral(int id)
        {
            return Json(new { data = employeeViolationService.GetMonayORMoral(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(EmployeeViolationVM model)
        {
            int MoneyORMoral = employeeViolationService.GetMonayORMoral(model.ViolationTypeId);
            if (!(ModelState.IsValid) || (model.ViolationStatus== 0 && model.ID!=0) ||(MoneyORMoral==(int)ViolationType.FromMoney && model.ViolationValue==null)|| (MoneyORMoral == (int)ViolationType.FromMoney && model.FromMonth == 0)|| (MoneyORMoral == (int)ViolationType.FromMoney && model.FromYear == 0))
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = employeeViolationService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
               
            }


        }
        public JsonResult GetViolationEmployee()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeViolationService.GetAll(Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(int id)
        {
            return Json(new { data = employeeViolationService.Delete(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getEmployeeViolationByID(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeViolationService.GetByID(id, Language) }, JsonRequestBehavior.AllowGet);
        }
    }
}