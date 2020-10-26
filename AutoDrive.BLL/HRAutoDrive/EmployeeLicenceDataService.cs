using AutoDrive.DAL.AutoDriveDB;
using AutoDrive.DAL.Models;
using AutoDrive.Static.Enums;

using System;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using AutoDrive.Core.Repository;
using AutoDrive.Core.UnitOfWork;
using AutoDrive.Core.HR;
using AutoDrive.VM.AutoDriveHR;
using AutoMapper;
using System.Linq;
using AutoDriveResources;

namespace AutoDrive.BLL.HRAutoDrive
{
   public class EmployeeLicenceDataService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private Repository<EmployeeLicenceData> repository;
        public EmployeeLicenceDataService()
        {
            repository = new Repository<EmployeeLicenceData>(unitOfWork);
        }
        public EmployeeLicenceDataVM GetEmployeeLicenceDataByID(int id)
        {
            return Mapper.Map(repository.Get(id), new EmployeeLicenceDataVM());
        }
        public IEnumerable<EmployeeLicenceDataVM> EmployeeLicenceDataVMs(int employeeid)
        {

            return Mapper.Map(repository.Find(x => x.EmployeeId == employeeid).ToList(), new List<EmployeeLicenceDataVM>());
        }
        public bool SaveInDB(EmployeeLicenceDataVM vM)
        {
            try
            {
                if (vM.ID == 0)
                {
                    if (repository.FristOrDefault(x => x.EmployeeId==vM.EmployeeId&&x.ReleaseDate == vM.ReleaseDate && x.LicenseNumber == vM.LicenseNumber && x.LicenceTypeHRId == vM.LicenceTypeHRId) != null)
                        return false;
                    repository.Add(Mapper.Map(vM, new EmployeeLicenceData()));
                    unitOfWork.Save();
                    return true;
                }
                else
                {
                    if (repository.FristOrDefault(x =>x.ID!=vM.ID&&x.EmployeeId==vM.EmployeeId&& x.ReleaseDate == vM.ReleaseDate && x.LicenseNumber == vM.LicenseNumber && x.LicenceTypeHRId == vM.LicenceTypeHRId) != null)
                        return false;
                    repository.Update(Mapper.Map(vM, new EmployeeLicenceData()));
                    unitOfWork.Save();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public string delete(int ID)
        {

            repository.Remove(repository.Get(ID));
            unitOfWork.Save();
            return Messages.DeleteSucc;
        }
    }
}
