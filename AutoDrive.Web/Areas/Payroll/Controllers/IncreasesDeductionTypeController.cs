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
    public class IncreasesDeductionTypeController : Controller
    {
        // GET: Payroll/IncreasesDeductionType
        private IncreasesDeductionTypeService increasesDeductionTypeService = new IncreasesDeductionTypeService();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Save(IncreasesDeductionTypeVM model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { msg = increasesDeductionTypeService.SaveInDataBase(model) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult CheckName(String Name, int Id)
        {
            return Json(data: new { msg = increasesDeductionTypeService.CheckName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckENName(String Name, int Id)
        {
            return Json(data: new { msg = increasesDeductionTypeService.CheckEnName(Name, Id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIncreasesDeductionType()
        {
            return Json(new { data = increasesDeductionTypeService.GetAll() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getIncreasesDeductionTypeByID(int id)
        {
            return Json(new { data = increasesDeductionTypeService.GetByID(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult deleteIncreasesDeductionType(int id)
        {
            return Json(new { data = increasesDeductionTypeService.Delete(id) }, JsonRequestBehavior.AllowGet);

        }
    }
}