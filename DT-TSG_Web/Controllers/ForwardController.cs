using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTTSG_BLL;
using DTTSG_Model;
using DTTSG_Model.ViewModel;

namespace DTTSG_Web.Controllers
{
    public class ForwardController : Controller
    {
        BookReservationManager bookReservation = new BookReservationManager();

        // GET: Forward
        public ActionResult ForwardList()
        {
            #region 测试预约
            var userInfo = Session["User"] as UserInfo;

            ViewBag.Reservation = bookReservation.GetReservationPagerList(1,8, UserId:userInfo.UserId);
            
            #endregion
             
            return View();
        }

        public ActionResult ForwardBook(int bookId)
        {
            var userInfo = Session["User"] as UserInfo;
            var result = bookReservation.ResvervationBook(userInfo.UserId,bookId);
            if (result ==1)
            {
                return Json(new AjaxBackInfo(1, "预约成功"));
            }
            else
            {
                return Redirect("~/404.html");
            }
            
        }

    }
}