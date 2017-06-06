using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace PL.Infrastructure
{
    public static class AjaxHelperExtention
    {
        public static MvcHtmlString ActionLinkWithGlyphicon(this AjaxHelper ajaxHelper, 
            string glyphiconClass, string linkText, string actionName, string controllerName, AjaxOptions ajaxOptions)
        {
            var tag = new TagBuilder("i");
            tag.MergeAttribute("class", glyphiconClass);
            var stringToReplace = "stringToReplace";
            var link = ajaxHelper.ActionLink(stringToReplace, actionName, controllerName, ajaxOptions);
            return MvcHtmlString.Create(link.ToString().Replace(stringToReplace, tag + linkText));
        }
    }
}