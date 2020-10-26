using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Roles ="SuperAdmin")]
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult LoadMenu(int? ID)
        {

            Session["MenuID"] = ID;

            //if (MenuID ==(int) @AutoDrive.Static.Enums.Menu.HumanResource)
            //{
            //    return PartialView("~/Views/Shared/Partial/_HumanResourceMenu.cshtml");
            //}
            //if (MenuID ==(int) @AutoDrive.Static.Enums.Menu.AutoDriveMain)
            //{
            //    return PartialView("~/Views/Shared/Partial/_AutoDriveMainMenu.cshtml");
            //}
            //if (MenuID ==(int) @AutoDrive.Static.Enums.Menu.Payroll)
            //{
            //    return PartialView("~/Views/Shared/Partial/_PayrollMenu.cshtml");
            //}
            //return PartialView("~/Views/Shared/Partial/_HumanResourceMenu.cshtml");

            return RedirectToAction("index");
        }
    }
}