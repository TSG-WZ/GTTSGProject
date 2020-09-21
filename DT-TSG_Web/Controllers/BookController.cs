using DTTSG_BLL.Book;
using DTTSG_Common;
using DTTSG_DAL.Book;
using DTTSG_Model;
using System.Web.Mvc;

namespace DTTSG_Web.Controllers
{
    public class BookController : Controller
    {
        BookManager bookManager = new BookManager();
        BookTypeServer bookType = new BookTypeServer();


        //public ActionResult BookList(BookInfo bookInfo , int pageIndex = 1, int pageSize = 10)
        //{
        //    var result = bookManager.GetBookList(bookInfo.B_TypeId, pageIndex, pageSize).ToJson();
        //    return Json(result);
        //}



        // GET: Book
        public ActionResult BookList(BookInfo bookInfo, int pageIndex = 1)
        {
            #region 测试查询分页

            ViewBag.BookTypeList = bookType.GetBooKTypeList();
            //ViewBag.BookListPages = bookManager.GetBookList(bookInfo.B_TypeId, pageIndex, 8);

            #endregion


            #region 测试增删改查
            //bookManager.AddBookInfo(bookInfo);
            //bookManager.DelBookInfo(bookInfo);
            //bookInfo.BookAuthor = "bglb";
            //bookManager.UpdateBookInfo(bookInfo);
            #endregion
            return View();
        }

        //[HttpPost]
        /// <summary>
        /// 获取图书列表页
        /// </summary>
        /// <param name="bookInfo">图书信息(来自前端页面)</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">当前页</param>
        /// <returns>返回视图</returns>
       
        
        public ActionResult GetBookPagerData(BookInfo bookInfo, int pageindex = 1)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = bookManager.GetBookList(bookInfo.B_TypeId, pageindex, 8); //固定八条数据
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }

        public ActionResult BookInfo(int BookId)
        {



        // 借书 ：
        // 1. 点击图书 =>显示图书详情 --> 传BookId =>返回BookInfo // Get请求
        // 2. 点击借书按钮  => 传BookId ，Session["UserId"] // POST请求，返回状态码，-1，0 1 2
        // 3. 根据状态码，提示借阅结果
        // 参数 ：书的Id，用户Id，

    

            return View();
        }

    }
}
