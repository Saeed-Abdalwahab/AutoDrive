using System.Web.Mvc;

namespace AutoDrive.Web.Areas.AutoDriveMain
{
    public class AutoDriveMainAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AutoDriveMain";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AutoDriveMain_default",
                "AutoDriveMain/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}