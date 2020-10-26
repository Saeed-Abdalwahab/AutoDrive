using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AutoDrive.BLL.AutoDrivePayroll
{
    public class MotivationEmployeeService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public MotivationEmployee CreatemotivationEmployee(MotivationEmployeeVM model)
        {
            //create motivation employee
            MotivationEmployee motivationEmployee = new MotivationEmployee();
            motivationEmployee.MotivationDate = model.MotivationDate;
            motivationEmployee.MotivationTypeId = model.MotivationTypeId;
            motivationEmployee.WithSalaryOrForm = (int)model.WithSalaryOrForm;
            motivationEmployee.InMonth = (int)model.InMonth;
            motivationEmployee.InYear = (int)model.InMonth == 0 ? 0 : model.InYear;
            motivationEmployee.MotivationStatus = (int)MotivationStatus.underRevision;
            motivationEmployee.Note = model.Note;
            motivationEmployee.Value = model.Value;
            motivationEmployee.EmployeeId = model.EmployeeId;
            context.MotivationEmployees.Add(motivationEmployee);
            context.SaveChanges();
            return motivationEmployee;
        }
        public MotivationEmployee editmotivationEmployee(MotivationEmployee motivationEmployee ,MotivationEmployeeVM model)
        {
            motivationEmployee.ID = model.ID;
            motivationEmployee.MotivationDate = model.MotivationDate;
            motivationEmployee.MotivationTypeId = model.MotivationTypeId;
            motivationEmployee.Value = model.Value;
            motivationEmployee.EmployeeId = model.EmployeeId;
            motivationEmployee.InMonth = (int)model.InMonth;
            motivationEmployee.InYear = model.InYear;
            motivationEmployee.Note = model.Note;
            motivationEmployee.MotivationStatus = (int)model.MotivationStatus;
            motivationEmployee.WithSalaryOrForm = (int)model.WithSalaryOrForm;
            context.Entry(motivationEmployee).State = EntityState.Modified;
            context.SaveChanges();
            return motivationEmployee;
        }
        public EmployeeMoneyDetail CreateemployeeMoneyDetail(int EmpID, int MoneyID, int EmployeeMoneyDetialID, double value, int TableID, string TableName, string operation, int? IncreasesDeductionSettID)
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
        public EmployeeMoney createEmployeeMoney(int EmployeeId,int EmployeeMoneyTypeID,DateTime TransDate,float Total,int status,int? month,int? year,DateTime? ReceiptDate)
        {
            EmployeeMoney employeeMoney = new EmployeeMoney();
            employeeMoney.EmployeeId = EmployeeId;
            employeeMoney.EmployeeMoneyTypeId = EmployeeMoneyTypeID;
            employeeMoney.TransDate = TransDate;
            employeeMoney.TotalMoney = Total;
            employeeMoney.Status = status;
            employeeMoney.InMonth = month == 0 ? null : month;
            employeeMoney.InYear = year == 0 ? null : year;
            employeeMoney.ReceiptDate = ReceiptDate;
            context.EmployeeMoneys.Add(employeeMoney);
            context.SaveChanges();
            return employeeMoney;
        }
        public string SaveInDataBase(MotivationEmployeeVM model)
        {
            string result = "";
            try
            {

                if (model.ID == 0)
                {
                    CreatemotivationEmployee(model);
                    result = "true";
                }
                else
                {
                    //modify motivation employee
                    MotivationEmployee motivationEmployee = context.MotivationEmployees.FirstOrDefault(ME => ME.ID == model.ID);
                    motivationEmployee = editmotivationEmployee(motivationEmployee, model);
                    if (motivationEmployee.MotivationStatus==(int)MotivationStatus.Approval)
                    {
                        if (model.WithSalaryOrForm == RewardType.Form)
                        {
                            EmployeeMoney employeeMoney = createEmployeeMoney(motivationEmployee.EmployeeId, (int)EmployeeMoneyType.Reward, DateTime.Now, (float)motivationEmployee.Value, (int)SalaryStatus.underRevision, motivationEmployee.InMonth, motivationEmployee.InYear, null);
                            EmployeeMoneyDetail employeeMoneyDetail = CreateemployeeMoneyDetail(motivationEmployee.EmployeeId, employeeMoney.ID, (int)EmployeeMoneyTypeDetials.Reward, motivationEmployee.Value, motivationEmployee.ID, "MotivationEmployee", "+", null);
                        }
                        else
                        {
                            int? month, year;
                            if (motivationEmployee.InMonth == 0)
                            {
                                month = null;
                                year = null;
                            }
                            else
                            {
                                month = motivationEmployee.InMonth;
                                year = motivationEmployee.InYear;
                            }
                            EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == motivationEmployee.EmployeeId && EM.InMonth == month && EM.InYear == year && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                            if (employeeMoney != null)
                            {
                                EmployeeMoneyDetail employeeMoneyDetail = CreateemployeeMoneyDetail(motivationEmployee.EmployeeId, employeeMoney.ID, (int)EmployeeMoneyTypeDetials.Reward, motivationEmployee.Value, motivationEmployee.ID, "MotivationEmployee", "+", null);
                                List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == motivationEmployee.EmployeeId).ToList();
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
                                EmployeeMoney obj = createEmployeeMoney(motivationEmployee.EmployeeId, (int)EmployeeMoneyType.Salary, DateTime.Now, (float)motivationEmployee.Value, (int)SalaryStatus.underRevision, motivationEmployee.InMonth, motivationEmployee.InYear, null);
                                EmployeeMoneyDetail employeeMoneyDetail = CreateemployeeMoneyDetail(motivationEmployee.EmployeeId, obj.ID, (int)EmployeeMoneyTypeDetials.Reward, motivationEmployee.Value, motivationEmployee.ID, "MotivationEmployee", "+", null);

                            }
                        }
                    }
                     result = "true";
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            
            try
            {
                MotivationEmployee motivationEmployee = context.MotivationEmployees.FirstOrDefault(ME => ME.ID == id);
                context.MotivationEmployees.Remove(motivationEmployee);
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
            return result;
        }
        public MotivationEmployeeVM GetByID(int id,string Language)
        {
            MotivationEmployee obj = context.MotivationEmployees.FirstOrDefault(BSS => BSS.ID == id);
            if (Language== "ar-EG")
            {
                return new MotivationEmployeeVM()
                {
                    ID = obj.ID,
                    MotivationDate = obj.MotivationDate,
                    EmployeeId = obj.EmployeeId,
                    MotivationTypeId = obj.MotivationTypeId,
                    InMonth = (Months)obj.InMonth,
                    InYear = obj.InYear,
                    Note = obj.Note,
                    MotivationStatus = (MotivationStatus)obj.MotivationStatus,
                    WithSalaryOrForm = (RewardType)obj.WithSalaryOrForm,
                    Value = obj.Value,
                    EmployeeName = obj.Employee.Name
                };
            }
            else
            {
                return new MotivationEmployeeVM()
                {
                    ID = obj.ID,
                    MotivationDate = obj.MotivationDate,
                    EmployeeId = obj.EmployeeId,
                    MotivationTypeId = obj.MotivationTypeId,
                    InMonth = (Months)obj.InMonth,
                    InYear = obj.InYear,
                    Note = obj.Note,
                    MotivationStatus = (MotivationStatus)obj.MotivationStatus,
                    WithSalaryOrForm = (RewardType)obj.WithSalaryOrForm,
                    Value = obj.Value,
                    EmployeeName = obj.Employee.EnName
                };
            }
           
        }
        public List<MotivationEmployeeVM> GetAll(string language)
        {
            List<MotivationEmployeeVM> motivationEmployeesLst = new List<MotivationEmployeeVM>();
            try
            {
                if (language== "ar-EG")
                {
                    motivationEmployeesLst = context.MotivationEmployees.ToList().Select(ME => new MotivationEmployeeVM()
                    {
                        ID = ME.ID,
                        MotivationDate = ME.MotivationDate,
                        MotivationTypeName = ME.MotivationType.Name,
                        EmployeeName = ME.Employee.Name,
                        InMonthName = ME.InMonth == 0 ? "" : ((Months)ME.InMonth).GetDisplayName(),
                        InyearName = ME.InYear == 0 ? "" : ME.InYear.ToString(),
                        Note = ME.Note,
                        MotivationStatusName = ((MotivationStatus)ME.MotivationStatus).GetDisplayName(),
                        WithsalaryOrFromName = ((RewardType)ME.WithSalaryOrForm).GetDisplayName(),
                        Value = ME.Value,
                        check = (ME.MotivationStatus == (int)MotivationStatus.Approval) ? true : false
                    }).ToList();
                }
                else
                {
                    motivationEmployeesLst = context.MotivationEmployees.ToList().Select(ME => new MotivationEmployeeVM()
                    {
                        ID = ME.ID,
                        MotivationDate = ME.MotivationDate,
                        MotivationTypeName = ME.MotivationType.EnName,
                        EmployeeName = ME.Employee.EnName,
                        InMonthName = ME.InMonth == 0 ? "" : ((Months)ME.InMonth).GetDisplayName(),
                        InyearName = ME.InYear == 0 ? "" : ME.InYear.ToString(),
                        Note = ME.Note,
                        MotivationStatusName = ((MotivationStatus)ME.MotivationStatus).GetDisplayName(),
                        WithsalaryOrFromName = ((RewardType)ME.WithSalaryOrForm).GetDisplayName(),
                        Value = ME.Value,
                        check = (ME.MotivationStatus == (int)MotivationStatus.Approval) ? true : false
                    }).ToList();
                }
               
            }
            catch (Exception ex)
            {
                motivationEmployeesLst = null;
            }
            return motivationEmployeesLst;
        }
    }
}
