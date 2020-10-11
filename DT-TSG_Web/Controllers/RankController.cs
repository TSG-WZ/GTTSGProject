using DTTSG_BLL;
using DTTSG_Web.Filts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    [UserAutorizeAttribute]
    public class RankController : Controller
    {
        RankManager rankManager = new RankManager();
        public ActionResult RankList()
        {

            //var bookList = rankManager.GetMaxBorrowBook(10); // 借阅次数最多的10本书 按照借阅次数排序
            //var userList = rankManager.GetMaxBorrowUser(10); // 借阅书籍最多的10個人 按照借阅数量排序

            //int userBorrowCount = rankManager.GetBorrowUserCount(20200921);// 查询此人的借阅总数
            //int bookBorrowCount = rankManager.GetBorrowBookCount(4);// 查询本书的借阅总数
            return View();
        }

       
    }
}