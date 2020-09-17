using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class BorrowController : Controller
    {
        // GET: Borrow
        public ActionResult BorrowList()
        {
            return View();
        }
    }
}