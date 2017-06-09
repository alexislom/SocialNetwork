using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool isBanned = filterContext.HttpContext.User.IsInRole("BannedUser");
            if (isBanned)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                    filterContext.Result = new PartialViewResult { ViewName = "BannedUserPage" };
                else filterContext.Result = new ViewResult { ViewName = "BannedUserPage" };

            }
        }
    }
}