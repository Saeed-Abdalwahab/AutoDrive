using System.Web;
using System.Web.Optimization;

namespace AutoDrive.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
               //Bootstrap
               "~/vendors/bootstrap/dist/css/bootstrap.min.css",
               "~/vendors/bootstrap-rtl/dist/css/bootstrap-rtl.min.css",

               //Font Awesome
               "~/vendors/font-awesome/css/font-awesome.min.css",

               //NProgress
               "~/vendors/nprogress/nprogress.css",

                //bootstrap-progressbar
                "~/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",

                //iCheck
                "~/vendors/iCheck/skins/flat/green.css",

                //bootstrap-daterangepicker
                "~/vendors/bootstrap-daterangepicker/daterangepicker.css",
                //Select2
                "~/vendors/select2/dist/css/select2.min.css",

                //Switchery
                "~/vendors/switchery/dist/switchery.min.css",

                //starrr
                "~/vendors/starrr/dist/starrr.css",

                //Custom Theme Style
                "~/build/css/custom-rtl.css",

                 //DataTables
                 "~/DataTables/datatables.css",

                //toastr
                "~/Content/toastr.min.css",

                //chosen
                "~/Content/chosen.css",
                //"~/Content/gijgo/combined/gijgo.min.css",

                //loading
                "~/Loading/src/css/preloader.css",

                //My Style
                

                    "~/Content/mystyle.css",

                    "~/Content/mystyleAR.css"


           ));

            bundles.Add(new StyleBundle("~/Contenten/css").Include(


                  "~/vendors/bootstrap/dist/css/bootstrap.min.css",

                   "~/vendors/font-awesome/css/font-awesome.min.css",

                  "~/vendors/nprogress/nprogress.css",

                   "~/vendors/iCheck/skins/flat/green.css",


                   "~/build/css/custom.min.css",

                 //DataTables
                 "~/DataTables/datatables.css",

                //toastr
                "~/Content/toastr.min.css",

                //chosen
                "~/Content/chosen.css",

                //"~/Content/gijgo/combined/gijgo.min.css",

                //loading
                "~/Loading/src/css/preloader.css",

                    "~/Content/mystyle.css"



          ));
            bundles.Add(new ScriptBundle("~/test/Gentelella").Include(

                    //jQuery
                    "~/vendors/jquery/dist/jquery.min.js",

                    //Bootstrap
                    "~/vendors/bootstrap/dist/js/bootstrap.min.js",

                    //FastClick
                    "~/vendors/fastclick/lib/fastclick.js",

                    //NProgress
                    "~/vendors/nprogress/nprogress.js",

                    //bootstrap-progressbar
                    "~/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",

                    //iCheck
                    "~/vendors/iCheck/icheck.min.js",

                    //bootstrap-daterangepicker
                    "~/vendors/moment/min/moment.min.js",
                    "~/vendors/bootstrap-daterangepicker/daterangepicker.js",

                    //bootstrap-wysiwyg
                    "~/vendors/bootstrap-wysiwyg/js/bootstrap-wysiwyg.min.js",
                    "~/vendors/jquery.hotkeys/jquery.hotkeys.js",
                    "~/vendors/google-code-prettify/src/prettify.js",


                    //jQuery Tags Input
                    "~/vendors/jquery.tagsinput/src/jquery.tagsinput.js",


                    //Switchery
                    "~/vendors/switchery/dist/switchery.min.js",

                    //Select2
                    "~/vendors/select2/dist/js/select2.full.min.js",

                    //Parsley
                    "~/vendors/parsleyjs/dist/parsley.min.js",
                    "~/vendors/parsleyjs/dist/i18n/fa.js",

                    //Autosize
                    "~/vendors/autosize/dist/autosize.min.js",

                    //jQuery autocomplete
                    "~/vendors/devbridge-autocomplete/dist/jquery.autocomplete.min.js",

                    //starrr
                    "~/vendors/starrr/dist/starrr.js",

                    //Custom Theme Scripts
                    "~/build/js/custom.min.js",

                  //DataTables
                  "~/DataTables/datatables.min.js",

                    //toastr
                    "~/Scripts/toastr.min.js",

                    
                    

                    //"~/Scripts/gijgo/combined/gijgo.min.js",

                 //loading
                 "~/Loading/src/js/jquery.preloader.min.js",

                 //chosen
                 "~/Scripts/chosen.jquery.min.js"



                    ));

            bundles.Add(new ScriptBundle("~/testen/Gentelella").Include(


            "~/vendors/jquery/dist/jquery.min.js",

             "~/vendors/bootstrap/dist/js/bootstrap.min.js",

              "~/vendors/fastclick/lib/fastclick.js",

              "~/vendors/nprogress/nprogress.js",


              "~/build/js/custom.min.js"
              //"~/Scripts/gijgo/combined/gijgo.min.js"

        ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(

                     "~/Scripts/jquery.validate*",
                     "~/Scripts/jquery.unobtrusive-ajax.js"

                     ));
        }
    }
}
