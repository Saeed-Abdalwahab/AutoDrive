using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class ReceivingHonestyController : Controller
    {
        // GET: HR/ReceivingHonesty

        private EmployeeService EmployeeService;
        HonestyService HonestyService;
        private ReturnVM RT;
        public ReceivingHonestyController()
        {
            EmployeeService = new EmployeeService();
            HonestyService = new HonestyService();

            RT = new ReturnVM();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReceivingHonestyFromEmployee(Guid ID)
        {
            var model = EmployeeService.GetByid(ID);
            return View(model);
        }

        public ActionResult GetHonestyByEmID(int id)
        {
            var model = HonestyService.GetHonestyByEmID(id);

            return Json(new { data = model }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Edite(DateTime date, int id)
        {
            try
            {
                RT.Type = "Success";
                RT.Message = "تم احفظ بنجاح";
                HonestyService.ReceivingHonesty(date, id);
                return Json(new {data=RT }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                RT.Type = "Error";
                RT.Message = "حدث خطاء حاول مرة اخرى";
                return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}