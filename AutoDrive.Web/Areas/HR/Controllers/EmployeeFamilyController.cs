using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using AutoDriveResources;
using StockPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class EmployeeFamilyController : Controller
    {
        private ReturnVM RT;
        private EmployeeFamilyService EmployeeFamilyService;
        //private Repository<Employee> repository;
        public EmployeeFamilyController()
        {
            EmployeeFamilyService = new EmployeeFamilyService();
            RT = new ReturnVM();
        }

        // GET: HR/EmployeeFamily
        public ActionResult GetFamily(int ID)
        {
            var Model= EmployeeFamilyService.GetFamilyByEmpId(ID);
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                foreach (var item in Model)
                {
                    item.ArName = item.EnName;
                    if (item.Genderid == "1")
                    {
                        item.Genderid = Gender.Male.ToString();
                    }
                    else
                    {
                        item.Genderid = Gender.female.ToString();

                    }
                    if (item.Relationid == "1")
                    {
                        item.Relationid = "husband / wife";

                    }
                    else
                    {
                        item.Relationid = "son / daughter";

                    }
                }
            }
            else
            {
                foreach (var item in Model)
                {
                   

                    if (item.Genderid == "1")
                    {
                        item.Genderid = "ذكر";
                    }
                    else
                    {
                        item.Genderid = "انثى";

                    }
                    if (item.Relationid == "1")
                    {
                        item.Relationid = "زوج / زوجة";

                    }
                    else
                    {
                        item.Relationid = "ابن / ابنة";

                    }
                }

            }



                

            return Json(new { data = Model },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(int ID = 0 , int EmployeeID = 0 )
        {
            try
            {
                var Model = EmployeeFamilyService.GetFamilyById(ID);
                if (Model == null)
                {
                    Model = new EmployeeFamilyVM();
                }

                Model.EmployeeId = EmployeeID;

                string result = PartialView("~/Areas/HR/Views/EmployeeFamily/_Create.cshtml", Model).RenderToString();
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
        public ActionResult Create(EmployeeFamilyVM Model, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeFamilyService.Save(Model);
                    RT.Type = "Success";
                    RT.Message = Messages.AddSuccess;
                    return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string result = PartialView("~/Areas/HR/Views/EmployeeFamily/_Create.cshtml", Model).RenderToString();
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
                var Model = EmployeeFamilyService.GetFamilyById(ID);
                string result = PartialView("~/Areas/HR/Views/EmployeeFamily/_Delete.cshtml", Model).RenderToString();
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
        public ActionResult Delete(EmployeeFamilyVM Model)
        {
            try
            {

                    EmployeeFamilyService.Delete(Model);
                    RT.Type = "Success";
                    RT.Message = Messages.DeleteSucc;
                    return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
              
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