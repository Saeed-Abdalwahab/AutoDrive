using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AutoDrive.Web.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language

        public ActionResult ChangeLanguage(string SelectedLanguage, string returnURL)
        {
        
                if (SelectedLanguage != null)
                {
                    Thread.CurrentThread.CurrentCulture =
                        CultureInfo.CreateSpecificCulture(SelectedLanguage);
                    Thread.CurrentThread.CurrentUICulture =
                        new CultureInfo(SelectedLanguage);
                    var cookie = new HttpCookie("Language");
                    cookie.Value = SelectedLanguage;
                    Response.Cookies.Add(cookie);
                }
                return Redirect(returnURL);
        }    
    }
}