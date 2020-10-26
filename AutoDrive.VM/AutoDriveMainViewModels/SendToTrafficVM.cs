using AutoDriveResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.VM.AutoDriveMainViewModels
{
    public class SendToTrafficVM
    {
        public int ID { get; set; }

        [Display(Name = "TraineeId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int TraineeId { get; set; }

        [Display(Name = "TraineeEvaluationId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int TraineeEvaluationId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "Required")]
        [Display(Name = "SendDate", ResourceType = typeof(AutoDriveResources.Resources))]
        public string SendDate { get; set; }

        [Display(Name = "SenderEmployeeId", ResourceType = typeof(AutoDriveResources.Resources))]
        public int? SenderEmployeeId { get; set; }


        [Display(Name = "TraineeGuid", ResourceType = typeof(AutoDriveResources.Resources))]
        public Guid? TraineeGuid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string ArName { get; set; }

        [Display(Name = "EnName", ResourceType = typeof(AutoDriveResources.Resources))]
        [StringLength(50)]
        public string EnName { get; set; }

        [Display(Name = "Code", ResourceType = typeof(AutoDriveResources.Resources))]
        public string Code { get; set; }


        [Display(Name = "FileNo", ResourceType = typeof(AutoDriveResources.Resources))]
        public string FileNo { get; set; }

        //[Display(Name = "EnEmpName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string EnEmpName { get; set; }

        //[Display(Name = "ArEmpName", ResourceType = typeof(AutoDriveResources.Resources))]
        public string ArEmpName { get; set; }
    }
}
