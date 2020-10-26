using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static;
using AutoDrive.Static.Enums;
using AutoDrive.VM;
using AutoDrive.VM.AutoDrivePayroll;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL
{
    public class IncreasingDeductionSettingService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List<IncreasingDeductionSettingVM> GetAll(String Language)
        {
            List<IncreasingDeductionSettingVM> model = new List<IncreasingDeductionSettingVM>();
            try
            {
                if (Language == "ar-EG")
                {
                    List<IncreasesDeductionsSetting> model1 = context.IncreasesDeductionsSettings.ToList();
                    model = context.IncreasesDeductionsSettings.ToList().Select(IDS => new IncreasingDeductionSettingVM
                    {
                        ID = IDS.ID,
                        Value = IDS.Value,
                        Name = IDS.Name,
                        EnName = IDS.EnName,
                        IncreasesDeductionsType = IDS.IncreasesDeductionsType.Name
                        ,
                        BasicSalaryORALLName = IDS.BasicSalaryOrAll == null ? "" : ((BasicSalaryType)IDS.BasicSalaryOrAll).GetDisplayName(),
                        RatioORValueName = ((PayingType)IDS.RatioOrValue).GetDisplayName(),
                        IncreaseORDeduction = IDS.IncreasesDeductionsType.IncreasesOrDeductions
                    }).ToList();
                }
                else
                {
                    List<IncreasesDeductionsSetting> model1 = context.IncreasesDeductionsSettings.ToList();
                    model = context.IncreasesDeductionsSettings.ToList().Select(IDS => new IncreasingDeductionSettingVM
                    {
                        ID = IDS.ID,
                        Value = IDS.Value,
                        Name = IDS.Name,
                        EnName = IDS.EnName,
                        IncreasesDeductionsType = IDS.IncreasesDeductionsType.EnName
                        ,
                        BasicSalaryORALLName = IDS.BasicSalaryOrAll==null?"":((BasicSalaryType)IDS.BasicSalaryOrAll).GetDisplayName(),
                        RatioORValueName = ((PayingType)IDS.RatioOrValue).GetDisplayName(),
                        IncreaseORDeduction = IDS.IncreasesDeductionsType.IncreasesOrDeductions
                    }).ToList();
                }
             
            }
            catch (Exception ex)
            {
                model = null;
            }
            return model;
        }
        public IncreasingDeductionSettingVM GetByID(int id)
        {
            IncreasesDeductionsSetting increasesDeductionsSetting = context.IncreasesDeductionsSettings.SingleOrDefault(IDS => IDS.ID == id);
            return new IncreasingDeductionSettingVM()
            {
                ID = increasesDeductionsSetting.ID,
                Value = increasesDeductionsSetting.Value,
                Name = increasesDeductionsSetting.Name,
                EnName=increasesDeductionsSetting.EnName,
                IncreasesDeductionsTypeId = increasesDeductionsSetting.IncreasesDeductionsTypeId,
                BasicSalaryOrAll = increasesDeductionsSetting.BasicSalaryOrAll==null?0:(BasicSalaryType)increasesDeductionsSetting.BasicSalaryOrAll,
                RatioOrValue = (PayingType)increasesDeductionsSetting.RatioOrValue
            };
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                IncreasesDeductionsSetting increasesDeductionsSetting = context.IncreasesDeductionsSettings.SingleOrDefault(IDS => IDS.ID == id);
                context.IncreasesDeductionsSettings.Remove(increasesDeductionsSetting);
                context.SaveChanges();
                result = true;
            }catch(Exception ex)
            {
                throw ex;

            }
            return result;
        }
        public bool SaveInDataBase(IncreasingDeductionSettingVM increasingDeductionSettingVM)
        {
            bool result = false;
            try
            {
                if (increasingDeductionSettingVM.ID==0)
                {
                    IncreasesDeductionsSetting obj = context.IncreasesDeductionsSettings.FirstOrDefault(IDS => IDS.Name == increasingDeductionSettingVM.Name||IDS.EnName==increasingDeductionSettingVM.EnName);
                    if (obj==null)
                    {
                        IncreasesDeductionsSetting increasesDeductionsSetting = new IncreasesDeductionsSetting();
                        increasesDeductionsSetting.Name = increasingDeductionSettingVM.Name;
                        increasesDeductionsSetting.EnName = increasingDeductionSettingVM.EnName;
                        increasesDeductionsSetting.RatioOrValue = (int)increasingDeductionSettingVM.RatioOrValue;
                        increasesDeductionsSetting.IncreasesDeductionsTypeId = increasingDeductionSettingVM.IncreasesDeductionsTypeId;
                        increasesDeductionsSetting.Value = increasingDeductionSettingVM.Value;
                        if ((int)increasingDeductionSettingVM.BasicSalaryOrAll == 0)
                        {
                            increasesDeductionsSetting.BasicSalaryOrAll = null;
                        }
                        else
                            increasesDeductionsSetting.BasicSalaryOrAll =(int)increasingDeductionSettingVM.BasicSalaryOrAll;
                        context.IncreasesDeductionsSettings.Add(increasesDeductionsSetting);
                        context.SaveChanges();
                        
                    }
                    else
                    {
                        result = true;
                    }
                    
                }
                else
                {
                    IncreasesDeductionsSetting obj = context.IncreasesDeductionsSettings.FirstOrDefault(IDS => IDS.Name == increasingDeductionSettingVM.Name||IDS.EnName==increasingDeductionSettingVM.EnName);
                    context.Dispose();
                    if (obj == null||obj.ID==increasingDeductionSettingVM.ID)
                    {
                        context = new ApplicationDbContext();
                        IncreasesDeductionsSetting increasesDeductionsSetting = new IncreasesDeductionsSetting();
                        increasesDeductionsSetting.ID = increasingDeductionSettingVM.ID;
                        increasesDeductionsSetting.Name = increasingDeductionSettingVM.Name;
                        increasesDeductionsSetting.EnName = increasingDeductionSettingVM.EnName;
                        increasesDeductionsSetting.Value = increasingDeductionSettingVM.Value;
                        increasesDeductionsSetting.IncreasesDeductionsTypeId = increasingDeductionSettingVM.IncreasesDeductionsTypeId;
                        increasesDeductionsSetting.RatioOrValue = (int)increasingDeductionSettingVM.RatioOrValue;
                        if ((int)increasingDeductionSettingVM.BasicSalaryOrAll == 0)
                        {
                            increasesDeductionsSetting.BasicSalaryOrAll = null;
                        }
                        else
                            increasesDeductionsSetting.BasicSalaryOrAll = (int)increasingDeductionSettingVM.BasicSalaryOrAll;
                        context.Entry(increasesDeductionsSetting).State = EntityState.Modified;
                        context.SaveChanges();

                    }
                    else
                    {
                        result = true;
                    }

                }
               
               

            }catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public bool CheckName(string Name, int id)
        {
            bool res = false;
            if (id == 0)
            {

                IncreasesDeductionsSetting obj = context.IncreasesDeductionsSettings.FirstOrDefault(VT => VT.Name == Name);
                if (obj == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                IncreasesDeductionsSetting obj = context.IncreasesDeductionsSettings.FirstOrDefault(VT => VT.Name == Name);
                if (obj == null || obj.ID == id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public bool CheckENName(string Name, int Id)
        {
            bool res = false;
            if (Id == 0)
            {

                IncreasesDeductionsSetting obj = context.IncreasesDeductionsSettings.FirstOrDefault(VT => VT.EnName == Name);
                if (obj == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                IncreasesDeductionsSetting obj = context.IncreasesDeductionsSettings.FirstOrDefault(VT => VT.EnName == Name);
                if (obj == null || obj.ID == Id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
    }
}
