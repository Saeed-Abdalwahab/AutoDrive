using AutoDrive.BLL.HRAutoDrive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class LicenceKindHRController : Controller
    {
        private LicenceKindHRServices LicenceKindHRServices;
        public LicenceKindHRController()
        {
            LicenceKindHRServices = new LicenceKindHRServices();
        }

        // GET: HR/LicenceKindHR
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult FetchLicenceKidHr(string LicencTypID)
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower()) { 
                var List = LicenceKindHRServices.licenceKindHRs(int.Parse(LicencTypID)).ToList().Select(x => new { ID = x.ID, Name = x.EnName });
            return Json(new { data = List },JsonRequestBehavior.AllowGet);
            }
            else
            {
                var List = LicenceKindHRServices.licenceKindHRs(int.Parse(LicencTypID)).ToList().Select(x => new { ID = x.ID, Name = x.Name });
                return Json(new { data = List }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}