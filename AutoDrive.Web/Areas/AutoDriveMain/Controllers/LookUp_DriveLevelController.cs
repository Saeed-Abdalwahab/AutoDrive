﻿using AutoDrive.BLL.AutoDriveMain;
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
    public class LookUp_DriveLevelController : Controller
    {
        private LookUp_DriveLevelBLL DriveLevelBLL_Obj = new LookUp_DriveLevelBLL();
        // GET: DriveLevel
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var DriveLevel_List = DriveLevelBLL_Obj.Getall();
            return Json(new { data = DriveLevel_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(DriveLevelBLL_Obj.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(DriveLevelVM DriveLevelVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = DriveLevelVM_Obj.ID == 0 ? DriveLevelBLL_Obj.Save(DriveLevelVM_Obj) : DriveLevelBLL_Obj.Edit(DriveLevelVM_Obj);
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
                return Json(new { status = true, Msg = DriveLevelBLL_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}