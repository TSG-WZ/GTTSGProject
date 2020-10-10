using DTTSG_BLL;
using DTTSG_Model;
using DTTSG_Model.ViewModel;
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

            ViewBag.CollectList = userCollectManager.GetCollectList(userInfo.UserId);// 用户收藏列表
            return View();
        }

        public ActionResult AddCollect(int bookId)
        {
            var userInfo = Session["User"] as UserInfo;


            if (!userCollectManager.CollectCheck(userInfo.UserId, bookId))
            {
                return Json(new AjaxBackInfo(0, "已收藏过该书"));
            }

            int result = userCollectManager.AddCollect(userId: userInfo.UserId, bookId: 9);// 添加收藏
            if (result == 1)
            {
                return Json(new AjaxBackInfo(1, "收藏成功！"));

            }
            return Json(new AjaxBackInfo(-1, "网咯错误！"));

        }

    }
}