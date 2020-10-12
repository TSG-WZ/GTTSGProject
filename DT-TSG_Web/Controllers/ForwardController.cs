using DTTSG_BLL;
using DTTSG_Model;
using DTTSG_Model.ViewModel;
using DTTSG_Web.Filts;
using System;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    [UserAutorizeAttribute]
    public class ForwardController : Controller
    {
        BookReservationManager bookReservation = new BookReservationManager();
        BorrowManager borrowManager = new BorrowManager();
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
            json.Data = bookReservation.GetReservationPagerList(pageIndex, 8, UserId: userInfo.UserId,FoTypeId:1);//预约中
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        /// <summary>
        /// 已取走
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult GetPassForwardListData(int pageIndex)
        {
            UserInfo userInfo = Session["User"] as UserInfo;
            JsonResult json = new JsonResult();
            json.Data = bookReservation.GetReservationPagerList(pageIndex, 8, UserId: userInfo.UserId, FoTypeId: 2);
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
        /// <summary>
        /// 预约过期
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult GetEndForwardListData(int pageIndex)
        {
            UserInfo userInfo = Session["User"] as UserInfo;
            JsonResult json = new JsonResult();
            json.Data = bookReservation.GetReservationPagerList(pageIndex, 8, UserId: userInfo.UserId, FoTypeId: 3);
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }


        /// <summary>
        /// 取书
        /// </summary>
        /// <param name="foId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FetchBook(int foId)
        {
            var model = bookReservation.GetForwardModel(foId);
            if (DateTime.Compare(model.F_EndTime, DateTime.Now) < 0)// 预约时间外
            {
                bookReservation.CancelResvervationBook(foId);
                return Json(new AjaxBackInfo(-1, "该书预约时间已过"));
            }
            else
            {
                model.Fo_TypeId = 2;// 已取走
                bookReservation.UpdateResvervation(model);
            }

            // 借书
            var result = borrowManager.BookBrrow(model.BookId, model.UserId);
            if (result == 1)
            {
                return Json(new AjaxBackInfo(1, "借阅成功"));
            }
            else if (result == 0)
            {
                return Json(new AjaxBackInfo(0, "借阅失败"));
            }
            else
            {
                return Json(new AjaxBackInfo(-1, "此书已借出！"));
            }


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
            {
                return Json(new AjaxBackInfo(1, "预约成功"));
            }
            else if (result == -1)
            {
                return Json(new AjaxBackInfo(-1, "该书已经被其他用户预约"));
            }
            else
            {
                return Json(new AjaxBackInfo(0, "网络错误,请稍后重试"));
            }
        }


    }
}