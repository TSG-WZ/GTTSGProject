using DTTSG_BLL;
using DTTSG_Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class NoticeController : Controller
    {
        NoticeManager noticeManager = new NoticeManager();
        // GET: Notice
        public ActionResult NoticeList()
        {
            var userinfo = Session["User"] as UserInfo;
           // List<Notice> noticeList= noticeManager.GetNoticeList(userinfo.UserId);// 没有分页的方法 返回noticeList
            ViewBag.ReadNoticeList = noticeManager.GetNoticeList(userinfo.UserId, isRead:1); // 已读
            ViewBag.UnReadNoticeList = noticeManager.GetNoticeList(userinfo.UserId,isRead:0);// 未读
            //ViewBag.NoticeList = noticeManager.GetNoticePagerList(0,8,userinfo.UserId);// 有分页的方法  返回Pager
            return View();
        }

        public ActionResult NoticeConfirm(int noticeId)
        {
            var userinfo = Session["User"] as UserInfo;

            int result = noticeManager.ReadNotice(noticeId);
            if (result == 0)
            {
                return Json(false);

            }
            else
            {
                return Json(true);

            }
        }
    }
}