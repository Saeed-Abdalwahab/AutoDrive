using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.VM.AutoDriveMainViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class TraineeEvaluationController : Controller
    {
        // GET: AutoDriveMain/TraineeEvaluation
        private TraineeEvaluationBLL traineeEvaluationBLL = new TraineeEvaluationBLL();

        public ActionResult Index(TraineeEvaluationVM traineeEvaluation_VMObj=null)
        {

            TraineeEvaluationVM vm_obj = new TraineeEvaluationVM();
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeId = new SelectList(traineeEvaluationBLL.GetallTrainees(), "ID", "EnName");
                ViewBag.LicenceTypeId = new SelectList(traineeEvaluationBLL.GetallLicenceTypes(), "ID", "EnName");
                //ViewBag.LicenceCategoryId = new SelectList(traineeEvaluationBLL.GetallLicenceCategories(), "ID", "EnName");
                ViewBag.StudyPeriodSettingId = new SelectList(traineeEvaluationBLL.GetallStudyPeriodSettings(), "ID", "EnName");
                ViewBag.EvaluationEmployeeId = new SelectList(traineeEvaluationBLL.GetallEvaluationEmployees(), "ID", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.TraineeId = new SelectList(traineeEvaluationBLL.GetallTrainees(), "ID", "ArName");
                ViewBag.LicenceTypeId = new SelectList(traineeEvaluationBLL.GetallLicenceTypes(), "ID", "Name");
                //ViewBag.LicenceCategoryId = new SelectList(traineeEvaluationBLL.GetallLicenceCategories(), "ID", "Name");
                ViewBag.StudyPeriodSettingId = new SelectList(traineeEvaluationBLL.GetallStudyPeriodSettings(), "ID", "Name");
                ViewBag.EvaluationEmployeeId = new SelectList(traineeEvaluationBLL.GetallEvaluationEmployees(), "ID", "Name");

                
            }
            if (traineeEvaluation_VMObj == null)
            {
                Session["TraineeFound"] = "0";
                Session["EnTraineeFound"] = "0";
            }
            else {

            }
            ViewBag.DataCheck = "0";
            return View(vm_obj);
        }

        [HttpGet]
        public ActionResult GetTraineeEvaluation(TraineeEvaluationVM traineeEvaluation_VMObj)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {

                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeId = new SelectList(traineeEvaluationBLL.GetallTrainees(), "ID", "EnName");
                ViewBag.LicenceTypeId = new SelectList(traineeEvaluationBLL.GetallLicenceTypes(), "ID", "EnName");
                //ViewBag.LicenceCategoryId = new SelectList(traineeEvaluationBLL.GetallLicenceCategories(), "ID", "EnName");
                ViewBag.StudyPeriodSettingId = new SelectList(traineeEvaluationBLL.GetallStudyPeriodSettings(), "ID", "EnName");
                ViewBag.EvaluationEmployeeId = new SelectList(traineeEvaluationBLL.GetallEvaluationEmployees(), "ID", "Name");

            }
            else
            {

                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.TraineeId = new SelectList(traineeEvaluationBLL.GetallTrainees(), "ID", "ArName");
                ViewBag.LicenceTypeId = new SelectList(traineeEvaluationBLL.GetallLicenceTypes(), "ID", "Name");
                //ViewBag.LicenceCategoryId = new SelectList(traineeEvaluationBLL.GetallLicenceCategories(), "ID", "Name");
                ViewBag.StudyPeriodSettingId = new SelectList(traineeEvaluationBLL.GetallStudyPeriodSettings(), "ID", "Name");
                ViewBag.EvaluationEmployeeId = new SelectList(traineeEvaluationBLL.GetallEvaluationEmployees(), "ID", "Name");

            }
            TraineeEvaluationVM traineeEvaluation_Obj = traineeEvaluationBLL.Getall(traineeEvaluation_VMObj);

            if (traineeEvaluation_Obj != null)
            {
                if (traineeEvaluation_Obj.ID > 0)
                {
                    ViewBag.DataCheck = "1";
                    TempData["DataStatus"] = "DataSaved";
                }
                else
                {
                    ViewBag.DataCheck = "2";
                    TempData["DataStatus"] = "DataNotSaved";
                }

            }
            return View(traineeEvaluation_Obj);
        }



        [HttpGet]
        public JsonResult GetTraineeEvaluation_byId(int ID)
        {

            return Json(new { data = traineeEvaluationBLL.GetByID(ID) }, JsonRequestBehavior.AllowGet);
        }


       

        public JsonResult GetTraineeCode_Age_Photo(int TraineeId)
        {

            TraineeEvaluationVM obj = traineeEvaluationBLL.GetTraineeCode_Age_PhotoBLL(TraineeId);


            string fname = obj.TraineePhotopath;
            string fPath = Server.MapPath("~/Upload/");
            string fPathName = Path.Combine(fPath, fname);

            string fullPath = fPathName;
            if (System.IO.File.Exists(fullPath))
            {
                
            }
            else {
                obj.TraineePhotopath = "t.png";
            }

           
            return Json(new { obj = obj }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetLicenceGategoryByLicenceTypeId(int licenceId)
        {
            var data = traineeEvaluationBLL.GetLicenceGategoryByLicenceTypeBLL(licenceId);
            return Json( data , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateTraineeEvaluation(TraineeEvaluationVM traineeEvaluation_VMObj)
        {
            bool objNotFound = traineeEvaluationBLL.SaveinDataBase(traineeEvaluation_VMObj);
            if (objNotFound==true)
            {

                Session["TraineeFound"] = "تم الحفظ بنجاح"; ;
                Session["EnTraineeFound"] = "Created Successfully";
                //TraineeEvaluationVM obj
                return RedirectToAction("Index"/*, obj*/);
            }
            else {
                Session["TraineeFound"] = "هذا التقييم موجود من قبل";
                Session["EnTraineeFound"] = "Trainee Evaluation Aready Used";
                return RedirectToAction("Index", traineeEvaluation_VMObj);
            }

        }
    

    [HttpPost]
        public ActionResult EditTraineeEvaluation(TraineeEvaluationVM traineeEvaluation_VMObj)
        {

            if (ModelState.IsValid)
            {
                var obj = traineeEvaluationBLL.SaveinDataBase(traineeEvaluation_VMObj);

                Session["TraineeFound"] = "تم الحفظ بنجاح"; ;
                Session["EnTraineeFound"] = "Edit Successfully";

                return RedirectToAction("GetTraineeEvaluation", traineeEvaluation_VMObj);
            }
            else
            {
                Session["TraineeFound"] = "أكمل البيانات";
                Session["TraineeFound"] = "هذا التقييم موجود من قبل";
                Session["EnTraineeFound"] = "Trainee Evaluation Aready Used";

                return RedirectToAction("GetTraineeEvaluation", traineeEvaluation_VMObj);
            }
        }


        [HttpPost]
        public ActionResult DeleteTraineeEvaluation(TraineeEvaluationVM traineeEvaluation_VMObj)
        {
            traineeEvaluationBLL.Delete(traineeEvaluation_VMObj.ID);

            Session["TraineeFound"] = "تم الحذف بنجاح";
            return RedirectToAction("Index");
        }


        //-----------------------------------------------------------------------------------------------

        
        // GET: TE
        public ActionResult IndexTE()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetTraineeEvaluation_List()
        {

            var traineeEvaluation_List = traineeEvaluationBLL.GetallTraineeEvaluationList();
            return Json(new { data = traineeEvaluation_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTraineeEvaluationDatatable_byId(int ID)
        {
            var obj = traineeEvaluationBLL.GetByID(ID);

            return Json(new { data = obj }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save_TraineeEvaluationDataTable(TraineeEvaluationVM traineeEvaluationVM_Obj)
        {

            return Json(new { msg = traineeEvaluationBLL.SaveinDataBaseDataTable(traineeEvaluationVM_Obj) }, JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        public JsonResult Delete_TraineeEvaluationDataTable(TraineeEvaluationVM traineeEvaluationVM_Obj)
        {

            return Json(new { msg = traineeEvaluationBLL.DeleteDataBaseDataTable(traineeEvaluationVM_Obj) }, JsonRequestBehavior.AllowGet);


        }


        [HttpGet]
        public ActionResult GetDeleteDataTable(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var traineeEvaluationVM_Obj = traineeEvaluationBLL.GetByID((int)ID);
            if (traineeEvaluationVM_Obj == null)
            {
                return HttpNotFound();
            }
            return PartialView(traineeEvaluationVM_Obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteDataTable(int ID)
        {

            var r = traineeEvaluationBLL.Delete(ID);
            return r;
        }
    }
}