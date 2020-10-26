using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class EmployeeDepartmentController : Controller
    {
        private DepartmentService departmentService = new DepartmentService();
        private EmployeeDepartmentService service = new EmployeeDepartmentService();
        // GET: HR/EmployeeDepartment
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Departments(string Text)
        {
            var  Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            
                return Json( departmentService.Departments(Text,Language).ToList() , JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetForm(int ID=0,int? EmployeeID=null)
        {
            if (ID > 0)
            {
                return PartialView(service.GetEmployeeDepartmentByID(ID));

            }
            EmployeeDepartmentVM vM = new EmployeeDepartmentVM();
            vM.EmployeeId = EmployeeID;
            vM.StartDate = DateTime.Now.Date;
            
            return PartialView(vM);
        }

        public JsonResult Getall(int ID)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                return Json(new { data = service.employeeDepartmentVMs(ID).Select(x => new { ID = x.ID, Name = x.DepartmentEnName, StartDate = x.StartDate, EndDate = x.EndDate }).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = service.employeeDepartmentVMs(ID).Select(x => new { ID = x.ID, Name = x.DepartmentName, StartDate = x.StartDate, EndDate = x.EndDate }).ToList() }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpPost]
        public JsonResult Save(EmployeeDepartmentVM vM)
        {
            var result = service.SaveInDB(vM);
            return result == true ? Json(new { status = true, msg = Messages.SaveSucc }, JsonRequestBehavior.AllowGet) : Json(new { status = false, msg = Messages.SaveErr }, JsonRequestBehavior.AllowGet);
        }
[HttpPost]
        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, msg = service.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}