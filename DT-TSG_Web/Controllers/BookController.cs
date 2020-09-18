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

        // GET: Book
        public ActionResult BookList()
        {
            BookInfo bookInfo = bookManager.GetBookList().FirstOrDefault();
            #region 测试
            //bookManager.AddBookInfo(bookInfo);
            //bookManager.DelBookInfo(bookInfo);
            //bookInfo.BookAuthor = "bglb";
            //bookManager.UpdateBookInfo(bookInfo);
            #endregion
            ViewBag.BookList = bookManager.GetBookList();
            return View();
        }

    }
}