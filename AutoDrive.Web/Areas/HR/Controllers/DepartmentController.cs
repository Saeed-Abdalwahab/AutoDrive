using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Linq;
using System.Web.Mvc;


namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: HR/Department
        DepartmentService service = new DepartmentService();
        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.Departments = new SelectList(service.Getall(0), "ID", "EnName");
            }
            else
            {
                ViewBag.Departments = new SelectList(service.Getall(0), "ID", "Name");
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
                return Json(service.GetallTree().Select(x => new { id = x.ID, parent = (x.ParentId != null ? x.ParentId.ToString() : "#"), text = x.EnName }), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(service.GetallTree().Select(x => new { id = x.ID, parent = (x.ParentId != null ? x.ParentId.ToString() : "#"), text = x.Name }), JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(service.Get(ID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(DepartmentVM DepartmentVM)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var Mssg = DepartmentVM.ID == 0 ? service.Save(DepartmentVM) : service.Edit(DepartmentVM);
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