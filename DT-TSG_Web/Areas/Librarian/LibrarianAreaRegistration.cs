using System.Web.Mvc;

namespace DTTSG_Web.Areas.Librarian
{
    public class LibrarianAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Librarian";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Librarian_default",
                "Librarian/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "DTTSG_Web.Areas.Librarian.Controllers" }
            );
            context.MapRoute(
                "Librarian_Pager",
                 "Librarian/{controller}/{action}/{pageSize}/{pageIndex}"
            );
        }
    }
}