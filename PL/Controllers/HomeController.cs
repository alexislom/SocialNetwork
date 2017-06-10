using BLL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Authorize]
        public ActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{
                //var user = _userService.GetOneByPredicate(u => u.UserName == User.Identity.Name);

                //if (User.IsInRole("BannedUser"))
                //{
                //    return View("Error");
                //}
                //return RedirectToAction("Index", "Profile");
            //}
            //return RedirectToAction("Login", "Account");
            return View();
        }
    }
}