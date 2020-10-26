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
    public class TestResultController : Controller
    {
        private TestResultBLL TestResultBLL_Obj = new TestResultBLL();

        // GET: TestResult
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var TestResult_List = TestResultBLL_Obj.Getall();
            return Json(new { data = TestResult_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(TestResultBLL_Obj.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(TestResultVM TestResultVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = TestResultVM_Obj.ID == 0 ? TestResultBLL_Obj.Save(TestResultVM_Obj) : TestResultBLL_Obj.Edit(TestResultVM_Obj);
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
                return Json(new { status = true, Msg = TestResultBLL_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { msg = jobNameBLL.DeleteDataBaseDataTable(jobNameVM_Obj) }, JsonRequestBehavior.AllowGet);


        }
    }
}