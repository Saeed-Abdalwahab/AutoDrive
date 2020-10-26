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
    public class MotivationEmployeeController : Controller
    {
        private MotivationEmployeeService motivationEmployeeService = new MotivationEmployeeService();
        // GET: Payroll/MotivationEmployee
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                ViewBag.MotivationTypes = new SelectList(context.MotivationTypes.ToList(), "ID", "EnName");
            }
            else
            ViewBag.MotivationTypes = new SelectList(context.MotivationTypes.ToList(), "ID", "Name");
            
            ViewBag.Employees = new SelectList(context.Employees.ToList(), "ID", "Name");
            int year = DateTime.Now.Year;
            Dictionary<int, int> YearsLST = new Dictionary<int, int>();
            for (int i = year-2; i <= year+2; i++)
            {
                YearsLST.Add(i, i);
            }
            ViewBag.YearsLst = new SelectList(YearsLST, "Key", "Value",DateTime.Now.Year);
            return View();
        }
        public JsonResult Save(MotivationEmployeeVM model)
        {
            if (ModelState.IsValid )
             {
                return Json(new { msg = motivationEmployeeService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult GetMotivationEmployee()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = motivationEmployeeService.GetAll(Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(int id)
        {
            return Json(new { data = motivationEmployeeService.Delete(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getMotivationEmployeeByID(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = motivationEmployeeService.GetByID(id, Language) }, JsonRequestBehavior.AllowGet);
        }
    }
}