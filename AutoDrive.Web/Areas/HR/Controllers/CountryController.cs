using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class CountryController : Controller
    {
        CountryService service = new CountryService();

        // GET: HR/Country
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Getall()
        {
            return Json(new { data = service.Getall() }, JsonRequestBehavior.AllowGet) ;
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(service.Get(ID),JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(CountryVM countryVM)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                 var Mssg=  countryVM.ID==0? service.Save(countryVM):service.Edit(countryVM);
                    return Mssg=="" ?Json(new {status=true }, JsonRequestBehavior.AllowGet): Json(new { status = false, Msg =Mssg }, JsonRequestBehavior.AllowGet);
                }
                catch(Exception ex)
                {
                    return Json(new { status = false,msg=ex.Message }, JsonRequestBehavior.AllowGet);

                }

            }
            else
            {
                return Json(new { status = false,Msg=Messages.NotValidMsg }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult Delete(int ID)
        {
            try { 
            return Json(new { status = true, Msg = service.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}