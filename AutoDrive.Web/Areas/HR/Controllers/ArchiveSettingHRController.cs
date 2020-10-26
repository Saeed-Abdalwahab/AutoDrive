using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers 
{
    public class ArchiveSettingHRController : Controller
    {
        // GET: HR/ArchiveSettingHR
        ArchiveSettingHRService service = new ArchiveSettingHRService();
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.ArchiveSettingHRs = new SelectList(service.Getall(0), "ID", "EnName");
            }
            else
            {
                ViewBag.ArchiveSettingHRs = new SelectList(service.Getall(0), "ID", "Name");
            }
            return View();
        }
        public JsonResult Getall(int ID)
        {
            return Json(new { data = service.Getall(ID) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TreeData()
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                return Json(service.GetallTree().Select(x => new { id = x.ID, parent = (x.ParentID != null ? x.ParentID.ToString() : "#"), text = x.EnName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(service.GetallTree().Select(x => new { id = x.ID, parent = (x.ParentID != null ? x.ParentID.ToString() : "#"), text = x.Name }), JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(service.Get(ID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(ArchiveSettingHRVM ArchiveSettingHRVM)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var Mssg = ArchiveSettingHRVM.ID == 0 ? service.Save(ArchiveSettingHRVM) : service.Edit(ArchiveSettingHRVM);
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

        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = service.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }
        }
        //Check Name Exist or Not
        public JsonResult NameExist(string Name, string ID)
        {
            return Json(service.NameCheck(Name, int.Parse(ID)), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ENNameExist(string EnName, string ID)
        {
            return Json(service.ENNameCheck(EnName, int.Parse(ID)), JsonRequestBehavior.AllowGet);
        }
    }
}