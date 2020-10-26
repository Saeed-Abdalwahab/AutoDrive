using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{
    public class ArchiveDocumentService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List<EmployeeArchiveVM> GetFiles(int ID,int EmployeeId)
        {
            return context.EmployeeArchives.Where(e => e.EmployeeId == EmployeeId && e.ArchiveSettingHRsId == ID).Select(d =>
                 new EmployeeArchiveVM { 
                   ID=d.ID,
                   ImageName=d.ImageName,
                 }).ToList();
        }

        public EmployeeArchiveVM GetfileByID(int ID)
        {
            return context.EmployeeArchives.Where(e => e.ID == ID).Select(d =>
                new EmployeeArchiveVM {

                    ID=d.ID,
                    Date=(DateTime)d.Date,
                    ArchiveSettingHRsId=d.ArchiveSettingHRsId,
                    ImageName=d.ImageName,
                    Number=d.Number,
                    Notes=d.Notes,
                    EmployeeId=d.EmployeeId

                }).FirstOrDefault();

        }

        public int Save(EmployeeArchiveVM Model)
        {
            if (Model.ID==0)
            {
                EmployeeArchive archive = new EmployeeArchive();

                archive.Number = Model.Number;
                archive.Date = Convert.ToDateTime(Model.DateString);
                archive.Notes = Model.Notes;
                archive.ImageName = Model.ImageName;
                archive.EmployeeId = Model.EmployeeId;
                archive.ArchiveSettingHRsId = Model.ArchiveSettingHRsId;

                context.EmployeeArchives.Add(archive);
                context.SaveChanges();
                return archive.ID;

            }
            else
            {
                EmployeeArchive archive = context.EmployeeArchives.Find(Model.ID);

                archive.Number = Model.Number;
                archive.Date = Convert.ToDateTime(Model.DateString);
                archive.Notes = Model.Notes;
                archive.ImageName = Model.ImageName;
                archive.EmployeeId = Model.EmployeeId;
                archive.ArchiveSettingHRsId = Model.ArchiveSettingHRsId;
                context.SaveChanges();
                return archive.ID;
            }

           


         
        }

        public void Delete(EmployeeArchiveVM Model)
        {
            EmployeeArchive archive = context.EmployeeArchives.Find(Model.ID);

            context.EmployeeArchives.Remove(archive);

            context.SaveChanges();
        }
    }
}
