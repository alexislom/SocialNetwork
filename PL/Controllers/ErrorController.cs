using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ErrorController : Controller
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ViewResult Index()
        {
            logger.Info("Forbidden action");
            return View("Error");
        }
        public ViewResult NotFound()
        {
            logger.Info("Page not found");
            Response.StatusCode = 404;
            return View("NotFound");
        }
    }
}