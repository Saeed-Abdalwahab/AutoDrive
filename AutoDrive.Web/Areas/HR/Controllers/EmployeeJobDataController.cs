using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class EmployeeJobDataController : Controller
    {
        // GET: HR/EmployeeJobData
        private EmployeeJobDataService service = new EmployeeJobDataService();



        public PartialViewResult GetForm(int ID = 0, int EmployeeID = 0)
        {
            var obj = service.GetEmployeeJobDataByID(ID);
            EmployeeService EmployeeService = new EmployeeService();

                 var Cook = Request.Cookies["Language"];
            var Language = Cook != null && Cook.Value.ToLower() == "En-Us".ToLower() ? "En" : "Ar";
            if (Language == "En")
            {
                ViewBag.JobDegrees = new SelectList(EmployeeService.JobDegrees(), "ID", "EnName");
                ViewBag.CarrerFields = new SelectList(EmployeeService.CarrerFields(), "ID", "EnName");
                ViewBag.JobLevels = new SelectList(EmployeeService.JobLevels(), "ID", "EnName");
                ViewBag.JobNames = new SelectList(EmployeeService.JobNames(), "ID", "EnName");
                ViewBag.JobFunctions = new SelectList(EmployeeService.JobFunctions(), "ID", "EnName");
            }
            else
            {
                ViewBag.JobDegrees = new SelectList(EmployeeService.JobDegrees(), "ID", "Name");
                ViewBag.CarrerFields = new SelectList(EmployeeService.CarrerFields(), "ID", "Name");
                ViewBag.JobLevels = new SelectList(EmployeeService.JobLevels(), "ID", "Name");
                ViewBag.JobNames = new SelectList(EmployeeService.JobNames(), "ID", "Name");
                ViewBag.JobFunctions = new SelectList(EmployeeService.JobFunctions(), "ID", "Name");
            }
            if (ID > 0)
            {
              
                return PartialView(obj);
            }
       

            EmployeeJobDataVM vM = new EmployeeJobDataVM();
            vM.EmployeeId = EmployeeID;
            vM.StartDate = DateTime.Now.Date;

            return PartialView(vM);
        }

        public JsonResult Getall(int ID)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                return Json(new
                {
                    data = service.EmployeeJobDataVMs(ID)
            .Select(x => new {
                ID = x.ID,
                JobDegreeName = x.JobDegreeEnName,
                JobFunctionName = x.JobFunctionEnName,
                JobLevelName = x.JobLevelEnName,
                JobNameName = x.JobNameEnName,
                CarrerFieldName = x.CarrerFieldEnName,
                StartDate = x.StartDate,
                EndDate = x.EndDate

            }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    data = service.EmployeeJobDataVMs(ID)
            .Select(x => new {
                ID = x.ID,
                JobDegreeName = x.JobDegreeName,
                JobFunctionName = x.JobFunctionName,
                JobLevelName = x.JobLevelName,
                JobNameName = x.JobNameName,
                CarrerFieldName = x.CarrerFieldName,
                StartDate = x.StartDate,
                EndDate = x.EndDate

            }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            

        }
        [HttpPost]
        public JsonResult Save(EmployeeJobDataVM vM)
        {
            var result = service.SaveInDB(vM);
            return result == true ? Json(new { status = true, msg = Messages.SaveSucc }, JsonRequestBehavior.AllowGet) : Json(new { status = false, msg = Messages.SaveErr +"\n \t"+Messages.DataSavedBefor }, JsonRequestBehavior.AllowGet);
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