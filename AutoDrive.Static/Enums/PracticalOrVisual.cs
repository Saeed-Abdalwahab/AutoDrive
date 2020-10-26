using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrive.Static.Enums
{
    public enum PracticalOrVisual
    {
      


            [Display(Name = "PracticalStudy")]
            PracticalStudy = 1,

            [Display(Name = "VisualStudy")]
            VisualStudy = 2

      
    }
}
