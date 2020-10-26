using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveMainViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class TraineeController : Controller
    {
        // GET: AutoDriveMain/Trainee
        private TraineeBLL traineeBLL = new TraineeBLL();

        public ActionResult Index(Guid? traineeGuid)
        {

            
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                
                ViewBag.CheckUS = "En-Us";
                ViewBag.Nationality = new SelectList(traineeBLL.GetallNatoinalities(), "ID", "EnName");
                ViewBag.Religion = new SelectList(traineeBLL.GetallReligions(), "ID", "EnName");
                ViewBag.NationalType = new SelectList(traineeBLL.GetallNationalTypes(), "ID", "EnName");
                ViewBag.SpeechLanguage = new SelectList(traineeBLL.GetallSpeechLanguages(), "ID", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.Nationality = new SelectList(traineeBLL.GetallNatoinalities(), "ID", "Name");
                ViewBag.Religion = new SelectList(traineeBLL.GetallReligions(), "ID", "Name");
                ViewBag.NationalType = new SelectList(traineeBLL.GetallNationalTypes(), "ID", "Name");
                ViewBag.SpeechLanguage = new SelectList(traineeBLL.GetallSpeechLanguages(), "ID", "Name");

            }

            ViewBag.DataCheck = "0";
            Session["En_msg"]="0";
            Session["Ar_msg"]="0";
            TraineeVM trainee_VMObj = new TraineeVM();
            if (traineeGuid != null)
            {
                trainee_VMObj = traineeBLL.Getall(traineeGuid);
                

                return View(trainee_VMObj);
            }
            return View(trainee_VMObj);
         
        }

        [HttpGet]
        public ActionResult GetTrainee(Guid? traineeGuid)
        {

            ViewBag.DataCheck = "0";
            TraineeVM trainee_VMObj = new TraineeVM();
            if (traineeGuid != null)
            {
                trainee_VMObj = traineeBLL.Getall(traineeGuid);
               // return View(trainee_VMObj);
            }

            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {

                ViewBag.CheckUS = "En-Us";
                ViewBag.Nationality = new SelectList(traineeBLL.GetallNatoinalities(), "ID", "EnName", trainee_VMObj.NationalityId);
                ViewBag.Religion = new SelectList(traineeBLL.GetallReligions(), "ID", "EnName", trainee_VMObj.ReligionId);
                ViewBag.NationalType = new SelectList(traineeBLL.GetallNationalTypes(), "ID", "EnName", trainee_VMObj.NationalTypeId);
                ViewBag.SpeechLanguage = new SelectList(traineeBLL.GetallSpeechLanguages(), "ID", "EnName", trainee_VMObj.SpeechLanguageId);

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.Nationality = new SelectList(traineeBLL.GetallNatoinalities(), "ID", "Name", trainee_VMObj.NationalityId);
                ViewBag.Religion = new SelectList(traineeBLL.GetallReligions(), "ID", "Name", trainee_VMObj.ReligionId);
                ViewBag.NationalType = new SelectList(traineeBLL.GetallNationalTypes(), "ID", "Name", trainee_VMObj.NationalTypeId);
                ViewBag.SpeechLanguage = new SelectList(traineeBLL.GetallSpeechLanguages(), "ID", "Name", trainee_VMObj.SpeechLanguageId);

            }

            //Session["Ar_msg"] = obj.Ar_msg;
            //Session["En_msg"] = obj.En_msg;

            return View(trainee_VMObj);
        }

      

        [HttpGet]
        public JsonResult GetTrainee_byId(int ID)
        {

            return Json(new { data = traineeBLL.GetByID(ID) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateTrainee(TraineeVM trainee_VMObj, HttpPostedFileBase f1)
        {
            
            if (ModelState.IsValid)
            {
                var obj= traineeBLL.SaveinDataBase(trainee_VMObj);
                decimal filesize=1000;           
                if(f1!=null)
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png" };
                    var fileExt = System.IO.Path.GetExtension(f1.FileName).Substring(1).ToLower();
                    if (!supportedTypes.Contains(fileExt))
                    {
                        var msg = "File Extension Is InValid - Only Upload jpg/jpeg/png/ File";                       
                    }
                    else if (f1.ContentLength > (filesize * 1024))
                    {
                        var msg = "File size Should Be UpTo " + filesize + "KB";                       
                    }
                    else
                    {
                        string fname = obj.ID.ToString() + Path.GetExtension(f1.FileName);
                        string fPath = Server.MapPath("~/Upload/");
                        string fPathName = Path.Combine(fPath, fname);



                        f1.SaveAs(fPathName);


                        obj.Photopath = fname;
                        traineeBLL.SaveinDataBase(obj);
                        var msg = "File Is Successfully Uploaded";                        
                    }
                }
                else
                {
                    var msg = "Upload Container Should Not Be Empty or Contact Admin";                    
                }

                Session["Ar_msg"] = obj.Ar_msg;
                Session["En_msg"] = obj.En_msg;

                TempData["DataStatus"] = "تم الحفظ بنجاح";               
                return RedirectToAction("GetTrainee", new { traineeGuid = obj.TraineeGuid });
            }
            else
            {
                //Session["Ar_msg"] = obj.Ar_msg;
                //Session["En_msg"] = obj.En_msg;

                TempData["DataStatus"] = "أكمل البيانات";
                return RedirectToAction("GetTrainee",new { traineeGuid = trainee_VMObj.TraineeGuid} );
            }

        }

        [HttpPost]
        public ActionResult EditTrainee(TraineeVM trainee_VMObj, HttpPostedFileBase f1)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {

                ViewBag.CheckUS = "En-Us";
                ViewBag.Nationality = new SelectList(traineeBLL.GetallNatoinalities(), "ID", "EnName", trainee_VMObj.NationalityId);
                ViewBag.Religion = new SelectList(traineeBLL.GetallReligions(), "ID", "EnName", trainee_VMObj.ReligionId);
                ViewBag.NationalType = new SelectList(traineeBLL.GetallNationalTypes(), "ID", "EnName", trainee_VMObj.NationalTypeId);
                ViewBag.SpeechLanguage = new SelectList(traineeBLL.GetallSpeechLanguages(), "ID", "EnName", trainee_VMObj.SpeechLanguageId);

            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.Nationality = new SelectList(traineeBLL.GetallNatoinalities(), "ID", "Name", trainee_VMObj.NationalityId);
                ViewBag.Religion = new SelectList(traineeBLL.GetallReligions(), "ID", "Name", trainee_VMObj.ReligionId);
                ViewBag.NationalType = new SelectList(traineeBLL.GetallNationalTypes(), "ID", "Name", trainee_VMObj.NationalTypeId);
                ViewBag.SpeechLanguage = new SelectList(traineeBLL.GetallSpeechLanguages(), "ID", "Name", trainee_VMObj.SpeechLanguageId);

            }
            if (ModelState.IsValid)
            {
               var obj= traineeBLL.SaveinDataBase(trainee_VMObj);


                decimal filesize = 1000;
                if (f1 != null)
                {
                    var supportedTypes = new[] { "jpg", "jpeg", "png" };
                    var fileExt = System.IO.Path.GetExtension(f1.FileName).Substring(1);
                    if (!supportedTypes.Contains(fileExt))
                    {
                        var msg = "File Extension Is InValid - Only Upload jpg/jpeg/png/ File";
                    }
                    else if (f1.ContentLength > (filesize * 1024))
                    {
                        var msg = "File size Should Be UpTo " + filesize + "KB";
                    }
                    else
                    {
                        string fname = obj.ID.ToString() + Path.GetExtension(f1.FileName);
                        string fPath = Server.MapPath("~/Upload/");
                        string fPathName = Path.Combine(fPath, fname);



                        string fullPath = fPathName;
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        f1.SaveAs(fPathName);

                        obj.Photopath = fname;
                        traineeBLL.SaveinDataBase(obj);

                        var msg = "File Is Successfully Uploaded";
                    }
                }
                else
                {
                    var msg = "Upload Container Should Not Be Empty or Contact Admin";
                }

                Session["Ar_msg"] = obj.Ar_msg;
                Session["En_msg"] = obj.En_msg;
                TempData["DataStatus"] = "تم التعديل بنجاح";
                return RedirectToAction("GetTrainee",new { traineeGuid = obj.TraineeGuid });
            }
            else
            {
                TempData["DataStatus"] = "أكمل البيانات";
                return RedirectToAction("GetTrainee", new { traineeGuid = trainee_VMObj.TraineeGuid });
            }
        }


        [HttpPost]
        public ActionResult DeleteTrainee(Guid traineeGuid, HttpPostedFileBase f1)
        {
            traineeBLL.Delete(traineeGuid);
            //string fname = trainee_VMObj.ID.ToString() + Path.GetExtension(f1.FileName);
            //string fPath = Server.MapPath("~/Upload/");
            //string fPathName = Path.Combine(fPath, fname);

            //string fullPath = fPathName;
            //if (System.IO.File.Exists(fullPath))
            //{
            //    System.IO.File.Delete(fullPath);
            //}

            TempData["DataStatus"] = "تم الحذف بنجاح";
            return RedirectToAction("Index");
        }


    }
}