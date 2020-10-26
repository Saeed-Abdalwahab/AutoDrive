using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.DAL.AutoDriveDB.HR;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.BLL.HRAutoDrive
{

    public class ItemService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private IRepository<Item> repository;
        ApplicationDbContext context = new ApplicationDbContext();
        public ItemService()
        {
            repository = new Repository<Item>(unitOfWork);
        }

        public List<ItemIndexVM> Getall(int id)
        {

            var List = id != 0 ? repository.Find(x => x.ItemParentId == id).ToList() : repository.Find(x => x.ItemParentId == null).ToList();
            return Mapper.Map(List, new List<ItemIndexVM>());
        }
        public List<ItemIndexVM> GetAll(int id)
        {

            var List =repository.Find(x => x.ItemType == ((int)ItemTypes.Class).ToString()).ToList();
            var lst = new List<Item>();

            foreach (var item in List)
            {
                if (!context.Honestys.Any(O=>O.ItemId==item.ID && O.ItemId !=id))
                {
                    lst.Add(item);
                }
            }

            return Mapper.Map(lst, new List<ItemIndexVM>());
        }
        public List<Item> GetallTree()
        {
            var List = repository.GetAll().ToList();
            return List;
        }

        public ItemIndexVM Get(int ID)
        {
            var x = Mapper.Map(repository.Get(ID), new ItemIndexVM());
            return x ;
               
        }
        public void Save(ItemIndexVM itemVM)
        {
            if (itemVM.ID==0)
            {
                var Item = new Item();

                Item.ItemCode = itemVM.ItemCode;
                Item.ItemParentId = itemVM.ItemParentId;
                Item.ItemPathImage = itemVM.ItemPathImage;
                Item.ItemType = ((int)itemVM.ItemType).ToString();
                Item.Name = itemVM.Name;
                Item.EnName = itemVM.EnName;
                Item.SerialNumber = itemVM.SerialNumber;
                context.Items.Add(Item);
                context.SaveChanges();
            }
            else
            {
               
                var Item= context.Items.Find(itemVM.ID);

                Item.ItemCode = itemVM.ItemCode;
                Item.ItemParentId = itemVM.ItemParentId;
                Item.ItemPathImage = itemVM.ItemPathImage;
                Item.ItemType = itemVM.ItemType.ToString();
                Item.Name = itemVM.Name;
                Item.EnName = itemVM.EnName;
                Item.SerialNumber = itemVM.SerialNumber;


                
                context.SaveChanges();




            }
           
        }
        public List<ItemIndexVM> GetItemByParentID(int ParentID)
        {
            return context.Items.Where(O => O.ItemParentId == ParentID).Select(D => new ItemIndexVM
            {
                ID=D.ID,
                EnName=D.EnName,
                Name=D.Name                
            }).ToList();
        }

        public void Delete(ItemIndexVM Model)
        {
            var item = context.Items.Find(Model.ID);

            context.Items.Remove(item);

            context.SaveChanges();
        }
    }
}
