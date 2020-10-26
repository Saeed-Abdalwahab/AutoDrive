using AutoDrive.BLL.AutoDrivePayroll;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class JobDegreeController : Controller
    {
        private JobDegreeService jobDegreeService = new JobDegreeService();
        // GET: Payroll/JobDegree
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Save(JobDegreeVM model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { msg = jobDegreeService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult CheckName(String Name,int Id)
        {
            return Json(new { msg = jobDegreeService.CheckName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckENName(String Name, int Id)
        {
            return Json(new { msg = jobDegreeService.CheckENName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckDegreeSort(int DegreeSort, int Id)
        {
            return Json(new { msg = jobDegreeService.CheckDegreeSort(DegreeSort, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetJobDegree()
        {
            return Json(new { data = jobDegreeService.GetAll() }, JsonRequestBehavior.AllowGet);
        }
        public void delete(int id)
        {
            jobDegreeService.Delete(id);
        }
        public JsonResult getJobDegreeByID(int id)
        {
            return Json(new { data = jobDegreeService.GetByID(id) }, JsonRequestBehavior.AllowGet);
        }
    }
}