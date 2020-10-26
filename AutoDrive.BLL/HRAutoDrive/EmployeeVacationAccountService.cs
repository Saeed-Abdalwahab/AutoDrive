using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
    public class EmployeeVacationAccountService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool SaveInDataBase(EmployeeVacationAccountVM model)
        {
            bool result = false;
            try
            {
                if (model.ID==0)
                {
                    EmployeeVacationAccount obj = context.EmployeeVacationAccounts.FirstOrDefault(EVA => EVA.EmployeeId == model.EmployeeId && EVA.HolidayTypeId == model.HolidayTypeId && (EVA.StartDate <= model.StartDate && EVA.EndDate >= model.StartDate) && (EVA.StartDate <= model.EndDate && EVA.EndDate >= model.EndDate));
                    if (obj==null)
                    {
                        EmployeeVacationAccount employeeVacationAccount = new EmployeeVacationAccount();
                        employeeVacationAccount.HolidayTypeId = model.HolidayTypeId;
                        employeeVacationAccount.EmployeeId = model.EmployeeId;
                        employeeVacationAccount.DaysNumber = model.DaysNumber;
                        employeeVacationAccount.StartDate = model.StartDate;
                        employeeVacationAccount.Year = model.Year;
                        employeeVacationAccount.EndDate = model.EndDate;
                        context.EmployeeVacationAccounts.Add(employeeVacationAccount);
                        context.SaveChanges();
                    }
                    else
                    {
                        result = false;
                        return result;
                    }
                    

                }
                else
                {
                    EmployeeVacationAccount obj = context.EmployeeVacationAccounts.FirstOrDefault(EVA => EVA.EmployeeId == model.EmployeeId && EVA.HolidayTypeId == model.HolidayTypeId && (EVA.StartDate <= model.StartDate && EVA.EndDate >= model.StartDate) && (EVA.StartDate <= model.EndDate && EVA.EndDate >= model.EndDate));
                    if (obj == null ||obj.ID==model.ID)
                    {
                        EmployeeVacationAccount employeeVacationAccount = context.EmployeeVacationAccounts.FirstOrDefault(EVA => EVA.ID == model.ID);
                        employeeVacationAccount.HolidayTypeId = model.HolidayTypeId;
                        employeeVacationAccount.EmployeeId = model.EmployeeId;
                        employeeVacationAccount.DaysNumber = model.DaysNumber;
                        employeeVacationAccount.StartDate = model.StartDate;
                        employeeVacationAccount.Year = model.Year;
                        employeeVacationAccount.EndDate = model.EndDate;
                        context.Entry(employeeVacationAccount).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                    {
                        result = false;
                        return result;
                    }
                   
                }
                result = true;
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<EmployeeVacationAccountVM> GetAll(string Language)
        {
            try
            {
                if (Language=="en-US")
                {
                    List<EmployeeVacationAccountVM> model = context.EmployeeVacationAccounts.ToList().Select(EVA => new EmployeeVacationAccountVM()
                    {
                        ID = EVA.ID,
                        EmpName = EVA.employee.EnName,
                        HolidayTypeName = EVA.holidayType.EnName,
                        DaysNumber = EVA.DaysNumber,
                        Year = EVA.Year,
                        StartDate = EVA.StartDate,
                        EndDate = EVA.EndDate
                    }).ToList();
                    return model;
                }
                else
                {
                    List<EmployeeVacationAccountVM> model = context.EmployeeVacationAccounts.ToList().Select(EVA => new EmployeeVacationAccountVM()
                    {
                        ID = EVA.ID,
                        EmpName = EVA.employee.Name,
                        HolidayTypeName = EVA.holidayType.Name,
                        DaysNumber = EVA.DaysNumber,
                        Year = EVA.Year,
                        StartDate = EVA.StartDate,
                        EndDate = EVA.EndDate
                    }).ToList();
                    return model;
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public EmployeeVacationAccountVM getEmployeeVacationAccountByID(int id,string Language)
        {
            try
            {
                EmployeeVacationAccount employeeVacationAccount = context.EmployeeVacationAccounts.FirstOrDefault(EVA => EVA.ID == id);

                if (Language=="en-Us")
                {
                    return new EmployeeVacationAccountVM()
                    {
                        ID = employeeVacationAccount.ID,
                        EmpName = employeeVacationAccount.employee.EnName,
                        EmployeeId = employeeVacationAccount.EmployeeId,
                        HolidayTypeId = employeeVacationAccount.HolidayTypeId,
                        DaysNumber = employeeVacationAccount.DaysNumber,
                        Year = employeeVacationAccount.Year,
                        StartDate = employeeVacationAccount.StartDate,
                        EndDate = employeeVacationAccount.EndDate
                    };
                }
                else
                {
                    return new EmployeeVacationAccountVM()
                    {
                        ID = employeeVacationAccount.ID,
                        EmpName = employeeVacationAccount.employee.Name,
                        EmployeeId = employeeVacationAccount.EmployeeId,
                        HolidayTypeId = employeeVacationAccount.HolidayTypeId,
                        DaysNumber = employeeVacationAccount.DaysNumber,
                        Year = employeeVacationAccount.Year,
                        StartDate = employeeVacationAccount.StartDate,
                        EndDate = employeeVacationAccount.EndDate
                    };
                }
                  
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool deleteEmployeeVacationAccount(int id)
        {
            bool res = false;
            try
            {
                EmployeeVacationAccount model = context.EmployeeVacationAccounts.FirstOrDefault(EVA => EVA.ID == id);
                context.EmployeeVacationAccounts.Remove(model);
                context.SaveChanges();
                res = true;
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<EmployeeVacationAccountInquiryVM> seacrh(EmployeeVacationAccountVM model,string Language)
        {
            List<EmployeeVacationAccountInquiryVM> employeeVacationAccountInquiriesLst = new List<EmployeeVacationAccountInquiryVM>();
            List<EmployeeVacationAccount> employeeVacationAccountsLst = context.EmployeeVacationAccounts.Where(EVA => EVA.EmployeeId == model.EmployeeId && (model.HolidayTypeId == -1 || EVA.HolidayTypeId == model.HolidayTypeId) && (model.StartDate <= EVA.StartDate && model.EndDate >= EVA.StartDate) && (model.StartDate <= EVA.EndDate && model.EndDate >= EVA.EndDate)).ToList();
            if (employeeVacationAccountsLst.Count>0)
            {
                var AccountGroupsLst = employeeVacationAccountsLst.GroupBy(AG => AG.HolidayTypeId).ToList();
                foreach (var AccountGroup in AccountGroupsLst)
                {
                    double DaysTotal = 0;
                    foreach (var Account in AccountGroup)
                    {
                        DaysTotal += (Account.EndDate - Account.StartDate).TotalDays + 1;
                    }
                    EmployeeVacationAccount employeeVacationAccount = AccountGroup.FirstOrDefault();            
                    List<EmployeeVacation> employeeVacationsLst = context.EmployeeVacations.Where(EV => EV.EmployeeId == model.EmployeeId && EV.HolidayTypeId == employeeVacationAccount.HolidayTypeId && (model.StartDate <= EV.FromDate && model.EndDate >= EV.FromDate) && (model.StartDate <= EV.ToDate && model.EndDate >= EV.ToDate)).ToList();
                    if (employeeVacationsLst.Count>0)
                    {
                        DaysTotal = DaysTotal - employeeVacationsLst.Sum(EV => (EV.ToDate - EV.FromDate).TotalDays + 1);
                        if (Language == "en-US")
                        {
                            employeeVacationAccountInquiriesLst.Add(new EmployeeVacationAccountInquiryVM
                            {
                                column1 = employeeVacationAccount.holidayType.EnName,
                                column3 = "The remaining balance to date "+model.EndDate.ToString("dd/MM/yyyy")+" is " + DaysTotal.ToString(),
                            });
                        }
                        else
                        {
                            employeeVacationAccountInquiriesLst.Add(new EmployeeVacationAccountInquiryVM
                            {
                                column1 = employeeVacationAccount.holidayType.Name,
                                column3 = "الرصيد المتبقى الى تاريخ " + model.EndDate.ToString("dd/MM/yyyy") + " هو " + DaysTotal.ToString()
                            });
                        }
                        foreach (var employeeVacation in employeeVacationsLst)
                        {
                            double DaysVacation = (employeeVacation.ToDate - employeeVacation.FromDate).TotalDays + 1;
                            employeeVacationAccountInquiriesLst.Add(new EmployeeVacationAccountInquiryVM()
                            {
                                column1 = employeeVacation.FromDate.ToString("dd/MM/yyyy"),
                                column2 = employeeVacation.ToDate.ToString("dd/MM/yyyy"),
                                column3 = DaysVacation.ToString()
                            });
                        }
                    }
                    else
                    {
                        if (Language == "en-US")
                        {
                            employeeVacationAccountInquiriesLst.Add(new EmployeeVacationAccountInquiryVM
                            {
                                column1 = employeeVacationAccount.holidayType.EnName,
                                column3 ="The remaining balance to date " + model.EndDate.ToString("dd/MM/yyyy") + " is " + DaysTotal.ToString()
                            });
                        }
                        else
                        {
                            employeeVacationAccountInquiriesLst.Add(new EmployeeVacationAccountInquiryVM
                            {
                                column1 = employeeVacationAccount.holidayType.Name,
                                column3 = "الرصيد المتبقى الى تاريخ " + model.EndDate.ToString("dd/MM/yyyy")+" هو "+DaysTotal.ToString()
                            });
                        }
                    }
                }
            }
            return employeeVacationAccountInquiriesLst;
        }
    }
}
