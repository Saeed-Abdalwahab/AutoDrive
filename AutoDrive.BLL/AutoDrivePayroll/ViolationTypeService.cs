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
    public class ViolationTypeService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool SaveInDataBase(ViolationTypeVM model)
        {
            bool result = false;

            try
            {
                if (model.ID == 0)
                {
                    DAL.AutoDriveDB.ViolationType obj = context.ViolationTypes.FirstOrDefault(JD => JD.Name == model.Name || JD.EnName == model.Name);
                    if (obj == null)
                    {
                        DAL.AutoDriveDB.ViolationType violationType = new DAL.AutoDriveDB.ViolationType();
                        violationType.EnName = model.EnName;
                        violationType.Name = model.Name;
                        violationType.DaysNumber = model.DaysNumber;
                        if ((int)model.FromBaseSalaryOrOverall == 0)
                        {
                            violationType.FromBaseSalaryOrOverall = null;
                        }
                        else
                            violationType.FromBaseSalaryOrOverall = (int)model.FromBaseSalaryOrOverall;
                        violationType.FromMoneyOrMoral = (int)model.FromMoneyOrMoral;
                        context.ViolationTypes.Add(violationType);
                        context.SaveChanges();
                        result = true;
                    }
                    else
                        result = false;

                }
                else
                {
                    DAL.AutoDriveDB.ViolationType obj = context.ViolationTypes.FirstOrDefault(VT => VT.Name == model.Name || VT.EnName == model.EnName);
                    context.Dispose();
                    if (obj == null || obj.ID == model.ID)
                    {
                        context = new ApplicationDbContext();
                        DAL.AutoDriveDB.ViolationType violationType = new DAL.AutoDriveDB.ViolationType();
                        violationType.ID = model.ID;
                        violationType.Name = model.Name;
                        violationType.EnName = model.EnName;
                        violationType.DaysNumber = model.DaysNumber;
                        violationType.FromMoneyOrMoral = (int)model.FromMoneyOrMoral;
                        if ((int)model.FromBaseSalaryOrOverall == 0)
                        {
                            violationType.FromBaseSalaryOrOverall = null;
                        }
                        else
                            violationType.FromBaseSalaryOrOverall = (int)model.FromBaseSalaryOrOverall;
                        context.Entry(violationType).State = EntityState.Modified;
                        context.SaveChanges();
                        result = true;
                    }
                    else
                        result = false;

                }
            }
            catch (Exception ex)
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

                DAL.AutoDriveDB.ViolationType violationType = context.ViolationTypes.FirstOrDefault(VT => VT.Name == Name);
                if (violationType == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                DAL.AutoDriveDB.ViolationType violationType = context.ViolationTypes.FirstOrDefault(VT => VT.Name == Name);
                if (violationType == null || violationType.ID == id)
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

                DAL.AutoDriveDB.ViolationType violationType = context.ViolationTypes.FirstOrDefault(VT => VT.EnName == Name);
                if (violationType == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                DAL.AutoDriveDB.ViolationType violationType = context.ViolationTypes.FirstOrDefault(VT => VT.EnName == Name);
                if (violationType == null || violationType.ID == Id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public List<ViolationTypeVM> GetAll()
        {
            
            try
            {
                List<ViolationTypeVM> violationTypesLst = context.ViolationTypes.ToList().Select(VT => new ViolationTypeVM()
                {
                    ID = VT.ID,
                    Name = VT.Name,
                    EnName = VT.EnName,
                    BasicSalaryOROverall =VT.FromBaseSalaryOrOverall==null?"":((BasicORAllType)VT.FromBaseSalaryOrOverall).GetDisplayName(),
                    FromMoneyORMoralName = ((Static.Enums.ViolationType)VT.FromMoneyOrMoral).GetDisplayName(),
                    DaysNum = VT.DaysNumber.ToString()?? ""
                }).ToList();

                return violationTypesLst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
        public ViolationTypeVM GetByID(int id)
        {
            try
            {
                DAL.AutoDriveDB.ViolationType violationType = context.ViolationTypes.FirstOrDefault(VT => VT.ID == id);
                return new ViolationTypeVM()
                {
                    ID = violationType.ID,
                    Name = violationType.Name,
                    EnName = violationType.EnName,
                    DaysNumber = violationType.DaysNumber,
                    FromBaseSalaryOrOverall = violationType.FromBaseSalaryOrOverall==null?0:(BasicORAllType)violationType.FromBaseSalaryOrOverall,
                    FromMoneyOrMoral = (Static.Enums.ViolationType)violationType.FromMoneyOrMoral
                };
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
                DAL.AutoDriveDB.ViolationType violationType = context.ViolationTypes.FirstOrDefault(VT => VT.ID == id);
                context.ViolationTypes.Remove(violationType);
                context.SaveChanges();
                result = true;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
