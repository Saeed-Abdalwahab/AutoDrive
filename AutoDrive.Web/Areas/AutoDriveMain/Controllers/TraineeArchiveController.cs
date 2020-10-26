using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class TraineeArchiveController : Controller
    {
        // GET: AutoDriveMain/TraineeArchive
        TraineeArchiveBLL TraineeArchiveBLL_Obj = new TraineeArchiveBLL();
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.TraineeGuid = new SelectList(TraineeArchiveBLL_Obj.GetAllTrainee(), "TraineeGuid", "EnName");

            }
            else
            {
                ViewBag.CheckUS = "Ar-UsEgy";
                ViewBag.TraineeGuid = new SelectList(TraineeArchiveBLL_Obj.GetAllTrainee(), "TraineeGuid", "ArName");

            }
            string ww = ConfigurationManager.AppSettings["TraineeLocalPath"];
            ViewBag.uploadImg = ww;
            return View();
        }


        [HttpGet]
        public ActionResult GetDetails(Guid TraineeGuid)
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.CheckUS = "En-Us";
                ViewBag.Departments = new SelectList(TraineeArchiveBLL_Obj.Getall22(0), "ID", "EnName");
            }
            else
            {
                ViewBag.CheckUS = "Ar-Egy";
                ViewBag.Departments = new SelectList(TraineeArchiveBLL_Obj.Getall22(0), "ID", "Name");
            }
            TraineeArchiveVM Obj = TraineeArchiveBLL_Obj.GetbyId(TraineeGuid);

      

            return View(Obj);
        }








      
        public JsonResult TreeData()
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                return Json(TraineeArchiveBLL_Obj.GetallTree().Select(x => new { id = x.ID, parent = (x.ParentId != null ? x.ParentId.ToString() : "#"), text = x.EnName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(TraineeArchiveBLL_Obj.GetallTree().Select(x => new { id = x.ID, parent = (x.ParentId != null ? x.ParentId.ToString() : "#"), text = x.Name }), JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult GetbyID(int ID)
        {
            var data2 = TraineeArchiveBLL_Obj.Get2(ID);
            data2.ImgVirtualPath = ConfigurationManager.AppSettings["TraineeImgVirtualPath"] + data2.ImageName;

            return Json(new { data = data2 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTraineeByCode(int NCode)
        {
            var data = TraineeArchiveBLL_Obj.GetTraineeByCodeBLL(NCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public ActionResult CreateTraineeArchive(TraineeArchiveVM TraineeArchiveVM_Obj, HttpPostedFileBase f1)
        {

            var obj = new TraineeArchiveVM();
            obj = TraineeArchiveBLL_Obj.SaveinDataBase(TraineeArchiveVM_Obj);
            // TraineeArchiveBLL_Obj.SaveinDataBase(TraineeArchiveVM_Obj);
            decimal filesize = 1000;
                if (f1 != null)
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
                    string fname =obj.ID.ToString() + Path.GetExtension(f1.FileName) ; //f1.FileName TraineeArchiveVM_Obj.ID.ToString() + Path.GetExtension(f1.FileName);
                //string fPath = Server.MapPath("~/Upload/");
                //string fPath = "E:/New folder";
                    if (!Directory.Exists(ConfigurationManager.AppSettings["TraineeLocalPath"]))
                    {
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["TraineeLocalPath"]);
                    }
                    string fPath2 = (ConfigurationManager.AppSettings["TraineeLocalPath"]);
                    string fPathName = Path.Combine(fPath2, fname);




                        f1.SaveAs(fPathName);


                        TraineeArchiveVM_Obj.ImageName = fname;
                        obj= TraineeArchiveBLL_Obj.SaveinDataBase(TraineeArchiveVM_Obj);
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

               Session["ASDID"] = obj.ArchiveSettingDriveId;
                return RedirectToAction("GetDetails", new { traineeGuid = obj.TraineeGuid });
           

        }



        [HttpGet]
        public JsonResult Getall()
        {

            var TraineeArchive_List = TraineeArchiveBLL_Obj.Getall();
            foreach (var item in TraineeArchive_List)
            {
                item.ImgVirtualPath = ConfigurationManager.AppSettings["TraineeImgVirtualPath"] + item.ImageName;
            }
            return Json(new { data = TraineeArchive_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Getall22(int? ID=0)
        {

            int ASDID = Convert.ToInt32(Session["ASDID"]);

            if (ASDID == 0 && ID == 0)
            {
                var TraineeArchive_List = TraineeArchiveBLL_Obj.Getall22(ID);
                foreach (var item in TraineeArchive_List)
                {
                    item.ImgVirtualPath = ConfigurationManager.AppSettings["TraineeImgVirtualPath"] + item.ImageName;
                }
                return Json(new { data = TraineeArchive_List }, JsonRequestBehavior.AllowGet);
            }
            else if (ID != 0) {
                var TraineeArchive_List = TraineeArchiveBLL_Obj.Getall22(ID);
                foreach (var item in TraineeArchive_List)
                {
                    item.ImgVirtualPath = ConfigurationManager.AppSettings["TraineeImgVirtualPath"] + item.ImageName;
                }
                return Json(new { data = TraineeArchive_List }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var TraineeArchive_List = TraineeArchiveBLL_Obj.Getall22(ASDID);
                foreach (var item in TraineeArchive_List)
                {
                    item.ImgVirtualPath = ConfigurationManager.AppSettings["TraineeImgVirtualPath"] + item.ImageName;
                }
                return Json(new { data = TraineeArchive_List }, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public JsonResult Save(TraineeArchiveVM TraineeArchiveVM_Obj)
        {
            var attachedFile = Session["attachedFile"];
           
                try
                {
                    Session["TId"] = TraineeArchiveVM_Obj.ID;

                    Session["TG"] = TraineeArchiveVM_Obj.TraineeGuid;
                    var Mssg = TraineeArchiveBLL_Obj.Edit(TraineeArchiveVM_Obj);

                    return Mssg == "" ? Json(new { status = true }, JsonRequestBehavior.AllowGet) : Json(new { status = false, Msg = Mssg }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { status = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);

                }
           
        }


        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = TraineeArchiveBLL_Obj.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { msg = jobNameBLL.DeleteDataBaseDataTable(jobNameVM_Obj) }, JsonRequestBehavior.AllowGet);


        }

  

       

        [HttpPost]
        public ActionResult Upload_File()
        {
            var attachedFile = System.Web.HttpContext.Current.Request.Files["_File"];
            var cb = Session["TG"];
            int TId =int.Parse( Session["TID"].ToString());
            Guid TraineeGuid =Guid.Parse(cb.ToString());
            TraineeArchiveVM Obj = TraineeArchiveBLL_Obj.Get2(TId);
            decimal filesize = 1000;
            if (attachedFile != null)
            {
                var supportedTypes = new[] { "jpg", "jpeg", "png" };
                var fileExt = System.IO.Path.GetExtension(attachedFile.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    var msg = "File Extension Is InValid - Only Upload jpg/jpeg/png/ File";
                }
                else if (attachedFile.ContentLength > (filesize * 1024))
                {
                    var msg = "File size Should Be UpTo " + filesize + "KB";
                }
                else
                {



                    string fname = TId + Path.GetExtension(attachedFile.FileName); /* + Path.GetExtension(attachedFile.FileName)  attachedFile.FileName*/

                    

                    if (!Directory.Exists(ConfigurationManager.AppSettings["TraineeLocalPath"]))
                    {
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["TraineeLocalPath"]);
                    }

                    
                    string fPath2 = (ConfigurationManager.AppSettings["TraineeLocalPath"]);
                    string fPathName = Path.Combine(fPath2, fname);



                    string fullPath = fPathName;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    attachedFile.SaveAs(fPathName);

                    Obj.ImageName = fname;
                    TraineeArchiveBLL_Obj.EditFile(Obj);

                    var msg = "File Is Successfully Uploaded";
                }
            }
            else
            {
                var msg = "Upload Container Should Not Be Empty or Contact Admin";
            }

            return Json(null);
        }
    

       
    }
}