using AutoDrive.BLL;
using AutoDrive.BLL.AutoDrivePayroll;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class BasicSalarySettingController : Controller
    {
        // GET: Payroll/BasicSalarySetting
        BasicSalarySettingService basicSalarySettingService = new BasicSalarySettingService();
        public ActionResult Index()
        {

            ApplicationDbContext context = new ApplicationDbContext();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                ViewBag.JobDegrees = new SelectList(context.JobDegrees.ToList(), "ID", "ENName");
                ViewBag.JobLevels = new SelectList(context.JobLevels.ToList(), "ID", "EnName");
            }
            else
            {
                ViewBag.JobDegrees = new SelectList(context.JobDegrees.ToList(), "ID", "Name");
                ViewBag.JobLevels = new SelectList(context.JobLevels.ToList(), "ID", "Name");
            }
           
            return View();
        }
        public JsonResult Save(BasicSalarySettingVM model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { msg = basicSalarySettingService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }

          
        }
        public JsonResult GetBasicSalarySetting()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = basicSalarySettingService.GetAll(Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getBasicSalarySettingByID(int id) { 
            return Json(new { data = basicSalarySettingService.GetByID(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(int id)
        {
            return Json(new { data = basicSalarySettingService.Delete(id) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IncreasesDeductionsView(int id)
        {
            IncreasingDeductionSettingService increasingDeductionSettingService = new IncreasingDeductionSettingService();
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            ViewBag.IncreasesDeductions = increasingDeductionSettingService.GetAll(Language);
            List<AddIncreasingDeductionToJob> addIncreasingDeductionToJobsLst = basicSalarySettingService.GetAddIncreasingDeductionToJobs().Where(AIDJ => AIDJ.BasicSalarySettingId == id).ToList();
            ViewBag.AddIncreasesDeduction = (from addIncreasesDeduction in addIncreasingDeductionToJobsLst
                                             select addIncreasesDeduction.IncreasingDeductionSettingId).ToList();
            ViewBag.BasicSalaryID = id;
            return View("");
       }
        public void SaveAddingIncreasesDeductionToJob(int BasicSalaryId,int IncresaesDeductionSettingId)
        {
            basicSalarySettingService.SaveAddingIncreasesDeductionToJob(BasicSalaryId, IncresaesDeductionSettingId);
        }
        public void DeleteAddingIncreasesDeductionToJob(int BasicSalaryId, int IncresaesDeductionSettingId)
        {
            basicSalarySettingService.DeleteAddingIncreasesDeductionToJob(BasicSalaryId, IncresaesDeductionSettingId);
        }


    }

}

