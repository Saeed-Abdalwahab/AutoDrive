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
    public class EmployeeExperienceController : Controller
    {
        // GET: HR/EmployeeExperience
        private EmployeeExperienceService service = new EmployeeExperienceService();
    

  
        public PartialViewResult GetForm(int ID = 0, int? EmployeeID = null)
        {
            var obj = service.GetEmployeeExperienceByID(ID);
            var List = DateTimeFormatInfo.CurrentInfo.MonthNames.Select((monthName, index) => new { ID = (index + 1).ToString(), Name = monthName }).ToList();
            List.RemoveAt(12);
            var Years = Enumerable.Range(1990, DateTime.Now.Year - 1990 + 1).Select(x => new { ID = x, Name = x }).ToList();


            var Cook = Request.Cookies["Language"];
            var Language = Cook != null && Cook.Value.ToLower() == "En-Us".ToLower() ? "En" : "Ar";


            if (ID > 0)
            {
                ViewBag.Monthes = new SelectList(List.ToList(), "ID", "Name");
                ViewBag.Years = new SelectList(Years, "ID", "Name");

                return PartialView(obj);
            }
            ViewBag.Monthes = new SelectList(List.ToList(), "ID", "Name");
            ViewBag.Years = new SelectList(Years, "ID", "Name");

            EmployeeExperienceVM vM = new EmployeeExperienceVM();
            vM.EmployeeId = EmployeeID;
            
            return PartialView(vM);
        }

        public JsonResult Getall(int ID)
        {
      
                return Json(new { data = service.EmployeeExperienceVMs(ID)
                    .Select(x => new { 
                        ID = x.ID,
                        CompanyName = x.CompanyName,
                        CompanyAddress = x.CompanyAddress ,
                        JobSpecification = x.JobSpecification,
                        FromYear =x.FromYear,
                        ToYear = x.ToYear,
                        FromMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(x.FromMonth),
                        ToMonth = DateTimeFormatInfo.CurrentInfo.GetMonthName(x.ToMonth)

                    }).ToList() }, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        public JsonResult Save(EmployeeExperienceVM vM)
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