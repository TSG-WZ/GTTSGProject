using DTTSG_Web.Filts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    [UserAutorizeAttribute]
    public class RankController : Controller
    {
        public ActionResult RankList()
        {
            return View();
        }
    }
}