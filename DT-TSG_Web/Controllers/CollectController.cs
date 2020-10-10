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
        UserCollectManager userCollectManager = new UserCollectManager();
        // GET: Collect
        public ActionResult CollectList()
        {
          
            var userInfo = Session["User"] as UserInfo;
               
            // var result =  userCollectManager.AddCollect(userId:userInfo.UserId,bookId:9); 添加收藏
            ViewBag.CollectList =  userCollectManager.GetCollectList(userInfo.UserId);// 用户收藏列表
            return View();
        }

    }
}