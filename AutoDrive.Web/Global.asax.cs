using AutoDrive.BLL.AutoMapperConfigration;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoDrive.VM.Helper;
using System.Globalization;

namespace AutoDrive.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutomapperWebProfile.Run();
            ModelBinders.Binders.Add(typeof(DateTime), new CustomDateBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new NullableCustomDateBinder());
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                try
                {

                System.Threading.Thread.CurrentThread.CurrentCulture =
                    new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo(cookie.Value);


                    CultureInfo cInf = new CultureInfo(cookie.Value);

                    cInf.DateTimeFormat.DateSeparator = "/";
                    cInf.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                    cInf.DateTimeFormat.LongDatePattern = "dd/MM/yyyy hh:mm:ss tt";

                    System.Threading.Thread.CurrentThread.CurrentCulture = cInf;
                    System.Threading.Thread.CurrentThread.CurrentUICulture = cInf;
                }
                catch
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture =
                  new System.Globalization.CultureInfo("ar-EG");
                    System.Threading.Thread.CurrentThread.CurrentUICulture =
                        new System.Globalization.CultureInfo("ar-EG");
                }
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentCulture =
                    new System.Globalization.CultureInfo("ar-EG");
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                    new System.Globalization.CultureInfo("ar-EG");
            }

            //CultureInfo cInf = new CultureInfo("en-ZA", false);

            //cInf.DateTimeFormat.DateSeparator = "/";
            //cInf.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            //cInf.DateTimeFormat.LongDatePattern = "dd/MM/yyyy hh:mm:ss tt";

            //System.Threading.Thread.CurrentThread.CurrentCulture = cInf;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = cInf;
        }
    }
}
