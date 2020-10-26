using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.Static.Files;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using StockPortal.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class ArchiveDocumentController : Controller
    {

        private EmployeeService EmployeeService;
        ArchiveDocumentService ArchiveDocumentService;
        private ReturnVM RT;
        public ArchiveDocumentController()
        {
            EmployeeService = new EmployeeService();
            ArchiveDocumentService = new ArchiveDocumentService();

            RT = new ReturnVM();
        }
        // GET: HR/ArchiveDocument
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmployeeArchive(Guid ID)
        {
            var model = EmployeeService.GetByid(ID);
            return View(model);
        }
        public ActionResult GetFiles(int ID ,int EmployeeId)
        {
            try
            {
                var model = ArchiveDocumentService.GetFiles(ID, EmployeeId);
                foreach (var item in model)
                {
                    string Extension = Path.GetExtension(item.ImageName);
                    item.ImageUrl = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + EmployeeId + "/" + item.ImageName);
                    if (Path.GetExtension(item.ImageName) == ".pdf")
                    {
                        item.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + "pdf.jpeg");
                    }
                    else
                    {
                        item.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + EmployeeId+"/" + item.ImageName);
                    }
                }
                string result = PartialView("~/Areas/HR/Views/ArchiveDocument/Partails/_Files.cshtml", model).RenderToString();
                RT.Type = "Success";
                return Json(new { data = RT, View = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                RT.Type = "Success";
                return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult Create(int ID)
        {
            try
            {
                var model = ArchiveDocumentService.GetfileByID(ID);

                if (model != null)
                {
                    
                    model.DateString = String.Format("{0:dd/MM/yyyy}", model.Date);
                    if (Path.GetExtension(model.ImageName) == ".pdf")
                    {
                        model.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + "pdf.jpeg");
                    }
                    else
                    {
                        model.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"]+model.EmployeeId+ "/" + model.ImageName);
                    }
                }
              

                string result = PartialView("~/Areas/HR/Views/ArchiveDocument/Partails/_Create.cshtml", model).RenderToString();
                RT.Type = "Success";
                return Json(new { data = RT, View = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                RT.Type = "Error";
                RT.Message = Messages.ErrorDataBase;
                return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Create(EmployeeArchiveVM Model, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {

                            string fname;
                            fname = file.FileName;
                            // Get the complete folder path and store the file inside it.    
                            var fileName = Path.GetFileName(fname);
                            var _name = FileHelper.GetFileNewNamewithoutfolder(fileName, Path.GetExtension(fileName));
                            Model.ImageName = _name;

                            bool folderExists = Directory.Exists(ConfigurationManager.AppSettings["EmployeeLocalPath"] + Model.EmployeeId + "\\");
                            if (!folderExists)
                            {
                                Directory.CreateDirectory(ConfigurationManager.AppSettings["EmployeeLocalPath"] + Model.EmployeeId + "\\");
                            }
                            var path = Path.Combine(ConfigurationManager.AppSettings["EmployeeLocalPath"] + Model.EmployeeId + "/" + _name);
                            file.SaveAs(path);
                            if (Model.ID != 0)
                            {
                                var rs = ArchiveDocumentService.GetfileByID(Model.ID);
                                var str = Path.Combine(ConfigurationManager.AppSettings["EmployeeLocalPath"]+Model.EmployeeId+ "/" + rs.ImageName);
                                if (System.IO.File.Exists(str))
                                {
                                    System.IO.File.Delete(str);
                                }
                            }
                        }
                        else if (Model.ID != 0)
                        {
                            var Item = ArchiveDocumentService.GetfileByID(Model.ID);
                            Model.ImageName = Item.ImageName;
                        }

                    }




                 int ID= ArchiveDocumentService.Save(Model);



                    var model = ArchiveDocumentService.GetFiles(Model.ArchiveSettingHRsId, Model.EmployeeId);
                    foreach (var item in model)
                    {
                        item.ImageUrl = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] +Model.EmployeeId+"/"+ item.ImageName);

                        if (Path.GetExtension(item.ImageName) == ".pdf")
                        {
                            item.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + "pdf.jpeg");
                        }
                        else
                        {
                            item.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + Model.EmployeeId + "/" + item.ImageName);
                        }
                    }
                    string result = PartialView("~/Areas/HR/Views/ArchiveDocument/Partails/_Files.cshtml", model).RenderToString();

                    RT.Type = "Success";
                    RT.Message = Messages.AddSuccess;

                    return Json(new { data = RT , View = result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string result = PartialView("~/Areas/HR/Views/Item/_Files.cshtml", Model).RenderToString();
                    RT.Type = "NotValid";
                    return Json(new { data = RT, View = result }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                RT.Type = "Error";
                RT.Message = Messages.ErrorDataBase;
                return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int ID = 0)
        {
            try
            {
                var test = ArchiveDocumentService.GetfileByID(ID);
                string result = PartialView("~/Areas/HR/Views/ArchiveDocument/Partails/_Delete.cshtml", test).RenderToString();
                RT.Type = "Success";
                return Json(new { data = RT, View = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                RT.Type = "Error";
                RT.Message = Messages.ErrorDataBase;
                return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(EmployeeArchiveVM Model)
        {
            try
            {
                var rs = ArchiveDocumentService.GetfileByID(Model.ID);
                var path = Path.Combine(ConfigurationManager.AppSettings["EmployeeLocalPath"]+ Model.EmployeeId + "/" + rs.ImageName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                ArchiveDocumentService.Delete(Model);

                var model = ArchiveDocumentService.GetFiles(rs.ArchiveSettingHRsId, rs.EmployeeId);
                foreach (var item in model)
                {
                    if (Path.GetExtension(item.ImageName) == ".pdf")
                    {
                        item.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + "pdf.jpeg");
                    }
                    else
                    {
                        item.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"]+Model.EmployeeId+ "/" + item.ImageName);
                    }
                }

                
              

                string result = PartialView("~/Areas/HR/Views/ArchiveDocument/Partails/_Files.cshtml", model).RenderToString();

                RT.Type = "Success";
                RT.Message = Messages.AddSuccess;

                return Json(new { data = RT, View = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                RT.Type = "Error";
                RT.Message = Messages.ErrorDataBase;
                return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}