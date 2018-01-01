using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using GroupDocs.Annotation;
using GroupDocs.Annotation.Common.License;

namespace GroupDocs.Annotation_for.NET
{
    public class Global : HttpApplication
    {
        private static string _licensePath = "E:\\GroupDocs.Total.lic";
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            License l = new License();
            if (System.IO.File.Exists(_licensePath))
            {
                try
                {
                    l.SetLicense(_licensePath);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}