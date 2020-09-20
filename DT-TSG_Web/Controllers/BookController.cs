using DTTSG_BLL.Book;
using DTTSG_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class BookController : Controller
    {
        BookManager bookManager = new BookManager();

        //[HttpPost]
        /// <summary>
        /// 获取图书列表页
        /// </summary>
        /// <param name="bookInfo">图书信息(来自前端页面)</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns>返回视图</returns>
        public ActionResult BookList(BookInfo bookInfo, int pagesize = 8, int pageindex = 1)
        {
            ViewBag.BookList = bookManager.GetBookList(pagesize, pageindex, bookInfo);
            return View();
        }
        
        public ActionResult GetBookPagerData(BookInfo bookInfo, int pagesize = 8, int pageindex = 1)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = bookManager.GetBookList(pagesize, pageindex, bookInfo);
            return jsonResult;
        }

        public ActionResult BookInfo(int BookId)
        {

            return View();
        }
    }
}