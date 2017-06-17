using System.Web.Mvc;

namespace PL.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }
    }
}