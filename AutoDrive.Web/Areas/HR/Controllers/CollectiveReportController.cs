using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode;
using ReportViewer = Microsoft.Reporting.WebForms.ReportViewer;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using IReportServerCredentials = Microsoft.Reporting.WebForms.IReportServerCredentials;
using System.Data.Entity;
using System.Text;

namespace AutoDrive.Web.Areas.HR.Controllers
{
    public class CollectiveReportController : Controller
    {
        // GET: HR/CollectiveReport
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TestRpt()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = false;
            reportViewer.Width = Unit.Pixel(1200);
            reportViewer.Height = Unit.Pixel(800);
            reportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            reportViewer.ServerReport.ReportServerUrl =
                new Uri(ConfigurationManager.ConnectionStrings["ReportServerURL"].ConnectionString);
            reportViewer.ProcessingMode = ProcessingMode.Remote;
            //IReportServerCredentials irsc = new Helpers.CustomReportCredentials(
            //    ConfigurationManager.ConnectionStrings["ReportServerUser"].ConnectionString,
            //    ConfigurationManager.ConnectionStrings["ReportServerPass"].ConnectionString,
            //    ConfigurationManager.ConnectionStrings["ReportServerDomin"].ConnectionString);
            //reportViewer.ServerReport.ReportServerCredentials = irsc;
            reportViewer.ServerReport.ReportPath = "/ADHR/collective/AllEmployee";
            reportViewer.ShowCredentialPrompts = true;
            reportViewer.ShowParameterPrompts = true;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowRefreshButton = true;
            Microsoft.Reporting.WebForms.ReportParameter[] reportParameterCollection = new Microsoft.Reporting.WebForms.ReportParameter[2];
            reportParameterCollection[0] = new Microsoft.Reporting.WebForms.ReportParameter("params1", "242");
            reportParameterCollection[1] = new Microsoft.Reporting.WebForms.ReportParameter("UserName", "mohamed ibrahim");
            reportViewer.ServerReport.SetParameters(reportParameterCollection);
            reportViewer.ServerReport.Refresh();
            ViewBag.ReportViewer = reportViewer;
            return View();

        }
    }
}