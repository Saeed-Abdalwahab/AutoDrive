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
    public class SpeechLanguageController : Controller
    {
        // GET: AutoDriveMain/SpeechLanguage
        private SpeechLanguageBLL SpeechLanguageBLL_Obj = new SpeechLanguageBLL();

        // GET: SpeechLanguage
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult Getall()
        {

            var SpeechLanguage_List = SpeechLanguageBLL_Obj.Getall();
            return Json(new { data = SpeechLanguage_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {


            return Json(SpeechLanguageBLL_Obj.Get(ID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(SpeechLanguageVM SpeechLanguageVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = SpeechLanguageVM_Obj.ID == 0 ? SpeechLanguageBLL_Obj.Save(SpeechLanguageVM_Obj) : SpeechLanguageBLL_Obj.Edit(SpeechLanguageVM_Obj);
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
                return Json(new { status = true, Msg = SpeechLanguageBLL_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}