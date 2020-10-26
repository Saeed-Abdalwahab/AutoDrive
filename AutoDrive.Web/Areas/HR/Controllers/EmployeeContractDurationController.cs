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
    public class EmployeeContractDurationController : Controller
    {
        // GET: HR/EmployeeContractDuration

        private EmployeeContractDurationService service = new EmployeeContractDurationService();



        public PartialViewResult GetForm(int ID = 0, int EmployeeID = 0)
        {
            var obj = service.GetEmployeeContractDurationByID(ID);
            EmployeeService EmployeeService = new EmployeeService();
            var Cook = Request.Cookies["Language"];
            var Language = Cook != null && Cook.Value.ToLower() == "En-Us".ToLower() ? "En" : "Ar";

            if (ID > 0)
            {
                if (Language == "En")
                {
                    ViewBag.employeeStatus = new SelectList(EmployeeService.employeeStatus(), "ID", "EnName",obj.EmployeeStatusId);

                }
                else
                {
                    ViewBag.employeeStatus = new SelectList(EmployeeService.employeeStatus(), "ID", "Name", obj.EmployeeStatusId);

                }
                return PartialView(obj);

            }
            if (Language == "En")
            {
                ViewBag.employeeStatus = new SelectList(EmployeeService.employeeStatus(), "ID", "EnName");

            }
            else
            {
                ViewBag.employeeStatus = new SelectList(EmployeeService.employeeStatus(), "ID", "Name");


            }
            EmployeeContractDurationVM vM = new EmployeeContractDurationVM();
            vM.EmployeeId = EmployeeID;
            vM.FromDate = DateTime.Now.Date;
            

            return PartialView(vM);
        }

        public JsonResult Getall(int ID)
        {
            var cz = service.EmployeeContractDurationVMs(ID);
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                return Json(new
                {
                    data = service.EmployeeContractDurationVMs(ID)
                    .Select(x => new {
                        ID = x.ID,
                        EmployeeStatusName = x.EmployeeStatusEnName,
                        EmployeeStatusKindName = x.EmployeeStatusKindEnName,
                        Duration = x.Duration,
                        EndDate = x.EndDate,
                        FromDate = x.FromDate
                    }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    data = service.EmployeeContractDurationVMs(ID)
                     .Select(x => new {
                         ID = x.ID,
                         Duration = x.Duration,
                         EmployeeStatusName = x.EmployeeStatusName,
                         EmployeeStatusKindName = x.EmployeeStatusKindName,
                         EndDate = x.EndDate,
                         FromDate = x.FromDate
                     }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Save(EmployeeContractDurationVM vM)
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