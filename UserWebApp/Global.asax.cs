using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using UserWebApp.App_Start;

namespace UserWebApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           GlobalConfiguration.Configure(WebApiConfig.Register);
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.init();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string greska = Server.GetLastError().GetBaseException().Message;
            Response.Redirect("Error.aspx");
        }
    }
}