using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class LicenceCategoryController : Controller
    {

        // GET: AutoDriveMain/LicenceCategory
        private LicenceCategoryBLL LicenceCategoryBLL_Obj = new LicenceCategoryBLL();
        
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.LicenceTypeId = new SelectList(LicenceCategoryBLL_Obj.GetallLicenceType(), "ID", "EnName");
            }
            else {
                ViewBag.CheckUS = "Ar-Egy";

                ViewBag.LicenceTypeId = new SelectList(LicenceCategoryBLL_Obj.GetallLicenceType(), "ID", "Name");

            }
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var LicenceCategory_List = LicenceCategoryBLL_Obj.Getall();
            return Json(new { data = LicenceCategory_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(LicenceCategoryBLL_Obj.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(LicenceCategoryVM LicenceCategoryVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = LicenceCategoryVM_Obj.ID == 0 ? LicenceCategoryBLL_Obj.Save(LicenceCategoryVM_Obj) : LicenceCategoryBLL_Obj.Edit(LicenceCategoryVM_Obj);
                    return Mssg == "" ? Json(new { status = true }, JsonRequestBehavior.AllowGet) : Json(new { status = false, Msg = Mssg }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { status = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json(new { status = false, Msg = Messages.NotValidMsg }, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = LicenceCategoryBLL_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}