using Library_BLL.Factory;
using Library_BLL.Interfaces;
using Library_Common;
using Library_Model;
using Library_UI.Models;
using DTTSG_Web.Areas.Librarian.Filts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Areas.Librarian.Controllers
{
    [LibrarianAutorize]
    /// <summary>
    /// 图书管理员借阅控制器
    /// </summary>
    public class BorrowController : Controller
    {
        ILibrarianManager iLibrarianManager = IoCUnity.Resolve<ILibrarianManager>();
        /// <summary>
        /// 借阅信息列表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public ActionResult BorrowList(int pagesize = 10, int pageindex = 1)
        {
            ViewBag.BorrowList = iLibrarianManager.GetAllBorrowList(pagesize, pageindex);
            return View();
        }

        /// <summary>
        /// 搜索页面Json借阅列表(正常跳页和分页跳转)
        /// </summary>
        /// <param name="borrowInfo">借阅信息</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns>Json对象</returns>
        public JsonResult GetBorrowPagerData(BorrowModel borrowInfo, int pagesize = 10, int pageindex = 1)
        {
            JsonResult jsonResult = new JsonResult();
            if (borrowInfo.Bid != 0 || borrowInfo.Sid != 0 || borrowInfo.BookId != 0)
                jsonResult.Data = iLibrarianManager.GetBorrowInfoById(borrowInfo, pagesize, pageindex);
            else
                jsonResult.Data = iLibrarianManager.GetAllBorrowList(pagesize, pageindex);
            return jsonResult;
        }

        /// <summary>
        /// 归还图书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult ReturnBook(int Bid, string Bname)
        {
            if (iLibrarianManager.ReturnBorrowInfo(Session["libUser"] as LibrarianInfo, Bid, Bname))
            {
                return Json(new AjaxBackInfo(1, "还书成功!"));
            }
            return Json(new AjaxBackInfo(2, "还书失败,其他问题请联系管理员!"));
        }

        /// <summary>
        /// 延期归还
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DelayReturn(int Bid, string Bname)
        {
            if (iLibrarianManager.DelayReturnTime(new BorrowInfo() { Bid = Bid }, Bname))
            {
                return Json(new AjaxBackInfo(1, "延期成功!"));
            }
            return Json(new AjaxBackInfo(2, "延期失败,其他问题请联系管理员!"));
        }

        /// <summary>
        /// 借书
        /// </summary>
        /// <returns></returns>
        public ActionResult BorrowBook()
        {
            return View();
        }

        /// <summary>
        /// Ajax借书提交
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="bookInfo"></param>
        /// <returns></returns>
        public JsonResult AddBorrowInfo(UserInfo userInfo, BookInfo bookInfo)
        {
            if (iLibrarianManager.AddBorrowInfo(userInfo, Session["libUser"] as LibrarianInfo, bookInfo))
            {
                return Json(new AjaxBackInfo(1, "借阅成功!"));
            }
            return Json(new AjaxBackInfo(2, "借阅失败,请检查学号或图书编号!"));
        }

    }
}