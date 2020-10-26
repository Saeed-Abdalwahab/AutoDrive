using AutoDrive.BLL.HRAutoDrive;
using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using AutoDrive.VM.Helper;
using AutoDriveResources;
using StockPortal.Helper;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeService EmployeeService;
        private ReturnVM RT;
        private HonestyService HonestyService;
        ArchiveDocumentService ArchiveDocumentService;
        //private Repository<Employee> repository;
        public EmployeeController()
        {
            //If you want to use Generic Repository with Unit of work
            //repository = new Repository<Employee>(unitOfWork);
            //If you want to use Specific Repository with Unit of work
            EmployeeService = new EmployeeService();
            HonestyService = new HonestyService();
            RT = new ReturnVM();
            ArchiveDocumentService = new ArchiveDocumentService();
        }

        // GET: HR/Employee
        public ActionResult Index()
        {
            return View();
        }        
        [HttpGet]
        public ActionResult AddEmployee()
        {


        var List=   DateTimeFormatInfo.CurrentInfo .MonthNames.Select((monthName, index) => new  {    ID = (index + 1).ToString(), Name = monthName  }).ToList();
            List.RemoveAt(12);
            ViewBag.Monthes = new SelectList(List.ToList(), "ID", "Name");
            var Years=  Enumerable.Range(1990, DateTime.Now.Year - 1990 + 1).Select(x=>new { ID=x,Name=x}).ToList();
            ViewBag.Years = new SelectList(Years, "ID", "Name");

            var Cook = Request.Cookies["Language"];
            if (Cook!=null &&Cook.Value.ToLower()== "En-Us".ToLower())
            {
                ViewBag.Nationalties = new SelectList(EmployeeService.Nationalities(), "ID", "EnName");
                ViewBag.Religions = new SelectList(EmployeeService.Religions(), "ID", "EnName");
                ViewBag.BloodGroups = new SelectList(EmployeeService.BloodGroups(), "ID", "EnName");
                ViewBag.licenceTypeHRs = new SelectList(EmployeeService.licenceTypeHRs(), "ID", "EnName");
                ViewBag.areas = new SelectList(EmployeeService.areas(), "ID", "EnName");
                ViewBag.certificationTypes = new SelectList(EmployeeService.certificationTypes(), "ID", "EnName");
                ViewBag.certificationGrades = new SelectList(EmployeeService.certificationGrades(), "ID", "EnName");
                ViewBag.employeeStatus = new SelectList(EmployeeService.employeeStatus(), "ID", "EnName", (int)EmployeeStatus.InService);
                ViewBag.countries = new SelectList(EmployeeService.countries(), "ID", "EnName");
                ViewBag.maritalStatus = new SelectList(EmployeeService.maritalStatus(), "ID", "EnName");
                ViewBag.JobDegrees = new SelectList(EmployeeService.JobDegrees(), "ID", "EnName");
                ViewBag.CarrerFields = new SelectList(EmployeeService.CarrerFields(), "ID", "EnName");
                ViewBag.JobLevels = new SelectList(EmployeeService.JobLevels(), "ID", "EnName");
                ViewBag.JobNames = new SelectList(EmployeeService.JobNames(), "ID", "EnName");
                ViewBag.JobFunctions = new SelectList(EmployeeService.JobFunctions(), "ID", "EnName");
                ViewBag.EmployeeStatusKinds = new SelectList(EmployeeService.EmployeeStatusKinds(), "ID", "EnName");

            }
            else
            {
                ViewBag.Nationalties = new SelectList(EmployeeService.Nationalities(), "ID", "Name");
                ViewBag.Religions = new SelectList(EmployeeService.Religions(), "ID", "Name");
                ViewBag.BloodGroups = new SelectList(EmployeeService.BloodGroups(), "ID", "Name");
                ViewBag.licenceTypeHRs = new SelectList(EmployeeService.licenceTypeHRs(), "ID", "Name");
                ViewBag.areas = new SelectList(EmployeeService.areas(), "ID", "Name");
                ViewBag.certificationTypes = new SelectList(EmployeeService.certificationTypes(), "ID", "Name");
                ViewBag.certificationGrades = new SelectList(EmployeeService.certificationGrades(), "ID", "Name");
                ViewBag.employeeStatus = new SelectList(EmployeeService.employeeStatus(), "ID", "Name",(int)EmployeeStatus.InService);
                ViewBag.countries = new SelectList(EmployeeService.countries(), "ID", "Name");
                ViewBag.maritalStatus = new SelectList(EmployeeService.maritalStatus(), "ID", "Name");
                ViewBag.JobDegrees = new SelectList(EmployeeService.JobDegrees(), "ID", "Name");
                ViewBag.CarrerFields = new SelectList(EmployeeService.CarrerFields(), "ID", "Name");
                ViewBag.JobLevels = new SelectList(EmployeeService.JobLevels(), "ID", "Name");
                ViewBag.JobNames = new SelectList(EmployeeService.JobNames(), "ID", "Name");
                ViewBag.JobFunctions = new SelectList(EmployeeService.JobFunctions(), "ID", "Name");
                ViewBag.EmployeeStatusKinds = new SelectList(EmployeeService.EmployeeStatusKinds(), "ID", "Name");


            }
            return View(new EmployeeDepartMentQulificationsVM());
        }
        [HttpPost]
        public JsonResult AddEmployee([Bind( Exclude = "employeeVM.ProfileImge")]EmployeeDepartMentQulificationsVM employeeDepartMentQulificationsVM)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFile Picture;
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                     Picture = System.Web.HttpContext.Current.Request.Files["ProfileImg"];

                    string PictureName = Path.GetFileNameWithoutExtension(Picture.FileName.Replace(" ", ""));
                    string PictureExtention = Path.GetExtension(Picture.FileName);
                    employeeDepartMentQulificationsVM.employeeVM.ProfileImge = PictureName + PictureExtention;
                    Employee employee = new Employee();
                   var Result= EmployeeService.SaveAllEmployeeInfo(employeeDepartMentQulificationsVM,out employee);
                    if(employee == null)
                    {
                        return Json(new { status = false, msg = Messages.SaveErr });

                    }
                    string folder = Server.MapPath(string.Format("~/Images/EmployeeImges/{0}/", employee.ID.ToString()));

               var path=     Path.Combine(folder, PictureName + PictureExtention);
                    DirectoryInfo di = Directory.CreateDirectory(folder);
                    Picture.SaveAs(path);
                    return Json(new { status = Result, EmployeeId = employee.EmployeeGuid,EmpID=employee.ID });
                }
            }
            return Json(new { status = false, msg = Messages.SaveErr });


        }

        [HttpGet]
        public JsonResult EmployeesSearch(string Text)
        {
            var Cook = Request.Cookies["Language"];
            string Language = "ar-EG";
            if (Cook != null && Cook.Value.ToLower() == "en-us".ToLower())
            {
                Language = "en-US";
            }
            var MODEL = EmployeeService.Employees(Text, Language);
            return Json(EmployeeService.Employees(Text, Language).Select(x=>new {Name=x.Name,ID=x.ID,GUID=x.EmployeeGuid}), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string ID)
        {
            EmployeeDepartMentQulificationsVM VM= new EmployeeDepartMentQulificationsVM();
            VM.employeeVM= EmployeeService.GetByID(ID);
            //VM.employeeVM.DateOfBirth = VM.employeeVM.DateOfBirth.Date;
            var Cook = Request.Cookies["Language"];
            if (Cook != null && Cook.Value.ToLower() == "En-Us".ToLower())
            {
                ViewBag.Nationalties = new SelectList(EmployeeService.Nationalities(), "ID", "EnName",VM.employeeVM.NationalityId);
                ViewBag.Religions = new SelectList(EmployeeService.Religions(), "ID", "EnName", VM.employeeVM.ReligionId);
                ViewBag.BloodGroups = new SelectList(EmployeeService.BloodGroups(), "ID", "EnName", VM.employeeVM.BloodGroupId);
                ViewBag.areas = new SelectList(EmployeeService.areas(), "ID", "EnName", VM.employeeVM.BirthPlaceAreaId);
            
                ViewBag.countries = new SelectList(EmployeeService.countries(), "ID", "EnName", VM.employeeVM.BirthPlaceCountryId);
                ViewBag.maritalStatus = new SelectList(EmployeeService.maritalStatus(), "ID", "EnName", VM.employeeVM.MaritalStatusId);

            }
            else
            {
                ViewBag.Nationalties = new SelectList(EmployeeService.Nationalities(), "ID", "Name", VM.employeeVM.NationalityId);
                ViewBag.Religions = new SelectList(EmployeeService.Religions(), "ID", "Name", VM.employeeVM.ReligionId);
                ViewBag.BloodGroups = new SelectList(EmployeeService.BloodGroups(), "ID", "Name", VM.employeeVM.BloodGroupId);
                ViewBag.areas = new SelectList(EmployeeService.areas(), "ID", "Name", VM.employeeVM.BirthPlaceAreaId);
                ViewBag.countries = new SelectList(EmployeeService.countries(), "ID", "Name", VM.employeeVM.BirthPlaceCountryId);
                ViewBag.maritalStatus = new SelectList(EmployeeService.maritalStatus(), "ID", "Name", VM.employeeVM.MaritalStatusId);

            }
            return View(VM);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "employeeVM")] EmployeeVM employeeVM,HttpPostedFileBase Img)
        {
            if (ModelState.IsValid)
            {
                bool check = true;
                if (Img != null)
                {
                    employeeVM.ProfileImge = Img.FileName.Replace(" ", "");
                 check=   EmployeeService.EditEmployee(employeeVM);
                    string folder = Server.MapPath(string.Format("~/Images/EmployeeImges/{0}/", employeeVM.ID.ToString()));
                    DirectoryInfo di = Directory.CreateDirectory(folder);
                    Img.SaveAs(folder + Img.FileName.Replace(" ", ""));
                }
                else
                {
                 check=   EmployeeService.EditEmployee(employeeVM);

                }
                TempData["msg"] = check==true ? Messages.SaveSucc: Messages.SaveErr;
                return RedirectToAction("Edit", new { ID = employeeVM.ID });

            }
            else
            {
                TempData["msg"] = Messages.SaveErr;
                return RedirectToAction("Edit", new { ID = employeeVM.ID });
            }
        }


        public ActionResult EmployeeHonesty(int ID)
        {

            var Model = HonestyService.GetHonestyByEmID(ID);

            foreach (var item in Model)
            {
                item.ImageUrl = ConfigurationManager.AppSettings["ItemImgVirtualPath"] + item.ImageNme;
            }

            return PartialView("~/Areas/HR/Views/Employee/Partails/_EmployeeHonesty.cshtml", Model);

        }


        public ActionResult GetFiles(int ID, int EmployeeId)
        {
            try
            {
                var model = ArchiveDocumentService.GetFiles(ID, EmployeeId);
                foreach (var item in model)
                {
                    string Extension = Path.GetExtension(item.ImageName);
                    item.ImageUrl = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + EmployeeId + "/" + item.ImageName);
                    if (Path.GetExtension(item.ImageName) == ".pdf")
                    {
                        item.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + "pdf.jpeg");
                    }
                    else
                    {
                        item.Imagepath = Path.Combine(ConfigurationManager.AppSettings["EmployeeImgVirtualPath"] + EmployeeId + "/" + item.ImageName);
                    }
                }
                string result = PartialView("~/Areas/HR/Views/Employee/Partails/_Files.cshtml", model).RenderToString();
                RT.Type = "Success";
                return Json(new { data = RT, View = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                RT.Type = "Success";
                return Json(new { data = RT }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}