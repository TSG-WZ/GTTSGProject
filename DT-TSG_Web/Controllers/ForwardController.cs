using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTTSG_BLL;
using DTTSG_Model;
using DTTSG_Model.ViewModel;
using DTTSG_Web.Filts;

namespace DTTSG_Web.Controllers
{
    [UserAutorizeAttribute]
    public class ForwardController : Controller
    {
        BookReservationManager bookReservation = new BookReservationManager();

        // GET: Forward
        public ActionResult ForwardList()
        {
            return View();
        }

        /// <summary>
        /// ajax分页获取预约信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult GetForwardListData(int pageIndex)
        {
            UserInfo userInfo = Session["User"] as UserInfo;
            JsonResult json = new JsonResult();
            json.Data = bookReservation.GetReservationPagerList(pageIndex, 8, UserId: userInfo.UserId);// 用户收藏列表
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
        /// <summary>
        /// 获取已结束列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult GetEndForwardListData(int pageIndex)
        {
            UserInfo userInfo = Session["User"] as UserInfo;
            JsonResult json = new JsonResult();
            json.Data = bookReservation.GetReservationPagerList(pageIndex, 8, UserId: userInfo.UserId);// 用户收藏列表
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        /// <summary>
        /// 预约图书
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public ActionResult ForwardBook(int bookId)
        {
            var userInfo = Session["User"] as UserInfo;
            var result = bookReservation.ResvervationBook(userInfo.UserId, bookId);
            if (result == 1)
                return Json(new AjaxBackInfo(1, "预约成功"));
            else if (result == -1)
                return Json(new AjaxBackInfo(-1, "该书已经被其他用户预约"));
            else
                return Json(new AjaxBackInfo(0, "网络错误,请稍后重试"));
        }


    }
}