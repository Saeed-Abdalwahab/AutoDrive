﻿using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class LicenceTypeHRController : Controller
    {
        // GET: HR/LicenceTypeHR
        LicenceTypeHRService service = new LicenceTypeHRService();

        public ActionResult Index()
        {

            return View();
        }
        public JsonResult Getall()
        {
            return Json(new { data = service.Getall() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(service.Get(ID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(LicenceTypeHRVM LicenceTypeHRVM)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var Mssg = LicenceTypeHRVM.ID == 0 ? service.Save(LicenceTypeHRVM) : service.Edit(LicenceTypeHRVM);
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

        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = service.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
        }
        //Check Name Exist or Not
        public JsonResult NameExist(string Name, string ID)
        {
            return Json(service.NameCheck(Name, int.Parse(ID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ENNameExist(string EnName, string ID)
        {
            return Json(service.ENNameCheck(EnName, int.Parse(ID)), JsonRequestBehavior.AllowGet);
        }
    }
}