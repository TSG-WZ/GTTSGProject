using DTTSG_BLL;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTTSG_Model.ViewModel;

namespace DTTSG_Web.Controllers
{
    public class CollectController : Controller
    {
        UserCollectManager userCollectManager = new UserCollectManager();
        // GET: Collect
        public ActionResult CollectList()
        {
            return View();
        }
        public ActionResult GetCollectPagerData(int pageindex)
        {
            var userInfo = Session["User"] as UserInfo;
            JsonResult json = new JsonResult();
            json.Data = userCollectManager.GetCollectPagerList(userInfo.UserId, pageindex, 8);// 用户收藏列表
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public ActionResult AddCollect(int bookId)
        {
            AjaxBackInfo json = null;
            var userInfo = Session["User"] as UserInfo;
            if (!userCollectManager.CollectCheck(userInfo.UserId, bookId))
            {
                json = new AjaxBackInfo(-1, "已经收藏!");
                return Json(json);
            }
            int result = userCollectManager.AddCollect(userId: userInfo.UserId, bookId: bookId); //添加收藏
            if (result>0)
            {
                json = new AjaxBackInfo(1, "收藏成功");
            }
            else
            {
                json = new AjaxBackInfo(-1, "未知错误");
            }
            return Json(json);
        }

        /// <summary>
        /// ajax取消收藏
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        public ActionResult CancelCollect(int collectId)
        {
            var userInfo = Session["User"] as UserInfo;
            int result = userCollectManager.CancelCollect(collectId); //添加收藏
            AjaxBackInfo json = null;
            if (result > 0)
            {
                json = new AjaxBackInfo(1, "取消成功");
            }
            return Json(json);
        }
    }
}