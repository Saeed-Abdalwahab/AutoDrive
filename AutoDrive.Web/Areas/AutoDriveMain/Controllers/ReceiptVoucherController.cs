using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.VM.AutoDriveMainViewModels;
using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class ReceiptVoucherController : Controller
    {
        // GET: AutoDriveMain/ReceiptVoucher
        private ReceiptVoucherBLL receiptVoucherBLL = new ReceiptVoucherBLL();

        public ActionResult Index(ReceiptVoucherVM receiptVoucher_VMObj = null)
        {
            

            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                
                ViewBag.LangEn = true;
                ViewBag.CodeId = new SelectList(receiptVoucherBLL.GetallTrainee(), "ID", "EnName");
                ViewBag.TraineeId = new SelectList(receiptVoucherBLL.GetallTrainee(), "ID", "EnName");
                ViewBag.EmployeeSafeId = new SelectList(receiptVoucherBLL.GetallSafeEmployees(), "ID", "EnName"); 
            }
            else
            {
                ViewBag.LangEn = false;
                ViewBag.CodeId = new SelectList(receiptVoucherBLL.GetallTrainee(), "ID", "ArName");
                ViewBag.TraineeId = new SelectList(receiptVoucherBLL.GetallTrainee(), "ID", "ArName");
                ViewBag.EmployeeSafeId = new SelectList(receiptVoucherBLL.GetallSafeEmployees(), "ID", "Name");


            }
            ViewBag.CourseReservationId = new SelectList(receiptVoucherBLL.GetallCourseReservation(), "ID", "CourseCost");
            return View(receiptVoucher_VMObj);
        }

        [HttpPost]
        public ActionResult CreatereceiptVoucher(ReceiptVoucherVM receiptVoucher_VMObj)
        {

            if (ModelState.IsValid)
            {
                var obj = receiptVoucherBLL.CreateReceiptVoucherBLL(receiptVoucher_VMObj);


                return RedirectToAction("Index");
            }
            else
            {
                receiptVoucher_VMObj.ReceiptVoucher_Msg = "اكمل البيانات المطلوبه";
                return RedirectToAction("Index", receiptVoucher_VMObj);
            }
        }

      

        public JsonResult GetReceiptVoucherByCode(int NCode)
        {
            ReceiptVoucherVM data = receiptVoucherBLL.GetReceiptVoucherByCodeBLL(NCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }





        #region DataTable
        [HttpGet]
        public JsonResult Getall()
        {

            var receiptVoucher_List = receiptVoucherBLL.Getall();
            return Json(new { data = receiptVoucher_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetbyID(int ID)
        {
            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {

                ViewBag.CheckUS = "En-Us";
            }
            else {
                ViewBag.CheckUS = "Ar-Egy";
            }

                return Json(receiptVoucherBLL.Get(ID), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Save(ReceiptVoucherVM ReceiptVoucherVM_Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Mssg = receiptVoucherBLL.Edit(ReceiptVoucherVM_Obj);
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

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            try
            {
                return Json(new { status = true, Msg = receiptVoucherBLL.delete(ID) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { status = false, Msg = Messages.DeleteErr }, JsonRequestBehavior.AllowGet);
            }


        }
        
        #endregion
    }
}