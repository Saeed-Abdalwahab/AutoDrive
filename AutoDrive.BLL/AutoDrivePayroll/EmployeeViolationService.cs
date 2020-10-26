using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.AutoDrivePayroll
{
    public class EmployeeViolationService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public int GetMonayORMoral(int id)
        {
            return context.ViolationTypes.FirstOrDefault(VT => VT.ID == id).FromMoneyOrMoral;
        }
        public EmployeeViolation createEmployeeViolation(EmployeeViolationVM model)
        {
            EmployeeViolation employeeViolation = new EmployeeViolation();
            employeeViolation.EmployeeId = model.EmployeeId;
            employeeViolation.ViolationTypeId = model.ViolationTypeId;
            employeeViolation.ViolationStatus =(int) ViolationStatus.underRevision;
            employeeViolation.ViolationDate = model.ViolationDate;
            if (model.FromMonth == 0)
            {
                employeeViolation.FromMonth = null;
            }
            else
                employeeViolation.FromMonth =(int) model.FromMonth;
            if (model.FromYear == 0)
            {
                employeeViolation.FromYear = null;
            }
            else
                employeeViolation.FromYear = model.FromYear; ;
            employeeViolation.ViolationValue = model.ViolationValue;
            context.EmployeeViolations.Add(employeeViolation);
            context.SaveChanges();
            return employeeViolation;
        }
        public EmployeeViolation EditEmployeeviolation(EmployeeViolationVM model)
        {
            EmployeeViolation employeeViolation = context.EmployeeViolations.FirstOrDefault(EV => EV.ID == model.ID);
            employeeViolation.EmployeeId = model.EmployeeId;
            employeeViolation.ViolationTypeId = model.ViolationTypeId;
            employeeViolation.ViolationStatus = (int)model.ViolationStatus;
            employeeViolation.ViolationDate = model.ViolationDate;
            if (model.FromMonth == 0)
            {
                employeeViolation.FromMonth = null;
            }
            else
                employeeViolation.FromMonth = (int)model.FromMonth;
            if (model.FromYear == 0)
            {
                employeeViolation.FromYear = null;
            }
            else
                employeeViolation.FromYear = model.FromYear; ;
            employeeViolation.ViolationValue = model.ViolationValue;
            context.Entry(employeeViolation).State = EntityState.Modified;
            context.SaveChanges();
            return employeeViolation;
        }
        public void EmployeeMoneyNotExisted(EmployeeViolation employeeViolation)
        {
           
            try
            {
                EmployeeMoney employeeMoney = new EmployeeMoney();
                employeeMoney.EmployeeId = employeeViolation.EmployeeId;
                employeeMoney.InMonth = employeeViolation.FromMonth;
                employeeMoney.InYear = employeeViolation.FromYear;
                employeeMoney.TotalMoney = 0;
                employeeMoney.TransDate = DateTime.Now;
                employeeMoney.Status = (int)ViolationStatus.underRevision;
                employeeMoney.EmployeeMoneyTypeId = 1;
                context.EmployeeMoneys.Add(employeeMoney);
                context.SaveChanges();
                EmployeeMoneyDetail employeeMoneyDetail = new EmployeeMoneyDetail();
                employeeMoneyDetail.EmployeeId = employeeViolation.EmployeeId;
                employeeMoneyDetail.EmployeeMoneyId = employeeMoney.ID;
                employeeMoneyDetail.FromTableId = employeeViolation.ID;
                employeeMoneyDetail.FromTableName = "EmployeeViolation";
                employeeMoneyDetail.OperationType = "-";
                employeeMoneyDetail.Value = employeeViolation.ViolationValue ?? 0;
                employeeMoneyDetail.EmployeeMoneyTypeDetailsId = 4;
                context.EmployeeMoneyDetails.Add(employeeMoneyDetail);
                context.SaveChanges();
                List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == employeeMoney.ID).ToList();

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
            catch (Exception ex)
            {

                throw ex;
            }
         
        }
        public void EmployeeMoneyExisted(EmployeeViolation employeeViolation,EmployeeMoney obj)
        {
            EmployeeMoneyDetail employeeMoneyDetail = new EmployeeMoneyDetail();
            employeeMoneyDetail.EmployeeId = employeeViolation.EmployeeId;
            employeeMoneyDetail.EmployeeMoneyId = obj.ID;
            employeeMoneyDetail.FromTableId = employeeViolation.ID;
            employeeMoneyDetail.FromTableName = "EmployeeViolation";
            employeeMoneyDetail.OperationType = "-";
            employeeMoneyDetail.Value = employeeViolation.ViolationValue ?? 0;
            employeeMoneyDetail.EmployeeMoneyTypeDetailsId = 4;
            context.EmployeeMoneyDetails.Add(employeeMoneyDetail);
            context.SaveChanges();
            List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == obj.ID).ToList();
            if (employeeMoneyDetailsLst != null)
            {
                obj.TotalMoney = 0;
                foreach (var employeeMoneyDetial in employeeMoneyDetailsLst)
                {
                    if (employeeMoneyDetial.OperationType == "+")
                        obj.TotalMoney += employeeMoneyDetial.Value;
                    else
                        obj.TotalMoney -= employeeMoneyDetial.Value;
                }
                context.Entry(obj).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public string SaveInDataBase(EmployeeViolationVM model)
        {
            string result = "";
            try
            {
                if (model.ID == 0)
                {
                    int MoralORMoney = GetMonayORMoral(model.ViolationTypeId);
                    if (MoralORMoney == (int)Static.Enums.ViolationType.Moral)
                    {
                        createEmployeeViolation(model);
                        result = "true";
                    }
                    else
                    {
                        EmployeeViolation employeeViolation = createEmployeeViolation(model);
                        EmployeeMoney obj = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == model.EmployeeId && EM.InMonth == (int)model.FromMonth && EM.InYear == model.FromYear && EM.EmployeeMoneyTypeId == 1);
                        if (obj == null)
                        {
                            if (model.ViolationStatus == ViolationStatus.Approval)
                            {
                                EmployeeMoneyNotExisted(employeeViolation);
                            }
                            result = "true";
                        }
                        else
                        {
                            if (obj.Status == (int)SalaryStatus.underRevision)
                            {
                                if (model.ViolationStatus == ViolationStatus.Approval)
                                {
                                    EmployeeMoneyExisted(employeeViolation, obj);
                                }

                                result = "true";
                            }
                            else
                            {
                                if (obj.Status == (int)SalaryStatus.ApprovalAmount)
                                {
                                    result = "adopted";
                                }
                                else if (obj.Status == (int)SalaryStatus.CancelEvent)
                                {
                                    result = "canceled";
                                }
                                else
                                {
                                    result = "received";
                                }
                            }
                        }
                    }
                }
                else
                {
                    int MoralORMoney = GetMonayORMoral(model.ViolationTypeId);
                    if (MoralORMoney == (int)Static.Enums.ViolationType.Moral)
                    {
                        EmployeeViolation employeeViolation = EditEmployeeviolation(model);
                        result = "true";
                    }
                    else
                    {
                        if (model.ViolationStatus==ViolationStatus.underRevision)
                        {
                            EmployeeViolation employeeViolation = EditEmployeeviolation(model);
                            result = "true";
                        }
                        else
                        {
                            EmployeeViolation employeeViolation = EditEmployeeviolation(model);
                            EmployeeMoney obj = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == model.EmployeeId && EM.InMonth == (int)model.FromMonth && EM.InYear == model.FromYear && EM.EmployeeMoneyTypeId == 1);
                            if (obj == null)
                            {
                                    EmployeeMoneyNotExisted(employeeViolation);
                                    result = "true";
                            }
                            else
                            {
                                if (obj.Status == (int)SalaryStatus.underRevision)
                                {
                                    EmployeeMoneyExisted(employeeViolation, obj);
                                    result = "true";
                                }
                                else
                                {
                                    if (obj.Status == (int)SalaryStatus.ApprovalAmount)
                                    {
                                        result = "adopted";
                                    }
                                    else if (obj.Status == (int)SalaryStatus.CancelEvent)
                                    {
                                        result = "canceled";
                                    }
                                    else
                                    {
                                        result = "received";
                                    }
                                }
                            }
                        }
                    }

                    }
             }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        public List<EmployeeViolationVM> GetAll(string language)
        {
            List<EmployeeViolationVM> employeeViolationsLst = new List<EmployeeViolationVM>();
            try
            {
                if (language == "ar-EG")
                {
                    employeeViolationsLst = context.EmployeeViolations.ToList().Select(EV => new EmployeeViolationVM()
                    {
                        ID = EV.ID,
                        EmpName = EV.Employee.Name,
                        ViolationTypeName = EV.ViolationType.Name,
                        Status = ((ViolationStatus)EV.ViolationStatus).GetDisplayName(),
                        ViolationDate = EV.ViolationDate,
                        Val = (EV.ViolationValue.ToString() ?? ""),
                        FromMonthName = (EV.FromMonth == null || EV.FromMonth == 0) ? "" : ((Months)EV.FromMonth).GetDisplayName(),
                        FromYearName = (EV.FromYear == 0 || EV.FromYear == null) ? "" : EV.FromYear.ToString(),
                        check = (EV.ViolationStatus == (int)ViolationStatus.Approval) ? true : false
                    }).ToList();
                }
                else
                {
                    employeeViolationsLst = context.EmployeeViolations.ToList().Select(EV => new EmployeeViolationVM()
                    {
                        ID = EV.ID,
                        EmpName = EV.Employee.EnName,
                        ViolationTypeName = EV.ViolationType.EnName,
                        Status = ((ViolationStatus)EV.ViolationStatus).GetDisplayName(),
                        ViolationDate = EV.ViolationDate,
                        Val = (EV.ViolationValue.ToString() ?? ""),
                        FromMonthName = (EV.FromMonth == null || EV.FromMonth == 0) ? "" : ((Months)EV.FromMonth).GetDisplayName(),
                        FromYearName = (EV.FromYear == 0 || EV.FromYear == null) ? "" : EV.FromYear.ToString(),
                        check = (EV.ViolationStatus == (int)ViolationStatus.Approval) ? true : false
                    }).ToList();

                }

            }
            catch (Exception ex)
            {
                employeeViolationsLst = null;
            }
            return employeeViolationsLst;
        }
        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                EmployeeViolation employeeViolation = context.EmployeeViolations.FirstOrDefault(EV => EV.ID == id);
                context.EmployeeViolations.Remove(employeeViolation);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
        public EmployeeViolationVM GetByID(int id, string Language)
        {
            EmployeeViolation obj = context.EmployeeViolations.FirstOrDefault(EV => EV.ID == id);
            if (Language == "ar-EG")
            {
               EmployeeViolationVM vM= new EmployeeViolationVM()
                {
                    ID = obj.ID,
                    EmpName = obj.Employee.Name,
                    EmployeeId = obj.EmployeeId,
                    ViolationTypeId = obj.ViolationTypeId,
                    ViolationValue = obj.ViolationValue,
                    FromMonth = (obj.FromMonth == null) ? 0 : (Months)obj.FromMonth,
                    FromYearName = obj.FromYear==null?"":obj.FromYear.ToString(),
                    ViolationDate = obj.ViolationDate,
                    ViolationStatus = (ViolationStatus)obj.ViolationStatus,
                    Val=obj.ViolationValue==null?"":obj.ViolationValue.ToString()
                };
                return vM;
               
            }
            else
            {
                return new EmployeeViolationVM()
                {
                    ID = obj.ID,
                    EmpName = obj.Employee.EnName,
                    EmployeeId = obj.EmployeeId,
                    ViolationTypeId = obj.ViolationTypeId,
                    ViolationValue = obj.ViolationValue,
                    FromMonth = (obj.FromMonth == null) ? 0 : (Months)obj.FromMonth,
                    FromYearName = obj.FromYear == null ? "" : obj.FromYear.ToString(),
                    ViolationDate = obj.ViolationDate,
                    ViolationStatus = (ViolationStatus)obj.ViolationStatus,
                    Val = obj.ViolationValue == null ? "" : obj.ViolationValue.ToString()
                };
            }

        }
    }
}
