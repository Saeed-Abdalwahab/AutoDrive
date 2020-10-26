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
    public class EmployeeLoanService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool checkDate(DateTime start,DateTime end)
        {
            if (DateTime.Now>=start&&DateTime.Now<=end)
            {
                return true;
            }
            return false;
        }
        public IEnumerable<Employee> SearchEmployee(string Any, string language)
        {

            if (language == "en-US")
            {
                return context.EmployeeStatusTransactions.Where(x => x.EmployeeStatusKindId==(int)Static.Enums.EmployeeStatusKind.Fixed  && x.EmployeeStatusId==(int)EmployeeStatus.InService && x.Employee.EnName.Contains(Any)).ToList().Where(x=> checkDate(x.StartDate, x.EndDate)).ToList().Select(x => new Employee()
                {
                    ID = x.Employee.ID,
                    Name = x.Employee.EnName
                });
            }
            else
            {
                return context.EmployeeStatusTransactions.Where(x => x.EmployeeStatusKindId == (int)Static.Enums.EmployeeStatusKind.Fixed && x.EmployeeStatusId == (int)EmployeeStatus.InService && x.Employee.Name.Contains(Any)).ToList().Where(x => checkDate(x.StartDate, x.EndDate)).ToList().Select(x => new Employee()
                {
                    ID = x.Employee.ID,
                    Name = x.Employee.Name

                });
            }

        }
        public EmployeeMoneyDetail CreateEmployeeMoneyDetail(int EmpMoneyID, int EmpID,int EmpMoneyTypeDetID,float value,int? TableID,string TableFrom,String Opeartion,int? IncreasingSettingID)
        {
            EmployeeMoneyDetail employeeMoneyDetail = new EmployeeMoneyDetail();
            employeeMoneyDetail.EmployeeMoneyId = EmpMoneyID;
            employeeMoneyDetail.EmployeeId = EmpID;
            employeeMoneyDetail.EmployeeMoneyTypeDetailsId = EmpMoneyTypeDetID;
            employeeMoneyDetail.Value = value;
            employeeMoneyDetail.FromTableId = TableID;
            employeeMoneyDetail.FromTableName = TableFrom;
            employeeMoneyDetail.IncreasesDeductionsSettingId = IncreasingSettingID;
            employeeMoneyDetail.OperationType = Opeartion;
            context.EmployeeMoneyDetails.Add(employeeMoneyDetail);
            context.SaveChanges();
            return employeeMoneyDetail;
        }
        public string SaveInDataBase(EmployeeLoanVM model,string Language)
        {
            string result = "false";
            try
            {
                if (model.ID==0)
                {
                    for (int i = model.FromYear; i <= model.ToYear; i++)
                    {
                        int beginMonth, EndMonth;
                        if (i == model.FromYear)
                        {
                            beginMonth = (int)model.FromMonth;
                        } else
                            beginMonth = 1;
                        if (i == model.ToYear)
                        {
                            EndMonth = (int)model.ToMonth;
                        }
                        else
                            EndMonth = 12;

                        for (int j = beginMonth; j <= EndMonth; j++)
                        {
                            EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == model.EmployeeID && EM.InMonth == j && EM.InYear == i && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                            if (employeeMoney!=null)
                            {
                                if (employeeMoney.Status==(int)SalaryStatus.ApprovalAmount)
                                {
                                    if (Language == "ar-EG")
                                    {
                                        result = "لا يمكن اضافة السلفة لانه تم اعتماد المرتب فى شهر" + ((Months)j).GetDisplayName() + " فى سنة" + i + "برجاء ادخال شهور سداد اخرى ";
                                    }
                                    else
                                    {
                                        result = "It can not add Loan because Salary was approvaed in Month " + ((Months)j).GetDisplayName() + " in year " + i+ " Please enter another Payment's Months";
                                    }
                                    return result;
                                }
                                else if(employeeMoney.Status==(int)SalaryStatus.ReceivableReceived)
                                {
                                    if (Language == "ar-EG")
                                    {
                                        result = "لا يمكن اضافة السلفة لانه تم تسليم المرتب فى شهر" + ((Months)j).GetDisplayName() + " فى سنة" + i + "برجاء ادخال شهور سداد اخرى ";
                                    }
                                    else
                                    {
                                        result = "It can not add Loan because Salary was received in Month " + ((Months)j).GetDisplayName() + " in year " + i + " Please enter another Payment's Months";
                                    }
                                    return result;
                                }
                                else if (employeeMoney.Status == (int)SalaryStatus.CancelEvent)
                                {
                                    if (Language == "ar-EG")
                                    {
                                        result = "لا يمكن اضافة السلفة لانه تم الغاء المرتب فى شهر" + ((Months)j).GetDisplayName() + " فى سنة" + i + "برجاء ادخال شهور سداد اخرى ";
                                    }
                                    else
                                    {
                                        result = "It can not add Loan because Salary was canceled in Month " + ((Months)j).GetDisplayName() + " in year " + i + " Please enter another Payment's Months";
                                    }
                                    return result;
                                }
                            }
                        }
                    }
                    EmployeeLoan employeeLoan = new EmployeeLoan();
                    employeeLoan.FromMonth =(int) model.FromMonth;
                    employeeLoan.FromYear = model.FromYear;
                    employeeLoan.EmployeeID = model.EmployeeID;
                    employeeLoan.LoanDate = model.LoanDate;
                    employeeLoan.LoanStatus = (int)LoanStatus.underRevision;
                    employeeLoan.LoanValue = model.LoanValue;
                    employeeLoan.MonthlyValue = model.MonthlyValue;
                    employeeLoan.ToMonth = (int)model.ToMonth;
                    employeeLoan.ToYear = model.ToYear;
                    employeeLoan.UnderPaymentOrPaid = model.UnderPaymentOrPaid;
                    context.EmployeeLoans.Add(employeeLoan);
                    context.SaveChanges();
                    result = "true";
                }
                else
                {
                    for (int i = model.FromYear; i <= model.ToYear; i++)
                    {
                        int beginMonth, EndMonth;
                        if (i == model.FromYear)
                        {
                            beginMonth = (int)model.FromMonth;
                        }
                        else
                            beginMonth = 1;
                        if (i == model.ToYear)
                        {
                            EndMonth = (int)model.ToMonth;
                        }
                        else
                            EndMonth = 12;

                        for (int j = beginMonth; j <= EndMonth; j++)
                        {
                            EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == model.EmployeeID && EM.InMonth == j && EM.InYear == i && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                            if (employeeMoney != null)
                            {
                                if (employeeMoney.Status == (int)SalaryStatus.ApprovalAmount)
                                {
                                    if (Language == "ar-EG")
                                    {
                                        result = "لا يمكن اضافة السلفة لانه تم اعتماد المرتب فى شهر" + ((Months)j).GetDisplayName() + " فى سنة" + i + "برجاء ادخال شهور سداد اخرى ";
                                    }
                                    else
                                    {
                                        result = "It can not add Loan because Salary was approvaed in Month " + ((Months)j).GetDisplayName() + " in year " + i + " Please enter another Payment's Months";
                                    }
                                    return result;
                                }
                                else if (employeeMoney.Status == (int)SalaryStatus.ReceivableReceived)
                                {
                                    if (Language == "ar-EG")
                                    {
                                        result = "لا يمكن اضافة السلفة لانه تم تسليم المرتب فى شهر" + ((Months)j).GetDisplayName() + " فى سنة" + i + "برجاء ادخال شهور سداد اخرى ";
                                    }
                                    else
                                    {
                                        result = "It can not add Loan because Salary was received in Month " + ((Months)j).GetDisplayName() + " in year " + i + " Please enter another Payment's Months";
                                    }
                                    return result;
                                }
                                else if (employeeMoney.Status == (int)SalaryStatus.CancelEvent)
                                {
                                    if (Language == "ar-EG")
                                    {
                                        result = "لا يمكن اضافة السلفة لانه تم الغاء المرتب فى شهر" + ((Months)j).GetDisplayName() + " فى سنة" + i + "برجاء ادخال شهور سداد اخرى ";
                                    }
                                    else
                                    {
                                        result = "It can not add Loan because Salary was canceled in Month " + ((Months)j).GetDisplayName() + " in year " + i + " Please enter another Payment's Months";
                                    }
                                    return result;
                                }
                            }
                        }
                    }
                    EmployeeLoan employeeLoan = context.EmployeeLoans.FirstOrDefault(EL => EL.ID == model.ID);
                    employeeLoan.FromMonth = (int)model.FromMonth;
                    employeeLoan.FromYear = model.FromYear;
                    employeeLoan.EmployeeID = model.EmployeeID;
                    employeeLoan.LoanDate = model.LoanDate;
                    employeeLoan.LoanStatus = (int)model.LoanStatus;
                    employeeLoan.LoanValue = model.LoanValue;
                    employeeLoan.MonthlyValue = model.MonthlyValue;
                    employeeLoan.ToMonth = (int)model.ToMonth;
                    employeeLoan.ToYear = model.ToYear;
                    employeeLoan.UnderPaymentOrPaid = model.UnderPaymentOrPaid;
                    context.Entry(employeeLoan).State = EntityState.Modified;
                    context.SaveChanges();
                    if (model.LoanStatus==LoanStatus.Approval)
                    {
                        for (int i = model.FromYear; i <= model.ToYear; i++)
                        {
                            int beginMonth, EndMonth;
                            if (i == model.FromYear)
                            {
                                beginMonth = (int)model.FromMonth;
                            }
                            else
                                beginMonth = 1;
                            if (i == model.ToYear)
                            {
                                EndMonth = (int)model.ToMonth;
                            }
                            else
                                EndMonth = 12;

                            for (int j = beginMonth; j <= EndMonth; j++)
                            {
                                EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == model.EmployeeID && EM.InMonth == j && EM.InYear == i && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                                if (employeeMoney!=null)
                                {
                                    EmployeeMoneyDetail employeeMoneyDetail = CreateEmployeeMoneyDetail(employeeMoney.ID, model.EmployeeID, (int)EmployeeMoneyTypeDetials.Loan, (float)model.MonthlyValue, model.ID, "EmployeeLoan", "-", null);
                                    List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == model.EmployeeID).ToList();
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
                                    result = "true";
                                }
                                else
                                {
                                    EmployeeMoney obj = new EmployeeMoney();
                                    obj.EmployeeId = model.EmployeeID;
                                    obj.EmployeeMoneyTypeId = (int)EmployeeMoneyType.Salary;
                                    obj.InMonth = j;
                                    obj.InYear = i;
                                    obj.Status = (int)SalaryStatus.underRevision;
                                    obj.TotalMoney = 0;
                                    obj.TransDate = DateTime.Now;
                                    context.EmployeeMoneys.Add(obj);
                                    context.SaveChanges();
                                    EmployeeMoneyDetail employeeMoneyDetail = CreateEmployeeMoneyDetail(obj.ID, model.EmployeeID, (int)EmployeeMoneyTypeDetials.Loan, (float)model.MonthlyValue, model.ID, "EmployeeLoan", "-", null);
                                    List<EmployeeMoneyDetail> employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == obj.ID && EMD.EmployeeId == model.EmployeeID).ToList();
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
                                    result = "true";
                                }
                            }
                        }
                    }
                    result = "true";
                }
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<EmployeeLoanVM> GetAll(string Language)
        {
            try
            {
                List<EmployeeLoanVM> model = new List<EmployeeLoanVM>();
                if (Language == "ar-EG")
                {
                    model = context.EmployeeLoans.ToList().Select(EL => new EmployeeLoanVM()
                    {
                        ID = EL.ID,
                        EmpName = EL.Employee.Name,
                        LoanValue = EL.LoanValue,
                        LoanDate = EL.LoanDate,
                        Status = ((LoanStatus)EL.LoanStatus).GetDisplayName(),
                        FromMonthName = EL.FromMonth == 0 ? "" : ((Months)EL.FromMonth).GetDisplayName(),
                        fromYearName = EL.FromYear.ToString(),
                        ToMonthName = EL.ToMonth == 0 ? "" : ((Months)EL.ToMonth).GetDisplayName(),
                        ToYearName = EL.ToYear.ToString(),
                        MonthlyValue=EL.MonthlyValue,
                        UnderPayORPaidName=EL.UnderPaymentOrPaid==false?"تحت السداد":"تم الدفع بالكامل",
                        check=(EL.LoanStatus==(int)LoanStatus.underRevision?true:false)
                    }).ToList();
                }
                else
                {
                    model = context.EmployeeLoans.ToList().Select(EL => new EmployeeLoanVM()
                    {
                        ID = EL.ID,
                        EmpName = EL.Employee.EnName,
                        LoanValue = EL.LoanValue,
                        LoanDate = EL.LoanDate,
                        Status = ((LoanStatus)EL.LoanStatus).GetDisplayName(),
                        FromMonthName = EL.FromMonth == 0 ? "" : ((Months)EL.FromMonth).GetDisplayName(),
                        fromYearName = EL.FromYear.ToString(),
                        ToMonthName = EL.ToMonth == 0 ? "" : ((Months)EL.ToMonth).GetDisplayName(),
                        ToYearName = EL.ToYear.ToString(),
                        MonthlyValue = EL.MonthlyValue,
                        UnderPayORPaidName = EL.UnderPaymentOrPaid == false ? "Under Payment" : "Paid",
                        check = (EL.LoanStatus == (int)LoanStatus.underRevision ? true : false)
                    }).ToList();
                }
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public bool Delete(int id)
        {
            bool result = false;

            try
            {
                EmployeeLoan employeeLoan = context.EmployeeLoans.FirstOrDefault(EL => EL.ID == id);
                context.EmployeeLoans.Remove(employeeLoan);
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
        public EmployeeLoanVM GetByID(int id,string Language)
        {
            try
            {
                EmployeeLoan employeeLoan = context.EmployeeLoans.FirstOrDefault(EL => EL.ID == id);
                if (Language=="en-US")
                {
                    return new EmployeeLoanVM()
                    {
                        ID = employeeLoan.ID,
                        EmployeeID = employeeLoan.EmployeeID,
                        EmpName = employeeLoan.Employee.EnName,
                        LoanValue = employeeLoan.LoanValue,
                        LoanDate = employeeLoan.LoanDate,
                        FromMonth = (Months)employeeLoan.FromMonth,
                        FromYear = employeeLoan.FromYear,
                        ToMonth = (Months)employeeLoan.ToMonth,
                        ToYear = employeeLoan.ToYear,
                        MonthlyValue = employeeLoan.MonthlyValue,
                        LoanStatus = (LoanStatus)employeeLoan.LoanStatus,
                        UnderPayORPaidName = employeeLoan.UnderPaymentOrPaid.ToString()
                    };
                }
                else
                {
                    return new EmployeeLoanVM()
                    {
                        ID = employeeLoan.ID,
                        EmployeeID = employeeLoan.EmployeeID,
                        EmpName = employeeLoan.Employee.Name,
                        LoanValue = employeeLoan.LoanValue,
                        LoanDate = employeeLoan.LoanDate,
                        FromMonth = (Months)employeeLoan.FromMonth,
                        FromYear = employeeLoan.FromYear,
                        ToMonth = (Months)employeeLoan.ToMonth,
                        ToYear = employeeLoan.ToYear,
                        MonthlyValue = employeeLoan.MonthlyValue,
                        LoanStatus = (LoanStatus)employeeLoan.LoanStatus,
                        UnderPayORPaidName = employeeLoan.UnderPaymentOrPaid.ToString()
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<UnderPaymentEmployeeLoanVM> GetUnderPaymentEmployeeLoan(string Language)
        {
            try
            {
                List<EmployeeLoan> employeeLoansLst = context.EmployeeLoans.Where(EL => EL.UnderPaymentOrPaid == false).ToList();
                List<UnderPaymentEmployeeLoanVM> underPaymentEmployeeLoansLst = new List<UnderPaymentEmployeeLoanVM>();
                double value = 0;
                if (employeeLoansLst != null)
                {
                    foreach (var employeeLoan in employeeLoansLst)
                    {
                        value = 0;
                        for (int i = employeeLoan.FromYear; i <= employeeLoan.ToYear; i++)
                        {
                            int beginMonth, EndMonth;
                            if (i == employeeLoan.FromYear)
                            {
                                beginMonth = (int)employeeLoan.FromMonth;
                            }
                            else
                                beginMonth = 1;
                            if (i == employeeLoan.ToYear)
                            {
                                EndMonth = (int)employeeLoan.ToMonth;
                            }
                            else
                                EndMonth = 12;

                            for (int j = beginMonth; j <= EndMonth; j++)
                            {
                                EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == employeeLoan.EmployeeID && EM.InMonth == j && EM.InYear == i && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
                                if (employeeMoney != null)
                                {
                                    if (employeeMoney.Status == (int)SalaryStatus.ApprovalAmount || employeeMoney.Status == (int)SalaryStatus.ReceivableReceived)
                                    {
                                        EmployeeMoneyDetail employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeId == employeeLoan.EmployeeID && EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeMoneyTypeDetailsId == (int)EmployeeMoneyTypeDetials.Loan && EMD.FromTableId == employeeLoan.ID && EMD.FromTableName == "EmployeeLoan");
                                        if (employeeMoneyDetail != null)
                                        {
                                            value += employeeMoneyDetail.Value;
                                        }
                                    }
                                }

                            }
                        }
                        if(employeeLoan.LoanValue==value)
                        {
                            EmployeeJobData employeeJobData = context.EmployeeJobDatas.FirstOrDefault(EJD => EJD.EmployeeId == employeeLoan.EmployeeID);
                            EmployeeDepartment employeeDepartment = context.EmployeeDepartments.FirstOrDefault(ED => ED.EmployeeId == employeeLoan.EmployeeID && ED.StartDate <= DateTime.Now && (ED.EndDate == null || ED.EndDate >= DateTime.Now));
                            UnderPaymentEmployeeLoanVM underPaymentEmployeeLoan = new UnderPaymentEmployeeLoanVM();
                            if (Language=="en-US")
                            {
                                underPaymentEmployeeLoan.EmpName = employeeLoan.Employee.EnName;
                                if (employeeJobData != null)
                                {
                                    underPaymentEmployeeLoan.JobDegree = employeeJobData.JobDegree.ENName;
                                    underPaymentEmployeeLoan.JobLevel = employeeJobData.JobLevel.EnName;
                                    underPaymentEmployeeLoan.LevelSort = employeeJobData.JobLevel.LevelSort;
                                    underPaymentEmployeeLoan.DegreeSort = employeeJobData.JobDegree.DegreeSort;
                                }
                                else
                                {
                                    underPaymentEmployeeLoan.JobDegree = "";
                                    underPaymentEmployeeLoan.JobLevel = "";
                                    underPaymentEmployeeLoan.LevelSort = 0;
                                    underPaymentEmployeeLoan.DegreeSort = 0;
                                }
                                if (employeeDepartment != null)
                                    underPaymentEmployeeLoan.DeptName = employeeDepartment.Department.EnName;
                                else
                                    underPaymentEmployeeLoan.DeptName = "";
                                underPaymentEmployeeLoan.LoanValue = employeeLoan.LoanValue.ToString();
                                underPaymentEmployeeLoan.LoanStatus = employeeLoan.UnderPaymentOrPaid == false ? "Under Payment" : "Payment";
                                underPaymentEmployeeLoan.TotalPaid = value.ToString();
                                underPaymentEmployeeLoan.EmloyeeLoanID = employeeLoan.ID;
                            }
                            else
                            {
                                underPaymentEmployeeLoan.EmpName = employeeLoan.Employee.Name;
                                if (employeeJobData != null)
                                {
                                    underPaymentEmployeeLoan.JobDegree = employeeJobData.JobDegree.Name;
                                    underPaymentEmployeeLoan.JobLevel = employeeJobData.JobLevel.Name;
                                    underPaymentEmployeeLoan.LevelSort = employeeJobData.JobLevel.LevelSort;
                                    underPaymentEmployeeLoan.DegreeSort = employeeJobData.JobDegree.DegreeSort;
                                }
                                else
                                {
                                    underPaymentEmployeeLoan.JobDegree = "";
                                    underPaymentEmployeeLoan.JobLevel = "";
                                    underPaymentEmployeeLoan.LevelSort = 0;
                                    underPaymentEmployeeLoan.DegreeSort = 0;
                                }
                                if (employeeDepartment != null)
                                    underPaymentEmployeeLoan.DeptName = employeeDepartment.Department.Name;
                                else
                                    underPaymentEmployeeLoan.DeptName = "";
                                underPaymentEmployeeLoan.LoanValue = employeeLoan.LoanValue.ToString();
                                underPaymentEmployeeLoan.LoanStatus = employeeLoan.UnderPaymentOrPaid == false ? "تحت السداد" : "تم الدفع بالكامل";
                                underPaymentEmployeeLoan.TotalPaid = value.ToString();
                                underPaymentEmployeeLoan.EmloyeeLoanID = employeeLoan.ID;
                            }
                            underPaymentEmployeeLoansLst.Add(underPaymentEmployeeLoan);
                        }
                    }
                    if (underPaymentEmployeeLoansLst!=null)
                    {
                        underPaymentEmployeeLoansLst = (from underPaymentEmployeeLoan in underPaymentEmployeeLoansLst
                                                        orderby underPaymentEmployeeLoan.DegreeSort, underPaymentEmployeeLoan.LevelSort ascending
                                                        select underPaymentEmployeeLoan).ToList<UnderPaymentEmployeeLoanVM>();
                    }
                    return underPaymentEmployeeLoansLst;
                }
                else
                    return underPaymentEmployeeLoansLst;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool ConvertToPayment(int id)
        {
            bool res = false;
            try
            {
                EmployeeLoan employeeLoan = context.EmployeeLoans.FirstOrDefault(EL => EL.ID == id);
                employeeLoan.UnderPaymentOrPaid = true;
                context.Entry(employeeLoan).State = EntityState.Modified;
                context.SaveChanges();
                res = true;
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
