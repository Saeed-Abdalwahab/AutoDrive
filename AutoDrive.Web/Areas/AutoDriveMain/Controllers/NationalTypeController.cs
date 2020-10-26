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
    public class NationalTypeController : Controller
    {
        // GET: AutoDriveMain/NationalType
        private NationalTypeBLL nationalTypeBLL = new NationalTypeBLL();

        // GET: NationalType
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var nationalType_List = nationalTypeBLL.Getall();
            return Json(new { data = nationalType_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(nationalTypeBLL.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(NationalTypeVM nationalTypeVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = nationalTypeVM_Obj.ID == 0 ? nationalTypeBLL.Save(nationalTypeVM_Obj) : nationalTypeBLL.Edit(nationalTypeVM_Obj);
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
                return Json(new { status = true, Msg = nationalTypeBLL.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { msg = jobNameBLL.DeleteDataBaseDataTable(jobNameVM_Obj) }, JsonRequestBehavior.AllowGet);


        }
    }
}