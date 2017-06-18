using System.Web.Mvc;
using CustomLogger;

namespace PL.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger = CustLogger.GetCurrentClassLogger;

        public ViewResult Index()
        {
            _logger.Info("Forbidden action");
            return View("Error");
        }
        public ViewResult NotFound()
        {
            _logger.Info("Page not found");
            Response.StatusCode = 404;
            return View("NotFound");
        }
    }
}