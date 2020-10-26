using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class TestController : Controller
    {
        private TestBLL TestBLL_Obj = new TestBLL();

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var Test_List = TestBLL_Obj.Getall();
            return Json(new { data = Test_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(TestBLL_Obj.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(TestVM TestVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = TestVM_Obj.ID == 0 ? TestBLL_Obj.Save(TestVM_Obj) : TestBLL_Obj.Edit(TestVM_Obj);
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
                return Json(new { status = true, Msg = TestBLL_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { msg = jobNameBLL.DeleteDataBaseDataTable(jobNameVM_Obj) }, JsonRequestBehavior.AllowGet);


        }
    }
}