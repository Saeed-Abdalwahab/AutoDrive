using AutoDrive.BLL.AutoDrivePayroll;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class JobLevelController : Controller
    {
        // GET: Payroll/JobLevel
        private JobLevelService JobLevelService = new JobLevelService();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveJobLevel(JobLevelVM model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { msg = JobLevelService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult CheckName(String Name, int Id)
      {
            return Json(new { msg = JobLevelService.CheckName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckLevelSort(int LevelSort, int Id)
        {
            return Json(new { msg = JobLevelService.CheckLevelSort(LevelSort, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckENName(String Name, int Id)
        {
            return Json(new { msg = JobLevelService.CheckENName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetJobLevel()
        {
            return Json(new { data = JobLevelService.GetALL() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(int id)
        {
            return Json(new { data = JobLevelService.GetById(id) }, JsonRequestBehavior.AllowGet);
        }
        public void Delete(int id)
        {
             JobLevelService.Delete(id);
        }
    }
}