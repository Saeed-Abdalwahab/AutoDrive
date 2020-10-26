﻿using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class CertificationSpecDepartController : Controller
    {
        private CertificationSpecDepartService service;
        public CertificationSpecDepartController()
        {
            service = new CertificationSpecDepartService();
        }

        // GET: HR/CertificationSpecDepart
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CertificationSpecifics = new SelectList(service.CertificationSpecifics(), "ID", "EnName");
            }
            else
            {
                ViewBag.CertificationSpecifics = new SelectList(service.CertificationSpecifics(), "ID", "Name");
            }
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
        public JsonResult Save(CertificationSpecDepartVM CertificationSpecDepartVM)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var Mssg = CertificationSpecDepartVM.ID == 0 ? service.Save(CertificationSpecDepartVM) : service.Edit(CertificationSpecDepartVM);
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
        public JsonResult FetchCertificationspecDepart(string ID)
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                var List = service.certificationSpecDeparts(int.Parse(ID)).ToList().Select(x => new { ID = x.ID, Name = x.EnName });
                return Json(new { data = List }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var List = service.certificationSpecDeparts(int.Parse(ID)).ToList().Select(x => new { ID = x.ID, Name = x.Name });
                return Json(new { data = List }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}