using AutoDrive.BLL.HRAutoDrive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class employeeStatusKindsController : Controller
    {
        // GET: HR/employeeStatusKinds
        private employeeStatusKindservice CertificationSpecificService;
        public employeeStatusKindsController()
        {
            CertificationSpecificService = new employeeStatusKindservice();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult fetchemployestatuskinds(string ID)
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                var List = CertificationSpecificService.employeeStatusKinds(int.Parse(ID)).ToList().Select(x => new { ID = x.ID, Name = x.EnName });
                return Json(new { data = List }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var List = CertificationSpecificService.employeeStatusKinds(int.Parse(ID)).ToList().Select(x => new { ID = x.ID, Name = x.Name });
                return Json(new { data = List }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}