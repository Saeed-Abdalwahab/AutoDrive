using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static;
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
    public class SalariesService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public EmployeeMoney CreateEmployeeMoney(SalariesVM model,int EmpID)
        {
            EmployeeMoney employeeMoney = new EmployeeMoney();
            employeeMoney.InMonth = (int)model.Month;
            employeeMoney.InYear = model.Year;
            employeeMoney.EmployeeId = EmpID;
            employeeMoney.TransDate = DateTime.Now;
            employeeMoney.TotalMoney = 0;
            employeeMoney.EmployeeMoneyTypeId = 1;
            employeeMoney.Status = (int)SalaryStatus.underRevision;
            context.EmployeeMoneys.Add(employeeMoney);
            context.SaveChanges();
            return employeeMoney;
        }
        public void CreateEmployeeMoneyDetials(int EmpID,int MoneyID,int? EmpMoneyDetialsType,float value,int? tableID,String TableNAME,string Opeartion,int? IncreasedeductionSettingID)
        {
            EmployeeMoneyDetail employeeMoneyDetail = new EmployeeMoneyDetail();
            employeeMoneyDetail.EmployeeId = EmpID;
            employeeMoneyDetail.EmployeeMoneyId = MoneyID;
            employeeMoneyDetail.EmployeeMoneyTypeDetailsId = EmpMoneyDetialsType;
            employeeMoneyDetail.Value = value;
            employeeMoneyDetail.FromTableId = tableID;
            employeeMoneyDetail.FromTableName = TableNAME;
            employeeMoneyDetail.OperationType = Opeartion;
            employeeMoneyDetail.IncreasesDeductionsSettingId = IncreasedeductionSettingID;
            context.EmployeeMoneyDetails.Add(employeeMoneyDetail);
            context.SaveChanges();
        }
        public bool CalculateSalaries(SalariesVM model)
        {
            bool res = false;
            try
            {
                List<Employee> employeesLst = context.Employees.ToList();
                foreach (var employee in employeesLst)
                {
                    EmployeeStatusTransaction employeeStatusTransaction = context.EmployeeStatusTransactions.Where(EST => EST.EmployeeId == employee.ID).OrderByDescending(EST=>EST.StartDate).FirstOrDefault();
                    if (employeeStatusTransaction != null && employeeStatusTransaction.EmployeeStatusId == (int)EmployeeStatus.InService && (employeeStatusTransaction.EmployeeStatusKindId ==(int)Static.Enums.EmployeeStatusKind.UnderExperience || employeeStatusTransaction.EmployeeStatusKindId == (int)Static.Enums.EmployeeStatusKind.Fixed))
                    {
                        EmployeeJobData employeeJobData = context.EmployeeJobDatas.FirstOrDefault(EJD => EJD.EmployeeId == employee.ID);
                        if (employeeJobData != null)
                        {
                            BasicSalarySetting basicSalarySetting = context.BasicSalarySettings.FirstOrDefault(BSS => BSS.JobDegreeId == employeeJobData.JobDegreeId && BSS.JobLevelId == employeeJobData.JobLevelId);
                            if (basicSalarySetting!=null)
                            {
                                List<AddIncreasingDeductionToJob> addIncreasingDeductionToJobsLst = context.AddIncreasingDeductionToJobs.Where(ADTJ => ADTJ.BasicSalarySettingId == basicSalarySetting.ID).ToList();
                                List<IncreasesDeductionsSetting> increasesDeductionsSettingsLst = new List<IncreasesDeductionsSetting>();
                                if (addIncreasingDeductionToJobsLst!=null)
                                {
                                    List<IncreasesDeductionsSetting> objLst = context.IncreasesDeductionsSettings.ToList();
                                    increasesDeductionsSettingsLst = (from AIDTJ in addIncreasingDeductionToJobsLst
                                                                      from IDS in objLst
                                                                      where IDS.ID == AIDTJ.IncreasingDeductionSettingId
                                                                      select IDS).ToList<IncreasesDeductionsSetting>();
                                }
                                EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == employee.ID && EM.InMonth == (int)model.Month && EM.InYear == model.Year && EM.EmployeeMoneyTypeId == 1);
                                if (employeeMoney==null)
                                {
                                    employeeMoney = CreateEmployeeMoney(model, employee.ID);
                                }
                                double ValueAll = basicSalarySetting.ChangedSalary + basicSalarySetting.Salary;
                                EmployeeMoneyDetail employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeMoneyTypeDetailsId ==(int)EmployeeMoneyTypeDetials.Basic && EMD.FromTableId == basicSalarySetting.ID && EMD.FromTableName == "BasicSalarySetting");
                                if(employeeMoneyDetail==null)
                                  CreateEmployeeMoneyDetials(employee.ID, employeeMoney.ID, (int)EmployeeMoneyTypeDetials.Basic, (float)basicSalarySetting.Salary, basicSalarySetting.ID, "BasicSalarySetting", "+", null);
                                employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeMoneyTypeDetailsId == (int)EmployeeMoneyTypeDetials.Changed && EMD.FromTableId == basicSalarySetting.ID && EMD.FromTableName == "BasicSalarySetting");
                                if (employeeMoneyDetail == null)
                                    CreateEmployeeMoneyDetials(employee.ID, employeeMoney.ID, (int)EmployeeMoneyTypeDetials.Changed, (float)basicSalarySetting.ChangedSalary, basicSalarySetting.ID, "BasicSalarySetting", "+", null);
                                if (increasesDeductionsSettingsLst!=null)
                                {
                                    foreach (var IncreaseDeduction in increasesDeductionsSettingsLst)
                                    {
                                        if (IncreaseDeduction.RatioOrValue == (int)PayingType.Value)
                                        {
                                            if (IncreaseDeduction.IncreasesDeductionsType.IncreasesOrDeductions == (int)IncreasesDeductionType.Increase)
                                            {
                                                employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.IncreasesDeductionsSettingId == IncreaseDeduction.ID);
                                                if(employeeMoneyDetail==null)
                                                  CreateEmployeeMoneyDetials(employee.ID, employeeMoney.ID, null, (float)IncreaseDeduction.Value, null, null, "+", IncreaseDeduction.ID);
                                                ValueAll += IncreaseDeduction.Value;
                                            }
                                            else
                                            {
                                                employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.IncreasesDeductionsSettingId == IncreaseDeduction.ID);
                                                if (employeeMoneyDetail == null)
                                                    CreateEmployeeMoneyDetials(employee.ID, employeeMoney.ID, null, (float)IncreaseDeduction.Value, null, null, "-", IncreaseDeduction.ID);
                                                ValueAll -= IncreaseDeduction.Value;
                                            }
                                        }
                                    }
                                    float value = 0;
                                    foreach (var IncreaseDeduction in increasesDeductionsSettingsLst)
                                    {
                                        if (IncreaseDeduction.RatioOrValue == (int)PayingType.Ratio)
                                        {
                                            if (IncreaseDeduction.BasicSalaryOrAll == (int)BasicSalaryType.All)
                                                value = (float)((IncreaseDeduction.Value * ValueAll) / 100);
                                            else
                                                value = (float)((IncreaseDeduction.Value * basicSalarySetting.Salary) / 100);

                                            if (IncreaseDeduction.IncreasesDeductionsType.IncreasesOrDeductions == (int)IncreasesDeductionType.Increase)
                                            {
                                                employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.IncreasesDeductionsSettingId == IncreaseDeduction.ID);
                                                if (employeeMoneyDetail == null)
                                                    CreateEmployeeMoneyDetials(employee.ID, employeeMoney.ID, null, value, null, null, "+", IncreaseDeduction.ID);
                                            }
                                            else
                                            {
                                                employeeMoneyDetail = context.EmployeeMoneyDetails.FirstOrDefault(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.IncreasesDeductionsSettingId == IncreaseDeduction.ID);
                                                if (employeeMoneyDetail == null)
                                                    CreateEmployeeMoneyDetials(employee.ID, employeeMoney.ID, null, value, null, null, "-", IncreaseDeduction.ID);
                                            }
                                        }
                                    }
                                }
                               
                                List<EmployeeMoneyDetail> employeeMoneyDetailsLst= context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeMoneyId == employeeMoney.ID && EMD.EmployeeId == employee.ID).ToList();
                                if (employeeMoneyDetailsLst!=null)
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
                        }
                    }
                }
                res = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return res;
        }
        public List<ViewSalaryVM> ViewSalaries(int[] DeptIDs, int Month, int Year, int StatusSalary, String Language)
        {
            try
            {
                List<ViewSalaryVM> model = new List<ViewSalaryVM>();
                if (DeptIDs != null)
                {
                    List<Employee> employeeLst = context.Employees.ToList();
                    List<int> DepartIDs = DeptIDs.OfType<int>().ToList();
                    if (employeeLst != null)
                    {
                        foreach (var employee in employeeLst)
                        {
                            EmployeeDepartment employeeDepartment = (from EmpDept in context.EmployeeDepartments.ToList()
                                                                     from DeptId in DepartIDs
                                                                     where EmpDept.EmployeeId == employee.ID && EmpDept.DepartmentId == DeptId && EmpDept.StartDate.Month <= Month && (EmpDept.EndDate == null || EmpDept.EndDate.Value.Month >= Month) && EmpDept.StartDate.Year <= Year && (EmpDept.EndDate == null || EmpDept.EndDate.Value.Year >= Year)
                                                                     select EmpDept).FirstOrDefault();
                            if (employeeDepartment != null)
                            {
                                EmployeeStatusTransaction employeeStatusTransaction = context.EmployeeStatusTransactions.FirstOrDefault(EST => EST.EmployeeId == employee.ID);
                                if (employeeStatusTransaction != null && employeeStatusTransaction.EmployeeStatusId == 1 && (employeeStatusTransaction.EmployeeStatusKindId == 1 || employeeStatusTransaction.EmployeeStatusKindId == 3))
                                {
                                    DateTime dateTime = new DateTime(Year, Month, 1);
                                    EmployeeJobData employeeJobData = context.EmployeeJobDatas.FirstOrDefault(EJD => EJD.EmployeeId == employee.ID &&EJD.StartDate<=dateTime&&(EJD.EndDate==null||EJD.EndDate>=dateTime));
                                    if (employeeJobData!=null)
                                    {
                                        int PerviousMonth = Month == 1 ? 12 : Month - 1;
                                        EmployeeMoney employeeMoneyCurrent;
                                        EmployeeMoney employeeMoneyPervious;
                                        if (StatusSalary == (int)SalariesStatus.All)
                                        {
                                            employeeMoneyCurrent = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == employee.ID && EM.InMonth == Month && EM.InYear == Year &&(EM.Status==(int)SalariesStatus.Approval|| EM.Status == (int)SalariesStatus.underRevision));
                                            employeeMoneyPervious= context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == employee.ID && EM.InMonth == PerviousMonth && EM.InYear == Year);
                                        }
                                        else
                                        {
                                            employeeMoneyCurrent = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == employee.ID && EM.InMonth == Month && EM.InYear == Year && (EM.Status == StatusSalary));
                                            employeeMoneyPervious = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == employee.ID && EM.InMonth == PerviousMonth && EM.InYear == Year);
                                        }
                                        if (employeeMoneyCurrent!=null)
                                        {
                                            if (Language== "ar-EG")
                                            {
                                                ViewSalaryVM viewSalaryVM = new ViewSalaryVM();
                                                viewSalaryVM.Employeename = employee.Name;
                                                viewSalaryVM.EmployeeMoneyID = employeeMoneyCurrent.ID;
                                                viewSalaryVM.EmpID = employee.ID;
                                                viewSalaryVM.ManagementName = employeeDepartment.Department.Name;
                                                viewSalaryVM.JobDegree = employeeJobData.JobDegree.Name;
                                                viewSalaryVM.JobLevel = employeeJobData.JobLevel.Name;
                                                viewSalaryVM.PerviousMonth = employeeMoneyPervious == null ? "" : employeeMoneyPervious.TotalMoney.ToString();
                                                viewSalaryVM.currentMonth = employeeMoneyCurrent.TotalMoney.ToString();
                                                viewSalaryVM.StatusName = ((SalariesStatus)employeeMoneyCurrent.Status).GetDisplayName();
                                                viewSalaryVM.check = employeeMoneyCurrent.Status == (int)SalaryStatus.ApprovalAmount ? false : true;
                                                viewSalaryVM.DegreeSort = employeeJobData.JobDegree.DegreeSort;
                                                viewSalaryVM.LevelSort = employeeJobData.JobLevel.LevelSort;
                                                model.Add(viewSalaryVM);
                                            }
                                            else
                                            {
                                                ViewSalaryVM viewSalaryVM = new ViewSalaryVM();
                                                viewSalaryVM.Employeename = employee.EnName;
                                                viewSalaryVM.EmployeeMoneyID = employeeMoneyCurrent.ID;
                                                viewSalaryVM.EmpID = employee.ID;
                                                viewSalaryVM.ManagementName = employeeDepartment.Department.EnName;
                                                viewSalaryVM.JobDegree = employeeJobData.JobDegree.ENName;
                                                viewSalaryVM.JobLevel = employeeJobData.JobLevel.EnName;
                                                viewSalaryVM.PerviousMonth = employeeMoneyPervious == null ? "" : employeeMoneyPervious.TotalMoney.ToString();
                                                viewSalaryVM.currentMonth = employeeMoneyCurrent.TotalMoney.ToString();
                                                viewSalaryVM.StatusName = ((SalariesStatus)employeeMoneyCurrent.Status).GetDisplayName();
                                                viewSalaryVM.check = employeeMoneyCurrent.Status == (int)SalaryStatus.ApprovalAmount ? false : true;
                                                viewSalaryVM.DegreeSort = employeeJobData.JobDegree.DegreeSort;
                                                viewSalaryVM.LevelSort = employeeJobData.JobLevel.LevelSort;
                                                model.Add(viewSalaryVM);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        model = (from m in model
                                 orderby m.JobDegree, m.JobDegree ascending
                                 select m).ToList();
                        return model;
                    }
                    else
                        return model;
                }
                else
                    return model;
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool EmployeeSalaryApproval(int id)
        {
            bool result=false;
            try
            {
                EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.ID == id);
                employeeMoney.Status = (int)SalaryStatus.ApprovalAmount;
                context.Entry(employeeMoney).State = EntityState.Modified;
                context.SaveChanges();
                result = true;
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool EmployeesSalariesApproval(int[] employeeMoneyIDS)
        {
            bool res = false;
            try
            {
                List<int> employeeMoneyIDsLst = employeeMoneyIDS.OfType<int>().ToList();
                foreach (int employeeMoneyID in employeeMoneyIDsLst)
                {
                    EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.ID == employeeMoneyID);
                    employeeMoney.Status = (int)SalaryStatus.ApprovalAmount;
                    context.Entry(employeeMoney).State = EntityState.Modified;
                    context.SaveChanges();
                }
                res = true;
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GetName(EmployeeMoneyDetail employeeMoneyDetail,string Language)
        {
            if (employeeMoneyDetail.IncreasesDeductionsSettingId!=null&&employeeMoneyDetail.FromTableId==null&&employeeMoneyDetail.FromTableName==null)
            {
                if (Language=="en-US")
                {
                    return employeeMoneyDetail.IncreasesDeductionsSetting.EnName;
                }
                else
                {
                    return employeeMoneyDetail.IncreasesDeductionsSetting.Name;
                }
            }
            else
            {
                return ((EmployeeMoneyTypeDetials)employeeMoneyDetail.EmployeeMoneyTypeDetailsId).GetDisplayName();
            }
        }
        public SearchedEmployeeSalaryVM Search(InquiryEmployeeSalaryVM inquiryEmployeeSalary,string Language)
        {
            SearchedEmployeeSalaryVM searchedEmployeeSalary = new SearchedEmployeeSalaryVM();
            searchedEmployeeSalary.Month = ((Months)inquiryEmployeeSalary.Month).GetDisplayName();
            searchedEmployeeSalary.Year = inquiryEmployeeSalary.Year;
            Employee employee = context.Employees.FirstOrDefault(E => E.ID == inquiryEmployeeSalary.EmployeeID);
            EmployeeMoney employeeMoney = context.EmployeeMoneys.FirstOrDefault(EM => EM.EmployeeId == inquiryEmployeeSalary.EmployeeID && EM.InMonth == (int)inquiryEmployeeSalary.Month && EM.InYear == inquiryEmployeeSalary.Year && EM.EmployeeMoneyTypeId == (int)EmployeeMoneyType.Salary);
            DateTime dateTime = new DateTime(inquiryEmployeeSalary.Year, (int)inquiryEmployeeSalary.Month, 1);
            EmployeeDepartment employeeDepartment = context.EmployeeDepartments.Where(ED => ED.EmployeeId == inquiryEmployeeSalary.EmployeeID && ED.StartDate <= dateTime && (ED.EndDate == null || ED.EndDate >= dateTime)).FirstOrDefault();
            EmployeeJobData employeeJobData = context.EmployeeJobDatas.Where(EJD => EJD.EmployeeId == inquiryEmployeeSalary.EmployeeID && EJD.StartDate <= dateTime && (EJD.EndDate == null || EJD.EndDate >= dateTime)).FirstOrDefault();
            double DetialsIncreaseTotal = 0, DetialsDeductionTotal = 0;
            if (employeeMoney!=null)
            {
                searchedEmployeeSalary.SalaryStatus = ((SalaryStatus)employeeMoney.Status).GetDisplayName();
                var employeeMoneyDetailsLst = context.EmployeeMoneyDetails.Where(EMD => EMD.EmployeeId == inquiryEmployeeSalary.EmployeeID && EMD.EmployeeMoneyId == employeeMoney.ID).GroupBy(EMD=>new { EMD.EmployeeMoneyTypeDetailsId, EMD.IncreasesDeductionsSettingId }).ToList();
                if (employeeMoneyDetailsLst!=null)
                {
                    searchedEmployeeSalary.DeductionList = new Dictionary<string, string>();
                    searchedEmployeeSalary.IncreaseList = new Dictionary<string, string>();
                    foreach (var employeeMoneyDetailgroup in employeeMoneyDetailsLst)
                    {
                        EmployeeMoneyDetail employeeMoneyDetail = employeeMoneyDetailgroup.FirstOrDefault();
                        string name = GetName(employeeMoneyDetail, Language);
                        DetialsDeductionTotal = 0;
                        DetialsIncreaseTotal = 0;
                        if (employeeMoneyDetail.OperationType == "+")
                        {
                            foreach (var item in employeeMoneyDetailgroup)
                            {
                                DetialsIncreaseTotal += item.Value;
                            }
                            searchedEmployeeSalary.IncreaseTotal += DetialsIncreaseTotal;
                            searchedEmployeeSalary.IncreaseList.Add(name, DetialsIncreaseTotal.ToString());
                        }
                        else
                        {
                            foreach (var item in employeeMoneyDetailgroup)
                            {
                                DetialsDeductionTotal += item.Value;
                            }
                            searchedEmployeeSalary.DeductionTotal += DetialsDeductionTotal;
                            searchedEmployeeSalary.DeductionList.Add(name, DetialsDeductionTotal.ToString());

                        }
                    }
                }
                else
                {
                    searchedEmployeeSalary.IncreaseList = null;
                    searchedEmployeeSalary.DeductionList = null;
                    searchedEmployeeSalary.DeductionTotal = 0;
                    searchedEmployeeSalary.IncreaseTotal = 0;
                }
                searchedEmployeeSalary.Total = searchedEmployeeSalary.IncreaseTotal - searchedEmployeeSalary.DeductionTotal;
               
            }
            else
            {
                if (Language== "en-US")
                {
                    searchedEmployeeSalary.SalaryStatus = "Not existed";
                }
                else
                {
                    searchedEmployeeSalary.SalaryStatus = "غير موجود";
                }
                searchedEmployeeSalary.IncreaseList = null;
                searchedEmployeeSalary.DeductionList = null;
                searchedEmployeeSalary.DeductionTotal = 0;
                searchedEmployeeSalary.IncreaseTotal = 0;
                searchedEmployeeSalary.Total = 0;
            }
            if (Language=="en-US")
            {
                if (employee==null)
                {
                    searchedEmployeeSalary.EmpName = "";
                    searchedEmployeeSalary.EmployeeCode = "";
                }
                else
                {
                    searchedEmployeeSalary.EmpName = employee.EnName;
                    searchedEmployeeSalary.EmployeeCode = employee.Code.ToString();
                }
                if (employeeJobData==null)
                {
                    searchedEmployeeSalary.JobDegreeName = "";
                    searchedEmployeeSalary.JobLevelName = "";
                    searchedEmployeeSalary.JobName = "";
                }
                else
                {
                    searchedEmployeeSalary.JobDegreeName =employeeJobData.JobDegree.ENName;
                    searchedEmployeeSalary.JobLevelName = employeeJobData.JobLevel.EnName;
                    searchedEmployeeSalary.JobName = employeeJobData.JobName.EnName;
                }
                if (employeeDepartment==null)
                {
                    searchedEmployeeSalary.DeptName = "";
                }
                else
                {
                    searchedEmployeeSalary.DeptName = employeeDepartment.Department.EnName;
                }
            }
            else
            {
                if (employee == null)
                {
                    searchedEmployeeSalary.EmpName = "";
                    searchedEmployeeSalary.EmployeeCode = "";
                }
                else
                {
                    searchedEmployeeSalary.EmpName = employee.Name;
                    searchedEmployeeSalary.EmployeeCode = employee.Code.ToString();
                }
                if (employeeJobData == null)
                {
                    searchedEmployeeSalary.JobDegreeName = "";
                    searchedEmployeeSalary.JobLevelName = "";
                    searchedEmployeeSalary.JobName = "";
                }
                else
                {
                    searchedEmployeeSalary.JobDegreeName = employeeJobData.JobDegree.Name;
                    searchedEmployeeSalary.JobLevelName = employeeJobData.JobLevel.Name;
                    searchedEmployeeSalary.JobName = employeeJobData.JobName.Name;
                }
                if (employeeDepartment == null)
                {
                    searchedEmployeeSalary.DeptName = "";
                }
                else
                {
                    searchedEmployeeSalary.DeptName = employeeDepartment.Department.Name;
                }
            }
            return searchedEmployeeSalary;
        }
    }
}
