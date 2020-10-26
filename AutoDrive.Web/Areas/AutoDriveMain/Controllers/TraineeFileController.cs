using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.VM.AutoDriveMainViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class TraineeFileController : Controller
    {
        // GET: AutoDriveMain/TraineeFile
        private TraineeFileBLL traineeFileBLL = new TraineeFileBLL();

        public ActionResult Index(Guid? traineeGuid )
        {

            ViewBag.CodeId = new SelectList(traineeFileBLL.GetallCodeTrainee(), "ID", "Code");

            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {

                ViewBag.LangEn = true;

            }
            else
            {

                ViewBag.LangEn = false;

            }
            TempData["DataStatus"] = "";
            TraineeFileVM traineeFile_VMObj = new TraineeFileVM();
            if (traineeGuid!=null)
            {
               traineeFile_VMObj = traineeFileBLL.GetTraineeFile(traineeGuid);
                ViewBag.CodeId = new SelectList(traineeFileBLL.GetallCodeTrainee(), "ID", "Code", traineeFile_VMObj.ID);
                return View(traineeFile_VMObj);
            }
            return View(traineeFile_VMObj);
        }

        //[HttpGet]
        //public ActionResult GetTraineeFile(TraineeFileVM traineeFile_VMObj)
        //{
        //    var Cook = Request.Cookies["Language"];

        //    if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
        //    {

               
        //    }
        //    else
        //    {

              
        //    }
        //    TraineeFileVM trainee_Obj = traineeFileBLL.GetTraineeFile(traineeFile_VMObj);

        //    if (trainee_Obj != null)
        //    {
        //        if (trainee_Obj.ID > 0)
        //        {
        //            ViewBag.DataCheck = "1";
        //            TempData["DataStatus"] = "DataSaved";
        //        }
        //        else
        //        {
        //            ViewBag.DataCheck = "2";
        //            TempData["DataStatus"] = "DataNotSaved";
        //        }

        //    }
        //    return View(trainee_Obj);
        //}



      

      

        [HttpPost]
        public ActionResult EditTraineeFile(TraineeFileVM traineeFile_VMObj)
        {

            if (ModelState.IsValid)
            {
                var obj = traineeFileBLL.SaveinDataBase(traineeFile_VMObj);

                TempData["DataStatus"] = "تم التعديل بنجاح";
                return RedirectToAction("Index",new { traineeGuid = traineeFile_VMObj.TraineeGuid } );
            }
            else
            {
                TempData["DataStatus"] = "أكمل البيانات";
                return RedirectToAction("Index", new { traineeGuid = traineeFile_VMObj.TraineeGuid });
            }
        }


        [HttpPost]
        public ActionResult DeleteTraineeFile(TraineeFileVM traineeFile_VMObj)
        {
          var obj=  traineeFileBLL.DeleteTraineeFileBLL(traineeFile_VMObj);
           

            TempData["DataStatus"] = "تم الحذف بنجاح";
            return RedirectToAction("Index", new { traineeGuid = traineeFile_VMObj.TraineeGuid });
        }



        public JsonResult GetTraineeFileByCode(int NCode)
        {
            var data = traineeFileBLL.GetTraineeFileByCodeBLL(NCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}