using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class NoticeController : Controller
    {
        // GET: Notice
        public ActionResult NoticeList()
        {
            return View();
        }

        public ActionResult NoticeConfirm(int noticeId)
        {
            return Json(true);
        }
    }
}