using AutoDrive.BLL;
using AutoDrive.DAL.Models;
using AutoDrive.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class IncreasingDeductionSettingController : Controller
    {
        // GET: Payroll/IncreasingDeductionSetting
        IncreasingDeductionSettingService increasingDeductionSettingService = new IncreasingDeductionSettingService();
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                ViewBag.Deductions = new SelectList(context.IncreasesDeductionsTypes.ToList(), "ID", "EnName");
            }
            else
                ViewBag.Deductions = new SelectList(context.IncreasesDeductionsTypes.ToList(), "ID", "Name");
            return View();
        }
        public JsonResult Save(IncreasingDeductionSettingVM increasingDeductionSettingVM)
        {
            if (!(ModelState.IsValid)||(increasingDeductionSettingVM.RatioOrValue==Static.PayingType.Ratio&&increasingDeductionSettingVM.BasicSalaryOrAll==0)||(increasingDeductionSettingVM.RatioOrValue==0) )
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = increasingDeductionSettingService.SaveInDataBase(increasingDeductionSettingVM) }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetIncreasingDeductionSetting()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = increasingDeductionSettingService.GetAll(Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getIncreasingDeductionSettingByID(int id)
        {
            return Json(new { data = increasingDeductionSettingService.GetByID(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteIncreasingDeductionSetting(int id)
        {
            return Json(new { data = increasingDeductionSettingService.Delete(id) }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult CheckName(String Name, int Id)
        {
            return Json(new { msg = increasingDeductionSettingService.CheckName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckENName(String Name, int Id)
        {
            return Json(new { msg = increasingDeductionSettingService.CheckENName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
    }
}