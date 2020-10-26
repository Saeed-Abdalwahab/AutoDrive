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

namespace AutoDrive.BLL.AutoDrivePayroll
{
    public class IncreasesDeductionTypeService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public bool SaveInDataBase(IncreasesDeductionTypeVM model)
        {
            bool result = false;
            try
            {
                if (model.ID == 0)
                {
                    IncreasesDeductionsType obj = context.IncreasesDeductionsTypes.FirstOrDefault(IDS => IDS.Name == model.Name||IDS.EnName==model.EnName);
                    if (obj == null)
                    {
                        IncreasesDeductionsType increasesDeductionsType = new IncreasesDeductionsType();
                        increasesDeductionsType.Name = model.Name;
                        increasesDeductionsType.EnName = model.EnName;
                        increasesDeductionsType.IncreasesOrDeductions= (int)model.IncreasesOrDeductions;
                        context.IncreasesDeductionsTypes.Add(increasesDeductionsType);
                        context.SaveChanges();

                    }
                    else
                    {
                        result = true;
                    }

                }
                else
                {
                    IncreasesDeductionsType obj = context.IncreasesDeductionsTypes.FirstOrDefault(IDS => IDS.Name == model.Name || IDS.EnName == model.EnName);
                    context.Dispose();
                    if (obj == null || obj.ID == model.ID)
                    {
                        context = new ApplicationDbContext();
                        IncreasesDeductionsType increasesDeductionsType = new IncreasesDeductionsType();
                        increasesDeductionsType.ID = model.ID;
                        increasesDeductionsType.Name = model.Name;
                        increasesDeductionsType.EnName = model.EnName;
                        increasesDeductionsType.IncreasesOrDeductions = (int)model.IncreasesOrDeductions;
                        context.Entry(increasesDeductionsType).State = EntityState.Modified;
                        context.SaveChanges();

                    }
                    else
                    {
                        result = true;
                    }

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

                IncreasesDeductionsType increasesDeductionsType = context.IncreasesDeductionsTypes.FirstOrDefault(IDT=>IDT.Name == Name);
                if (increasesDeductionsType == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                IncreasesDeductionsType increasesDeductionsType = context.IncreasesDeductionsTypes.FirstOrDefault(IDT => IDT.Name == Name);
                if (increasesDeductionsType == null || increasesDeductionsType.ID == id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public bool CheckEnName(string Name, int id)
        {
            bool res = false;
            if (id == 0)
            {

                IncreasesDeductionsType increasesDeductionsType = context.IncreasesDeductionsTypes.FirstOrDefault(IDT => IDT.EnName == Name);
                if (increasesDeductionsType == null)
                {
                    res = true;
                }
                else
                    res = false;
            }
            else
            {
                IncreasesDeductionsType increasesDeductionsType = context.IncreasesDeductionsTypes.FirstOrDefault(IDT => IDT.EnName == Name);
                if (increasesDeductionsType == null || increasesDeductionsType.ID == id)
                {
                    res = true;
                }
                else
                    res = false;
            }

            return res;

        }
        public List<IncreasesDeductionTypeVM> GetAll()
        {
            List<IncreasesDeductionTypeVM> model = new List<IncreasesDeductionTypeVM>();
            try
            {
                model = context.IncreasesDeductionsTypes.ToList().Select(IDT => new IncreasesDeductionTypeVM
                {
                    ID = IDT.ID,
                    Name = IDT.Name,
                    EnName=IDT.EnName,
                    IncreasesOrDeductions = (IncreasesDeductionType)IDT.IncreasesOrDeductions,
                    IncreasesOrDeductionsName = ((IncreasesDeductionType)IDT.IncreasesOrDeductions).GetDisplayName()
                }).ToList();
            }
            catch (Exception ex)
            {
                model = null;
            }
            return model;
        }
        public IncreasesDeductionTypeVM GetByID(int id)
        {
            IncreasesDeductionsType increasesDeductionsType = context.IncreasesDeductionsTypes.SingleOrDefault(IDT => IDT.ID == id);
            return new IncreasesDeductionTypeVM()
            {
                ID = increasesDeductionsType.ID,
                Name = increasesDeductionsType.Name,
                EnName=increasesDeductionsType.EnName,
                IncreasesOrDeductions = (IncreasesDeductionType)increasesDeductionsType.IncreasesOrDeductions
            };
        }
        public bool Delete(int id)
        {
            bool result = false;
            try
            {
                IncreasesDeductionsType increasesDeductionsType = context.IncreasesDeductionsTypes.SingleOrDefault(IDT => IDT.ID == id);
                context.IncreasesDeductionsTypes.Remove(increasesDeductionsType);
                context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return result;
        }
    }
}
