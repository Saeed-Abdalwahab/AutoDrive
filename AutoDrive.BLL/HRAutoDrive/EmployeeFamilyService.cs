using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
    public class EmployeeFamilyService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List<EmployeeFamilyVM> GetFamilyByEmpId(int ID)
        {
            return context.EmployeeFamilys.Where(E => E.EmployeeId == ID).Select(d =>
                new EmployeeFamilyVM
                {
                    ID = d.ID,
                    ArName = d.ArName,
                    EnName = d.EnName,
                    Note = d.Note,
                    Genderid = d.Gender.ToString(),
                    Relationid = d.Relation.ToString()

                }).ToList();
        }

        public EmployeeFamilyVM GetFamilyById(int ID)
        {
            return context.EmployeeFamilys.Where(E => E.ID == ID).Select(d =>
               new EmployeeFamilyVM
               {
                   ID = d.ID,
                   ArName = d.ArName,
                   EnName = d.EnName,
                   Note = d.Note,
                   Genderid = d.Gender.ToString(),
                   Relationid = d.Relation.ToString(),
                   Gender=(Gender)d.Gender,
                   Relation=(Relation)d.Relation,
                   

               }).FirstOrDefault();
        }

        public  void Save(EmployeeFamilyVM Model)
        {
            if (Model.ID == 0)
            {
                var EmployeeFamily = new EmployeeFamily();

                EmployeeFamily.ArName = Model.ArName;
                EmployeeFamily.EmployeeId = Model.EmployeeId;
                EmployeeFamily.EnName = Model.EnName;
                EmployeeFamily.Gender =(int) Model.Gender;
                EmployeeFamily.Relation =(int) Model.Relation;
                EmployeeFamily.Note = Model.Note;

                context.EmployeeFamilys.Add(EmployeeFamily);
                context.SaveChanges();
            }
            else
            {
                var EmployeeFamily = context.EmployeeFamilys.Find(Model.ID);
                EmployeeFamily.ArName = Model.ArName;
                EmployeeFamily.EmployeeId = Model.EmployeeId;
                EmployeeFamily.EnName = Model.EnName;
                EmployeeFamily.Gender = (int)Model.Gender;
                EmployeeFamily.Relation = (int)Model.Relation;
                EmployeeFamily.Note = Model.Note;


                context.SaveChanges();
            }
        }

        public void Delete(EmployeeFamilyVM Model)
        {
            var EmployeeFamily = context.EmployeeFamilys.Find(Model.ID);

            context.EmployeeFamilys.Remove(EmployeeFamily);

            context.SaveChanges();
        }

    }
}
