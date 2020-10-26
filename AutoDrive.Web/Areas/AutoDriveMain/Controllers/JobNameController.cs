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
    public class JobNameController : Controller
    {
        // GET: AutoDriveMain/JobName
        private JobNameBLL jobNameBLL = new JobNameBLL();

        // GET: JobName
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var jobName_List = jobNameBLL.Getall();
            return Json(new { data = jobName_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(jobNameBLL.Get(ID) , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(JobNameVM jobNameVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = jobNameVM_Obj.ID == 0 ? jobNameBLL.Save(jobNameVM_Obj) : jobNameBLL.Edit(jobNameVM_Obj);
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
                return Json(new { status = true, Msg = jobNameBLL.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { msg = jobNameBLL.DeleteDataBaseDataTable(jobNameVM_Obj) }, JsonRequestBehavior.AllowGet);


        }
    }
}