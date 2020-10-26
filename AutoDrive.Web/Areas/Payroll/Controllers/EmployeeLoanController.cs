using AutoDrive.BLL.AutoDrivePayroll;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.Payroll.Controllers
{
    public class EmployeeLoanController : Controller
    {
        EmployeeLoanService employeeLoanService = new EmployeeLoanService();
        // GET: Payroll/EmployeeLoan
        public ActionResult Index()
        {
            int year = DateTime.Now.Year;
            Dictionary<int, int> YearsLST = new Dictionary<int, int>();
            for (int i = year - 2; i <= year + 2; i++)
            {
                YearsLST.Add(i, i);
            }
            ViewBag.YearsLst = new SelectList(YearsLST, "Key", "Value", DateTime.Now.Year);
            Dictionary<bool, string> UnderPayORPaiedLST = new Dictionary<bool, string>();
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                UnderPayORPaiedLST.Add(false, "Under Payment");
                UnderPayORPaiedLST.Add(true, "Payment");
                
            }
            else
            {
                UnderPayORPaiedLST.Add(false, "تحت السداد");
                UnderPayORPaiedLST.Add(true, "تم الدفع بالكامل");
            }
            ViewBag.UnderPayORPaiedLST = new SelectList(UnderPayORPaiedLST, "Key", "Value");
            return View();
        }
        [HttpGet]
        public JsonResult SearchEmployee(string Text)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }

            return Json(employeeLoanService.SearchEmployee(Text, Language).ToList().Select(x => new { Name = x.Name, ID = x.ID }), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(EmployeeLoanVM model)
        {
            if (!(ModelState.IsValid)||(model.ToMonth==0)||(model.FromMonth==0)||(model.ID>0 && model.LoanStatus==0))
            {
                return Json(new { msg = "Model is null" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var Cook = Request.Cookies["Language"];
                string Language = "ar-EG";
                if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
                {
                    Language = "en-US";
                }
                return Json(new { msg = employeeLoanService.SaveInDataBase(model, Language) }, JsonRequestBehavior.AllowGet);

            }


        }
        public JsonResult GetEmployeeLoan()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeLoanService.GetAll(Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(int id)
        {
            return Json(new { data = employeeLoanService.Delete(id) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getEmployeeLoanByID(int id)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeLoanService.GetByID(id, Language) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoanPaymentIndex()
        {
            return View();
        }
        public JsonResult GetUnderPaymentEmployeeLoans()
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            return Json(new { data = employeeLoanService.GetUnderPaymentEmployeeLoan(Language) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ConvertToPayment(int id)
        {
            return Json(new { data = employeeLoanService.ConvertToPayment(id) }, JsonRequestBehavior.AllowGet);

        }
    }
}