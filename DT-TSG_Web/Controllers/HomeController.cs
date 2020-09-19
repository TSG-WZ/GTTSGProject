using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace DT_TSG_Web.Controllers
{
    public class HomeController : Controller
    {
        #region 用户首页框架以及首页信息
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Default()
        {
            return View();
        }
        #endregion

        public ActionResult RankList()
        {
            
            Response.Write(Dns.GetHostName());
            return View();
        }

    }
}