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
    public class LicenceTypeController : Controller
    {


        // GET: AutoDriveMain/LicenceType
        private LicenceTypeBLL LicenceTypeBLL_Obj = new LicenceTypeBLL();

        // GET: LicenceType
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var LicenceType_List = LicenceTypeBLL_Obj.Getall();
            return Json(new { data = LicenceType_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(LicenceTypeBLL_Obj.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(LicenceTypeVM LicenceTypeVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = LicenceTypeVM_Obj.ID == 0 ? LicenceTypeBLL_Obj.Save(LicenceTypeVM_Obj) : LicenceTypeBLL_Obj.Edit(LicenceTypeVM_Obj);
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
                return Json(new { status = true, Msg = LicenceTypeBLL_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}