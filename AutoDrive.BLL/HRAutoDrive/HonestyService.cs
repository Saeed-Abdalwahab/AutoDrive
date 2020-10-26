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
    public class HonestyService
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public List< HonestyVM> GetHonestyByEmID(int EmployeeID)
        {
            return context.Honestys.Where(h => h.EmployeeId == EmployeeID).Select(d =>

               new HonestyVM
               {
                   ID = d.ID,
                   HonestyDate = d.HonestyDate,
                   honestyEndDate=d.honestyEndDate,
                   ItemName = d.Item.Name,
                   ItemCode = d.Item.ItemCode,
                   ImageNme = d.Item.ItemPathImage
               }).ToList();
        }

        public HonestyVM GetHonestyByID(int ID)
        {
            return context.Honestys.Where(h => h.ID == ID).Select(d =>

               new HonestyVM
               {
                   ID = d.ID,
                   HonestyDate = d.HonestyDate,
                   honestyEndDate=d.honestyEndDate,
                   ItemName = d.Item.Name,
                   ItemId=d.ItemId,
               }).FirstOrDefault();
        }

        public void Save(HonestyVM Model)
        {
            if (Model.ID==0)
            {
                Honesty honesty = new Honesty();
                honesty.HonestyDate = Model.HonestyDate;
                honesty.EmployeeId = Model.EmployeeId;
                honesty.ItemId = Model.ItemId;
                honesty.honestyEndDate = Model.honestyEndDate;

                context.Honestys.Add(honesty);

            }
            else
            {
                Honesty honesty = context.Honestys.Find(Model.ID);

                honesty.HonestyDate = Model.HonestyDate;
                honesty.EmployeeId = Model.EmployeeId;
                honesty.ItemId = Model.ItemId;
                honesty.honestyEndDate = Model.honestyEndDate;
            }

            context.SaveChanges();
        }


        public void ReceivingHonesty(DateTime EndDate , int id)
        {
            Honesty honesty = context.Honestys.Find(id);

            honesty.honestyEndDate = EndDate;

            context.SaveChanges();
        }
        public void Delete(HonestyVM Model)
        {
            var item = context.Honestys.Find(Model.ID);

            context.Honestys.Remove(item);

            context.SaveChanges();
        }
    }
}
