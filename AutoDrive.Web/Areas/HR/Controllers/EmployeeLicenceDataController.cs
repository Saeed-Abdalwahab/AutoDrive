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
    public class EmployeeLicenceDataController : Controller
    {
        // GET: HR/EmployeeLicenceData
        private DepartmentService departmentService = new DepartmentService();
        private EmployeeLicenceDataService service = new EmployeeLicenceDataService();
       
       

        public PartialViewResult GetForm(int ID = 0, int EmployeeID = 0)
        {
            var EmployeeLiencesObj = service.GetEmployeeLicenceDataByID(ID);
         EmployeeService EmployeeService=new EmployeeService();
            var Cook = Request.Cookies["Language"];
            var Language = Cook != null && Cook.Value.ToLower() == "En-Us".ToLower() ? "En" : "Ar";
          
            if (ID > 0)
            {
                if (Language == "En")
                {
                    ViewBag.areas = new SelectList(EmployeeService.areas(), "ID", "EnName", EmployeeLiencesObj.SourceAreaId);
                    ViewBag.licenceTypeHRs = new SelectList(EmployeeService.licenceTypeHRs(), "ID", "EnName", EmployeeLiencesObj.LicenceTypeHRId);

                }
                else
                {
                    ViewBag.areas = new SelectList(EmployeeService.areas(), "ID", "Name", EmployeeLiencesObj.SourceAreaId);
                    ViewBag.licenceTypeHRs = new SelectList(EmployeeService.licenceTypeHRs(), "ID", "Name", EmployeeLiencesObj.LicenceTypeHRId);

                }
                return PartialView(EmployeeLiencesObj);

            }
            if (Language == "En")
            {
                ViewBag.areas = new SelectList(EmployeeService.areas(), "ID", "EnName");
                ViewBag.licenceTypeHRs = new SelectList(EmployeeService.licenceTypeHRs(), "ID", "EnName");

            }
            else
            {
                ViewBag.areas = new SelectList(EmployeeService.areas(), "ID", "Name");
                ViewBag.licenceTypeHRs = new SelectList(EmployeeService.licenceTypeHRs(), "ID", "Name");

            }
            EmployeeLicenceDataVM vM = new EmployeeLicenceDataVM();
            vM.EmployeeId = EmployeeID;

            return PartialView(vM);
        }

        public JsonResult Getall(int ID)
        {
            var cz = service.EmployeeLicenceDataVMs(ID);
            var Cook = Request.Cookies["Language"];
     
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
            return Json(new { data = service.EmployeeLicenceDataVMs(ID)
                .Select(x => new {
                    ID = x.ID,
                    LicenceKindHRName = x.LicenceKindHREnName,
                    LicenceTypeHRName = x.LicenceTypeHREnName,
                    SourceAreaName = x.SourceAreaEnName,
                    LicenseNumber = x.LicenseNumber,
                    ReleaseDate = x.ReleaseDate,
                    EndDate = x.EndDate 
                }).ToList() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = service.EmployeeLicenceDataVMs(ID)
                     .Select(x => new {
                         ID = x.ID,
                         LicenceKindHRName = x.LicenceKindHRName,
                         LicenceTypeHRName = x.LicenceTypeHRName,
                         SourceAreaName = x.SourceAreaName,
                         LicenseNumber = x.LicenseNumber,
                         ReleaseDate = x.ReleaseDate,
                         EndDate = x.EndDate
                     }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Save(EmployeeLicenceDataVM vM)
        {
            var result = service.SaveInDB(vM);
            return result == true ? Json(new { status = true, msg = Messages.SaveSucc  }, JsonRequestBehavior.AllowGet) : Json(new { status = false, msg = Messages.SaveErr + " \n \t" + Messages.DataSavedBefor }, JsonRequestBehavior.AllowGet);
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