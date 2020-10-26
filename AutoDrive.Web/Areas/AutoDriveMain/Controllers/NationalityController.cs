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
    public class NationalityController : Controller
    {
        // GET: AutoDriveMain/Nationality
        private NationalityBLL nationalityBLL = new NationalityBLL();

        // GET: Nationality
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var jobName_List = nationalityBLL.Getall();
            return Json(new { data = jobName_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(nationalityBLL.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(NationalityVM nationalityVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = nationalityVM_Obj.ID == 0 ? nationalityBLL.Save(nationalityVM_Obj) : nationalityBLL.Edit(nationalityVM_Obj);
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
                return Json(new { status = true, Msg = nationalityBLL.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { msg = jobNameBLL.DeleteDataBaseDataTable(jobNameVM_Obj) }, JsonRequestBehavior.AllowGet);


        }
    }
}