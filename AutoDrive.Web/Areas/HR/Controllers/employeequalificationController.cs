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
    public class employeequalificationController : Controller
    {
        // GET: HR/employeequalification

        private EmployeeQualificationService service = new EmployeeQualificationService();



        public PartialViewResult GetForm(int ID = 0, int EmployeeID = 0)
        {
            var obj = service.GetEmployeeQualificationByID(ID);
            EmployeeService EmployeeService = new EmployeeService();
            var List = DateTimeFormatInfo.CurrentInfo.MonthNames.Select((monthName, index) => new { ID = (index + 1).ToString(), Name = monthName }).ToList();
            List.RemoveAt(12);
            var Years = Enumerable.Range(1990, DateTime.Now.Year - 1990 + 1).Select(x => new { ID = x, Name = x }).ToList();
           

            var Cook = Request.Cookies["Language"];
            var Language = Cook != null && Cook.Value.ToLower() == "En-Us".ToLower() ? "En" : "Ar";

            if (ID > 0)
            {
                if (Language == "En")
                {
                    ViewBag.certificationTypes = new SelectList(EmployeeService.certificationTypes(), "ID", "EnName",obj.CetificationTypeId);
                    ViewBag.certificationGrades = new SelectList(EmployeeService.certificationGrades(), "ID", "EnName",obj.CertificationGradeId);
               
                }
                else
                {
                    ViewBag.certificationTypes = new SelectList(EmployeeService.certificationTypes(), "ID", "Name",obj.CetificationTypeId);
                    ViewBag.certificationGrades = new SelectList(EmployeeService.certificationGrades(), "ID", "Name",obj.CertificationGradeId);
                }
                ViewBag.Monthes = new SelectList(List.ToList(), "ID", "Name",obj.GraduationMonth);
                ViewBag.Years = new SelectList(Years, "ID", "Name",obj.GraduationYear);

                return PartialView(obj);

            }
            if (Language == "En")
            {
                ViewBag.certificationTypes = new SelectList(EmployeeService.certificationTypes(), "ID", "EnName");
                ViewBag.certificationGrades = new SelectList(EmployeeService.certificationGrades(), "ID", "EnName");
            }
            else
            {
                ViewBag.certificationTypes = new SelectList(EmployeeService.certificationTypes(), "ID", "Name");
                ViewBag.certificationGrades = new SelectList(EmployeeService.certificationGrades(), "ID", "Name");
            }
            ViewBag.Monthes = new SelectList(List.ToList(), "ID", "Name");
            ViewBag.Years = new SelectList(Years, "ID", "Name");

            EmployeeQualificationVM vM = new EmployeeQualificationVM();
            vM.EmployeeId = EmployeeID;

            return PartialView(vM);
        }

        public JsonResult Getall(int ID)
        {
            var cz = service.EmployeeQualificationVMs(ID);
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                return Json(new
                {
                    data = service.EmployeeQualificationVMs(ID)
                    .Select(x => new {
                        ID = x.ID,
                        CertificationGradeName = x.CertificationGradeENName,
                        CertificationSpecDepartName = x.CertificationSpecDepartEnName,
                        CertificationSpecific = x.CertificationSpecificEnName,
                        CertificationType = x.CertificationTypeEnName,
                        QualificationDiscribtion = x.QualificationDiscribtion,
                        GraduationMonth = x.GraduationMonth == null ? "-" : DateTimeFormatInfo.CurrentInfo.GetMonthName((int)x.GraduationMonth),
                        GraduationYear = x.GraduationYear
                    }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    data = service.EmployeeQualificationVMs(ID)
                     .Select(x => new {
                         ID = x.ID,
                         CertificationGradeName = x.CertificationGradeName,
                         CertificationSpecDepartName = x.CertificationSpecDepartName,
                         CertificationSpecific = x.CertificationSpecificName,
                         CertificationType = x.CertificationTypeName,
                         GraduationMonth = x.GraduationMonth==null?"-": DateTimeFormatInfo.CurrentInfo.GetMonthName((int)x.GraduationMonth),
                         GraduationYear = x.GraduationYear,
                         QualificationDiscribtion = x.QualificationDiscribtion
                     }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Save(EmployeeQualificationVM vM)
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