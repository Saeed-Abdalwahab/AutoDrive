using AutoDrive.BLL.AutoDriveMain;
using AutoDrive.VM.AutoDriveMainViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain.Controllers
{
    public class CourseReservationController : Controller
    {
        // GET: AutoDriveMain/CourseReservation

        private CourseReservationBLL courseReservationBLL = new CourseReservationBLL();

        public ActionResult Index(CourseReservationVM courseReservation_VMObj = null)
        {
            ViewBag.CodeId = new SelectList(courseReservationBLL.GetallTrainee(), "ID", "Code");

            var Cook = Request.Cookies["Language"];

            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.LangEn = true;
            }
            else
            {
                ViewBag.LangEn = false;
            }
            return View(courseReservation_VMObj);
        }

        [HttpPost]
        public ActionResult CreateCourseReservation(CourseReservationVM courseReservation_VMObj)
        {

            if (ModelState.IsValid)
            {
                var obj = courseReservationBLL.SaveinDataBase(courseReservation_VMObj);

            
                return RedirectToAction("Index");
            }
            else
            {
                courseReservation_VMObj.CourseReservation_Msg = "اكمل البيانات المطلوبه";
                return RedirectToAction("Index", courseReservation_VMObj);
            }
        }

        [HttpPost]
        public ActionResult EditCourseReservation(CourseReservationVM courseReservation_VMObj)
        {

            if (ModelState.IsValid)
            {
                var obj = courseReservationBLL.SaveinDataBase(courseReservation_VMObj);

                return RedirectToAction("Index", obj);
            }
            else
            {
                courseReservation_VMObj.CourseReservation_Msg = "اكمل البيانات المطلوبه";
                return RedirectToAction("Index", courseReservation_VMObj);
            }
        }


        [HttpPost]
        public ActionResult DeleteTraineeFile(CourseReservationVM courseReservation_VMObj)
        {
            courseReservationBLL.DeleteCourseReservationBLL(courseReservation_VMObj);


           
            return RedirectToAction("Index", courseReservation_VMObj);
        }

        public JsonResult GetCourseReservationByCode(int NCode)
        {
            var data = courseReservationBLL.GetCourseReservationByCodeBLL(NCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }





        #region DataTable
        public JsonResult GetAllCoursesReservation()
        {
            var courseReservation_List = courseReservationBLL.GetAllCourseReservationBLL();
            return Json(new { data = courseReservation_List }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCourseReservationDatatable_byId(int ID)
        {
            var obj = courseReservationBLL.GetByID(ID);

            return Json(new { data = obj }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save_CourseReservationDataTable(CourseReservationVM courseReservationVM_Obj)
        {

            return Json(new { msg = courseReservationBLL.SaveinDataBaseDataTable(courseReservationVM_Obj) }, JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        public JsonResult Delete_CourseReservationDataTable(CourseReservationVM courseReservationVM_Obj)
        {

            return Json(new { msg = courseReservationBLL.DeleteDataBaseDataTable(courseReservationVM_Obj) }, JsonRequestBehavior.AllowGet);


        }
        #endregion
    }
    }