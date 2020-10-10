using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class CollectController : Controller
    {
        // GET: Collect
        public ActionResult CollectList()
        {
            return View();
        }
        public ActionResult CancelCollect(int bookid)
        {
            return Json(true);
        }
    }
}