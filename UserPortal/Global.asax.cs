using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UserPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError();
                if (ex != null)
                {
                    if (ex.GetBaseException() != null)
                        ex = ex.GetBaseException();

                    if (ex.Message != null)
                    {
                        string requestURL = "" + Request.Url;
                        string userId = (Request.LogonUserIdentity != null && Request.LogonUserIdentity.User != null) ? Request.LogonUserIdentity.Name : Server.MachineName;

                        CodeLib.DAL.CommonDAL.InsertExceptionLog(CodeLib.DatabaseIdEnum.LogType_SiteException, null, requestURL, ex.Message, ex.StackTrace, userId);

                        Response.Redirect("~/Pages/Errors/500.html");
                    }
                }
            }
            catch (Exception ex)
            {
                CodeLib.DAL.CommonDAL.InsertExceptionLog(CodeLib.DatabaseIdEnum.LogType_SiteException, null, null, ex.Message, ex.StackTrace, null);
            }

            Server.ClearError();
        }
    }
}
