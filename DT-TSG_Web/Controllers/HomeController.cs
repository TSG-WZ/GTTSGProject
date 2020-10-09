using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using DTTSG_BLL;

namespace DTTSG_Web.Controllers
{
    public class HomeController : Controller
    {
        BookManager bookManager = new BookManager();
        BookReservationManager reservationManager = new BookReservationManager();

        #region 用户首页框架以及首页信息
        public ActionResult Index()
        {

            reservationManager.ResvervationCheck();
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
