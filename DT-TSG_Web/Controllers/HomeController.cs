using DTTSG_BLL;
using System.Net;
using System.Web.Mvc;
using DTTSG_Web.Filts;

namespace DTTSG_Web.Controllers
{
    [UserAutorizeAttribute]
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
            //一个数组 []
            return View();
        }
        #endregion
        
    }
}
