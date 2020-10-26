using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using System;
using System.Linq;
using System.Web.Mvc;
namespace AutoDrive.Web.Areas.HR.Controllers

{
    public class JobFunctionController : Controller
    {
        // GET: HR/JobFunction
        JobFunctionService service = new JobFunctionService();

        public ActionResult Index()
        {
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.JobNames = new SelectList(service.JobNames(), "ID", "EnName");
            }
            else
            {
                ViewBag.JobNames = new SelectList(service.JobNames(), "ID", "Name");

            }
            return View();
        }
        public JsonResult Getall()
        {
            return Json(new { data = service.Getall() }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FetchJobFunctions(int ID)
        {
            var Cook = Request.Cookies["Language"];
            string Language = Cook != null && Cook.Value.ToLower() == "En-Us".ToLower() ? "en" : "ar";
            return Json(service.GetJobFunctionsByJobNameId(ID, Language), JsonRequestBehavior.AllowGet);
        }
        public JsonResult FetchJobNameId(int ID)
        {
            return Json(service.JobNameIdByJobFunctionID(ID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            return Json(service.Get(ID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Save(JobFunctionVM JobFunctionVM)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var Mssg = JobFunctionVM.ID == 0 ? service.Save(JobFunctionVM) : service.Edit(JobFunctionVM);
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