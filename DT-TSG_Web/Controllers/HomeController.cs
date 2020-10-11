using DTTSG_BLL;
using System.Web.Mvc;
using DTTSG_Web.Filts;
using DTTSG_Model;


namespace DTTSG_Web.Controllers
{
    [UserAutorizeAttribute]
    public class HomeController : Controller
    {
        BookReservationManager reservationManager = new BookReservationManager();
        MessageManager messageManager = new MessageManager();
        RankManager rankManager = new RankManager();
        NoticeManager noticeManager = new NoticeManager();
        #region 用户首页框架以及首页信息
        public ActionResult Index()
        {
            UserInfo userInfo = Session["User"] as UserInfo;

             ViewBag.isHasUnReadList = noticeManager.GetNoticeList(userInfo.UserId,isRead:0).Count != 0; // 未读消息
            
            reservationManager.ResvervationCheck();// 检查是否有预约过期
            return View();
        }
        public ActionResult Default()
        {
            UserInfo userInfo = Session["User"] as UserInfo;
            ViewBag.MessageList = messageManager.GetMessageList(isValid: 1);// 有效的系统公告 isValid 是否有效 1 有效   0 无效
            int br = rankManager.BrrowBookCount(userInfo.UserId); // 借阅总数
            int bEd = rankManager.BrrowBookEd(userInfo.UserId); // 延期中
            int bIng = rankManager.BrrowBookIng(userInfo.UserId);// 借阅中
            int NR = br - bEd - bIng; // 未归还

            ViewBag.Statistics = new int[br,bEd,bIng,NR]; 
            
            return View();
        }
        #endregion
        
    }
}
