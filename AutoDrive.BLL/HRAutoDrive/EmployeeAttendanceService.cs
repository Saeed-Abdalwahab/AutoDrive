using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
    public class EmployeeAttendanceService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List<EmployeeAttendanceVM> SearchEmployee(string Any, string language)
        {
            List<EmployeeAttendanceVM> employees = new List<EmployeeAttendanceVM>();
            if (language == "en-US")
            {

                List<Employee> model = context.Employees.Where(x => x.EnName.Contains(Any)).ToList();

                if (model != null && model.Count > 0)
                {
                    foreach (var Employee in model)
                    {
                        EmployeeAttendanceVM employee = new EmployeeAttendanceVM();
                        employee.EmployeeId = Employee.ID;
                        employee.EmployeeName = Employee.EnName;
                        employee.Code = Employee.Code;
                        EmployeeDepartment employeeDepartment = (
                                                                from Empdept in context.EmployeeDepartments
                                                                where Employee.ID == Empdept.EmployeeId && Empdept.StartDate <= DateTime.Now && (Empdept.EndDate == null || Empdept.EndDate.Value >= DateTime.Now)
                                                                select Empdept).ToList<EmployeeDepartment>().FirstOrDefault();
                        if (employeeDepartment != null)
                        {
                            employee.DeptartmentName = employeeDepartment.Department.EnName;
                        }
                        else
                        {
                            employee.DeptartmentName = "";
                        }
                        employees.Add(employee);
                    }
                }
            }
            else
            {
                List<Employee> model = context.Employees.Where(x => x.Name.Contains(Any)).ToList();
                
                if (model != null && model.Count > 0)
                {
                    foreach (var Employee in model)
                    {
                        EmployeeAttendanceVM employee = new EmployeeAttendanceVM();
                        employee.EmployeeId = Employee.ID;
                        employee.EmployeeName = Employee.Name;
                        employee.Code = Employee.Code;
                        EmployeeDepartment employeeDepartment=(
                                                                from Empdept in context.EmployeeDepartments
                                                               where Employee.ID==Empdept.EmployeeId && Empdept.StartDate<=DateTime.Now&&(Empdept.EndDate==null||Empdept.EndDate.Value>=DateTime.Now)
                                                               select Empdept).ToList<EmployeeDepartment>().FirstOrDefault() ;
                        if (employeeDepartment!=null)
                        {
                            employee.DeptartmentName = employeeDepartment.Department.Name;
                        }
                        else
                        {
                            employee.DeptartmentName = "";
                        }
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }
        public string SaveInDataBase(EmployeeAttendanceVM model)
        {
            try
            {
                string result = "false";
                DateTime AttendancedateTime = Convert.ToDateTime(model.AttendanceTime);
                TimeSpan Time= new TimeSpan(AttendancedateTime.Hour, AttendancedateTime.Minute, AttendancedateTime.Second);
                EmployeeAttendance employeeAttendance = context.EmployeeAttendances.FirstOrDefault(EA => EA.EmployeeId == model.EmployeeId && EA.AttendanceType == (int)model.AttendanceType && EA.AttendanceDate == model.AttendanceDate && EA.AttendanceTime == Time);
                if (employeeAttendance==null||employeeAttendance.ID==model.ID)
                {
                    if (model.ID==0)
                    {
                        EmployeeAttendance obj = new EmployeeAttendance();
                        obj.EmployeeId = model.EmployeeId;
                        obj.AttendanceDate = model.AttendanceDate;
                        obj.AttendanceTime = Time;
                        obj.AttendanceType = (int)model.AttendanceType;
                        context.EmployeeAttendances.Add(obj);
                        context.SaveChanges();
                        result = "true";
                    }
                    else
                    {
                        EmployeeAttendance obj = context.EmployeeAttendances.FirstOrDefault(EA => EA.ID == model.ID);
                        obj.EmployeeId = model.EmployeeId;
                        obj.AttendanceDate = model.AttendanceDate;
                        obj.AttendanceTime = Time;
                        obj.AttendanceType = (int)model.AttendanceType;
                        context.Entry(obj).State = EntityState.Modified;
                        context.SaveChanges();
                        result = "true";
                    }
                }
                else
                {
                    result = "Employee Attendance is existed";
                }
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GetTime(TimeSpan Time, string Language)
        {
            string Minutes = Time.Minutes < 10 ? Time.Minutes.ToString() + "0" : Time.Minutes.ToString();
            string TimeString;
            if (Time.Hours >= 12)
            {
                if (Time.Hours == 12)
                {
                    TimeString = Time.Hours.ToString() + ":" + Minutes;
                }
                else
                {
                    int hours = Time.Hours - 12;
                    TimeString = hours.ToString() + ":" + Minutes;
                }
                if (Language == "ar-EG")
                {
                    TimeString = TimeString + " PM";
                }
                else
                {
                    TimeString = "PM " + TimeString;
                }
            }
            else
            {
                if (Time.Hours == 0)
                {
                    TimeString = (Time.Hours + 12).ToString() + ":" + Minutes;
                }
                else
                    TimeString = Time.Hours.ToString() + ":" + Minutes;
                if (Language == "ar-EG")
                {
                    TimeString = TimeString + " AM";
                }
                else
                {
                    TimeString = "AM " + TimeString;
                }
            }
            return TimeString;
        }
        public List<EmployeeAttendanceVM> GetALL(string Language)
        {
            try
            {
                if (Language=="en-US")
                {
                    return context.EmployeeAttendances.ToList().Select(EA => new EmployeeAttendanceVM()
                    {
                        ID = EA.ID,
                        EmployeeName = EA.employee.EnName,
                        AttendanceTime = GetTime(EA.AttendanceTime, "ar-EG"),
                        Type = ((AttendanceType)EA.AttendanceType).GetDisplayName(),
                        Date = EA.AttendanceDate.ToString("dd/MM/yyyy")
                    }).ToList();
                }
                else
                {
                    return context.EmployeeAttendances.ToList().Select(EA => new EmployeeAttendanceVM()
                    {
                        ID = EA.ID,
                        EmployeeName = EA.employee.Name,
                        AttendanceTime = GetTime(EA.AttendanceTime, "ar-EG"),
                        Type = ((AttendanceType)EA.AttendanceType).GetDisplayName(),
                        Date = EA.AttendanceDate.ToString("dd/MM/yyyy")
                    }).ToList();
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void delete(int id)
        {
            try
            {
                EmployeeAttendance employeeAttendance = context.EmployeeAttendances.FirstOrDefault(EA => EA.ID == id);
                context.EmployeeAttendances.Remove(employeeAttendance);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public EmployeeAttendanceVM GetByID(int id,string Language)
        {
            try
            {
                EmployeeAttendanceVM employeeAttendanceVM = new EmployeeAttendanceVM();
                EmployeeAttendance employeeAttendance = context.EmployeeAttendances.FirstOrDefault(EA => EA.ID == id);
                if (Language=="en-US")
                {
                    employeeAttendanceVM.ID = employeeAttendance.ID;
                    employeeAttendanceVM.EmployeeName = employeeAttendance.employee.EnName;
                    employeeAttendanceVM.EmployeeId = employeeAttendance.EmployeeId;
                    employeeAttendanceVM.Code = employeeAttendance.employee.Code;
                    employeeAttendanceVM.Date = employeeAttendance.AttendanceDate.ToString("dd/MM/yyyy");
                    employeeAttendanceVM.AttendanceTime = GetTime(employeeAttendance.AttendanceTime, "en-US");
                    employeeAttendanceVM.AttendanceType = (AttendanceType)employeeAttendance.AttendanceType;
                    EmployeeDepartment employeeDepartment = (    from Empdept in context.EmployeeDepartments
                                                                 where employeeAttendance.EmployeeId == Empdept.EmployeeId && Empdept.StartDate <= DateTime.Now && (Empdept.EndDate == null || Empdept.EndDate.Value >= DateTime.Now)
                                                                 select Empdept).ToList<EmployeeDepartment>().FirstOrDefault();
                    if (employeeDepartment != null)
                    {
                        employeeAttendanceVM.DeptartmentName = employeeDepartment.Department.EnName;
                    }
                    else
                    {
                        employeeAttendanceVM.DeptartmentName = "";
                    }
                }
                else
                {
                    employeeAttendanceVM.ID = employeeAttendance.ID;
                    employeeAttendanceVM.EmployeeName = employeeAttendance.employee.Name;
                    employeeAttendanceVM.EmployeeId = employeeAttendance.EmployeeId;
                    employeeAttendanceVM.Code = employeeAttendance.employee.Code;
                    employeeAttendanceVM.Date = employeeAttendance.AttendanceDate.ToString("dd/MM/yyyy");
                    employeeAttendanceVM.AttendanceTime = GetTime(employeeAttendance.AttendanceTime, "en-US");
                    employeeAttendanceVM.AttendanceType = (AttendanceType)employeeAttendance.AttendanceType;
                    EmployeeDepartment employeeDepartment = (from Empdept in context.EmployeeDepartments
                                                             where employeeAttendance.EmployeeId == Empdept.EmployeeId && Empdept.StartDate <= DateTime.Now && (Empdept.EndDate == null || Empdept.EndDate.Value >= DateTime.Now)
                                                             select Empdept).ToList<EmployeeDepartment>().FirstOrDefault();
                    if (employeeDepartment != null)
                    {
                        employeeAttendanceVM.DeptartmentName = employeeDepartment.Department.Name;
                    }
                    else
                    {
                        employeeAttendanceVM.DeptartmentName = "";
                    }
                }
                return employeeAttendanceVM;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
