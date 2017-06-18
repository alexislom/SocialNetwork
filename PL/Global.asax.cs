using ORM.EF;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CustomLogger;


namespace PL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILogger _logger = CustLogger.GetCurrentClassLogger;

        protected void Application_Start()
        {
            _logger.Info("The application started.");
            Database.SetInitializer(new DatabaseInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
