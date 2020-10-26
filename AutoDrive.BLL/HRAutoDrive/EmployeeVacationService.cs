using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
    public class EmployeeVacationService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool checkDate(DateTime start, DateTime end)
        {
            if (DateTime.Now >= start && DateTime.Now <= end)
            {
                return true;
            }
            return false;
        }
        public IEnumerable<Employee> SearchEmployee(string Any, string language)
        {

            if (language == "en-US")
            {
                return context.EmployeeStatusTransactions.Where(x => (x.EmployeeStatusKindId == (int)Static.Enums.EmployeeStatusKind.Fixed||x.EmployeeStatusKindId==(int)Static.Enums.EmployeeStatusKind.UnderExperience) && x.EmployeeStatusId == (int)EmployeeStatus.InService && x.Employee.EnName.Contains(Any)).ToList().Where(x => checkDate(x.StartDate, x.EndDate)).ToList().Select(x => new Employee()
                {
                    ID = x.Employee.ID,
                    Name = x.Employee.EnName
                });
            }
            else
            {
                return context.EmployeeStatusTransactions.Where(x => (x.EmployeeStatusKindId == (int)Static.Enums.EmployeeStatusKind.Fixed || x.EmployeeStatusKindId == (int)Static.Enums.EmployeeStatusKind.UnderExperience) && x.EmployeeStatusId == (int)EmployeeStatus.InService && x.Employee.Name.Contains(Any)).ToList().Where(x => checkDate(x.StartDate, x.EndDate)).ToList().Select(x => new Employee()
                {
                    ID = x.Employee.ID,
                    Name = x.Employee.Name

                });
            }

        }
        public bool compareIDs(List<EmployeeVacation> employeeVacations,int id)
        {
            foreach (var item in employeeVacations)
            {
                if (item.ID==id)
                {
                    return true;
                }
            }
            return false;
        }
        public string SaveInDataBase(EmployeeVacationVM model)
        {
            string result = "";
            try
            {
                EmployeeVacationAccount employeeVacationAccount = context.EmployeeVacationAccounts.FirstOrDefault(EVA => EVA.EmployeeId == model.EmployeeId && EVA.HolidayTypeId == model.HolidayTypeId && (EVA.StartDate <= model.FromDate && EVA.EndDate >= model.FromDate) && (EVA.StartDate <= model.ToDate && EVA.EndDate >= model.ToDate));
                List<EmployeeVacation> obj = context.EmployeeVacations.Where(EV => EV.EmployeeId == model.EmployeeId && (EV.FromDate <= model.FromDate && EV.ToDate >= model.FromDate) && (EV.FromDate <= model.ToDate && EV.ToDate >= model.ToDate)).ToList();
                List<EmployeeVacation> obj2 = context.EmployeeVacations.Where(EV => EV.EmployeeId == model.EmployeeId && ((EV.FromDate <= model.FromDate && EV.ToDate >= model.FromDate) || (EV.FromDate <= model.ToDate && EV.ToDate >= model.ToDate))).ToList();
                if (employeeVacationAccount!=null)
                {
                    if (obj.Count==0||compareIDs(obj,model.ID))
                    {
                        if (obj2.Count==0||(compareIDs(obj2,model.ID)&&obj2.Count<=1))
                        {
                            List<EmployeeVacation> employeeVacationsLst = context.EmployeeVacations.Where(EV => employeeVacationAccount.StartDate <= EV.FromDate && employeeVacationAccount.EndDate >= EV.FromDate && employeeVacationAccount.StartDate <= EV.ToDate && employeeVacationAccount.EndDate >= EV.ToDate).ToList();
                            double DaysTotal = 0;
                            foreach (var item in employeeVacationsLst)
                            {
                                double CountDays = (item.ToDate - item.FromDate).TotalDays + 1;
                                DaysTotal += CountDays;
                            }
                            if (!compareIDs(employeeVacationsLst,model.ID))
                            {
                                DaysTotal += (model.ToDate - model.FromDate).TotalDays + 1;
                            }
                            else
                            {
                                EmployeeVacation employeeVacation = context.EmployeeVacations.FirstOrDefault(EV => EV.ID == model.ID);
                                DaysTotal -= (employeeVacation.ToDate - employeeVacation.FromDate).TotalDays + 1;
                                DaysTotal += (model.ToDate - model.FromDate).TotalDays + 1;
                            }
                            
                            double DaysEmployeeVacationAccount = (employeeVacationAccount.EndDate - employeeVacationAccount.StartDate).TotalDays + 1;
                            if (DaysEmployeeVacationAccount >= DaysTotal)
                            {
                                if (model.ID == 0)
                                {
                                    EmployeeVacation employeeVacation = new EmployeeVacation();
                                    employeeVacation.EmployeeId = model.EmployeeId;
                                    employeeVacation.HolidayTypeId = model.HolidayTypeId;
                                    employeeVacation.FromDate = model.FromDate;
                                    employeeVacation.ToDate = model.ToDate;
                                    employeeVacation.NODays = model.NODays;
                                    employeeVacation.Notes = model.Notes;
                                    context.EmployeeVacations.Add(employeeVacation);
                                    context.SaveChanges();
                                }
                                else
                                {
                                    EmployeeVacation employeeVacation = context.EmployeeVacations.FirstOrDefault(EV => EV.ID == model.ID);
                                    employeeVacation.EmployeeId = model.EmployeeId;
                                    employeeVacation.HolidayTypeId = model.HolidayTypeId;
                                    employeeVacation.FromDate = model.FromDate;
                                    employeeVacation.ToDate = model.ToDate;
                                    employeeVacation.NODays = model.NODays;
                                    employeeVacation.Notes = model.Notes;
                                    context.Entry(employeeVacation).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                                result = "true";
                                return result;
                            }
                            else
                            {
                                result = "Employee Vacation Account is not enough";
                                return result;
                            }

                        }
                        else
                        {
                            result = "part of holiday is part from another holiday";
                            return result;
                        }
                       
                    }
                    else
                    {
                        result = "Employee Vacation is not null";
                        return result;
                    }
                  
                }
                else {
                    result = "Employee Vacation Account is null";
                    return result;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<EmployeeVacationVM> GetAll(string Language,DateTime From,DateTime To)
        {
            try
            {
                List<EmployeeVacationVM> model = new List<EmployeeVacationVM>();
                if (Language=="en-US")
                {
                    model = context.EmployeeVacations.Where(EV=>(From<=EV.FromDate&&To>=EV.FromDate)&&(From<=EV.ToDate&&To>=EV.ToDate)).ToList().Select(EV => new EmployeeVacationVM()
                    {
                        ID = EV.ID,
                        EmpName = EV.employee.EnName,
                        HolidayTypeName = EV.holidayType.EnName,
                        FromDate = EV.FromDate,
                        ToDate = EV.ToDate,
                        NODays = EV.NODays,
                        Notes = EV.Notes == null ? "" : EV.Notes,
                        check = (EV.EmployeeMoneyDetailsId != null && EV.VacationDedutionId != null) ? false : true
                    }).ToList();
                }
                else
                {
                    model = context.EmployeeVacations.Where(EV => (From <= EV.FromDate && To >= EV.FromDate) && (From <= EV.ToDate && To >= EV.ToDate)).ToList().Select(EV => new EmployeeVacationVM()
                    {
                        ID = EV.ID,
                        EmpName = EV.employee.Name,
                        HolidayTypeName = EV.holidayType.Name,
                        FromDate = EV.FromDate,
                        ToDate = EV.ToDate,
                        NODays = EV.NODays,
                        Notes = EV.Notes == null ? "" : EV.Notes,
                        check = (EV.EmployeeMoneyDetailsId != null && EV.VacationDedutionId != null) ? false : true
                    }).ToList();

                }
                model = model.OrderByDescending(m => m.FromDate).ThenBy(m => m.EmpName).ToList();
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool deleteEmployeeVacation(int id)
        {
            bool res = false;
            try
            {
                EmployeeVacation employeeVacation = context.EmployeeVacations.FirstOrDefault(EV => EV.ID == id);
                context.EmployeeVacations.Remove(employeeVacation);
                context.SaveChanges();
                res = true;
                return res;
              
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public EmployeeVacationVM getEmployeeVacationByID(string Language,int id)
        {
            try
            {
                EmployeeVacation model = context.EmployeeVacations.FirstOrDefault(EV => EV.ID == id);
                if (Language=="en-US")
                {
                    return new EmployeeVacationVM()
                    {
                        ID = model.ID,
                        EmployeeId = model.EmployeeId,
                        EmpName = model.employee.EnName,
                        HolidayTypeId = model.HolidayTypeId,
                        FromDate = model.FromDate,
                        ToDate = model.ToDate,
                        NODays = model.NODays,
                        Notes = model.Notes
                    };
                }
                else {
                    return new EmployeeVacationVM()
                    {
                        ID = model.ID,
                        EmployeeId = model.EmployeeId,
                        EmpName = model.employee.Name,
                        HolidayTypeId = model.HolidayTypeId,
                        FromDate = model.FromDate,
                        ToDate = model.ToDate,
                        NODays = model.NODays,
                        Notes = model.Notes
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
