using DTTSG_BLL;
using System.Net;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class HomeController : Controller
    {
        BookReservationManager reservationManager = new BookReservationManager();
        MessageManager messageManager = new MessageManager();
        #region 用户首页框架以及首页信息
        public ActionResult Index()
        {
            
            reservationManager.ResvervationCheck();// 检查是否有预约过期
            return View();
        }
        public ActionResult Default()
        {
            ViewBag.MessageList = messageManager.GetMessageList(isValid:1);// 有效的系统公告 isValid 是否有效 1 有效   0 无效

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
