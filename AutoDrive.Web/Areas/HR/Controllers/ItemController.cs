using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using StockPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoDriveResources;
using System.IO;
using AutoDrive.BLL;
using AutoDrive.Static.Files;
using AutoDrive.Static.Enums;
using System.Configuration;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class ItemController : Controller
    {
        ItemService ItemService = new ItemService();
        ReturnVM RT = new ReturnVM();
        // GET: HR/Item
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Getall(int ID=0)
        {
            var model = ItemService.Getall(ID);
         
                
            return Json(new { data = model  }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TreeData()
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                return Json(ItemService.GetallTree().Select(x => new { id = x.ID, parent = (x.ItemParentId != null ? x.ItemParentId.ToString() : "#"), text = x.EnName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(ItemService.GetallTree().Select(x => new { id = x.ID, parent = (x.ItemParentId != null ? x.ItemParentId.ToString() : "#"), text = x.Name }), JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult Create(int ID=0)
        {
            try
            {
                var test = ItemService.Get(ID);
                if (test!=null && test.ItemPathImage != null)
                {
                    test.ItemPathImage = Path.Combine(ConfigurationManager.AppSettings["ItemImgVirtualPath"] + test.ItemPathImage);
                }
                string result = PartialView("~/Areas/HR/Views/Item/_CreateItem.cshtml",test).RenderToString();
                RT.Type = "Success";
                return Json(new { data = RT, View = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                RT.Type = "Error";
                RT.Message = Messages.ErrorDataBase;
                return Json(new { data = RT}, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Create(ItemIndexVM Model, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Model.ItemParentId = Model.ItemParentId == 0 ? null : Model.ItemParentId;
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {

                            string fname;

                            // Checking for Internet Explorer    

                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                                // FileName = file.FileName;
                            }

                            // Get the complete folder path and store the file inside it.    
                            var fileName = Path.GetFileName(fname);
                            var _name = FileHelper.GetFileNewNamewithoutfolder(fileName, Path.GetExtension(fileName));

                            string path = Path.Combine(ConfigurationManager.AppSettings["ItemLocalPath"] + _name);
                            

                            file.SaveAs(path);
                            Model.ItemPathImage = _name;
                            if (Model.ID != 0)
                            {
                                var rs = ItemService.Get(Model.ID);
                                var str = Path.Combine(ConfigurationManager.AppSettings["EmployeeLocalPath"] + rs.ItemPathImage);
                                if (System.IO.File.Exists(str))
                                {
                                    System.IO.File.Delete(str);
                                }
                            }
                        }
                        else if (Model.ID != 0)
                        {
                            var Item = ItemService.Get(Model.ID);
                            Model.ItemPathImage = Item.ItemPathImage;
                        }

                    }
                    ItemService.Save(Model);
                    RT.Type = "Success";
                    RT.Message = Messages.AddSuccess;
                    return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string result = PartialView("~/Areas/HR/Views/Item/_CreateItem.cshtml",Model).RenderToString();
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

        public ActionResult ShowImage(int ID = 0)
        {
            try
            {
                var test = ItemService.Get(ID);
                if (test.ItemPathImage !=null)
                {
                    test.ItemPathImage = Path.Combine(ConfigurationManager.AppSettings["ItemImgVirtualPath"] + test.ItemPathImage);
                }

                string result = PartialView("~/Areas/HR/Views/Item/_ShowImage.cshtml", test).RenderToString();
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
        public ActionResult Delete(int ID = 0)
        {
            try
            {
                var test = ItemService.Get(ID);
                string result = PartialView("~/Areas/HR/Views/Item/_Delete.cshtml", test).RenderToString();
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
        public ActionResult Delete(ItemIndexVM Model)
        {
            try
            {
                if (ItemService.GetItemByParentID(Model.ID).Count==0)
                {
                    var rs = ItemService.Get(Model.ID);
                    var str = Path.Combine(ConfigurationManager.AppSettings["EmployeeLocalPath"] + rs.ItemPathImage);
                    if (System.IO.File.Exists(str))
                    {
                        System.IO.File.Delete(str);
                    }
                    ItemService.Delete(Model);
                    RT.Type = "Success";
                    RT.Message = Messages.DeleteSucc;
                    return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    RT.Type = "Error";
                    RT.Message = Messages.Deleterelated;
                    return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
                }
               
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