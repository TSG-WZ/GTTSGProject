using Library_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Areas.Librarian.Filts
{
    public class LibrarianAutorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session["libUser"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
            //base.OnAuthorization(filterContext);
        }
    }
}