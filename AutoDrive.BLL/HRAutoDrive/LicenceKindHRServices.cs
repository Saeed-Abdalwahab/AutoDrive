using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;
using AutoDrive.VM.AutoDriveHR;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Text;
using System.Threading.Tasks;
using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.Core.HR;


namespace AutoDrive.BLL.HRAutoDrive
{
   public class LicenceKindHRServices
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        public IEnumerable<LicenceKindHR> licenceKindHRs()
        {
            Repository<LicenceKindHR> repository = new Repository<LicenceKindHR>(unitOfWork);
            return repository.GetAll();
        }
        public IEnumerable<LicenceKindHR> licenceKindHRs(int LicenceTypeHRId)
        {
            Repository<LicenceKindHR> repository = new Repository<LicenceKindHR>(unitOfWork);
            return repository.Find(x=>x.LicenceTypeHRId==LicenceTypeHRId);
        }


    }
}
