using DTTSG_BLL;
using DTTSG_Model;
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
            #region 测试收藏以及公告
            //UserCollectManager userCollectManager = new UserCollectManager();
            //NoticeManager noticeManager = new NoticeManager();
            //var userInfo = Session["User"] as UserInfo;

            //Notice notice = new Notice()
            //{
            //    NoticeTime = DateTime.Now,
            //    N_TypeId = 2,
            //    NoticeTitle = "测试通知",
            //    NoticeContent = "感谢使用本系统！",
            //    UserId = userInfo.UserId,
            //    LibId = 1001
            //};

            //noticeManager.AddNotice(notice);
            //ViewBag.NoticeList = noticeManager.GetNoticeList(userInfo.UserId);
            //var result =  userCollectManager.AddCollect(userId:userInfo.UserId,bookId:9);
            //ViewBag.CollectList =  userCollectManager.GetCollectList(userInfo.UserId);
            #endregion
            return View();
        }

    }
}