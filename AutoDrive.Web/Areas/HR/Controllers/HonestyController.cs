using AutoDrive.BLL.HRAutoDrive;
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
    public class HonestyController : Controller
    {
        private EmployeeService EmployeeService;
        private HonestyService HonestyService;
        private ItemService ItemService;
        ReturnVM RT = new ReturnVM();
        public HonestyController()
        {
            EmployeeService = new EmployeeService();
            HonestyService = new HonestyService();
            ItemService = new ItemService();
        }

        // GET: HR/Honesty
        public ActionResult Employees()
        {
            return View();
        }
        public ActionResult Index(Guid id)
        {
            var model=EmployeeService.GetByid(id);
            return View(model);
        }

        public ActionResult GetHonestyByEmID(int id)
        {
               var model= HonestyService.GetHonestyByEmID(id);

            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
     
        }

        [HttpGet]
        public ActionResult Create(int id=0)
        {
            try
            {
                
                var Model = HonestyService.GetHonestyByID(id);
                Model.HonestyDatestring = String.Format("{0:dd/MM/yyyy}", Model.HonestyDate);
                ViewBag.ItemId = new SelectList(ItemService.GetAll(Model == null ? 0 : Model.ItemId), "ID", "Name",Model==null?0: Model.ItemId);
                string result = PartialView("~/Areas/HR/Views/Honesty/Partails/_Create.cshtml", Model).RenderToString();

                if (Model !=null)
                {
                    var test = ItemService.Get(Model.ItemId);
                    if (test.ItemPathImage != null)
                    {
                        test.ItemPathImage = Path.Combine(ConfigurationManager.AppSettings["ItemImgVirtualPath"] + test.ItemPathImage);
                    }

                    string Itemresult = PartialView("~/Areas/HR/Views/Honesty/Partails/_ItemDetails.cshtml", test).RenderToString();

                    RT.Type = "Success";
                    return Json(new { data = RT, View = result, ItemView = Itemresult }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string Itemresult = null;
                    RT.Type = "Success";
                    return Json(new { data = RT, View = result, ItemView = Itemresult }, JsonRequestBehavior.AllowGet);
                }
             
            }
            catch (Exception ex)
            {

                RT.Type = "Error";
                RT.Message = Messages.ErrorDataBase;
                return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Create(HonestyVM Model)
        {
            try
            {
                ViewBag.ItemId = new SelectList(ItemService.GetAll(Model.ItemId), "ID", "Name", Model == null ? 0 : Model.ItemId);
                if (ModelState.IsValid)
                {
                    Model.HonestyDate = Convert.ToDateTime(Model.HonestyDatestring);
                    HonestyService.Save(Model);
                    RT.Type = "Success";
                    RT.Message = Messages.AddSuccess;
                    return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string result = PartialView("~/Areas/HR/Views/Honesty/Partails/_Create.cshtml", Model).RenderToString();
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
                var test = HonestyService.GetHonestyByID(ID);
                string result = PartialView("~/Areas/HR/Views/Honesty/Partails/_Delete.cshtml", test).RenderToString();
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
        public ActionResult Delete(HonestyVM Model)
        {
            try
            {
                if (ItemService.GetItemByParentID(Model.ID).Count == 0)
                {
                    HonestyService.Delete(Model);
                    RT.Type = "Success";
                    RT.Message = Messages.DeleteSucc;
                    return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    RT.Type = "Error";
                    RT.Message = Messages.DeleteErr;
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



        public ActionResult ItemDetails(int ID = 0)
        {
            try
            {
                var test = ItemService.Get(ID);
                if (test.ItemPathImage != null)
                {
                    test.ItemPathImage = Path.Combine(ConfigurationManager.AppSettings["ItemImgVirtualPath"] + test.ItemPathImage);
                }

                string result = PartialView("~/Areas/HR/Views/Honesty/Partails/_ItemDetails.cshtml", test).RenderToString();
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
    }
}