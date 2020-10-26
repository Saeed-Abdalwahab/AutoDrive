using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace AutoDrive.BLL.AutoDrivePayroll
{
    public class VacationDeductionsService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List<VacationDeductionVM> Search(VacationDeductionFiltersVM model,string Language)
        {
            List<VacationDeductionVM> vacationDeductionsLst = new List<VacationDeductionVM>();
            
            if (model.ViewVacationId==1)
            {
                //Not Discounted Vacations 
               var EmployeeVacationGroups = context.EmployeeVacations.Where(EV => EV.EmployeeMoneyDetailsId == null && EV.VacationDedutionId == null && EV.FromDate.Month == (int)model.Month && EV.FromDate.Year == model.Year&&(model.HolidayTypeId==-1||EV.HolidayTypeId==model.HolidayTypeId) && (model.EmployeeId == null || EV.EmployeeId == model.EmployeeId)).GroupBy(EV => new { EV.EmployeeId, EV.HolidayTypeId }).ToList();
                if (EmployeeVacationGroups!=null)
                {
                    foreach (var EmployeeVacationGroup in EmployeeVacationGroups)
                    {
                        EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == EmployeeVacationGroup.Key.EmployeeId && EM.InMonth == (int)model.Month && EM.InYear == model.Year && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                        if (employeeMoney==null||employeeMoney.Status==(int)SalaryStatus.underRevision)
                        {
                            VacationDeductionVM vacationDeduction = new VacationDeductionVM();
                            vacationDeduction.EmployeeId = EmployeeVacationGroup.Key.EmployeeId;
                            vacationDeduction.HolidayTypeId = EmployeeVacationGroup.Key.HolidayTypeId;
                            DateTime dateTime = new DateTime(model.Year, (int)model.Month, 1);
                            EmployeeDepartment employeeDepartment = context.EmployeeDepartments.FirstOrDefault(ED => ED.EmployeeId == EmployeeVacationGroup.Key.EmployeeId && ED.StartDate <= dateTime && (ED.EndDate == null || ED.EndDate >= dateTime));
                            if (Language == "en-US")
                            {
                                vacationDeduction.EmpName = EmployeeVacationGroup.FirstOrDefault().employee.EnName;
                                vacationDeduction.HolidayName = EmployeeVacationGroup.FirstOrDefault().holidayType.EnName;
                                vacationDeduction.FirstMonth = (int)model.Month;
                                vacationDeduction.FirstYear = model.Year;
                                if (employeeDepartment!=null)
                                {
                                    vacationDeduction.DeptName = employeeDepartment.Department.EnName;
                                }
                                else
                                {
                                    vacationDeduction.DeptName = "";
                                }
                            }
                            else
                            {
                                vacationDeduction.EmpName = EmployeeVacationGroup.FirstOrDefault().employee.Name;
                                vacationDeduction.HolidayName = EmployeeVacationGroup.FirstOrDefault().holidayType.Name;
                                vacationDeduction.FirstMonth = (int)model.Month;
                                vacationDeduction.FirstYear = model.Year;
                                if (employeeDepartment != null)
                                {
                                    vacationDeduction.DeptName = employeeDepartment.Department.Name;
                                }
                                else
                                {
                                    vacationDeduction.DeptName = "";
                                }
                            }
                            foreach (var employeeVacation in EmployeeVacationGroup)
                            {
                                vacationDeduction.DaysNumber += employeeVacation.NODays;
                                
                                for (var dt = employeeVacation.FromDate; dt <= employeeVacation.ToDate; dt = dt.AddDays(1))
                                {
                                    vacationDeduction.DaysDate = (vacationDeduction.DaysDate==null?"":vacationDeduction.DaysDate + Environment.NewLine)  + dt.ToString("dd/MM/yyyy");
                                }
                            }
                            vacationDeductionsLst.Add(vacationDeduction);
                        }   
                    }
                }
               
            }
            else
            {
                //Discounted Vacations
                List<VacationDeduction> DiscountedvacationDeductionsLst = context.VacationDeductions.Where(VD => VD.DeductionFromMonth == (int)model.Month && VD.DeductionFromYear == model.Year && (model.EmployeeId == null || VD.EmployeeId == model.EmployeeId) && (model.HolidayTypeId == -1 || VD.HolidayTypeId == model.HolidayTypeId)).ToList();
                foreach (var DiscountedvacationDeductions in DiscountedvacationDeductionsLst)
                {
                    EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId ==DiscountedvacationDeductions.EmployeeId && EM.InMonth == (int)model.Month && EM.InYear == model.Year && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                    if (employeeMoney == null || employeeMoney.Status == (int)SalaryStatus.underRevision)
                    {
                        VacationDeductionVM vacationDeduction = new VacationDeductionVM();
                        vacationDeduction.EmployeeId = DiscountedvacationDeductions.EmployeeId;
                        vacationDeduction.HolidayTypeId = DiscountedvacationDeductions.HolidayTypeId;
                        vacationDeduction.ID = DiscountedvacationDeductions.ID;
                        vacationDeduction.DaysDate = DiscountedvacationDeductions.DaysDate;
                        vacationDeduction.DaysNumber = DiscountedvacationDeductions.DaysNumber;
                        vacationDeduction.DeductionFromYear = DiscountedvacationDeductions.DeductionFromYear;
                        vacationDeduction.DeductionFromMonth = DiscountedvacationDeductions.DeductionFromMonth;
                        vacationDeduction.MonthName = ((Months)DiscountedvacationDeductions.DeductionFromMonth).GetDisplayName();
                        vacationDeduction.DeductionValue = DiscountedvacationDeductions.DeductionValue;
                        DateTime dateTime = new DateTime(model.Year, (int)model.Month, 1);
                        EmployeeDepartment employeeDepartment = context.EmployeeDepartments.FirstOrDefault(ED => ED.EmployeeId == DiscountedvacationDeductions.EmployeeId && ED.StartDate <= dateTime && (ED.EndDate == null || ED.EndDate >= dateTime));
                        if (Language == "en-US")
                        {
                            vacationDeduction.EmpName = DiscountedvacationDeductions.Employee.EnName;
                            vacationDeduction.HolidayName = DiscountedvacationDeductions.holidayType.EnName;
                            if (employeeDepartment!=null)
                            {
                                vacationDeduction.DeptName = employeeDepartment.Department.EnName;
                            }
                            else
                            {
                                vacationDeduction.DeptName = "";
                            }
                        }
                        else
                        {
                            vacationDeduction.EmpName = DiscountedvacationDeductions.Employee.Name;
                            vacationDeduction.HolidayName = DiscountedvacationDeductions.holidayType.Name;
                            if (employeeDepartment != null)
                            {
                                vacationDeduction.DeptName = employeeDepartment.Department.Name;
                            }
                            else
                            {
                                vacationDeduction.DeptName = "";
                            }
                        }
                        vacationDeductionsLst.Add(vacationDeduction);
                    }   
                }
            }
            return vacationDeductionsLst;
        }
        public VacationDeduction CreateVacationDeduction(int EmployeeId, int HolidayTypeId, int DaysNumber, string DaysDate, double Value, int Month, int Year)
        {
            VacationDeduction model = new VacationDeduction();
            model.EmployeeId = EmployeeId;
            model.HolidayTypeId = HolidayTypeId;
            model.DaysNumber = DaysNumber;
            model.DaysDate = DaysDate;
            model.DeductionValue = Value;
            model.DeductionFromMonth = Month;
            model.DeductionFromYear = Year;
            context.VacationDeductions.Add(model);
            context.SaveChanges();
            return model;
        }
        public EmployeeMoneyDetail CreateemployeeMoneyDetail(int EmpID,int MoneyID,int EmployeeMoneyDetialID,double value,int TableID,string TableName,string operation,int? IncreasesDeductionSettID)
        {
            EmployeeMoneyDetail employeeMoneyDetail = new EmployeeMoneyDetail();
            employeeMoneyDetail.EmployeeId = EmpID;
            employeeMoneyDetail.EmployeeMoneyId = MoneyID;
            employeeMoneyDetail.EmployeeMoneyTypeDetailsId = EmployeeMoneyDetialID;
            employeeMoneyDetail.Value = (float)value;
            employeeMoneyDetail.FromTableId = TableID;
            employeeMoneyDetail.FromTableName = TableName;
            employeeMoneyDetail.OperationType = operation;
            employeeMoneyDetail.IncreasesDeductionsSettingId = IncreasesDeductionSettID;
            context.EmployeeMoneyDetails.Add(employeeMoneyDetail);
            context.SaveChanges();
            return employeeMoneyDetail;
        }
        public void save(int EmployeeId, int HolidayTypeId, int FirstMonth, int FirstYear, int DaysNumber, string DaysDate, double Value, int Month, int Year)
        {
            try
            {
                VacationDeduction vacationDeduction = CreateVacationDeduction(EmployeeId, HolidayTypeId, DaysNumber, DaysDate, Value, Month, Year);
                EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == EmployeeId && EM.InMonth == Month && EM.InYear == Year && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                int EmployeeMoneyDetialsID;
                if (employeeMoney!=null)
                {
                    EmployeeMoneyDetail employeeMoneyDetail = CreateemployeeMoneyDetail(EmployeeId, employeeMoney.ID, (int)EmployeeMoneyTypeDetials.Holiday, Value, vacationDeduction.ID, "VacationDeduction", "-", null);
                    List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == EmployeeId).ToList();
                    EmployeeMoneyDetialsID = employeeMoneyDetail.ID;
                    if (employeeMoneyDetailsLst != null)
                    {
                        employeeMoney.TotalMoney = 0;
                        foreach (var employeeMoneyDetial in employeeMoneyDetailsLst)
                        {
                            if (employeeMoneyDetial.OperationType == "+")
                                employeeMoney.TotalMoney += employeeMoneyDetial.Value;
                            else
                                employeeMoney.TotalMoney -= employeeMoneyDetial.Value;
                        }
                        context.Entry(employeeMoney).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                else
                {
                    EmployeeMoney obj = new EmployeeMoney();
                    obj.EmployeeId = EmployeeId;
                    obj.EmployeeMoneyTypeId = (int)EmployeeMoneyType.Salary;
                    obj.InMonth = Month;
                    obj.InYear = Year;
                    obj.Status = (int)SalaryStatus.underRevision;
                    obj.TotalMoney -= Value;
                    obj.TransDate = DateTime.Now;
                    context.EmployeeMoneys.Add(obj);
                    context.SaveChanges();
                    EmployeeMoneyDetail employeeMoneyDetail = CreateemployeeMoneyDetail(EmployeeId, obj.ID, (int)EmployeeMoneyTypeDetials.Holiday, Value, vacationDeduction.ID, "VacationDeduction", "-", null);
                    EmployeeMoneyDetialsID = employeeMoneyDetail.ID;
                }
                List<EmployeeVacation> employeeVacationsLst = context.EmployeeVacations.Where(EV => EV.EmployeeId == EmployeeId && EV.HolidayTypeId == HolidayTypeId && EV.FromDate.Month == FirstMonth && EV.FromDate.Year == FirstYear && EV.VacationDedutionId == null && EV.EmployeeMoneyDetailsId == null).ToList();
                foreach (var item in employeeVacationsLst)
                {
                    item.EmployeeMoneyDetailsId = EmployeeMoneyDetialsID;
                    item.VacationDedutionId = vacationDeduction.ID;
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
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
                VacationDeduction vacationDeduction = context.VacationDeductions.FirstOrDefault(VD => VD.ID == id);
                EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == vacationDeduction.EmployeeId && EM.InMonth == vacationDeduction.DeductionFromMonth && EM.InYear == vacationDeduction.DeductionFromYear && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                EmployeeMoneyDetail employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == vacationDeduction.EmployeeId && EMD.EmployeeMoneyTypeDetailsId == (int)EmployeeMoneyTypeDetials.Holiday && EMD.FromTableId == vacationDeduction.ID && EMD.FromTableName == "VacationDeduction");
                List<EmployeeVacation> employeeVacationsLst = context.EmployeeVacations.Where(EV => EV.VacationDedutionId == vacationDeduction.ID && EV.EmployeeMoneyDetailsId == employeeMoneyDetail.ID).ToList();
                if (employeeVacationsLst!=null)
                {
                    foreach (var item in employeeVacationsLst)
                    {
                        item.VacationDedutionId = null;
                        item.EmployeeMoneyDetailsId = null;
                        context.Entry(item).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
              
                if (employeeMoneyDetail!=null)
                {
                    context.EmployeeMoneyDetails.Remove(employeeMoneyDetail);
                    context.SaveChanges();
                }
                if (employeeMoney != null)
                {
                    List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == employeeMoney.EmployeeId).ToList();
                    if (employeeMoneyDetailsLst != null)
                    {
                        employeeMoney.TotalMoney = 0;
                        foreach (var employeeMoneyDetial in employeeMoneyDetailsLst)
                        {
                            if (employeeMoneyDetial.OperationType == "+")
                                employeeMoney.TotalMoney += employeeMoneyDetial.Value;
                            else
                                employeeMoney.TotalMoney -= employeeMoneyDetial.Value;
                        }
                        context.Entry(employeeMoney).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                if (vacationDeduction!=null)
                {
                    context.VacationDeductions.Remove(vacationDeduction);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void RemoveEmployeeMoneyDetials(EmployeeMoneyDetail employeeMoneyDetail)
        {
            context.EmployeeMoneyDetails.Remove(employeeMoneyDetail);
            context.SaveChanges();
        }
        public VacationDeductionVM edit(int Id, double Value, int Month, int Year,string Lanaguage)
        {
            try
            {
                int salaryStatus;
                VacationDeduction vacationDeduction = context.VacationDeductions.FirstOrDefault(VD => VD.ID == Id);
                EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == vacationDeduction.EmployeeId && EM.InMonth == vacationDeduction.DeductionFromMonth && EM.InYear == vacationDeduction.DeductionFromYear && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                EmployeeMoneyDetail employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == vacationDeduction.EmployeeId && EMD.EmployeeMoneyTypeDetailsId == (int)EmployeeMoneyTypeDetials.Holiday && EMD.FromTableId == vacationDeduction.ID && EMD.FromTableName == "VacationDeduction");
                bool check = false;
                if (vacationDeduction.DeductionFromMonth==Month&&vacationDeduction.DeductionFromYear==Year)
                {
                    employeeMoneyDetail.Value = Value;
                    context.Entry(employeeMoneyDetail).State = EntityState.Modified;
                    context.SaveChanges();
                    List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == employeeMoney.EmployeeId).ToList();
                    if (employeeMoneyDetailsLst != null)
                    {
                        employeeMoney.TotalMoney = 0;
                        foreach (var employeeMoneyDetial in employeeMoneyDetailsLst)
                        {
                            if (employeeMoneyDetial.OperationType == "+")
                                employeeMoney.TotalMoney += employeeMoneyDetial.Value;
                            else
                                employeeMoney.TotalMoney -= employeeMoneyDetial.Value;
                        }
                        context.Entry(employeeMoney).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    salaryStatus = employeeMoney.Status;
                    check = true;
                }
                else
                {
                    int OldEmployeeMoneyDetialID = employeeMoneyDetail.ID, NewEmployeeMoneyDetialID;
                    EmployeeMoney obj = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == vacationDeduction.EmployeeId && EM.InMonth == Month && EM.InYear == Year && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                    if (obj != null)
                    {
                        EmployeeMoneyDetail employeeMoneyDetail1 = CreateemployeeMoneyDetail(vacationDeduction.EmployeeId, obj.ID, (int)EmployeeMoneyTypeDetials.Holiday, Value, vacationDeduction.ID, "VacationDeduction", "-", null);
                        List<EmployeeMoneyDetail> employeeMoneyDetailsLst1 = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == obj.ID && EMD.EmployeeId == obj.EmployeeId).ToList();
                        if (employeeMoneyDetailsLst1 != null)
                        {
                            obj.TotalMoney = 0;
                            foreach (var employeeMoneyDetial in employeeMoneyDetailsLst1)
                            {
                                if (employeeMoneyDetial.OperationType == "+")
                                    obj.TotalMoney += employeeMoneyDetial.Value;
                                else
                                    obj.TotalMoney -= employeeMoneyDetial.Value;
                            }
                            context.Entry(obj).State = EntityState.Modified;
                            context.SaveChanges();

                        }
                        NewEmployeeMoneyDetialID = employeeMoneyDetail1.ID;
                        salaryStatus = obj.Status;
                    }
                    else
                    {
                        EmployeeMoney employeeMoney1 = new EmployeeMoney();
                        employeeMoney1.EmployeeId = vacationDeduction.EmployeeId;
                        employeeMoney1.EmployeeMoneyTypeId = (int)EmployeeMoneyType.Salary;
                        employeeMoney1.InMonth = Month;
                        employeeMoney1.InYear = Year;
                        employeeMoney1.Status = (int)SalaryStatus.underRevision;
                        employeeMoney1.TotalMoney -= Value;
                        employeeMoney1.TransDate = DateTime.Now;
                        context.EmployeeMoneys.Add(employeeMoney1);
                        context.SaveChanges();
                        EmployeeMoneyDetail employeeMoneyDetail1 = CreateemployeeMoneyDetail(vacationDeduction.EmployeeId, employeeMoney1.ID, (int)EmployeeMoneyTypeDetials.Holiday, Value, vacationDeduction.ID, "VacationDeduction", "-", null);
                        NewEmployeeMoneyDetialID = employeeMoneyDetail1.ID;
                        salaryStatus = employeeMoney1.Status;
                    }
                    List<EmployeeVacation> employeeVacationsLst = context.EmployeeVacations.Where(EV => EV.VacationDedutionId == vacationDeduction.ID && EV.EmployeeMoneyDetailsId == OldEmployeeMoneyDetialID).ToList();
                    foreach (var item in employeeVacationsLst)
                    {
                        item.EmployeeMoneyDetailsId = NewEmployeeMoneyDetialID;
                        context.Entry(item).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    RemoveEmployeeMoneyDetials(employeeMoneyDetail);
                    List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == employeeMoney.EmployeeId).ToList();
                    if (employeeMoneyDetailsLst != null)
                    {
                        employeeMoney.TotalMoney = 0;
                        foreach (var employeeMoneyDetial in employeeMoneyDetailsLst)
                        {
                            if (employeeMoneyDetial.OperationType == "+")
                                employeeMoney.TotalMoney += employeeMoneyDetial.Value;
                            else
                                employeeMoney.TotalMoney -= employeeMoneyDetial.Value;
                        }
                        context.Entry(employeeMoney).State = EntityState.Modified;
                        context.SaveChanges();

                    }
                    check = false;
                }
                vacationDeduction.DeductionValue = Value;
                vacationDeduction.DeductionFromMonth = Month;
                vacationDeduction.DeductionFromYear = Year;
                context.Entry(vacationDeduction).State = EntityState.Modified;
                context.SaveChanges();
                VacationDeductionVM vacationDeductionVM = new VacationDeductionVM();
                if (salaryStatus==(int)SalaryStatus.underRevision && check)
                {
                    vacationDeductionVM.EmployeeId = vacationDeduction.EmployeeId;
                    vacationDeductionVM.HolidayTypeId = vacationDeduction.HolidayTypeId;
                    vacationDeductionVM.DaysDate = vacationDeduction.DaysDate;
                    vacationDeductionVM.DaysNumber = vacationDeduction.DaysNumber;
                    vacationDeductionVM.DeductionFromMonth = vacationDeduction.DeductionFromMonth;
                    vacationDeductionVM.DeductionFromYear = vacationDeduction.DeductionFromYear;
                    vacationDeductionVM.DeductionValue = vacationDeduction.DeductionValue;
                    vacationDeductionVM.ID = vacationDeduction.ID;
                    vacationDeductionVM.MonthName = ((Months)vacationDeduction.DeductionFromMonth).GetDisplayName();
                    DateTime dateTime = new DateTime(vacationDeduction.DeductionFromYear, vacationDeduction.DeductionFromMonth, 1);
                    EmployeeDepartment employeeDepartment = context.EmployeeDepartments.FirstOrDefault(ED => ED.EmployeeId ==vacationDeduction.EmployeeId && ED.StartDate <= dateTime && (ED.EndDate == null || ED.EndDate >= dateTime));
                    if (Lanaguage == "en-US")
                    {
                        vacationDeductionVM.EmpName = vacationDeduction.Employee.EnName;
                        vacationDeductionVM.HolidayName = vacationDeduction.holidayType.EnName;
                        if (employeeDepartment != null)
                        {
                            vacationDeductionVM.DeptName = employeeDepartment.Department.EnName;
                        }
                        else
                        {
                            vacationDeductionVM.DeptName = "";
                        }
                    }
                    else
                    {
                        vacationDeductionVM.EmpName = vacationDeduction.Employee.Name;
                        vacationDeductionVM.HolidayName = vacationDeduction.holidayType.Name;
                        if (employeeDepartment != null)
                        {
                            vacationDeductionVM.DeptName = employeeDepartment.Department.Name;
                        }
                        else
                        {
                            vacationDeductionVM.DeptName = "";
                        }
                    }
                    return vacationDeductionVM;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int? GetCheckSalaryStatus(int EmployeeId, int Month, int Year)
        {
            int? res;
            try
            {
                EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == EmployeeId && EM.InMonth == Month && EM.InYear == Year && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                if (employeeMoney!=null)
                {
                    res = employeeMoney.Status;
                }
                else
                {
                    res = null;
                }
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
