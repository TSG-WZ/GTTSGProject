using System.Web.Mvc;

namespace DTTSG_Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "DTTSG_Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_Pager",
                 "Admin/{controller}/{action}/{pageSize}/{pageIndex}"
            );
        }
    }
}