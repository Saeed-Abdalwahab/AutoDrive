using AutoDrive.BLL.AutoDrivePayroll;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class ViolationTypeController : Controller
    {
        private ViolationTypeService violationTypeService = new ViolationTypeService();
        // GET: Payroll/ViolationType
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Save(ViolationTypeVM model)
        {
            if (!ModelState.IsValid ||(model.FromMoneyOrMoral==ViolationType.FromMoney && model.FromBaseSalaryOrOverall==0)|| (model.FromMoneyOrMoral == ViolationType.FromMoney && model.DaysNumber == null)||(model.FromMoneyOrMoral==0))
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
               
                return Json(new { msg = violationTypeService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult CheckName(String Name, int Id)
        {
            return Json(new { msg = violationTypeService.CheckName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckENName(String Name, int Id)
        {
            return Json(new { msg = violationTypeService.CheckENName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetViolationType()
        {
            
            return Json(new { data = violationTypeService.GetAll() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getViolationTypeByID(int id)
        {
            return Json(new { data = violationTypeService.GetByID(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(int id)
        {
            return Json(new { data = violationTypeService.Delete(id) }, JsonRequestBehavior.AllowGet);
        }
    }
}