using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTTSG_BLL;
using DTTSG_Model;

namespace DTTSG_Web.Controllers
{
    public class ForwardController : Controller
    {
        // GET: Forward
        public ActionResult ForwardList()
        {
            #region 测试预约
            var userInfo = Session["User"] as UserInfo;
            BookReservationManager bookReservation = new BookReservationManager();

            ViewBag.Reservation = bookReservation.GetReservationPagerList(1,8, UserId:userInfo.UserId);


            #endregion
             
            return View();
        }


    }
}